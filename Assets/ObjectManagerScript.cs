using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectManagerScript : MonoBehaviour {
	
	private Quaternion PointToCamera;
	
	//Circle Maintenance Variables
	public Transform Circle;
	private List<GameObject> Circles;
	private List<DateTime> StaticCircleBirth;
	private List<DateTime> CircleBirth;
	private GameObject CurrentCircle;
	private int CircleUID;
	
	public float gameSpeed;
	
	//Hero Object
	public GameObject Fish;
	//Water Object
	public GameObject Water;
	
	// Use this for initialization
	void Start () {
		PointToCamera = Quaternion.identity;
		PointToCamera.eulerAngles = new Vector3(90,180,0);
		Circles = new List<GameObject>();
		StaticCircleBirth = new List<DateTime>();
		CircleBirth = new List<DateTime>();
		CircleUID = 0;
		CurrentCircle = null;
		gameSpeed = 1.0f;
	}
	
	
	public void SetGameSpeed(float newSpeed)
	{
		gameSpeed = newSpeed;
	}
	public float GetGameSpeed()
	{
		return gameSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
		UpdateCircles(1.03f);
	}
	
	void HandleInput(){
		
		RippleWaterScript waterscript = Water.GetComponent("RippleWaterScript") as RippleWaterScript;
		//if left click
		if(Input.GetMouseButtonDown(0) && Circles.Count < 5){
			//find the location of click and create a circle
			Vector3 Circlepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,50));
			Instantiate(Circle,	Circlepoint,PointToCamera);
			
			//find the new circle and name it
			GameObject NewCircle = GameObject.Find("Circle(Clone)");
			NewCircle.name = NewCircle.name + CircleUID.ToString();
			CircleUID++;
			
			//add the new circle to our list
			Circles.Add(NewCircle);
			CircleBirth.Add(DateTime.Now);
			StaticCircleBirth.Add(new DateTime());
			waterscript.AddRipple(NewCircle.transform.position,DateTime.Now);
			
		}
	}
	
	void UpdateCircles(float speed){
		
		HeroFishScript script = Fish.GetComponent("HeroFishScript") as HeroFishScript;
		
		if(script.checkishurt()){
			foreach(GameObject circle in Circles){
				Destroy (circle);
			}
			Circles.Clear ();
			StaticCircleBirth.Clear();
			CircleBirth.Clear();
			CurrentCircle=null;
		}
		
		List<GameObject>.Enumerator circEnum = Circles.GetEnumerator();
		List<DateTime>.Enumerator birthEnum = CircleBirth.GetEnumerator();
		List<DateTime>.Enumerator staticbirthEnum = StaticCircleBirth.GetEnumerator();
		DateTime Default = new DateTime();
		int count = 0;
		
		while(circEnum.MoveNext())
		{
			birthEnum.MoveNext();
			staticbirthEnum.MoveNext();
			
			MoveCirclesScript circlescript = circEnum.Current.GetComponent("MoveCirclesScript") as MoveCirclesScript;
			if(staticbirthEnum.Current==Default){
					
				//remove any that get too large (this shouldn't happen)
				if(circlescript.GetRadius()>500){
					Destroy(circEnum.Current);
					CircleBirth.RemoveAt(count);
					StaticCircleBirth.RemoveAt(count);
					Circles.RemoveAt(count);
					break;
				}
				
				//get distance between point and fish
				float distance = Vector3.Distance(circEnum.Current.transform.position, Fish.transform.position);
	
				//if the circle collides with the fish
				if(circlescript.GetRadius() >=distance){					
					circEnum.Current.SendMessage("SetStatic");
					CurrentCircle = circEnum.Current;
					StaticCircleBirth.RemoveAt(count);
					StaticCircleBirth.Insert(count,DateTime.Now);
					break;
				}				
			}
			//remove if it has been static for 2 seconds
			else if((DateTime.Now - staticbirthEnum.Current).Seconds > 2){
				Destroy(circEnum.Current);
				StaticCircleBirth.RemoveAt(count);
				CircleBirth.RemoveAt(count);
				Circles.RemoveAt(count);
				break;
			}
			
			//Add Ripple
			circEnum.Current.SendMessage("Move");
			
			
						
			count++;
		}
		if(CurrentCircle)
			script.UpdateRotationPoint(new Vector3(CurrentCircle.transform.position.x,CurrentCircle.transform.position.y,50));
		else
			script.UpdateRotationPoint(new Vector3(0,0,0));
	}				

}


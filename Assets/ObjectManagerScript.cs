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
	private HeroFishScript script;
	
	//Hero Object
	public GameObject Fish;
	//Water Object
	public GameObject Water;
	//Circlometer
	public GameObject Circlometer;
	//Healthmeter
	public GameObject Healthmeter;
	
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
		script = Fish.GetComponent("HeroFishScript") as HeroFishScript;
	}
	
	void OnGUI () {
		if(script.GetHealth()==0){
			if(GUI.Button(new Rect (Screen.width/2-60,Screen.height/2-20,120,40),new GUIContent ("Restart"))){
				 Application.LoadLevel("Upstream");
			}
		}
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
		
		
		Healthmeter.renderer.material.mainTextureScale = new Vector2(script.GetHealth()/10.0f,1);
		Healthmeter.transform.localScale = new Vector3(2.5f-((100.0f-script.GetHealth())/40.0f),1,0.5f);
		Healthmeter.transform.position = new Vector3(-85-1.3f*(100.0f-script.GetHealth())/10.0f,68,25);
		
	}
	
	void HandleInput(){
		
		RippleWaterScript waterscript = Water.GetComponent("RippleWaterScript") as RippleWaterScript;
		//if left click
		if(Input.GetMouseButtonDown(0) && Circles.Count < 5){
			//find the location of click and create a circle
			Vector3 Circlepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,51));
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
			
			Circlometer.renderer.material.mainTextureScale = new Vector2(5-Circles.Count,1);
			Circlometer.transform.localScale = new Vector3(5-Circles.Count,1,1);
			Circlometer.transform.position = new Vector3(-74-5.0f*(Circles.Count),-68,25);
		}
	}
	
	void UpdateCircles(float speed){
		
		HeroFishScript script = Fish.GetComponent("HeroFishScript") as HeroFishScript;
		
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
					Circlometer.renderer.material.mainTextureScale = new Vector2(5-Circles.Count,1);
					Circlometer.transform.localScale = new Vector3(5-Circles.Count,1,1);
					Circlometer.transform.position = new Vector3(-74-5.0f*(Circles.Count),-68,25);
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
				Circlometer.renderer.material.mainTextureScale = new Vector2(5-Circles.Count,1);
				Circlometer.transform.localScale = new Vector3(5-Circles.Count,1,1);
				Circlometer.transform.position = new Vector3(-74-5.0f*(Circles.Count),-68,25);
				break;
			}
			
			//Add Ripple
			circEnum.Current.SendMessage("Move");
			
			
						
			count++;
		}
		
		if(script.checkishurt())
			CurrentCircle=null;
		if(CurrentCircle){
			MoveCirclesScript circlescript = CurrentCircle.GetComponent("MoveCirclesScript") as MoveCirclesScript;
			script.UpdateRotationPoint(new Vector3(CurrentCircle.transform.position.x,CurrentCircle.transform.position.y,50),circlescript.GetRadius());
		}
		else
			script.UpdateRotationPoint(new Vector3(0,0,0),0);
	}				

}


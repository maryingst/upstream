using UnityEngine;
using System;
using System.Collections.Generic;

public class MenuManagerScript : MonoBehaviour {
	
	private Quaternion PointToCamera;
	
	//Circle Maintenance Variables
	public Transform Circle;
	private List<GameObject> Circles;
	private List<DateTime> StaticCircleBirth;
	private List<DateTime> CircleBirth;
	private int CircleUID;
	
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
	}
	
	void OnGUI () {
		
		if(GUI.Button(new Rect (Screen.width/2-30,Screen.height/2-10,60,20),new GUIContent ("Play"))){
			 Application.LoadLevel("Upstream");
		}
		if(GUI.Button(new Rect (Screen.width/2-30,Screen.height/2+10,60,20),new GUIContent ("Credits"))){
			 Application.LoadLevel("Credits");
		}			
			
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
							
			}
			
			//Add Ripple
			circEnum.Current.SendMessage("Move");
			
			
						
			count++;
		}
	}				

}


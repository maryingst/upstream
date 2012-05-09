using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectManagerScript : MonoBehaviour {
	
	private Quaternion PointToCamera;
	
	//Circle Maintenance Variables
	public Transform Circle;
	private List<GameObject> Circles;
	private List<DateTime> StaticCircleBirth;
	private int CircleUID;
	
	//Hero Object
	public GameObject Fish;
	
	// Use this for initialization
	void Start () {
		PointToCamera = Quaternion.identity;
		PointToCamera.eulerAngles = new Vector3(90,180,0);
		Circles = new List<GameObject>();
		StaticCircleBirth = new List<DateTime>();
		CircleUID = 0;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
		UpdateCircles(1.03f);
		
		string name = Fish.name;
		AnimatedScript script = Fish.GetComponent(name + "Script") as AnimatedScript;		
		script.Move(Fish);
		
	}
	
	void HandleInput(){
		//if left click
		if(Input.GetMouseButtonDown(0)){
			//find the location of click and create a circle
			Vector3 Circlepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,50));
			Instantiate(Circle,	Circlepoint,PointToCamera);
			
			//find the new circle and name it
			GameObject NewCircle = GameObject.Find("Circle(Clone)");
			NewCircle.name = NewCircle.name + CircleUID.ToString();
			CircleUID++;
			
			//add the new circle to our list
			Circles.Add(NewCircle);
			StaticCircleBirth.Add(new DateTime());
			
		}
	}
	
	void UpdateCircles(float speed){
		
		string name = Fish.name;
		AnimatedScript script = Fish.GetComponent(name + "Script") as AnimatedScript;
		
		List<GameObject>.Enumerator circEnum = Circles.GetEnumerator();
		List<DateTime>.Enumerator birthEnum = StaticCircleBirth.GetEnumerator();
		DateTime Default = new DateTime();
		int count = 0;
		
		while(circEnum.MoveNext())
		{
			birthEnum.MoveNext();
			if(birthEnum.Current==Default){
				//increase the size if it hasn't been made static
				circEnum.Current.transform.localScale = new Vector3(circEnum.Current.transform.localScale.x*speed, 1,circEnum.Current.transform.localScale.z*speed);
				//remove any that get too large (this shouldn't happen)
				if(circEnum.Current.transform.localScale.x>50){
					Destroy(circEnum.Current);
					StaticCircleBirth.RemoveAt(count);
					Circles.RemoveAt(count);
					break;
				}
				
				//get distance between point and fish
				float distance = Vector3.Distance(circEnum.Current.transform.position, Fish.transform.position);

				if((circEnum.Current.transform.localScale.x)*5>=distance){
					script.UpdateRotationPoint(new Vector3(circEnum.Current.transform.position.x,circEnum.Current.transform.position.y,50));
					StaticCircleBirth.RemoveAt(count);
					StaticCircleBirth.Insert(count,DateTime.Now);
					break;
				}				
			}
			//remove if it has been static for 2 seconds
			else if((DateTime.Now - birthEnum.Current).Seconds > 2){
				Destroy(circEnum.Current);
				StaticCircleBirth.RemoveAt(count);
				Circles.RemoveAt(count);
				break;
			}
			count++;
		}
	}
}

using UnityEngine;
using System.Collections;
using System;

public class HeroFishScript : AnimatedScript {
	
	//Health System Variables
	private int health;
	private bool ishurt;
	private DateTime hurttime;
	
	
	// Use this for initialization
	override protected void Init () {
		CurrentAnimation = AnimationType.GoUp;
		FrameCount = 6;
		TotalFrames = new int[(int)AnimationType.AnimationCount] {6,6,0,0,0,0};
		BaseSpeed = new float[(int)AnimationType.AnimationCount] {1,1,0,0,0,0};
		CurrentFrame = 0;
		TotalTime = 0;
		Rotation = new Vector3(0,0,0);
		health = 100;
		ishurt = false;
		base.UpdateVelocity(0,-30);
	}
		
	//Use this to Move Shabba within bounds
	override public void Move(){
		if(!ishurt){
			Rotate ();
			base.Move();						
		}
	}
	
	public void Rotate(){
		if(Rotation.z!=0){
			float radius = Vector3.Distance(transform.position,Rotation);
			
			Vector3 direction = Vector3.right;
			if(Rotation.y-transform.position.y > 0){
				if(Rotation.x-transform.position.x<0)
					direction = Vector3.forward;
				else if(Rotation.x-transform.position.x>=0)
					direction = Vector3.back;
		    }
						
			if(direction!=Vector3.right){
				transform.RotateAround(Rotation, direction, (8000.0f/(radius)) * Time.deltaTime);
				transform.eulerAngles= new Vector3(90,180,0);
				Vector3 temp = (transform.position-Rotation);
				if(temp.x<0)
					transform.Rotate(-direction,(Mathf.Atan2(temp.y,temp.x)*180/Mathf.PI)-180,Space.World);
				else
					transform.Rotate(direction,(Mathf.Atan2(temp.y,temp.x)*180/Mathf.PI),Space.World);
			}
			else
				transform.eulerAngles= new Vector3(90,180,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		TotalTime += Time.deltaTime;
		UpdateAnimation();
		
		if(ishurt && (DateTime.Now - hurttime).Seconds>=1){
			ishurt=false;
			gameObject.renderer.material.color = Color.white;
		}
		Move ();
	}
	
	private void ApplyDamage(int damage){
		if(!ishurt){
			health-=damage;
			ishurt=true;
			hurttime = DateTime.Now;
			gameObject.renderer.material.color = Color.red;
		}
	}
	
	public bool checkishurt(){
		return ishurt;
	}
	
	public int GetHealth(){
		return health;
	}
}

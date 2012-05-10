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
			base.Move();						
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

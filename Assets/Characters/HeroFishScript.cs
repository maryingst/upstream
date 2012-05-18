using UnityEngine;
using System.Collections;
using System;

public class HeroFishScript : AnimatedScript {
	
	//Health System Variables
	private int health;
	private bool ishurt;
	private DateTime hurttime;
	private int totalScore;

	private Vector3 Rotation;
	private float Radius;

	//Collision Sound
	public AudioSource Thud;
	
	//hurt sound
	public AudioSource hurt;
	
	// Use this for initialization
	override protected void Init () {
		CurrentAnimation = AnimationType.GoUp;
		FrameCount = 6;
		TotalFrames = new int[(int)AnimationType.AnimationCount] {6,6,0,0,0,0};
		BaseSpeed = new float[(int)AnimationType.AnimationCount] {1,1,0,0,0,0};
		CurrentFrame = 0;
		TotalTime = 0;
		Rotation = new Vector3(0,0,0);
		Radius = 0;
		health = 100;
		totalScore = 0;
		ishurt = false;
		base.UpdateVelocity(0,-10);
	}
		
	//Use this to Move Shabba within bounds
	override public void Move(){
			Rotate ();
			base.Move();						
	}
	
	public void UpdateRotationPoint(Vector3 Point,float rad){
		Rotation = Point;
		Radius = rad;
	}

	
	public void Rotate(){
		if(Rotation.z!=0){
			float radius = Vector3.Distance(transform.position,Rotation);
			
			Vector3 direction = Vector3.right;
			if(Rotation.y-transform.position.y >= 0){
				if(Rotation.x-transform.position.x<0)
					direction = Vector3.forward;
				else if(Rotation.x-transform.position.x>=0)
					direction = Vector3.back;
		    }
			else if(Mathf.Abs(Rotation.x-transform.position.x)<Radius-10 && Rotation.y-transform.position.y<=-5 && !ishurt){
				base.UpdateVelocity(0,20);
				direction = Vector3.left;
			}
						
			if(direction!=Vector3.right && direction!=Vector3.left){
				base.UpdateVelocity(0,-60);
				transform.RotateAround(Rotation, direction, (5000.0f/(radius)) * Time.deltaTime);
				transform.eulerAngles= new Vector3(90,180,0);
				Vector3 temp = (transform.position-Rotation);
				if(temp.x<0)
					transform.Rotate(-direction,(Mathf.Atan2(temp.y,temp.x)*180/Mathf.PI)-180,Space.World);
				else
					transform.Rotate(direction,(Mathf.Atan2(temp.y,temp.x)*180/Mathf.PI),Space.World);
			}
			else if(direction==Vector3.right){
				transform.eulerAngles= new Vector3(90,180,0);
				base.UpdateVelocity(0,-10);
			}
		}
		else{
				transform.eulerAngles= new Vector3(90,180,0);
				base.UpdateVelocity(0,-10);
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
	
	void OnTriggerEnter(Collider other) {
        if(other.gameObject.name.Contains("Obstacle")){
			ApplyDamage (10);
		}
		if(other.gameObject.name.Contains("Glow")){
			AddScore(10);
			DestroyObject (other.gameObject);
			
			
			// TO-DO: ADD CHIME FOR PICK-UP
		}
    }
	
	private void AddScore(int score)
	{
		totalScore += score;	
	}
	
	private void ApplyDamage(int damage){
		if(!ishurt){
			health-=damage;
			if(health>0){
				ishurt=true;
				hurttime = DateTime.Now;
				gameObject.renderer.material.color = Color.red;
				if(!Thud.isPlaying)
					Thud.Play();
				if(health<30 && !hurt.isPlaying)
					hurt.Play();
			}
			else{
				gameObject.renderer.material.color = Color.red;
				health=0;
			}
		}
		
	}
	
	public bool checkishurt(){
		return ishurt;
	}
	
	public int GetHealth(){
		return health;
	}

}

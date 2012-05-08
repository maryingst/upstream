using UnityEngine;
using System.Collections;

public class HeroFishScript : AnimatedScript {

	// Use this for initialization
	override protected void Init () {
		CurrentAnimation = AnimationType.GoUp;
		FrameCount = 6;
		TotalFrames = new int[(int)AnimationType.AnimationCount] {0,0,0,0,6,6};
		BaseSpeed = new float[(int)AnimationType.AnimationCount] {0,0,0,0,5,5};
		CurrentFrame = 0;
		TotalTime = 0;
		Rotation = 0;
	}
	
	//Use this to update Velocity & Rotation from Input
	override protected void UpdateVR() {
		UpdateVelocity(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
	}
	
	//Use this to Move Shabba within bounds
	override public void Move(GameObject gobject){		
		base.Move(gobject);
	}
	
	// Update is called once per frame
	void Update () {
		TotalTime += Time.deltaTime;
		UpdateVR();
		UpdateAnimation();
	}
	
}

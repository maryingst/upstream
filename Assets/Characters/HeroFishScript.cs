using UnityEngine;
using System.Collections;

public class HeroFishScript : AnimatedScript {

	// Use this for initialization
	override protected void Init () {
		CurrentAnimation = AnimationType.GoUp;
		FrameCount = 6;
		TotalFrames = new int[(int)AnimationType.AnimationCount] {6,6,0,0,0,0};
		BaseSpeed = new float[(int)AnimationType.AnimationCount] {1,1,0,0,0,0};
		CurrentFrame = 0;
		TotalTime = 0;
		Rotation = 0;
	}
	
	//Use this to update Velocity & Rotation from Input
	protected void UpdateVR(GameObject Circle) {
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

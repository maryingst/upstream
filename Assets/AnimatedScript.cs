using UnityEngine;
using System.Collections;

public class AnimatedScript : MonoBehaviour {

		public enum AnimationType{
		GoDown,
		GoUp,
		GoRight,
		TurnLeft,
		TurnRight,
		GoLeft,
		AnimationCount
	}

	protected AnimationType CurrentAnimation;
	protected int FrameCount, CurrentFrame;
	protected float TotalTime;
	protected int[]	TotalFrames; 
	protected float[] BaseSpeed;
	public float[] Velocity = new float[2] {0,0};		
	public float Rotation;

	
	// Use this for initialization
	void Start () {
			Init();
	}
	
	protected virtual void Init(){
	}
	
	protected void UpdateVelocity(float x,float z){
		Velocity[0] = x;
		Velocity[1] = z;
	}
	protected void UpdateRotation(int direction,float rot){
		Rotation = direction*rot;
	}
	
	protected virtual void UpdateVR(){
	}

	public virtual void Move(GameObject gobject){
		string name = gobject.name.Remove(gobject.name.Length - 7,7);
		AnimatedScript script = gobject.GetComponent(name + "Script") as AnimatedScript;
		script.Rotation = 0;
			
		
		Vector3 currentpos = new Vector3(script.Velocity[0],script.Velocity[1],0);
		currentpos = currentpos + gobject.transform.localPosition;
		gobject.transform.localPosition = currentpos;
		gobject.transform.eulerAngles = new Vector3(90, 180, 0);
		gobject.transform.Rotate(new Vector3(0,script.Rotation*45,0));
		
	}
	
	protected void UpdateAnimation(){
		float Speed = Mathf.Sqrt(Velocity[0]*Velocity[0]+Velocity[1]*Velocity[1]) + BaseSpeed[(int)CurrentAnimation];
		if(CurrentAnimation==AnimationType.GoDown && Velocity[1]>0){
			CurrentAnimation=AnimationType.GoUp;
			CurrentFrame = TotalFrames[(int)CurrentAnimation] - CurrentFrame - 1;
			TotalTime = 0;
		}
		else if(CurrentAnimation == AnimationType.GoUp && Velocity[1]<0){
			CurrentAnimation=AnimationType.GoDown;
			CurrentFrame = TotalFrames[(int)CurrentAnimation] - CurrentFrame - 1;
			TotalTime = 0;
		}
		else if(TotalTime > (0.1f/Speed)){
			CurrentFrame++;
			TotalTime = 0;
			CurrentFrame = CurrentFrame%TotalFrames[(int)CurrentAnimation];
			
		}
		renderer.material.mainTextureOffset = new Vector2 ((float)CurrentFrame/(float)FrameCount,(float)CurrentAnimation/(float)AnimationType.AnimationCount);
	}
}

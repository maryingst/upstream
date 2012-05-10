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
	public Vector3 Rotation;

	
	// Use this for initialization
	void Start () {
			Init();
	}
	
	protected virtual void Init(){
	}
	
	public void UpdateVelocity(float x,float z){
		Velocity[0] = x;
		Velocity[1] = z;
	}
	public void UpdateRotationPoint(Vector3 Point){
		Rotation = Point;
	}

	public virtual void Move(){
		
		Vector3 currentpos = new Vector3(Velocity[0],Velocity[1],0) * Time.deltaTime;
		currentpos = currentpos + transform.localPosition;
		transform.localPosition = currentpos;
		
	}
	
	protected void UpdateAnimation(){
		float Speed = BaseSpeed[(int)CurrentAnimation];
		if(TotalTime > (0.1f/Speed)){
			CurrentFrame++;
			TotalTime = 0;
			CurrentFrame = CurrentFrame%TotalFrames[(int)CurrentAnimation];
			
		}
		renderer.material.mainTextureOffset = new Vector2 ((float)CurrentFrame/(float)FrameCount,(float)CurrentAnimation/(float)AnimationType.AnimationCount);
	}
}

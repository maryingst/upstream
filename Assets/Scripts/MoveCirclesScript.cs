using UnityEngine;
using System.Collections;

public class MoveCirclesScript : MonoBehaviour {
	
	
protected float scrollSpeed;	
protected bool stopped;
protected float radius;
protected float growspeed;
protected LineRenderer Circle;

	// Use this for initialization
	void Start () {
	
							/// TO-DO:  CHANGE THIS TO A SPEED VARIABLE
		scrollSpeed = 40.0f;
		stopped = false;
		growspeed=1.04f;
		radius = 1.0f;
		Circle = gameObject.GetComponent<LineRenderer>();
		Circle.SetVertexCount((int)radius*6);
		Circle.SetColors(Color.green,Color.green);
		Circle.SetWidth(5f,5f);
	}
	
	void ConstructCircle(){
		float y;
		float x;
		Circle.SetVertexCount((int)radius*6);
		for(int i=0;i<(int)radius*6;i++){
			y=Mathf.Cos ((i-((int)radius*3))*(Mathf.PI/(float)(((int)(radius*6) - 1)/2.0f)))*radius + transform.position.y;
			x=Mathf.Sin ((i-((int)radius*3))*(Mathf.PI/(float)(((int)(radius*6) - 1)/2.0f)))*radius + transform.position.x;
			Circle.SetPosition(i,new Vector3(x,y,transform.position.z));
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		ConstructCircle();
	}
	
	public void Move () {
		Vector3 newPosition;
		newPosition = transform.position;
		newPosition.y -= scrollSpeed * Time.deltaTime;
		transform.position = newPosition; 
		if(!stopped)
		{
			radius += scrollSpeed * Time.deltaTime;
		}
	}
	
	public float GetRadius () {
		return radius;
	}
	
	void SetStatic()
	{
		stopped = true;
		Circle.SetColors(Color.red,Color.red);
		//gameObject.renderer.material.SetTextureOffset = new Vector2(0.5f, 0.5f);
	}
}

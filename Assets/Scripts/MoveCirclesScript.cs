using UnityEngine;
using System.Collections;

public class MoveCirclesScript : MonoBehaviour {
	
	
protected float scrollSpeed;	
protected bool stopped;

	// Use this for initialization
	void Start () {
	
							/// TO-DO:  CHANGE THIS TO A SPEED VARIABLE
		scrollSpeed = 30.0f;
		stopped = false;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!stopped)
		{
			Vector3 newPosition;
			newPosition = transform.position;
			newPosition.y -= scrollSpeed * Time.deltaTime;
			
			transform.position = newPosition; 
		}
	}
	
	
	void SetStatic()
	{
		stopped = true;
		gameObject.renderer.material.color = Color.red;
		//gameObject.renderer.material.SetTextureOffset = new Vector2(0.5f, 0.5f);
	}
}

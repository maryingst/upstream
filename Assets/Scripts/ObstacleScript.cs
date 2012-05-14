using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	private float gameSpeed;
	
	// Use this for initialization
	void Start () {
		gameSpeed = (Camera.mainCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript).GetGameSpeed()*30;	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Obstacle falls
		Vector3 newPos;
		newPos = transform.position;
		newPos.y -= gameSpeed * Time.deltaTime;
		transform.position = newPos;
		
		
		// Obstacle dies
		if(newPos.y < -150.0f)
			DestroyObject(gameObject);
		
	
	}
}

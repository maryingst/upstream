using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// Obstacle falls
		Vector3 newPos;
		newPos = transform.position;
		newPos.y -= 100.0f * Time.deltaTime;
		transform.position = newPos;
		
		
		// Obstacle dies
		if(newPos.y < -150.0f)
			DestroyObject(gameObject);
		
	
	}
}

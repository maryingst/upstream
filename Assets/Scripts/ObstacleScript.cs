using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 newPos;
		newPos = transform.position;
		newPos.y -= 100.0f * Time.deltaTime;
		
		if(newPos.y < -150.0f)
			DestroyObject(gameObject);
		
		transform.position = newPos;
	
	}
}

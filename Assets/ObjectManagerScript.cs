using UnityEngine;
using System.Collections.Generic;

public class ObjectManagerScript : MonoBehaviour {

	public Transform Circle;
	private Quaternion PointToCamera;
	
	// Use this for initialization
	void Start () {
		PointToCamera = Quaternion.identity;
		PointToCamera.eulerAngles = new Vector3(90,180,0);
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
		UpdateCircles(1.03f);
	}
	
	void HandleInput(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 Circlepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane));
			Instantiate(Circle,	Circlepoint,PointToCamera);
		}
	}
	
	void UpdateCircles(float speed){
		foreach(GameObject circle in GameObject.FindGameObjectsWithTag("Respawn"))
		{
			
			circle.transform.localScale = new Vector3(circle.transform.localScale.x*speed, 1,circle.transform.localScale.z*speed);
			if(circle.transform.localScale.x>50)
				Destroy(circle);
		}
	}
}

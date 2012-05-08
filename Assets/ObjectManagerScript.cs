using UnityEngine;
using System.Collections;

public class ObjectManagerScript : MonoBehaviour {

	public Transform Circle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
	}
	
	void HandleInput(){
		if(Input.GetMouseButtonDown(0)){
			Vector3 Circlepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane));
			
			Instantiate(Circle,
				Circlepoint,
				Quaternion.identity);
		}
	}
}

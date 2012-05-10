using UnityEngine;
using System;
using System.Collections;


public class CameraScrollScript : MonoBehaviour {
	

	public GameObject refCamera;
	

protected float scrollSpeed;
	
	
	// Use this for initialization
	void Start () {
		
							// TO-DO: CHANGE THIS TO A SPEED VARIABLE
		ObjectManagerScript getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		
		scrollSpeed = getSpeedScript.GetGameSpeed() * 0.1f;
		
			}

	
	// Update is called once per frame
	void Update () {
		
		Vector2 newOffset;
		newOffset = gameObject.renderer.material.GetTextureOffset("_MainTex");
		newOffset.y += scrollSpeed * Time.deltaTime;
		
		gameObject.renderer.material.SetTextureOffset("_MainTex", newOffset);

	}
}

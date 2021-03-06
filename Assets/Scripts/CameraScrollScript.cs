using UnityEngine;
using System;
using System.Collections;


public class CameraScrollScript : MonoBehaviour {
	

	ObjectManagerScript getSpeedScript;
	

protected float scrollSpeed;
	
	
	// Use this for initialization
	void Start () {
		
	
		getSpeedScript = Camera.mainCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		if(getSpeedScript)
			scrollSpeed = getSpeedScript.GetGameSpeed() * 0.1f;
		else
			scrollSpeed =0.1f;
		
			}

	
	// Update is called once per frame
	void Update () {
		if(getSpeedScript)
			scrollSpeed = getSpeedScript.GetGameSpeed() * 0.1f;
		else
			scrollSpeed =0.1f;
		
		Vector2 newOffset;
		newOffset = gameObject.renderer.material.GetTextureOffset("_MainTex");
		newOffset.y += scrollSpeed * Time.deltaTime;
		
		gameObject.renderer.material.SetTextureOffset("_MainTex", newOffset);

	}
}

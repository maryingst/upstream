using UnityEngine;
using System;
using System.Collections;


public class CameraScrollScript : MonoBehaviour {
	

	public GameObject refCamera;
	ObjectManagerScript getSpeedScript;
	

protected float scrollSpeed;
	
	
	// Use this for initialization
	void Start () {
		
	
		getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;	
		scrollSpeed = getSpeedScript.GetGameSpeed() * 0.1f;
		
			}

	
	// Update is called once per frame
	void Update () {
		scrollSpeed = getSpeedScript.GetGameSpeed() * 0.1f;
		
		Vector2 newOffset;
		newOffset = gameObject.renderer.material.GetTextureOffset("_MainTex");
		newOffset.y += scrollSpeed * Time.deltaTime;
		
		gameObject.renderer.material.SetTextureOffset("_MainTex", newOffset);

	}
}

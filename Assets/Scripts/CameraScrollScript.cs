using UnityEngine;
using System.Collections;


public class CameraScrollScript : MonoBehaviour {
	

	
	

protected float scrollSpeed;
	
	
	// Use this for initialization
	void Start () {
		
							// TO-DO: CHANGE THIS TO A SPEED VARIABLE
		scrollSpeed = 0.12f;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 newOffset;
		newOffset = gameObject.renderer.material.GetTextureOffset("_MainTex");
		newOffset.y += scrollSpeed * Time.deltaTime;
		
		gameObject.renderer.material.SetTextureOffset("_MainTex", newOffset);
		
	//	gameObject.renderer.material.SetTextureOffset( "riverbed",
	//		gameObject.renderer.material.GetTextureOffset() + scrollSpeed * Time.deltaTime);
		
		//Material.SetTextureOffset( Material.GetTetureOffset() += scrollSpeed * Time.deltaTime);
	}
}

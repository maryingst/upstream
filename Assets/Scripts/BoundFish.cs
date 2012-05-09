using UnityEngine;
using System.Collections;

public class BoundFish : MonoBehaviour {
	
	
	public Transform refWalls;
	private Vector2 wallWidth;

	// Use this for initialization
	void Start () {
		
		wallWidth.x = refWalls.transform.position.x - (refWalls.transform.localScale.x * 5.0f);
		wallWidth.y = refWalls.transform.position.x + (refWalls.transform.localScale.x * 5.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x <= wallWidth.x)
		{
			
			Vector3 newPos;
			newPos = transform.position;
			newPos.x = wallWidth.x;
			transform.position = newPos;
			
					
			gameObject.renderer.material.color = Color.red;
			
				// TO-DO: CODE FOR DAMAGE
			
				// TO-DO: CODE FOR BOUNCE-BACK
		}
		else if (transform.position.x >= wallWidth.y)
		{
			Vector3 newPos;
			newPos = transform.position;
			newPos.x = wallWidth.y;
			transform.position = newPos;
			
			gameObject.renderer.material.color = Color.red;
				// TO-DO: CODE FOR DAMAGE
			
				// TO-DO: CODE FOR BOUNCE-BACK
		}
		else
		{
			gameObject.renderer.material.color = Color.white;	
		}
	
	}
}

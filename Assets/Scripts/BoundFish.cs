using UnityEngine;
using System.Collections;

public class BoundFish : MonoBehaviour {
	
	
	public Transform refWalls;
	private Vector2 wallWidth;
	private Vector2 wallHeight;
	HeroFishScript script;

	// Use this for initialization
	void Start () {
		
		wallWidth.x = refWalls.transform.position.x - (refWalls.transform.localScale.x * 4.0f);
		wallWidth.y = refWalls.transform.position.x + (refWalls.transform.localScale.x * 4.0f);
		wallHeight.x = Camera.main.ScreenToWorldPoint(new Vector3(0,0,50)).y;
		wallHeight.y = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,50)).y;
		
		script = gameObject.GetComponent("HeroFishScript") as HeroFishScript;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x < wallWidth.x)
		{
			
			Vector3 newPos;
			newPos = transform.position;
			newPos.x = wallWidth.x;
			transform.position = newPos;
			
			//CODE FOR DAMAGE
			gameObject.SendMessage("ApplyDamage",10);
			
				// TO-DO: CODE FOR BOUNCE-BACK
		}
		else if (transform.position.x > wallWidth.y)
		{
			Vector3 newPos;
			newPos = transform.position;
			newPos.x = wallWidth.y;
			transform.position = newPos;
			
			//CODE FOR DAMAGE
			gameObject.SendMessage("ApplyDamage",10);
			
				// TO-DO: CODE FOR BOUNCE-BACK
		}
		
		if (transform.position.y < wallHeight.x+transform.localScale.y*15)
		{
			Vector3 newPos;
			newPos = transform.position;
			newPos.y = wallHeight.x+transform.localScale.y*15;
			transform.position = newPos;
		
		}
		else if (transform.position.y > wallHeight.y)
		{
			script.Rotate();
			Vector3 newPos;
			newPos = transform.position;
			newPos.y = wallHeight.y;
			transform.position = newPos;
			
		}
	
	}
}

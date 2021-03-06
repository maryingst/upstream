using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	
	
	int numSpawner;
	int numCycles;
	float waitTime;
	
	public GameObject parentSpawner;
	SpawnerPatterns patterns;
	ManagingScript manager;
	
	float gameSpeed;
	public GameObject refCamera;
	ObjectManagerScript getSpeedScript;
	public Transform prefabObstacle;
	public Transform prefabGlow;
	
	
	// Use this for initialization
	void Start () {
		
		getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		gameSpeed = getSpeedScript.GetGameSpeed()/5f;
		
		waitTime = 1.0f;
		numCycles = 0;
		
		int isobstacle = Random.Range(1,100);
		
		     if (gameObject.CompareTag("Left"))
		{	numSpawner = 0; 
			
			}
		
		else if (gameObject.CompareTag("MidLeft"))
		{	numSpawner = 1; 
			
			}
		
		
		else if (gameObject.CompareTag("Mid"))
		{	numSpawner = 2; 
			
			}
		
		
		else if (gameObject.CompareTag("MidRight"))
		{	numSpawner = 3; 
			
			}
		
		
		else if (gameObject.CompareTag("Right"))
		{	numSpawner = 4; 
			
			}
		
		patterns = parentSpawner.GetComponent("SpawnerPatterns") as SpawnerPatterns;
		manager = parentSpawner.GetComponent("ManagingScript") as ManagingScript;
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameSpeed = getSpeedScript.GetGameSpeed();
		
		// countdown to next obstacle
		waitTime -= Time.deltaTime;
		
		int isobstacle = Random.Range(1,100);
		// obstacle created, new countdown starts
		if(waitTime <= 0.0f && numCycles < 10)
		{
			if(isobstacle<75)
				Instantiate (prefabObstacle, transform.position, Quaternion.identity);
			else
				Instantiate (prefabGlow, transform.position, Quaternion.identity);
			waitTime = gameSpeed * 4.0f;
			--numCycles;
			if(numCycles <= 0)
			{
				manager.addLast();
				numCycles = 1;
			}
		}
	
	}
	
	
	// Starting a new pattern: called by ManagingScript
	public void Cue (int newPattern, int newCycles)
	{
		//curPattern = newPattern;
		numCycles = newCycles; //manager.GetCycles();
		waitTime = 2.0f * patterns.GetTime(newPattern, numSpawner);				
	}
}

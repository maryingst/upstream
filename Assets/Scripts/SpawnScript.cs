using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	
	int curPattern;
	int curPlace;
	float waitTime;
	public GameObject parentSpawner;
	SpawnerPatterns patterns;
	ManagingScript manager;
	int patternLength;
	
	float gameSpeed;
	public GameObject refCamera;
	ObjectManagerScript getSpeedScript;
	public Transform prefabObstacle;
	
	
	// Use this for initialization
	void Start () {
		
		getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		gameSpeed = getSpeedScript.GetGameSpeed();
		
		waitTime = 100.0f;
		curPlace = 0;
		patterns = parentSpawner.GetComponent("SpawnerPatterns") as SpawnerPatterns;
		manager = parentSpawner.GetComponent("ManagingScript") as ManagingScript;
		patternLength = patterns.GetPatternLength();
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		gameSpeed = getSpeedScript.GetGameSpeed();
		
		waitTime -= gameSpeed * Time.deltaTime;
		
		if(waitTime <= 0.0f && curPlace >= 0)
		{
			Instantiate (prefabObstacle, transform.position, Quaternion.identity);
			waitTime = patterns.GetTime(curPattern, curPlace);
			++curPlace;
			if(curPlace >= 10)
			{
				manager.addLast();
				curPlace = -1;
			}
		}
	
	}
	
	public void Cue (int newPattern)
	{
		curPattern = newPattern;
		curPlace = 0;
		waitTime = 0.0f;
				
	}
}

using UnityEngine;
using System.Collections;

public class ManagingScript : MonoBehaviour {
	
	
	ObjectManagerScript getSpeedScript;
	SpawnScript[] spawners = new SpawnScript[5];
	
	int onLast;
	int numPatterns;
	int numCycles;
	int curPattern;
	bool cuePatterns;
	float gameSpeed;
	
	
	public GameObject refCamera;
	public Transform spawnerLeft;
	public Transform spawnerMid;
	public Transform spawnerRight;
	public Transform spawnerMidLeft;
	public Transform spawnerMidRight;
	

	// Use this for initialization
	void Start () {
		
		onLast = 5;
		SpawnerPatterns getNumPatterns = gameObject.GetComponent("SpawnerPatterns") as SpawnerPatterns;
		numPatterns = getNumPatterns.GetNumPatterns();
		getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		gameSpeed = getSpeedScript.GetGameSpeed();
		spawners[0] = spawnerLeft.GetComponent("SpawnScript") as SpawnScript;
		spawners[1] = spawnerMidLeft.GetComponent("SpawnScript") as SpawnScript;
		spawners[2] = spawnerMid.GetComponent("SpawnScript") as SpawnScript;
		spawners[3] = spawnerMidRight.GetComponent("SpawnScript") as SpawnScript;
		spawners[4] = spawnerRight.GetComponent("SpawnScript") as SpawnScript;
	
	}
	
	public void addLast()
	{
		++onLast;
	}

	
	// Update is called once per frame
	void Update () {
		gameSpeed = getSpeedScript.GetGameSpeed();
		
		// If a new pattern is needed
		if(onLast >= 5)
		{
			curPattern = Random.Range(0, numPatterns-1);
			numCycles = Random.Range(1, 9);
			
			onLast = 0;
			cuePatterns = true;
		}
	
		// If cueing the new pattern for the spawners
		if(cuePatterns)
		{
			for(int i = 0; i < 5; ++i)
			{
				spawners[i].Cue (curPattern, numCycles);
			}
		}
	}
}

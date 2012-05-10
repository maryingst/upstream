using UnityEngine;
using System.Collections;

public class ManagingScript : MonoBehaviour {
	//SpawningScript
	//float SpawnerLeftTimer;
	//float SpawnerMidTimer;
	//float SpawnerRightTimer;	
	//int[] currPatterns = new int[3];
	
	
	ObjectManagerScript getSpeedScript;
	SpawnScript[] spawners = new SpawnScript[3];
	float[] currWaitTime = new float[3];
	int onLast;
	int numPatterns;
	int curPattern;
	bool[] cuePattern = new bool[3];
	float gameSpeed;
	public GameObject refCamera;
	public Transform spawnerLeft;
	public Transform spawnerMid;
	public Transform spawnerRight;
	

	// Use this for initialization
	void Start () {
		
		onLast = 3;
		SpawnerPatterns getNumPatterns = gameObject.GetComponent("SpawnerPatterns") as SpawnerPatterns;
		numPatterns = getNumPatterns.GetNumPatterns();
		getSpeedScript = refCamera.GetComponent("ObjectManagerScript") as ObjectManagerScript;
		gameSpeed = getSpeedScript.GetGameSpeed();
		spawners[0] = spawnerLeft.GetComponent("SpawnScript") as SpawnScript;
		spawners[1] = spawnerMid.GetComponent("SpawnScript") as SpawnScript;
		spawners[2] = spawnerRight.GetComponent("SpawnScript") as SpawnScript;
	
	}
	
	public void addLast()
	{
		++onLast;
	}
	
	// Update is called once per frame
	void Update () {
		gameSpeed = getSpeedScript.GetGameSpeed();
		

			if(onLast >= 3)
		{
			curPattern = Random.Range(0, numPatterns);
			currWaitTime[0] = Random.Range(0.0f, 1.0f);
			if(currWaitTime[0] < 0.5f)
			{
				currWaitTime[1] = Random.Range(0.3f, 1.0f);
				currWaitTime[2] = Random.Range(0.5f, 1.0f);
			}
			else
			{
				currWaitTime[1] = Random.Range(0.0f, 0.8f);
				currWaitTime[2] = Random.Range(0.0f, 0.5f);
			}
			onLast = 0;
			cuePattern[0] = true;
			cuePattern[1] = true;
			cuePattern[2] = true;
		}
	
			
		if(cuePattern[0] == true)
		{
			currWaitTime[0] -= (gameSpeed * Time.deltaTime);
			if(currWaitTime[0] <= 0.0f)
			{
				spawners[0].Cue(curPattern);
				cuePattern[0] = false;
			}
		}
		if(cuePattern[1] == true)
		{
			currWaitTime[1] -= (gameSpeed * Time.deltaTime);
			if(currWaitTime[1] <= 0.0f)
			{
				spawners[1].Cue(curPattern);
				cuePattern[1] = false;
			}
		}
		if(cuePattern[2] == true)
		{
			currWaitTime[2] -= (gameSpeed * Time.deltaTime);
			if(currWaitTime[2] <= 0.0f)
			{
				spawners[2].Cue(curPattern);
				cuePattern[2] = false;
			}
		}
			
	}
}

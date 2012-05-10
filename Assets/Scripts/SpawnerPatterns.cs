//#define _PATTERN_LENGTH	10
//#define	_NUM_PATTERNS	2



using UnityEngine;
using System.Collections;



public class SpawnerPatterns : MonoBehaviour {
	
	
	static int _PATTERN_LENGTH = 10;
	static int _NUM_PATTERNS = 3;
	
	
	float[,] Patterns = new float[_NUM_PATTERNS, _PATTERN_LENGTH];


	
	public int GetNumPatterns()
	{
		return _NUM_PATTERNS;
	}
	public int GetPatternLength()
	{
		return _PATTERN_LENGTH;
	}
	
	// Use this for initialization
	void Start () {
	
		float [] Pattern1 = new float[] { 1.0f, 1.0f, 0.5f, 0.5f, 0.5f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f };
		float [] Pattern2 = new float[] { 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f };
		float [] Pattern3 = new float[] { 0.2f, 2.0f, 0.2f, 2.0f, 0.2f, 2.0f, 0.2f, 2.0f, 0.2f, 2.0f };
		
		
		//Patterns[0] = Pattern1;
		for(int i = 0; i < _PATTERN_LENGTH; ++i)
		{
			Patterns[0,i] = Pattern1[i];
			Patterns[1,i] = Pattern2[i];
			Patterns[2,i] = Pattern3[i];
		}
		
	}
	public float GetTime(int numPattern, int numPlace)
	{
		return Patterns[numPattern, numPlace];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

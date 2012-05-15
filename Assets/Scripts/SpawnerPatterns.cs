//#define _PATTERN_LENGTH	10
//#define	_NUM_PATTERNS	2



using UnityEngine;
using System.Collections;



public class SpawnerPatterns : MonoBehaviour {
	
// using these as #defines

	static int _NUM_NEW_PATTERNS = 5;
	
	
	float[,] NewPatterns = new float [_NUM_NEW_PATTERNS, 5];
	
	public int GetNumPatterns()
	{
		return _NUM_NEW_PATTERNS;
	}

	
	// Use this for initialization
	void Start () {
	
		
		// new kinds of patterns
		float [] Pattern1 = new float[] { 0.0f, 0.3f, 0.6f, 0.9f, 1.2f };
		float [] Pattern2 = new float[] { 1.2f, 0.9f, 0.6f, 0.3f, 0.0f };
		float [] Pattern3 = new float[] { 0.0f, 0.5f, 1.0f, 0.5f, 1.0f };
		float [] Pattern4 = new float[] { 1.0f, 0.5f, 0.0f, 0.5f, 1.0f };
		float [] Pattern5 = new float[] { 0.8f, 0.2f, 1.0f, 0.4f, 0.0f };
		
	
		for(int i = 0; i < 5; ++i)
		{
			NewPatterns[0,i] = Pattern1[i];
			NewPatterns[1,i] = Pattern2[i];
			NewPatterns[2,i] = Pattern3[i];
			NewPatterns[3,i] = Pattern4[i];
			NewPatterns[4,i] = Pattern5[i];
		}
		
	}
	public float GetTime(int numPattern, int numSpawner)
	{
		return NewPatterns[numPattern, numSpawner];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

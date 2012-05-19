using UnityEngine;
using System.Collections.Generic;

public class Score{
		public int score;
		public string name;
}

public class HighScoreScript : MonoBehaviour {
	
	
	
	private List<Score> scores;
	
	// Use this for initialization
	void Start () {
		scores = new List<Score>();
		LoadHighScores();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void LoadHighScores(){
		int i = 1;
		while(i <= 20 && PlayerPrefs.HasKey("Score" + i.ToString())){
			Score temp = new Score();
			temp.score = PlayerPrefs.GetInt("Score" + i.ToString());
			temp.name = PlayerPrefs.GetString("Name" + i.ToString());
			
			scores.Add(temp);
			i++;
		}
	}
	public bool IsTopScore(int score){
		int i = scores.Count-1;
		if(i==-1)
			return true;
		if(((Score)(scores.ToArray()[i])).score<score)
			return true;
		return false;
	}
	
	public void InsertScore(int score, string name){
		int index = 0;
		Score temp = new Score();
		temp.score = score;
		temp.name = name;
		foreach(Score entry in scores){
			if(score >= entry.score){
				scores.Insert(index,temp);
				break;
			}
			index++;
		}
		if(scores.Count==0)
			scores.Add (temp);
		if(scores.Count > 20)
			scores.RemoveRange(20,scores.Count-20);
		WriteScores();
	}
	
	private void WriteScores(){
		int index = 1;
		foreach(Score entry in scores){
			PlayerPrefs.SetInt("Score" + index.ToString(),entry.score);
			PlayerPrefs.SetString("Name" + index.ToString(),entry.name);
			index++;
		}
	}
	
	public List<Score> GetAllScores(){
		return scores;
	}
		
}

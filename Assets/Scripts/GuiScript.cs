using UnityEngine;
using System.Collections;

public class GuiScript : MonoBehaviour {
	
	public GameObject Fish;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		
		HeroFishScript script = Fish.GetComponent("HeroFishScript") as HeroFishScript;
		
		GUI.TextArea (new Rect (10,10,75,75), "Health: " + (script.GetHealth()).ToString());
	}
}

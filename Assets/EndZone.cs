using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {
	private bool finish_shank = false;
	private bool finish_rock = false;
	private bool finish_big = false;

	// Use this for initialization
	void Start () {
		print ("shank" + finish_shank);
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
	
	void OnCollisionEnter(Collision other){
		print(other.rigidbody.name);
		if(other.rigidbody.name == "Player Shank"){
			finish_shank = true;
			print ("shank" + finish_shank);
		}
		if(other.rigidbody.name == "Player Rock"){
			finish_rock = true;
		}
		if(other.rigidbody.name == "Player Big"){
			finish_big = true;
		}
	}
	
	
}

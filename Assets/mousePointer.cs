using UnityEngine;
using System.Collections;

public class mousePointer : MonoBehaviour {
	public static bool disappear = true;
	public GameObject particleSystem;
	public AudioClip clicked;
	public static bool mouseIsClicked = false;
	// Use this for initialization
	void Start () {
		renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(mouseIsClicked){
			SendMessage ("Clicked");
		}
		if(disappear){
			renderer.enabled = false;
		}
	}
	void Clicked(){
		audio.PlayOneShot(clicked);
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = transform.position.z - Camera.mainCamera.transform.position.z;
		mouseIsClicked = false;
		renderer.enabled = true;
		transform.position = Camera.mainCamera.ScreenToWorldPoint(mousePos);
		Instantiate (particleSystem, Camera.mainCamera.ScreenToWorldPoint(mousePos), transform.rotation);
	}
}

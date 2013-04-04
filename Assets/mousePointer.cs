using UnityEngine;
using System.Collections;
// ABL38 - mousePointer is for the code of the white circle that appears whenever you click any spot on the map
// it represents the spot of the position that the mouse clicked and disappears accordingly
// all code here is made by ABL38 - Andy Lin
public class mousePointer : MonoBehaviour {
	// specifies whether the mousePointer should disappear or not
	public static bool disappear = true;
	// the particles that explode every time you click
	public GameObject particleSystem;
	// the sound of the click
	public AudioClip clicked;
	// if the mouse is clicked or not
	public static bool mouseIsClicked = false;
	// Use this for initialization
	void Start () {
		renderer.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		// if the mouse is clicked, call the Clicked function
		if(mouseIsClicked){
			SendMessage ("Clicked");
		}
		// if the disappear is set to true, make the mousePointer invisible
		if(disappear){
			renderer.enabled = false;
		}
	}
	void Clicked(){
		// play the click sound
		audio.PlayOneShot(clicked);
		// create a Vector3 variable that holds the position of the clicked sot
		Vector3 mousePos = Input.mousePosition;
		// the z is set to the difference of the position to the camera
		mousePos.z = transform.position.z - Camera.mainCamera.transform.position.z;
		// set mouseIsclicked to false
		mouseIsClicked = false;
		// make it visible
		renderer.enabled = true;
		// change the position from the screen's mouse position to the game screen's mouse position
		transform.position = Camera.mainCamera.ScreenToWorldPoint(mousePos);
		// create a particle explosion at the clicked spot
		Instantiate (particleSystem, Camera.mainCamera.ScreenToWorldPoint(mousePos), transform.rotation);
	}
}

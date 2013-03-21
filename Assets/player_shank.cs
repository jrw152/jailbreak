using UnityEngine;
using System.Collections;

public class player_shank : MonoBehaviour {
	private Vector3 clickedTarget;
	public bool walking;
	public static bool controlled;
	private bool allowedToPlayAudio;
	private bool allowedToPlayWalkAudio = true;
	public float speed = 4;
	public AudioClip[] radioGo = new AudioClip[9];
	public AudioClip[] footsteps = new AudioClip[6];
	public string player = "a";
	private bool keyIsDown = false;
	
	// Use this for initialization
	void Start () {
		controlled = true;
		allowedToPlayAudio = true;
		clickedTarget = transform.position;
    	walking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (player)){
			keyIsDown = true;
		}
		if(Input.GetKeyUp (player)){
			keyIsDown = false;
		}
		if ((Input.GetMouseButtonUp(0)) && (controlled) && (keyIsDown)) {
			mousePointer.mouseIsClicked = true;
			if(allowedToPlayAudio){
				SendMessage ("PlayRadioSound");
			}
				// set the clickedTarget's position to that of the mouse
				clickedTarget.x = Input.mousePosition.x;
				clickedTarget.y = Input.mousePosition.y;
				clickedTarget.z = transform.position.z - Camera.mainCamera.transform.position.z;
				// walking is now true 
				walking = true;
		}
		if(walking){
			mousePointer.disappear = false;
			rigidbody.constraints = RigidbodyConstraints.None;
			// checks if the clickedTarget is far enough
			// prevents the character from inefficiently moving a little bit
			if((Vector2.Distance(Camera.mainCamera.ScreenToWorldPoint(clickedTarget),transform.position))>0.5){
				Vector3 worldPos = Camera.mainCamera.ScreenToWorldPoint(clickedTarget);
				transform.LookAt (worldPos);
				transform.Translate(Vector3.forward * Time.deltaTime*speed);
				
				if(allowedToPlayWalkAudio){
					SendMessage ("PlayWalkSound");
				}
			}
			else{
				rigidbody.constraints = RigidbodyConstraints.FreezeAll;
				mousePointer.disappear = true;
				walking = false;
			}
		}
	}
	void OnCollisionEnter(Collision collision){
		if(walking){
			mousePointer.disappear = true;
			walking = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	IEnumerator PlayRadioSound(){
		allowedToPlayAudio = false;
		yield return new WaitForSeconds (0.2f);
		audio.clip = radioGo[Random.Range(0, 9)];
		audio.PlayOneShot(audio.clip);
		yield return new WaitForSeconds (audio.clip.length);
		allowedToPlayAudio = true;
	}
	IEnumerator PlayWalkSound(){
		allowedToPlayWalkAudio = false;
		audio.PlayOneShot(footsteps[Random.Range(0, 6)]);
		yield return new WaitForSeconds (0.35f);
		allowedToPlayWalkAudio = true;
	}
}

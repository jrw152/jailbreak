using UnityEngine;
using System.Collections;

public class player_shank : MonoBehaviour {
	public string character;
	public static bool shank = false;
	public static bool rock = false;
	public static bool big = false;
	public static bool gitSelected = false;
	private Vector3 clickedTarget;
	public bool walking;
	public static bool controlled = false;
	private bool allowedToPlayAudio;
	private bool allowedToPlayWalkAudio = true;
	public float speed = 4;
	public AudioClip[] radioGo = new AudioClip[9];
	public AudioClip[] footsteps = new AudioClip[6];
	public string player = "a";
	private bool keyIsDown = false;
	public static int activeGUI = 0; //Used to prevent GUI selection automatically moving the player, Should only be set to 2 max.
	public int HealthInitial = 100;
	private int Health;
	private int[] ReturnHelper = new int[2];
	private int GuardDamage = 5;
	
	// Use this for initialization
	void Start () {
		allowedToPlayAudio = true;
		clickedTarget = transform.position;
    	walking = false;
		
		//Initializes Health Counter
		Health = HealthInitial;
		if(player=="a"){
			ReturnHelper[0]=1;
		}
		if(player=="s"){
			ReturnHelper[0]=2;
		}
		if(player=="d"){
			ReturnHelper[0]=3;
		}
		ReturnHelper[1]=Health;
		GUIScript.Helper = ReturnHelper;
	}
	
	// Update is called once per frame
	void Update () {
		if(activeGUI == 0){//Prevents unintended movement when using the GUI
			SendMessage ("ResetPlayer");
			SendMessage ("GetPlayerControl");
			if(Input.GetKeyDown (player)){
				keyIsDown = true;
			}
			if(Input.GetKeyUp (player)){
				keyIsDown = false;
			}
			if ((Input.GetMouseButtonUp(0)) && ((controlled) || (keyIsDown)) && (!gitSelected)) {
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
			gitSelected = false;
		}
		else{
			activeGUI -= 1;
		}		
	}
	
	void OnTriggerEnter(Collider other){
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
	
	void GetPlayerControl(){
		if((character.Equals ("shank")) && (shank)){
			controlled = true;
		}
		else if((character.Equals ("shank")) && (!shank)){
			controlled = false;
		}
		if((character.Equals ("rock")) && (rock)){
			controlled = true;
		}
		else if((character.Equals ("rock")) && (!rock)){
			controlled = false;
		}
		if((character.Equals ("big")) && (big)){
			controlled = true;
		}
		else if(character.Equals ("big") && (!big)){
			controlled = false;
		}
	}
	
	void ResetPlayer(){
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
	}
	
	void Injured(string x){
		//Damage
		if(x == ("Guard")){
			Health = Health - GuardDamage;
			ReturnHelper[1]=Health;
			if(Health<=0){
				DeadPlayer();
			}
			GUIScript.Helper = ReturnHelper;	
		}
	}
	
	
	void DeadPlayer(){
		transform.position=new Vector3(-92.69604f, -32.47769f, 0);
		Health= HealthInitial/2;
		HealthInitial= Health;
	}
}
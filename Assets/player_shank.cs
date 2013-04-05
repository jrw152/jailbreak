using UnityEngine;
using System.Collections;
// ABL38 - contrary to its name, the player_shank code is the main code used for every type of prisoner.
// the type of prisoner is deemed by the public string character, where you either input shank, rock, or big to specify what type of prisoner it is

// whenever you see other member's code, it is not my code. Most of the commented code found in here is my code.
public class player_shank : MonoBehaviour {

	// specifies what type of character it is
	public string character;
	// these booleans specify if these are being controlled or not
	public static bool shank = false;
	public static bool rock = false;
	public static bool big = false;
	// this specifies if the GUI is being selected, so the prisoner's wont walk towards the GUI when a button is clicked
	public static bool gitSelected = false;
	// contains the position of where the player has kicked
	private Vector3 clickedTarget;
	// if the character is walking or not
	public bool walking;
	// if this character is being controlled right now or not
	public static bool controlled = false;
	// these boolean values prevent any audio files from overlapping each other for each character
	private bool allowedToPlayAudio;
	private bool allowedToPlayWalkAudio = true;
	// specifies the speed of the character
	public float speed = 4;
	// these contain an array of sounds in which a random sound is chosen each time to be palyed
	public AudioClip[] radioGo = new AudioClip[9];
	public AudioClip[] footsteps = new AudioClip[6];
	//Key for this player -> if changed must also change in string[] Buttons
	public string player = "a";
	
	//jrw125: 
	private bool keyIsDown = false;
	
	//dry9
	public static int activeGUI = 0; //Used to prevent GUI selection automatically moving the player, Should only be set to 2 max.
	public int HealthInitial = 100; //Beginning Health - Can Change in Unity
	private int Health; //Keeps track of health
	public int GuardDamage = 1; //How much damage do collisions and being in the same spot deal
	private string[] Buttons = new string[3] {"a","s","d"}; //The keyboard strings for the three prisoners; used to denote different players toward there GUI output
	public int attempts = 0; //Number of deaths for prisoner
	private float x_bound = 125f; //Max X dimension size of prison
	private float y_bound = 50f; //Max Y dimension size of prison
	
	// Use this for initialization
	void Start () {
		// ABL38
		// initialize field values
		allowedToPlayAudio = true;
		clickedTarget = transform.position;
    	walking = false;
		
		//dry9
		//Initializes Health Counter and Sends to be parsed for GUI display
		GUIScript.keyboard_select = Buttons; //Makes sure both GUI & Players have the same keyboard code
		Health = HealthInitial; //Health is set to public initial
		UpdateHealth(); //Parsed to update GUI
	}
	
	// Update is called once per frame
	void Update () {
		
		//dry9
		if(activeGUI == 0){ //Prevents unintended movement when using the GUI
			SendMessage ("ResetPlayer"); //Resets awkward physics responses
			
			//jrw125
			SendMessage ("GetPlayerControl");
			if(Input.GetKeyDown (player)){
				keyIsDown = true;
			}
			if(Input.GetKeyUp (player)){
				keyIsDown = false;
			}
			
			// ABL38 - this is the start of my code
			if ((Input.GetMouseButtonUp(0)) && ((controlled) || (keyIsDown)) && (!gitSelected)) {
				// on the mouse button up, if the GUI button is clicked or its respective key is pressed, and the mouse is currently not on the GUI,
				// then these actions are performed
				mousePointer.mouseIsClicked = true;
				// set the mousePointer's mouseIsClicked field to true
				// if a radio sound is allowed to play, then call the function PlayRadioSound()
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
				// if the character is walking, the white circle indicator of where the mouse has been clicked has its visibility set to true
				mousePointer.disappear = false;
				// as the character is moving, there are no restraints for the rigidbody
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
			activeGUI -= 1;//Allows update cycles between clicking the GUI and being able to move
		}
		Check_Location();//Checks if prisoner has "escaped"
	}
	// ABL38
	void OnCollisionEnter(Collision other){
		// when any character collides with anything, if the "mousepointer," the white circle indicating the position you clicked
		// where you want the character to go to will disappear
		mousePointer.disappear = true;
		// stop the character from walking
		walking = false;
		// as you hit something, freeze the rigidbody to prevent it from moving
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	}
	// ABL38
	IEnumerator PlayRadioSound(){
		// the radio sound is about to be played, so this is set to false 
		allowedToPlayAudio = false;
		// wait for 0.2f seconds so the click sound will not interfere with the radio sound
		yield return new WaitForSeconds (0.2f);
		// play a random radio sound from the array of sounds
		audio.clip = radioGo[Random.Range(0, 9)];
		audio.PlayOneShot(audio.clip);
		// wait for how long the audio clip's length then set the allowedToPlayAudio to true to prevent
		// multiple radio sounds from being played from the same character
		yield return new WaitForSeconds (audio.clip.length);
		allowedToPlayAudio = true;
	}
	// ABL38
	IEnumerator PlayWalkSound(){
		// the footstep sound is being played, so this is set to false
		// prevents multiple footsteps from being played from the same person
		allowedToPlayWalkAudio = false;
		// play a random footstep sound from the array of footsteps
		audio.PlayOneShot(footsteps[Random.Range(0, 6)]);
		// the amount of seconds between each footstep sound
		float footstepDelay = 0.35f;
		// based on the character type, the footstep sound delay can be faster or slower
		if(character.Equals ("shank")){
			footstepDelay = 0.35f;
		}
		else if(character.Equals ("rock")){
			footstepDelay = 0.5f;
		}
		else if(character.Equals ("big")){
			footstepDelay = 0.75f;
		}
		// wait for footstepDelay seconds
		yield return new WaitForSeconds (footstepDelay);
		// then set this to true, so another footstep sound can be played in necessary
		allowedToPlayWalkAudio = true;
	}
	// ABL38
	// When the character button on the GUI is clicked, this checks which type of player this code is assigned to
	// and sets if that character is true or not based on the conditions
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
	
	//dry9
	//Prevents awkard physics responses from compiling into larger ones
	void ResetPlayer(){
		transform.position = new Vector3(transform.position.x, transform.position.y, 0); //resets position
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);  //resets rotation
	}
	
	//dry9
	//Updates the correct Health variable in the GUI depending on player
	private void UpdateHealth(){
		if(player==Buttons[0]){
			GUIScript.Helper[0]=Health;
		}
		if(player==Buttons[1]){
			GUIScript.Helper[1]=Health;
		}
		if(player==Buttons[2]){
			GUIScript.Helper[2]=Health;
		}
	}
	
	//dry9
	//Updates the correct Deaths variable in the GUI depending on player
	private void UpdateAttempts(){
		if(player==Buttons[0]){
			GUIScript.Attempts[0]=attempts;
		}
		if(player==Buttons[1]){
			GUIScript.Attempts[1]=attempts;
		}
		if(player==Buttons[2]){
			GUIScript.Attempts[2]=attempts;
		}
	}
	
	//dry9
	//player accepts damage if attacked by a "Guard"
	void Injured(string x){
		if(x == ("Guard")){//Tests whether tag is Guard
			Health = Health - GuardDamage;//Decreases damage
		}
		
		UpdateHealth();//Sends to be parsed for GUI update
		
		if(Health<=0){//if player is "dead"
			DeadPlayer();//player "dies"
		}
	}
	
	//jrw125
	void DeadPlayer(){
		transform.position=new Vector3(-92.69604f, -32.47769f, 0);
		Health= Mathf.Max((HealthInitial/2),25);
		UpdateHealth();
		HealthInitial= Health;
		attempts += 1;
		UpdateAttempts();
	}
	
	//dry9
	//Checks if player has escaped the prison and parses accordingly to be updated by GUI
	void Check_Location(){
		if(Mathf.Abs(transform.position.x) > x_bound || Mathf.Abs(transform.position.y) > y_bound)//Checks if outside rectangular prison bounds
		{
			if(player==Buttons[0]){
				GUIScript.free_prisoners[0]=true;
			}
			if(player==Buttons[1]){
				GUIScript.free_prisoners[1]=true;
			}
			if(player==Buttons[2]){
				GUIScript.free_prisoners[2]=true;
			}
		}
	}
}
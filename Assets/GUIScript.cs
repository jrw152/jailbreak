//C#
using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	private string SoapDropper = "Soap Dropper: ";
	private string ChuckRockly = "Chuck Rockly: ";
	private string RubADubTub = "Rub-a-Dub Tub: ";
	public static int[] Helper = new int[3];
	public static int[] Attempts = new int[3];
	public static string[] keyboard_select = new string[3];
	public static bool[] free_prisoners = new bool[3] {false,false,false};
	private int WaitASecondsAndQuit = 3;
    private bool isGameOver = false;
	
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,225,130), "Character Select & Health/Deaths:");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,200,30), SoapDropper)) {
			player_shank.gitSelected = true;
			player_shank.shank = true;
			player_shank.rock = false;
			player_shank.big = false;
			player_shank.activeGUI = 2;
		}
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,70,200,30), ChuckRockly)) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = true;
			player_shank.big = false;
			player_shank.activeGUI = 2;
		}
		// Make the second button.
		if(GUI.Button(new Rect(20,100,200,30), RubADubTub)) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = false;
			player_shank.big = true;
			player_shank.activeGUI = 2;
		}
		if (isGameOver) {	
		            GUI.Label(new Rect(Screen.width/2,Screen.height/2,200,50),"You Win");
					QuitGame();
		}
	}
	
	void Update (){
		SoapDropper = keyboard_select[0] + " - Soap Dropper: " + Helper[0] + " - " + Attempts[0];
		ChuckRockly = keyboard_select[1] + " - Chuck Rockly: " + Helper[1] + " - " + Attempts[1];
		RubADubTub = keyboard_select[2] + " - Rub-a-Dub Tub: " + Helper[2] + " - " + Attempts[2];
		
		if(free_prisoners[0] /*&& free_prisoners[1] && free_prisoners[2]*/){
			isGameOver = true;
		}
	}	

	IEnumerator QuitGame() {
	        yield return new WaitForSeconds(WaitASecondsAndQuit);
	        Application.Quit();
	}		
}

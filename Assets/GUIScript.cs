//C#
using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	private string SoapDropper = "Soap Dropper: ";
	private string ChuckRockly = "Chuck Rockly: ";
	private string RubADubTub = "Rub-a-Dub Tub: ";
	public static int[] Helper = new int[2];
	
	void OnGUI () {
		
		// Make a background box
		GUI.Box(new Rect(10,10,175,130), "Character Select & Health:");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,150,30), SoapDropper)) {
			player_shank.gitSelected = true;
			player_shank.shank = true;
			player_shank.rock = false;
			player_shank.big = false;
			player_shank.activeGUI = 2;
		}
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20, 100,150,30), ChuckRockly)) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = true;
			player_shank.big = false;
			player_shank.activeGUI = 2;
		}
		// Make the second button.
		if(GUI.Button(new Rect(20,70,150,30), RubADubTub)) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = false;
			player_shank.big = true;
			player_shank.activeGUI = 2;
		}
	}
	
	void Update (){
		if(Helper[0] == 1){
			SoapDropper = "Soap Dropper: " + Helper[1];
		}
		if(Helper[0] == 2){
			ChuckRockly = "Chuck Rockly: " + Helper[1];
		}
		if(Helper[0] == 3){
			RubADubTub = "Rub-a-Dub Tub: " + Helper[1];
		}
	}			
}

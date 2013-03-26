//C#
using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,130), "Character Select:");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,100,30), "Soap Dropper")) {
			player_shank.gitSelected = true;
			player_shank.shank = true;
			player_shank.rock = false;
			player_shank.big = false;
		}
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20, 100,100,30), "Chuck Rockly")) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = true;
			player_shank.big = false;
		}
		// Make the second button.
		if(GUI.Button(new Rect(20,70,100,30), "Rub-a-Dub Tub")) {
			player_shank.gitSelected = true;
			player_shank.shank = false;
			player_shank.rock = false;
			player_shank.big = true;
		}
	}
}
//Main Menu
//Attached to main camera

using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {

	
	public float guiPlacementX1;
	public float guiPlacementY1;
	
	void OnGUI(){
		
		//Display our buttons
		if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Back to Main")) {  
			Application.LoadLevel("MainMenu");
		}

	}
}
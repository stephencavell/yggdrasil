//Main Menu
//Attached to main camera

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	//public Texture backgroundTexture;
	
	public float guiPlacementX1;
	public float guiPlacementX2;
	public float guiPlacementY1;
	public float guiPlacementY2;
	
	void OnGUI(){
		
		//Display our background Texture
		//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		//Display our buttons
		if (GUI.Button (new Rect (Screen.width * guiPlacementX1, Screen.height * guiPlacementY1, Screen.width * .5f, Screen.height * .1f), "Play")) {  
			Application.LoadLevel("Scene 1 - Stephen test");
		}

		if (GUI.Button (new Rect (Screen.width * guiPlacementX2, Screen.height * guiPlacementY2, Screen.width * .5f, Screen.height * .1f), "Controls")) {  
			Application.LoadLevel("Instructions");
		}
	}
}
#pragma strict

public var buttonNormalTexture : Texture2D;
public var buttonHoverTexture : Texture2D;
public var windowTexture : Texture2D;
public var guiFont : Font;
public var width : int = 400;
public var height : int = 300;
public var spacing : int = 20;

private var MainMenu : Rect;
private var windowStyle : GUIStyle;
private var buttonStyle : GUIStyle;
private var playerObject : GameObject;
private var characterFemale : boolean;

function Start () {
	Screen.showCursor = !Screen.showCursor;
	windowStyle = GUIStyle();
	windowStyle.stretchWidth = true;
	windowStyle.stretchHeight = true;
	windowStyle.normal.background = windowTexture;
	windowStyle.richText = true;
	windowStyle.font = guiFont;
	windowStyle.fontSize = 20;

	buttonStyle = GUIStyle();
	buttonStyle.stretchWidth = true;
	buttonStyle.stretchHeight = true;
	buttonStyle.hover.background = buttonHoverTexture;
	buttonStyle.normal.background = buttonNormalTexture;
	buttonStyle.alignment = TextAnchor.MiddleCenter;
	buttonStyle.richText = true;
	buttonStyle.font = guiFont;
	buttonStyle.fontSize = 20;

	MainMenu = Rect(Screen.width/2 - width/2, Screen.height/2 - height/2, width, height);
	playerObject = GameObject.FindGameObjectWithTag("Player");
	if(GameObject.Find("Lif")==playerObject){
		characterFemale = false;
	} else if(GameObject.Find("Lifthrasir")==playerObject){
		characterFemale = true;
	}
	Debug.Log("Female: "+characterFemale);
}

function Update () {
	if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != 'MainMenu')
	{
		MainScript.isPause = !MainScript.isPause;
		Screen.showCursor = !Screen.showCursor;
		playerObject.SendMessage("pauseMenu",MainScript.isPause);
		if (MainScript.isPause) Time.timeScale = 0;
		else Time.timeScale = 1;
	}
}

function OnGUI () {
	if (MainScript.isPause) {
		GUI.Window(0, MainMenu, TheMainMenu, '', windowStyle);
		playerObject.SendMessage("monitorAudio", !MainScript.isPause);
	};
}

function TheMainMenu () {
	if(characterFemale){
		if (GUILayout.Button("<color=white>Main Menu</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			MainScript.ResetGame();
			Time.timeScale = 1;
			Application.LoadLevel("MainMenu");
		}
		GUILayout.Space(spacing);
		if (GUILayout.Button("<color=white>Restart</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			MainScript.ResetGame();
			Time.timeScale = 1;
			Application.LoadLevel('girlscene');
		}
		GUILayout.Space(spacing);
		if (GUILayout.Button("<color=white>Quit</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			Time.timeScale = 1;
			Application.Quit();
		}
	} else {
		if (GUILayout.Button("<color=white>Main Menu</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			MainScript.ResetGame();
			Time.timeScale = 1;
			Application.LoadLevel("MainMenu");
		}
		GUILayout.Space(spacing);
		if (GUILayout.Button("<color=white>Restart</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			MainScript.ResetGame();
			Time.timeScale = 1;
			Application.LoadLevel('stephenLevel');
		}
		GUILayout.Space(spacing);
		if (GUILayout.Button("<color=white>Quit</color>", buttonStyle)) {
			MainScript.isPause = !MainScript.isPause;
			Time.timeScale = 1;
			Application.Quit();
		}
	}
}

public function IsPaused(){
	return MainScript.isPause;
}
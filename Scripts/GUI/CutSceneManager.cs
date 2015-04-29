using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneManager : MonoBehaviour {
	
	private List<CutSceneObject> cutScenes = new List<CutSceneObject>();
	private GameObject playerObject;
	private GameObject roosterObject;
	private LifController _playerController;
	private int currentScene = 0;

	public Texture2D buttonNormalTexture;
	public Texture2D buttonHoverTexture;
	public Texture2D windowTexture;
	public Font guiFont;
	public int width = 400;
	public int height = 300;
	public int spacing = 20;
	
	private Rect MainMenu;
	private GUIStyle windowStyle;
	private GUIStyle buttonStyle;
	private bool firstDialogue;
	private bool start;
	
	void  Start (){
		windowStyle = new GUIStyle();
		windowStyle.stretchWidth = true;
		windowStyle.stretchHeight = true;
		windowStyle.normal.background = windowTexture;
		windowStyle.richText = true;
		windowStyle.font = guiFont;
		windowStyle.fontSize = 20;
		
		buttonStyle = new GUIStyle();
		buttonStyle.stretchWidth = true;
		buttonStyle.stretchHeight = true;
		buttonStyle.hover.background = buttonHoverTexture;
		buttonStyle.normal.background = buttonNormalTexture;
		buttonStyle.alignment = TextAnchor.MiddleCenter;
		buttonStyle.richText = true;
		buttonStyle.font = guiFont;
		buttonStyle.fontSize = 20;

		//Add Scenes
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam eu enim blandit, elementum turpis vitae, convallis quam. Sed ut feugiat libero, nec facilisis mi. "));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Rooster: Nullam vel quam neque. Duis varius efficitur ante, pulvinar tristique lacus hendrerit vel. Fusce tristique tellus vitae neque tempus fermentum. "));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: Pellentesque eget nibh imperdiet, fermentum elit eu, tempus libero. Fusce tempus aliquet pulvinar. Aenean vel turpis turpis"));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Rooster: Fusce pulvinar elementum sapien eget auctor. Donec dapibus sem sed sollicitudin rhoncus."));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player:Curabitur porta erat a enim vehicula, eget posuere sem consequat. Nam velit enim, fermentum in libero eu, tristique mattis arcu."));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Rooster: Vestibulum finibus rhoncus sapien, quis gravida quam porta non. Nulla facilisi. Proin arcu neque, elementum aliquam lacus commodo."));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: Nulla ullamcorper tincidunt imperdiet. Aliquam erat volutpat. Aliquam leo lectus, pulvinar ac magna et, mattis sodales felis."));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Rooster: Nam lobortis mattis volutpat. Praesent pulvinar fermentum ante. Nulla felis nulla, molestie nec turpis convallis, iaculis fringilla lacus."));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: Nam lobortis mattis volutpat. Praesent pulvinar fermentum ante. Nulla felis nulla, molestie nec turpis convallis, iaculis fringilla lacus."));

		firstDialogue = true;
		_playerController = playerObject.GetComponent<LifController>();
	}
	
	void  Update (){
		if(firstDialogue == true){
			Screen.showCursor = !Screen.showCursor;
			if(currentScene<cutScenes.Count){
				firstDialogue = false;
				cutScenes[currentScene].startAll();
				currentScene++;
			}
		} else if (Input.anyKeyDown&&!Input.GetKeyDown(KeyCode.Escape)&&!(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1)||Input.GetMouseButtonDown(2)))
		{
			Debug.Log ("Current Scene: "+currentScene+". Count: "+cutScenes.Count+". Controllable: "+_playerController.getControllable()+". Paused: "+MainScriptManager.isPause);
			if(currentScene<cutScenes.Count){
				if(MainScriptManager.isPause){
					MainScriptManager.isPause = !MainScriptManager.isPause;
					if (MainScriptManager.isPause) Time.timeScale = 0;
					else Time.timeScale = 1;
				} else {
					Debug.Log ("Not Paused");
					//MainScriptManager.isPause = !MainScriptManager.isPause;
					Debug.Log("Paused Next Speaker Will Start");
					cutScenes[currentScene].startAll();
					if(currentScene==cutScenes.Count-1){
						_playerController.setControllable(true);
					}
					currentScene++;
				}
			}
		}
	}
	
	void  OnGUI (){
		if (currentScene < cutScenes.Count) {
			GUI.TextArea (new Rect (50, Screen.height - 150, Screen.width - 100, 100), cutScenes [currentScene - 1].getDialogue (), Screen.width - 100);
			GUI.Label (new Rect (50, Screen.height - 150, Screen.width - 100, 100), GUIContent.none, windowStyle);
		}
	}

	public class CutSceneObject {
		private SmoothFollow _mainCamera;
		
		public Transform character;
		public string dialogue;

		public CutSceneObject(Transform c, string d){
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
			character = c;
			dialogue = d;
		}
		public void startAll(){
			goToTransform();
		}
		void goToTransform(){
			_mainCamera.target = character;
		}
		public string getDialogue(){
			return dialogue;
		}
	}
}

/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneManager : MonoBehaviour {


	private List<CutSceneObject> cutScenes = new List<CutSceneObject>();
	private GameObject playerObject;
	private GameObject roosterObject;
	private int currentScene = 0;

	private bool firstRunning;

	void Start () {
		firstRunning = false;
	}

	void Update () {
		if(firstRunning==true){
			playerObject = GameObject.FindGameObjectWithTag("Player");
			roosterObject = GameObject.FindGameObjectWithTag("Rooster");
			cutScenes.Add(new CutSceneObject(playerObject.transform, "test"));
			cutScenes.Add(new CutSceneObject(roosterObject.transform, "test"));
			foreach(CutSceneObject cut in cutScenes){//(int i=0;i<cutScenes.Count;i++){
				Debug.Log ("AND NOW IN HERE");
				cut.startAll();
			}
		}
	}

	public void FirstCut(){
		firstRunning = true;
	}

	public class CutSceneObject {
		private GameObject playerObject;
		private SmoothFollow _mainCamera;

		public Transform character;
		public string dialogue;
		public bool IsEnded;
		public CutSceneObject(Transform c, string d){
			playerObject = GameObject.FindGameObjectWithTag("Player");
			_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
			character = c;
			dialogue = d;
			IsEnded= false;
		}
		public void startAll(){
			goToTransform();
			createDialogueBox();
		}
		void goToTransform(){
			Debug.Log ("Character: "+character);
			_mainCamera.target = character;
		}
		void createDialogueBox(){

		}
		bool isInputPressed(){
			if (Input.anyKey) {
				return true;
			} else {
				return false;
			}
		}
	}
}*/

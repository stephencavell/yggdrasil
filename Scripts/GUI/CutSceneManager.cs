using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneManager : MonoBehaviour {
	
	private List<CutSceneObject> cutScenes = new List<CutSceneObject>();
	private GameObject playerObject;
	private GameObject roosterObject;
	private GameObject lokiObject;
	private GameObject ravenObject;
	private GameObject lifthrasirObject;
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
	private bool inPauseMenu;
	
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
		lokiObject = GameObject.FindGameObjectWithTag("Loki");
		ravenObject = GameObject.FindGameObjectWithTag("Raven");
		lifthrasirObject = GameObject.FindGameObjectWithTag("Lifthrasir");
		cutScenes.Add(new CutSceneObject(playerObject.transform, "In a series of events leading up to Ragnarok, Loki has just killed Baldur, signifying the imminent start of war and chaos in the 9 realms that span across Yggdrasil. "));
		cutScenes.Add(new CutSceneObject(lifthrasirObject.transform, "Loki has previously kidnapped Lifthrasir in an effort to prevent the repopulation of the world after Ragnarok. Lif has been attempting to track Loki and his efforts have landed him in Muspelheim, a land inhibited by the fire giants and demons.  "));
		cutScenes.Add(new CutSceneObject(ravenObject.transform, "One of Odin's raven, Huginn, is serving as a correspondent as you attempt to end Loki's trickery."));
		cutScenes.Add(new CutSceneObject(ravenObject.transform, "Odin: Lif, you are now in Muspelheim. As you can see, this world is the land of fire and lava. Many giants and demons live here. The ruler of this world, Surt, lives at the top of that volcano. Surt is known to be a very violent leader, a merciless slayer wielding a flaming sword. You must be careful."));
		cutScenes.Add(new CutSceneObject(lifthrasirObject.transform, "Player: Look, there’s Loki with Lifthrasir! I have to stop him and save her!"));
		cutScenes.Add(new CutSceneObject(lokiObject.transform, "Player: What is Loki doing now??"));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Odin: Oh, no. He is waking up the rooster. Quick, we have to stop him!"));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: What does the rooster have to do with anything? I need to save Lifthrasir!"));
		cutScenes.Add(new CutSceneObject(ravenObject.transform, "Odin: Wait, Lif. You must listen to me. We HAVE to stop that rooster. Surt is asleep right now. Loki has just convinced the rooster to wake him up and warn him that Ragnarok has begun. If that rooster gets to Surt and wakes him up, there is impending doom for all of us. He will use his sword to burn your home down!"));
		cutScenes.Add(new CutSceneObject(playerObject.transform, "Player: Well, what are we waiting for? Let’s stop that rooster!"));
		cutScenes.Add(new CutSceneObject(roosterObject.transform, "Raven: Okay. Be careful not to let it make too much noise. The bar at the top right shows the noise meter. If that meter reaches the top and stays there, then it’s all over."));
		cutScenes.Add(new CutSceneObject(playerObject.transform, ""));


		firstDialogue = true;
		inPauseMenu = false;
		_playerController = playerObject.GetComponent<LifController>();
	}
	
	void  Update (){
		if(inPauseMenu==false){
			if(firstDialogue == true){
				Screen.showCursor = !Screen.showCursor;
				if(currentScene<cutScenes.Count){
					firstDialogue = false;
					cutScenes[currentScene].startAll();
					currentScene++;
				}
			} else if (Input.anyKeyDown&&!Input.GetKeyDown(KeyCode.Escape)&&!(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1)||Input.GetMouseButtonDown(2)))
			{
				if(currentScene<cutScenes.Count){
					if(MainScriptManager.isPause){
						MainScriptManager.isPause = !MainScriptManager.isPause;
						if (MainScriptManager.isPause) Time.timeScale = 0;
						else Time.timeScale = 1;
					} else {
						cutScenes[currentScene].startAll();
						if(currentScene==cutScenes.Count-2){
							moveLoki();
						}
						if(currentScene==cutScenes.Count-1){
							_playerController.setControllable(true);
							playerObject.SendMessage("monitorAudio", true);
						}
						currentScene++;
					}
				}
			}
		}
	}
	
	void  OnGUI (){
		if(inPauseMenu==false){
			if (currentScene < cutScenes.Count) {
				//GUI.TextArea (new Rect (50, Screen.height - 150, Screen.width - 100, 100), cutScenes [currentScene - 1].getDialogue (), Screen.width - 100);
				GUI.Label(new Rect (55, Screen.height - 145, Screen.width - 110, 90), "<size=18>"+cutScenes [currentScene - 1].getDialogue()+"</size>");
				GUI.Label(new Rect (Screen.width-(0.25f*Screen.width), Screen.height - 75, 0.25f*Screen.width-5, 90), "<size=18>Press Any Button to Continue</size>");
				GUI.Label (new Rect (50, Screen.height - 150, Screen.width - 100, 100), GUIContent.none, windowStyle);
			}
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
	
	public void pauseMenu(bool val){
		inPauseMenu = val;
	}

	public void moveLoki(){
		Destroy (lokiObject);
		Destroy (lifthrasirObject);
	}
}

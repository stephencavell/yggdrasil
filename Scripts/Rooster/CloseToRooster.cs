using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {
	
	private GameObject playerObject;
	private CheckpointManager checkpointManager;
	private LifController _playerController;
	private SmoothFollow _mainCamera;
	private GameObject mainCameraSound;
	private CutSceneManager cutScene;

	private Animator lifAnim;
	
	private GameObject rooster;
	public Animator rooanim;
	public AudioSource roocatch;
	
	private int position;
	public Vector3 checkpoint1;
	public Vector3 checkpoint2;
	public Vector3 checkpoint3;
	public Vector3 checkpoint4;
	public Vector3 checkpoint5;
	public Vector3 checkpoint6;
	float StartPointX = 29.1f;
	float StartPointY = 7.424f;
	float ControlPointX = 40;
	float ControlPointY = 14;
	float EndPointX = 66;
	float EndPointY = 7;
	float CurveX;
	float CurveY;
	float BezierTime = 0;
	float crowInterval = 1;
	Transform mySphere;
	bool flying;
	bool roosterSound;
	
	
	// Use this for initialization
	void Start () {
		position = 0;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		lifAnim = playerObject.GetComponent<Animator> ();
		mainCameraSound = GameObject.FindGameObjectWithTag ("MainCamera");
		_mainCamera = mainCameraSound.GetComponent<SmoothFollow>();
		checkpointManager = playerObject.GetComponent<CheckpointManager>();
		_playerController = playerObject.GetComponent<LifController>();
		flying = false;
		roosterSound = true;
		cutScene = playerObject.GetComponent<CutSceneManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		rooanim.SetBool ("flying", flying);
		if(this.transform!=null){
			if(flying == true){
				BezierCurve();
			} else {
				StartPointX = this.transform.position.x;
				StartPointY = this.transform.position.y;
			}
			if(roosterSound==true&&!cutScene.inPauseMenu&&!cutScene.inCutScene()){
				if(crowInterval>=1){
					PlayRoosterCrow();
					crowInterval = 0;
				}
				crowInterval=Time.deltaTime*57;
			} else {
				PauseRoosterCrow();
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(flying==false){
				playerObject.SendMessage("ResetRoosterTime");
				if(StartPointX<35){
					StartPointX = checkpoint1.x;
					StartPointY = checkpoint1.y;
					EndPointX = checkpoint2.x;
					EndPointY = checkpoint2.y;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					Debug.Log ("Position: "+position+". StartPoint: ("+StartPointX+","+StartPointY+"). Control Point: ("+ControlPointX+","+ControlPointY+"). End Point: ("+EndPointX+","+EndPointY+")");
					
					flying = true;
					PauseRoosterCrow();
					mainCameraSound.SendMessage("PlayWingsFlapping");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					BezierTime = 0;
				} else if(StartPointX<60) {
					StartPointX = checkpoint2.x;
					StartPointY = checkpoint2.y;
					EndPointX = checkpoint3.x;
					EndPointY = checkpoint3.y;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					Debug.Log ("Position: "+position+". StartPoint: ("+StartPointX+","+StartPointY+"). Control Point: ("+ControlPointX+","+ControlPointY+"). End Point: ("+EndPointX+","+EndPointY+")");
					
					flying = true;
					PauseRoosterCrow();
					mainCameraSound.SendMessage("PlayWingsFlapping");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					BezierTime = 0;
				} else if(StartPointX<90) {
					StartPointX = checkpoint3.x;
					StartPointY = checkpoint3.y;
					EndPointX = checkpoint4.x;
					EndPointY = checkpoint4.y;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					Debug.Log ("Position: "+position+". StartPoint: ("+StartPointX+","+StartPointY+"). Control Point: ("+ControlPointX+","+ControlPointY+"). End Point: ("+EndPointX+","+EndPointY+")");
					
					flying = true;
					PauseRoosterCrow();
					mainCameraSound.SendMessage("PlayWingsFlapping");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					BezierTime = 0;
				} else if(StartPointX<110) {
					StartPointX = checkpoint4.x;
					StartPointY = checkpoint4.y;
					EndPointX = checkpoint5.x;
					EndPointY = checkpoint5.y;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					Debug.Log ("Position: "+position+". StartPoint: ("+StartPointX+","+StartPointY+"). Control Point: ("+ControlPointX+","+ControlPointY+"). End Point: ("+EndPointX+","+EndPointY+")");
					
					flying = true;
					PauseRoosterCrow();
					mainCameraSound.SendMessage("PlayWingsFlapping");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					BezierTime = 0;
				} else if(StartPointX<140) {
					StartPointX = checkpoint5.x;
					StartPointY = checkpoint5.y;
					EndPointX = checkpoint6.x;
					EndPointY = checkpoint6.y;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					Debug.Log ("Position: "+position+". StartPoint: ("+StartPointX+","+StartPointY+"). Control Point: ("+ControlPointX+","+ControlPointY+"). End Point: ("+EndPointX+","+EndPointY+")");
					
					flying = true;
					PauseRoosterCrow();
					mainCameraSound.SendMessage("PlayWingsFlapping");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					BezierTime = 0;
					BoxCollider2D box = GetComponent<BoxCollider2D>();
					box.size=new Vector2(box.size.x,1f);
				} else {
					Debug.Log ("End Collision");
					PauseRoosterCrow();
					roosterSound = false;
					lifAnim.SetBool("roocatch", true);
					_playerController.setControllable(false);
					this.transform.position = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y + 1);
					playerObject.SendMessage("monitorAudio", false);
					roocatch.Play();
					mainCameraSound.SendMessage("PlaySuccess");
				}
			}
		}
	}
	
	void PlayRoosterCrow(){
		mainCameraSound.SendMessage("PlayRoosterCrow");
	}
	
	void RevertToCheckpoint() {
		Debug.Log ("Reverting To Checkpoint");
		int checkpoint = playerObject.GetComponent<CheckpointManager>().GetCheckpoint();
		Vector3 newPosition = this.transform.position;
		if (checkpoint >= 6) {
			newPosition = checkpoint6;
		} else if (checkpoint == 5) {
			newPosition = checkpoint5;
		} else if (checkpoint == 4) {
			newPosition = checkpoint4;
		} else if (checkpoint == 3) {
			newPosition = checkpoint3;
		} else if (checkpoint == 2) {
			newPosition = checkpoint2;
		} else {
			newPosition = checkpoint1;
		}
		this.transform.position = newPosition;
	}
	
	void BezierCurve(){
		BezierTime = BezierTime + Time.deltaTime/6;
		if (BezierTime >= 1) {
			BezierTime = 0;
		}
		CurveX = (((1-BezierTime)*(1-BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
		this.transform.position = new Vector3(CurveX, CurveY, 0);
		if(this.transform.position.x >= EndPointX-2){
			flying = false;
			roosterSound = true;
			_mainCamera.target = playerObject.transform;
			this.transform.position = new Vector3(EndPointX, EndPointY, 0);
			_playerController.setControllable(true);
			mainCameraSound.SendMessage("PauseWingsFlapping");
		}
	}

	void PauseRoosterCrow(){
		mainCameraSound.SendMessage("PauseRoosterCrow");
	}
}

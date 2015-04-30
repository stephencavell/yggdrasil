using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {
	
	private GameObject playerObject;
	private CheckpointManager checkpointManager;
	private LifController _playerController;
	private SmoothFollow _mainCamera;
	
	private GameObject rooster;
	public Animator rooanim;
	public AudioSource wingFlap;
	
	private int position;
	public int newPositionX1;
	public int newPositionY1;
	public Vector3 checkpoint1;
	public Vector3 checkpoint2;
	public Vector3 checkpoint3;
	public Vector3 checkpoint4;
	public Vector3 checkpoint5;
	public Vector3 checkpoint6;
	float StartPointX = 0;
	float StartPointY = 0;
	float ControlPointX = 40;
	float ControlPointY = 14;
	float EndPointX = 66;
	float EndPointY = 7;
	float CurveX;
	float CurveY;
	float BezierTime = 0;
	Transform mySphere;
	bool flying;
	bool roosterSound;
	
	
	// Use this for initialization
	void Start () {
		position = 0;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>();
		checkpointManager = playerObject.GetComponent<CheckpointManager>();
		_playerController = playerObject.GetComponent<LifController>();
		EndPointX = newPositionX1;
		EndPointY = newPositionY1;
		flying = false;
		roosterSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		//rooster = GameObject.FindGameObjectWithTag ("Rooster");
		rooanim.SetBool ("flying", flying);
		if (flying && !wingFlap.isPlaying)
			wingFlap.Play ();
		if(this.transform!=null){
			PlayRoosterCrow();
			if(flying == true){
				BezierCurve();
			} else {
				StartPointX = this.transform.position.x;
				StartPointY = this.transform.position.y;
			}
			if(roosterSound==true){
				PlayRoosterCrow();
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log ("Collided With Rooster: "+other.gameObject.tag);
			if(flying==false){
				if(position==0){
					playerObject.SendMessage("ResetRoosterTime");
					EndPointX = newPositionX1;
					EndPointY = newPositionY1;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					//ControlPointY =;
					flying = true;
					playerObject.SendMessage("PauseRoosterCrow");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
					_playerController.setControllable(false);
					//Destroy(this.collider2D);
					BoxCollider2D box = GetComponent<BoxCollider2D>();
					box.size=new Vector2(0.5f,0.5f);
				} else if(position==1){
					Debug.Log("IN HERE");
					playerObject.SendMessage("PauseRoosterCrow");
					Destroy(this.gameObject);
					roosterSound = false;
				}
			}
		}
	}
	
	void PlayRoosterCrow(){
		playerObject.SendMessage("PlayRoosterCrow");
	}
	
	void RevertToCheckpoint() {
		int checkpoint = checkpointManager.GetCheckpoint();
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
		if(this.transform.position.x >= newPositionX1-1){
			Debug.Log ("Done");
			flying = false;
			roosterSound = true;
			this.transform.position = new Vector3(newPositionX1, newPositionY1, 0);
			_mainCamera.target = playerObject.transform;
			_playerController.setControllable(true);
		}
	}
}

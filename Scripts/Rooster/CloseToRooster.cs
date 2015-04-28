using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {

	private GameObject playerObject;
	private CheckpointManager checkpointManager;
	private SmoothFollow _mainCamera;
	
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
		EndPointX = newPositionX1;
		EndPointY = newPositionY1;
		flying = false;
		roosterSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Bezier Time: "+Time.deltaTime);
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
					//Vector3 newPosition = roosterObject.transform.position;
					//newPosition.x = newPositionX1;
					//newPosition.y = newPositionX2;
					//roosterObject.transform.position = newPosition;
					EndPointX = newPositionX1;
					EndPointY = newPositionY1;
					ControlPointX = StartPointX+((EndPointX-StartPointX)*3/4);
					//ControlPointY =;
					flying = true;
					playerObject.SendMessage("PauseRoosterCrow");
					roosterSound = false;
					position++;
					_mainCamera.target = this.transform;
				} else if(position==1){
					Debug.Log("IN HERE");
					playerObject.SendMessage("PauseRoosterCrow");
					Destroy(this);
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
		Debug.Log ("Bezier Curve Move");
		BezierTime = BezierTime + Time.deltaTime/10;
		if (BezierTime >= 1) {
			BezierTime = 0;
		}
		
		CurveX = (((1-BezierTime)*(1-BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
		this.transform.position = new Vector3(CurveX, CurveY, 0);
		Debug.Log ("Curve X: "+CurveX+". Curve Y: "+CurveY+". Flying: "+flying+".  StartPointX: "+StartPointX+".  StartPointY: "+StartPointY+". Control Point X: "+ControlPointX+". Control Point Y: "+ControlPointY+". EndPointX: "+EndPointX+". EndPointY: "+EndPointY);
		if(this.transform.position.x >= newPositionX1-5){
			Debug.Log ("Done");
			flying = false;
			roosterSound = true;
			this.transform.position = new Vector3(newPositionX1, newPositionY1, 0);
			_mainCamera.target = playerObject.transform;
		}
	}
}

using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {

	private GameObject playerObject;
	private GameObject roosterObject;
	private CheckpointManager checkpointManager;
	
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
	float ControlPointX = 100;
	float ControlPointY = 30;
	float EndPointX = 50;
	float EndPointY = 0;
	float CurveX;
	float CurveY;
	float BezierTime = 0;
	Transform mySphere;
	bool flying;
	bool roosterSound;

	// Use this for initialization
	void Awake () {
		position = 0;
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		playerObject = GameObject.FindGameObjectWithTag("Player");
		checkpointManager = playerObject.GetComponent<CheckpointManager>();
		Debug.Log ("Rooster Object: "+roosterObject);
	}

	void start(){
		StartPointX = roosterObject.transform.position.x;
		StartPointY = roosterObject.transform.position.y;
		EndPointX = newPositionX1;
		EndPointY = newPositionY1;
		flying = false;
		roosterSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		PlayRoosterCrow();
		if(flying == true){
			BezierCurve();
		}
		if(roosterSound==true){
			PlayRoosterCrow();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Collided With Rooster");
		if(other.gameObject.tag == "Player")
		{
			if(flying==false){
				if(position==0){
					playerObject.SendMessage("ResetRoosterTime");
					//Vector3 newPosition = roosterObject.transform.position;
					//newPosition.x = newPositionX1;
					//newPosition.y = newPositionX2;
					//roosterObject.transform.position = newPosition;
					EndPointX = newPositionX1;
					EndPointY = newPositionY1;
					flying = true;
					playerObject.SendMessage("PauseRoosterCrow");
					roosterSound = false;
					position++;
				} else if(position==1){
					Debug.Log("IN HERE");
					playerObject.SendMessage("PauseRoosterCrow");
					Destroy(this);
					Destroy(roosterObject);
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
		Vector3 newPosition = roosterObject.transform.position;
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
		roosterObject.transform.position = newPosition;
	}

	void BezierCurve(){
		BezierTime = BezierTime + Time.deltaTime;
		if (BezierTime >= 1) {
			BezierTime = 0;
		}
		
		CurveX = (((1-BezierTime)*(1-BezierTime)) * StartPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * EndPointX);
		CurveY = (((1-BezierTime)*(1-BezierTime)) * StartPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) + ((BezierTime * BezierTime) * EndPointY);
		roosterObject.transform.position = new Vector3(CurveX, CurveY, 0);
		Debug.Log ("Flying: "+flying+".  Rooster Object: "+roosterObject.transform.position.x+".  New Position: "+newPositionX1);
		if(roosterObject.transform.position.x >= newPositionX1-5){
			Debug.Log ("Done");
			flying = false;
			roosterSound = true;
			roosterObject.transform.position = new Vector3(newPositionX1, newPositionY1, 0);
		}
	}
}

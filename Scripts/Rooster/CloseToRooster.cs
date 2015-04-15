using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {

	private GameObject playerObject;
	private GameObject roosterObject;
	private CheckpointManager checkpointManager;
	
	private int position;
	public int newPositionX1 = 134;
	public int newPositionX2 = 20;
	public Vector3 checkpoint1;
	public Vector3 checkpoint2;
	public Vector3 checkpoint3;
	public Vector3 checkpoint4;
	public Vector3 checkpoint5;
	public Vector3 checkpoint6;

	// Use this for initialization
	void Start () {
		position = 0;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		checkpointManager = playerObject.GetComponent<CheckpointManager>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayRoosterCrow();
		//Debug.Log("This Transform Position: "+transform.position);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(position==0){
				playerObject.SendMessage("ResetRoosterTime");
				Vector3 newPosition = roosterObject.transform.position;
				newPosition.x = newPositionX1;
				newPosition.y = newPositionX2;
				roosterObject.transform.position = newPosition;
				position++;
			} else if(position==1){
				Debug.Log("IN HERE");
				playerObject.SendMessage("PauseRoosterCrow");
				Destroy(this);
				Destroy(roosterObject);
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
}

using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {

	private GameObject playerObject;
	private GameObject roosterObject;
	private CheckpointManager checkpointManager;
	
	private int position;
	public int newPositionX1 = 134;
	public int newPositionX2 = 20;
	public int checkpoint1X;
	public int checkpoint1Y;
	public int checkpoint2X;
	public int checkpoint2Y;
	public int checkpoint3X;
	public int checkpoint3Y;
	public int checkpoint4X;
	public int checkpoint4Y;
	public int checkpoint5X;
	public int checkpoint5Y;
	public int checkpoint6X;
	public int checkpoint6Y;

	// Use this for initialization
	void Start () {
		position = 0;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		checkpointManager = GetComponent<CheckpointManager>();
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
		Vector3 newPosition = this.transform.position;
		if (checkpoint >= 6) {
			newPosition.x = checkpoint6X;
			newPosition.y = checkpoint6Y;
		} else if (checkpoint == 5) {
			newPosition.x = checkpoint5X;
			newPosition.y = checkpoint5Y;
		} else if (checkpoint == 4) {
			newPosition.x = checkpoint4X;
			newPosition.y = checkpoint4Y;
		} else if (checkpoint == 3) {
			newPosition.x = checkpoint3X;
			newPosition.y = checkpoint3Y;
		} else if (checkpoint == 2) {
			newPosition.x = checkpoint2X;
			newPosition.y = checkpoint2Y;
		} else {
			newPosition.x = checkpoint1X;
			newPosition.y = checkpoint1Y;
		}
		this.transform.position = newPosition;
	}
}

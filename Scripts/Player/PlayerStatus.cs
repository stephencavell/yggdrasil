using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
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

	private CheckpointManager playerObject;

	// Use this for initialization
	void Start () {
		playerObject = GetComponent<CheckpointManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void RevertToCheckpoint() {
		int checkpoint = playerObject.GetCheckpoint();
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

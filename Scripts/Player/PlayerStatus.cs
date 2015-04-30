using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public Vector3 checkpoint1;
	public Vector3 checkpoint2;
	public Vector3 checkpoint3;
	public Vector3 checkpoint4;
	public Vector3 checkpoint5;
	public Vector3 checkpoint6;
	public Vector3 checkpoint7;

	private CheckpointManager playerObject;

	// Use this for initialization
	void Start () {
		playerObject = GetComponent<CheckpointManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void RevertToCheckpoint() {
		Debug.Log ("Reverting To Checkpoint");
		int checkpoint = playerObject.GetCheckpoint();
		Vector3 newPosition = this.transform.position;
		if (checkpoint >= 7) {
			newPosition = checkpoint7;
		} else if (checkpoint == 6) {
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
}

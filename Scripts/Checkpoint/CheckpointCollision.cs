using UnityEngine;
using System.Collections;

public class CheckpointCollision : MonoBehaviour {
	public int checkpoint;
	private GameObject playerObject;
	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Entered Checkpoint: "+checkpoint);
			playerObject.SendMessage("SetCheckpoint",checkpoint);
		}
	}
}

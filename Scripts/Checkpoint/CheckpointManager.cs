using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {
	private int checkpoint;
	// Use this for initialization
	void Start () {
		checkpoint = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetCheckpoint(int x){
		checkpoint = x;
	}

	void NextCheckpointReached(){
		checkpoint++;
	}

	public int GetCheckpoint(){
		return checkpoint;
	}
}

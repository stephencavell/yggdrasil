﻿using UnityEngine;
using System.Collections;

public class LevelBoundary : MonoBehaviour {
	
	private GameObject playerObject;
	private GameObject roosterObject;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.Find("RoosterSpace");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("This Transform Position: "+transform.position);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Player Dead");
			playerObject.SendMessage("PlayDeath");
			playerObject.SendMessage("RevertToCheckpoint");
			roosterObject.SendMessage("RevertToCheckpoint");
		}
	}
}
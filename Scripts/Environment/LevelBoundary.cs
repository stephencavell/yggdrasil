﻿using UnityEngine;
using System.Collections;

public class LevelBoundary : MonoBehaviour {
	
	private GameObject playerObject;
	private GameObject roosterObject;
	private GameObject _mainCamera;

	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("This Transform Position: "+transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Player Dead");
			_mainCamera.SendMessage("PlayDeath");
			playerObject.SendMessage("RevertToCheckpoint");
			roosterObject.SendMessage("RevertToCheckpoint");
			playerObject.SendMessage("ResetRoosterTime");
		}
	}
}

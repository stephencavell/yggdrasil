using UnityEngine;
using System.Collections;

public class LevelBoundary : MonoBehaviour {
	
	private GameObject playerObject;
	
	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindGameObjectWithTag("Player");
		//maincontroller = playerObject.GetComponent<MainGUI>("MainGUI");//GameObject.FindGameObjectWithTag("Player").GetComponent("MainGUI");
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
		}
	}
}

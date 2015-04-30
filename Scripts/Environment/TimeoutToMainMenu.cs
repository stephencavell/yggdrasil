using UnityEngine;
using System.Collections;

public class TimeoutToMainMenu : MonoBehaviour {

	// Use this for initialization

	public float timeout;
	void Start () {
		timeout = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeout > 5) {
			Application.LoadLevel("MainMenu");
		} else {
			timeout = timeout + Time.deltaTime;
		}
	}
}

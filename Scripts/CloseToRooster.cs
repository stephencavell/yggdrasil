using UnityEngine;
using System.Collections;

public class CloseToRooster : MonoBehaviour {

	private GameObject playerObject;
	private GameObject roosterObject;
	
	private int position;
	private int newPositionX1 = 134;
	private int newPositionX2 = 20;

	// Use this for initialization
	void Start () {
		position = 0;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		roosterObject = GameObject.FindGameObjectWithTag("Rooster");
		//maincontroller = playerObject.GetComponent<MainGUI>("MainGUI");//GameObject.FindGameObjectWithTag("Player").GetComponent("MainGUI");
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
}

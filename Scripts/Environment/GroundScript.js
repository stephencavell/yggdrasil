#pragma strict

private var player : GameObject;
private var playerScript : CharacterController2D;

function Start () {
	player = GameObject.FindGameObjectWithTag("Player");
	playerScript = player.GetComponent(CharacterController2D);
}

function Update () {

}

function OnCollisionEnter2D(theCollision : Collision2D){
	Debug.Log("Collision");
	playerScript.onPlatform = true;
    //if(theCollision.name == "player") {
     //  playerScript.onPlatform = true;
    //}
   //if(!theCollision.gameObject.rigidBody2D.isKinematic) //Only want kinematic rigidbodies
   //{
     /*for(var contact : ContactPoint2D in theCollision.contacts)
     {
       if(contact.normal.y > someThreshold)
       {
         onPlatform = true;
         groundedOn = theCollision.gameObject;
         break;
       }
     }*/
   //}
 }

function OnCollisionExit2D(theCollision : Collision2D){
	Debug.Log("Collided done");
	playerScript.onPlatform = true;
 }

function OnTriggerStay2D (hitInfo: Collider2D)
 {
 	Debug.Log("Collision");
    if(hitInfo.name == "player") {
       playerScript.onPlatform = true;
    }
 }
using UnityEngine;
using System.Collections;

public class LifController : MonoBehaviour {

	public float maxSpeed = 10f;
	public float walkSpeed = 10f;
	public float runSpeed = 15f;
	
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public bool running = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	public float jumpForce = 10f;

	public bool isControllable;

	public GameObject _mainCamera;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		isControllable = false;
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isControllable==true){
			running = false;
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);
			anim.SetBool ("running", running);

			float move = Input.GetAxis ("Horizontal");


			anim.SetFloat("Speed", Mathf.Abs (move));
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
			if(move!=0){
				if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)){
					running = true;
					anim.SetBool ("running", running);
					_mainCamera.SendMessage("PlayRunning");
					rigidbody2D.velocity = new Vector2 (move * runSpeed, rigidbody2D.velocity.y);
				} else {
					_mainCamera.SendMessage("PlayWalking");
					rigidbody2D.velocity = new Vector2 (move * walkSpeed, rigidbody2D.velocity.y);
				}
			}
			if(move > 0 && !facingRight)
				Flip ();
			else if(move < 0 && facingRight)
				Flip ();
		}
	}

	void Update() {
		if(isControllable==true){
			if (grounded && Input.GetKeyDown ("up")) {
				anim.SetBool ("Ground", false);
				rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			}
		}
	}




	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void setControllable(bool val){
		isControllable = val;
	}

	public bool getControllable(){
		return isControllable;
	}
}

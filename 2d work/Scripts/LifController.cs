using UnityEngine;
using System.Collections;

public class LifController : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	private CutSceneManager cut;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

	public float jumpForce = 10f;

	public bool isControllable;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		isControllable = false;
		cut = GetComponent<CutSceneManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isControllable==true){
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);

			float move = Input.GetAxis ("Horizontal");

			anim.SetFloat("Speed", Mathf.Abs (move));
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

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

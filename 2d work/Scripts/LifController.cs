using UnityEngine;
using System.Collections;

public class LifController : MonoBehaviour {

	public float maxSpeed = 10f;
	public float walkSpeed = 10f;
	public float runSpeed = 15f;

	public AudioSource walk;
	public AudioSource jump;

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

			if(grounded && move !=0 && !walk.isPlaying)
			{
				walk.Play();
			}

			anim.SetFloat("Speed", Mathf.Abs (move));
			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

			if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)){
				rigidbody2D.velocity = new Vector2 (move * runSpeed, rigidbody2D.velocity.y);
			} else {
				rigidbody2D.velocity = new Vector2 (move * walkSpeed, rigidbody2D.velocity.y);
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
				jump.Play();
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

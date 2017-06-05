using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour 
{
	public bool facingRight = true;
	public bool jump = false;
	public bool doubleJump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//1 << LayerMask....is basically a way of saying "We are only seeing if groundCheck is touching "Ground"
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) 
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{
		if (grounded) 
		{
			doubleJump = false;
		}

		float h = Input.GetAxis ("Horizontal");
		//Stops from getting a negative speed and screwing up calculations
		anim.SetFloat ("Speed", Mathf.Abs (h));

		//We have a speed cap 
		if (h * rb2d.velocity.x < maxSpeed) 
		{
			rb2d.AddForce (Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) 
		{
			//Mathf.Sign returns -1 or 1 depending on the sign of the input
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump) 
		{
			anim.SetTrigger ("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}

		//Double Jump
		if (Input.GetButtonDown("Jump") && !grounded && !doubleJump) 
		{
			anim.SetTrigger ("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			doubleJump = true;
		}
	}

	//This is the kind of function I could have used previously
	void Flip()
	{
		facingRight = !facingRight;
		//what
		Vector3 theScale = transform.localScale;
		//ohhh
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) 
		{
			anim.SetTrigger ("Die");
			rb2d.velocity = Vector2.zero;
			rb2d.angularVelocity = 0.0f;
		}
	}
}

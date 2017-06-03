using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Only apply the 
 * script to the prefab for testing,
 * we get erratic behavior otherwise
 */

public class EnemyScript : MonoBehaviour {

	Rigidbody2D rb;
	public float moveForce;
	public float velocityx, velocityy;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		//Okay so velocity works but not AddForce? Whatever I guess
		rb.velocity = new Vector2 (moveForce, rb.velocity.y);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("EnemyWalkZone")) 
		{
			//rotate
			Vector3 theScale = transform.localScale;
			//ohhh
			theScale.x *= -1;
			transform.localScale = theScale;
			Quaternion rotator = transform.localRotation;
			rotator.x *= -1;
			//Flips the force added to the Rigidbody2D
			moveForce *= -1;
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			Destroy (other.gameObject);
			Invoke("RestartGame", 5f);
		}
	}

	void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	*/
}

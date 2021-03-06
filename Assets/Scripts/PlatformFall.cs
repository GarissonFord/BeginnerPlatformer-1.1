﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

	public float fallDelay;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			//Calls "Fall" after fallDelay seconds
			Invoke ("Fall", fallDelay);
		}
	}

	void Fall()
	{
		rb2d.isKinematic = false;
	}
}

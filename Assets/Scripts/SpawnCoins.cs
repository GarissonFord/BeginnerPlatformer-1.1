using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour {

	public Transform[] coinSpawns;
	public GameObject coin;
	public GameObject enemy;

	// Use this for initialization
	void Start () 
	{
		Spawn ();
	}
	
	void Spawn()
	{
		for (int i = 0; i < coinSpawns.Length; i++) 
		{
			int coinFlip = Random.Range (0, 2);
			if (coinFlip > 0)
			{
				Instantiate (coin, coinSpawns [i].position, Quaternion.identity);
			} 
			else
			{
				//Instantiates either coins or an enemy
				Instantiate (enemy, coinSpawns [i].position, Quaternion.identity);
			}
		}
	}
}

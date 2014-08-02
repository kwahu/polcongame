using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour {

	public GameObject respawnedObject;
	public int renewalTime = 20; // seconds
	public bool respawned;

	void Start()
	{
		RespawnObject();
	}

	void OnCollisionEnter(Collision collision) 
	{
		if ( respawned )
		{
			var coinCollector = collision.gameObject.GetComponent<CoinPlayer>();

			if ( coinCollector != null )
			{
				coinCollector.GiveCoins(new List<Coin> { new Coin() });
			}

			respawned = false;
		}
	}
	
	void Update () 
	{
		if ( !respawned )
		{
			RespawnWithDelay();
		}
	}

	IEnumerator StartWait()
	{
		yield return StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(renewalTime);      
	}

	void RespawnWithDelay()
	{
		respawned = true;

		StartWait();

		RespawnObject();
	}

	void RespawnObject()
	{
		respawned = true;
	}
}

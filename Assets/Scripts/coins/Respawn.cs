using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : Photon.MonoBehaviour {
	
	public float timeToNextRespawn = 0;

	public int renewalTime = 20; // seconds
	public bool respawned;
	public bool respawnJustOnce = false;
	public bool activeRespawn = true;
	public List<Coin> coinsToPick = new List<Coin> { new Coin() };

	void Start()
	{
		RespawnObject();
	}
	 
	void OnTriggerEnter(Collider other)
	{
		if ( respawned )
		{
			var coinCollector = other.gameObject.GetComponent<ThirdPersonNetworkVikOculus>();

			if ( coinCollector != null )
			{
				Hide();

				timeToNextRespawn = renewalTime;

				coinCollector.GiveCoins(coinsToPick);
			}

			respawned = false;
		}
	}
	
	void Update () 
	{
		if ( !respawned )
		{
			RespawnWithDelay();
			return;
		}

		transform.Rotate(0f,1f,0);
	}

	void RespawnWithDelay()
	{
		timeToNextRespawn -= Time.deltaTime;

		RespawnObject();
	}

	void RespawnObject()
	{
		if ( !respawned && activeRespawn && timeToNextRespawn <= 0 )
		{
			Show();

			respawned = true;

			if ( respawnJustOnce )
			{
				activeRespawn = false;
			}
		}
	}

	void Hide()
	{
		renderer.enabled = false;
	}

	void Show()
	{
		renderer.enabled = true;
	}
}

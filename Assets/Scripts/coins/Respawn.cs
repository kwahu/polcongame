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
	 
	void OnTriggerEnter(Collider other)
	{
		if ( respawned )
		{
			var coinCollector = other.gameObject.GetComponent<ThirdPersonNetworkVikOculus>();

			if ( coinCollector != null )
			{
				Hide();

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
			return;
		}

		transform.Rotate(0f,1f,0);
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
		Show();

		respawned = true;
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

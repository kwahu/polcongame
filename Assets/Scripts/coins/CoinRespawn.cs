using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinRespawn : Photon.MonoBehaviour {
	
	public float timeToNextRespawn = 0;

	public int renewalTime = 20; // seconds
	public bool respawned;
	public bool respawnJustOnce = false;
	public bool activeRespawn = true;
	public int coinsToPick = 1;
	bool visible;
	public bool Visible {
		get {
			return visible;
		}
		set {
			visible = value;
		}
	}

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
				Visibility(false);

				timeToNextRespawn = renewalTime;

				coinCollector.GiveCoins(coinsToPick);
			}

			respawned = false;
		}
	}
	
	void Update () 
	{
		if (photonView.isMine)
		{
			if ( !respawned )
			{
				RespawnWithDelay();
				return;
			}

			transform.Rotate(0f,1f,0);
		}
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
			Visibility(true);

			respawned = true;

			if ( respawnJustOnce )
			{
				activeRespawn = false;
			}
		}
	}

	void Visibility(bool visible)
	{
		Visible = visible;
		renderer.enabled = visible;
	}

	#region Proton part

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		var visibleOld = Visible;

		if (stream.isWriting)
		{
			stream.SendNext(timeToNextRespawn);
			stream.SendNext(respawned);
			stream.SendNext(respawnJustOnce); 
			stream.SendNext(activeRespawn);
			stream.SendNext(coinsToPick);
			stream.SendNext(Visible);
		}
		else
		{
			timeToNextRespawn = (float)stream.ReceiveNext();
			respawned = (bool)stream.ReceiveNext();
			respawnJustOnce = (bool)stream.ReceiveNext();
			activeRespawn = (bool)stream.ReceiveNext();
			coinsToPick = (int)stream.ReceiveNext();

			Visible = (bool)stream.ReceiveNext();

			if ( Visible != visibleOld )
			{
				Visibility(Visible);
			}
		}
	}
			
	#endregion
}

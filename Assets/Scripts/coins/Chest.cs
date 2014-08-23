using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : Photon.MonoBehaviour, ICoinCollector {

	void Start()
	{
		// TODO connect with player, we will need this in give coins method
	}

	void OnTriggerEnter(Collider other)
	{
		var coinCollector = other.gameObject.GetComponent<ThirdPersonNetworkVikOculus>();
		
		if ( coinCollector != null )
		{	
			GiveCoins(coinCollector.TakeCoins());
		}
	}

	#region ICoinCollector implementation

	public void GiveCoins (int coins)
	{
		CollectedCoins += coins;
		
		var h = new ExitGames.Client.Photon.Hashtable();
		h.Add("coinsWithPlayerInChest", CollectedCoins);

		// TODO we need to connect photonView owner with appropriate player
		photonView.owner.SetCustomProperties(h);
	}
	
	public int TakeCoins ()
	{
		return CollectedCoins;
	}
	
	int collectedCoins = 0;
	public int CollectedCoins {
		get {
			return collectedCoins;
		}
		set {
			collectedCoins = value;
		}
	}

	#endregion

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{	
		if (stream.isWriting)
		{
			stream.SendNext(collectedCoins);
		}
		else
		{
			collectedCoins = (int)stream.ReceiveNext();
		}
	}
}

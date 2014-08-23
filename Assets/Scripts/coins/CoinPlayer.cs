using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CoinPlayer : MonoBehaviour, ICoinCollector 
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ICoinCollector implementation

	public void GiveCoins (int coins)
	{
		CollectedCoins += coins;
	}
	
	public int TakeCoins ()
	{
		int coins = CollectedCoins;
		return CollectedCoins;
	}
	
	int collectedCoins;
	public int CollectedCoins {
		get {
			return collectedCoins;
		}
		set {
			collectedCoins = value;
		}
	}

	#endregion
}

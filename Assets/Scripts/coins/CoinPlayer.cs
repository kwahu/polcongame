using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CoinPlayer : MonoBehaviour, ICoinCollector {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ICoinCollector

	private List<Coin> collectedCoins;
	public List<Coin> CollectedCoins 
	{ 
		get 
		{
			return collectedCoins;
		}
		set 
		{
			collectedCoins = value;
		}
	}

	public void GiveCoins(List<Coin> coins)
	{
		foreach (var coin in coins) 
		{
			collectedCoins.Add(coin);
		}
	}

	public List<Coin> TakeCoins()
	{
		var coinsCopy = collectedCoins;

		collectedCoins.Clear();

		return coinsCopy;
	}

	#endregion
}

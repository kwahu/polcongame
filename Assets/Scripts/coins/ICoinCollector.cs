using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICoinCollector {
	
	List<Coin> CollectedCoins { get; set; }

	// give coin to collector
	void GiveCoins(List<Coin> coins);

	// recive coins from collector
	List<Coin> TakeCoins();
}

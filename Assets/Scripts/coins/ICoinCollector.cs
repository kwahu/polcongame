using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ICoinCollector {
	
	int CollectedCoins { get; set; }

	// give coin to collector
	void GiveCoins(int coins);

	// recive coins from collector
	int TakeCoins();
}

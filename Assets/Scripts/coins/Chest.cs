﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : Photon.MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerType> ().heroType == this.GetComponent<PlayerType> ().heroType) {
						// TOCO check on trigger enter with right chest that belongs to player
						var coinCollector = other.gameObject.GetComponent<PlayerNetwork> ();

						if (coinCollector != null) {	
								coinCollector.MoveCoinsToChest ();
						}
				}
	}
}

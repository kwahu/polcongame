using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsHud : Photon.MonoBehaviour
{

		float part;
		float height;
		public float left = 0.2f;
		public float right = 0.615f;

		string printStats ()
		{
				PhotonPlayer[] players = PhotonNetwork.playerList;

		var customSettings = new ExitGames.Client.Photon.Hashtable ();
		
		customSettings = PhotonNetwork.room.customProperties;

				string playerData = "";
				foreach (var player in players) {
						//playerData += "\n" + player.name + " | " + player.customProperties [PhotonNetwork.playerName+"_coins"] + " | " + player.customProperties ["coinsInChest"];
			playerData += "\n" + player.name + " | " + customSettings [player.name+"_coins"] + " | " + customSettings [player.name+"_coinsInChest"];
				}

				return "Player | Coins | Chest Coins " + playerData;
		}

		void OnGUI ()
		{
				part = Screen.width / 6;
				height = Screen.height / 5.5f;

				if (GameManager.isOculus ()) {
						GUILayout.BeginArea (new Rect (Screen.width * left, height * 3, part, height));
						Draw ();
						GUILayout.EndArea ();

						GUILayout.BeginArea (new Rect (Screen.width * right, height * 3, part, height));
						Draw ();
						GUILayout.EndArea ();
				} else {
						GUILayout.BeginArea (new Rect (Screen.width / 2.3f, Screen.height / 1.5f, part, height));
						Draw ();
						GUILayout.EndArea ();
				}
		}

		void Draw ()
		{
				GUILayout.Box (printStats ());
		}
}

using UnityEngine;
using System.Collections;

public class StatsHud : Photon.MonoBehaviour {


	string printStats()
	{
		PhotonPlayer[] players = PhotonNetwork.playerList;

		string playerData = "";
		foreach (var player in players) {
			playerData = "\n" + player.name + " | TODO coins";
		}

		return "Player | Coins" + playerData;
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0.0f, 0.0f, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				GUILayout.Box (printStats());
			GUILayout.EndHorizontal();
		GUILayout.EndArea ();
	}
}

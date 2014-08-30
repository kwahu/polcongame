using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : Photon.MonoBehaviour
{
		string playerPrefabName = "Overseer"; //a default spectator

		void OnJoinedRoom ()
		{
				SetHeroStatus (MainMenu.selectedHero, true);
				if (MainMenu.selectedHero != null)
						playerPrefabName = MainMenu.selectedHero;
				StartGame ();
		}

		void SetHeroStatus (string name, bool state)
		{
				if (!MainMenu.isHero ()) 
						return;

				PhotonHashtable customSettings = PhotonNetwork.room.customProperties;
				customSettings [name] = state;
				PhotonNetwork.room.SetCustomProperties (customSettings);
		}
    
		IEnumerator OnLeftRoom ()
		{
				//Easy way to reset the level: Otherwise we'd manually reset the camera
				//Wait untill Photon is properly disconnected (empty room, and connected back to main server)
				while (PhotonNetwork.room!=null || PhotonNetwork.connected==false)
						yield return 0;

				Application.LoadLevel (Application.loadedLevel);
		}

		void StartGame ()
		{
				object[] objs = new object[1]; // Put our bool data in an object array, to send
				//    objs[0] = enabledRenderers;

				// Spawn our local player
				PhotonNetwork.Instantiate (this.playerPrefabName, transform.position, Quaternion.identity, 0, objs);
		}

		void OnGUI ()
		{
				if (PhotonNetwork.room == null)
						return; //Only display this GUI when inside a room

				if (GUILayout.Button ("Leave Room")) {
						SetHeroStatus (MainMenu.selectedHero, false);
						PhotonNetwork.LeaveRoom ();
				}

		if(!MainMenu.isHero () )
				ManualReleasingPlayers ();

		}

		void ManualReleasingPlayers ()
		{
				PhotonHashtable customSettings = PhotonNetwork.room.customProperties;
	
		foreach (string name in MainMenu.playerPrefabs)
						if (GUILayout.Button (name + " " + customSettings [name])) {
								customSettings [name] = !(bool)customSettings [name];
								PhotonNetwork.room.SetCustomProperties (customSettings);
						}
		}

		void OnDisconnectedFromPhoton ()
		{
				Debug.LogWarning ("OnDisconnectedFromPhoton");
		}

		void OnApplicationQuit ()
		{
				SetHeroStatus (MainMenu.selectedHero, false);
		}
}
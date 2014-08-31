using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : Photon.MonoBehaviour
{
		string playerPrefabName = "Overseer"; //a default spectator

		public GameObject camera_pc;
		public GameObject camera_oculus;

		void OnJoinedRoom ()
		{
				SetHeroStatus (MainMenu.selectedHero, true);
				SetupCamera ();
				StartGame ();
		}

		void SetupCamera ()
		{
				camera_oculus.SetActive (true);
				if (MainMenu.isHero ()) {
						playerPrefabName = MainMenu.selectedHero;

						if (isOculus ()) {
								PlayerNetwork.camera = camera_oculus;
								camera_pc.SetActive (false);
					
						} else {
								PlayerNetwork.camera = camera_pc;
								camera_oculus.SetActive (false);
						}
				} else {
						PlayerNetwork.camera = camera_pc;
						camera_oculus.SetActive (false);
				}
		}

		public static bool isOculus ()
		{
		if (OVRDevice.IsSensorPresent(0)) 
						return true;
				else
						return false;
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

				//find respawn point for this player name
				GameObject respawn = GameObject.Find (this.playerPrefabName + "_spawn");

		Debug.Log (this.playerPrefabName + "_spawn = " + respawn);
				// Spawn our local player
				PhotonNetwork.Instantiate (this.playerPrefabName, respawn.transform.position, Quaternion.identity, 0, objs);
		}

		void OnGUI ()
		{
				if (PhotonNetwork.room == null)
						return; //Only display this GUI when inside a room

				if (GUILayout.Button ("Leave Room")) {
						SetHeroStatus (MainMenu.selectedHero, false);
						PhotonNetwork.LeaveRoom ();
				}

				if (!MainMenu.isHero ())
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
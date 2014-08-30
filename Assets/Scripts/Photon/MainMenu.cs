using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class MainMenu : MonoBehaviour
{
		private string roomName = "polcon";
		public PhotonHashtable customSettings;
		public static string[] playerPrefabs = {"game","film","theater","books"};
		public static string selectedHero = null;

	public GameObject camera_pc;
	public GameObject camera_oculus;
	
		void Awake ()
		{
				//PhotonNetwork.logLevel = NetworkLogLevel.Full;
				//Connect to the main photon server. This is the only IP and port we ever need to set(!)
				if (!PhotonNetwork.connected)
						PhotonNetwork.ConnectUsingSettings ("v1.0"); // version of the game/demo. used to separate older clients from newer ones (e.g. if incompatible)

				//Load name from PlayerPrefs
				PhotonNetwork.playerName = PlayerPrefs.GetString ("playerName", "Guest" + Random.Range (1, 9999));
		}

		static public bool isHero ()
		{
				if (selectedHero == null)
						return false;
				else
						return true;
		}

		void OnGUI ()
		{
				if (!PhotonNetwork.connected) {
						ShowConnectingGUI ();
						return;   //Wait for a connection
				}
	
				if (PhotonNetwork.room != null)
						return; //Only when we're not in a Room

				GUILayout.BeginArea (new Rect ((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
				GUILayout.BeginHorizontal ();

				if (PhotonNetwork.countOfRooms > 0)
						JoinRoomAsOneOfHeroes ();
				else
						CreateRoomAsSpectator ();

				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
		}

		void CreateRoomAsSpectator ()
		{
		PlayerNetwork.camera = camera_pc;

				GUILayout.Label ("CREATE ROOM", GUILayout.Width (150));
				if (GUILayout.Button ("GO")) {
						PhotonNetwork.CreateRoom (roomName, true, true, 10, InitRoomSettings (), null);	
				}
		}

		void JoinRoomAsOneOfHeroes ()
		{
				GUILayout.Label ("WYBIERZ BOHATERA", GUILayout.Width (150));

		camera_pc.SetActive (false);
		camera_oculus.SetActive (true);
		PlayerNetwork.camera = camera_oculus;

		if (PhotonNetwork.GetRoomList ().Length > 0) {
						customSettings = PhotonNetwork.GetRoomList () [0].customProperties;
						//display available heroes
						foreach (string name in playerPrefabs)
								if ((bool)customSettings [name] == false && GUILayout.Button (name)) {
										selectedHero = name;
										PhotonNetwork.JoinRoom (roomName);
								}
				}
		}

		void ShowConnectingGUI ()
		{
				GUILayout.BeginArea (new Rect ((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));
				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Connecting to Photon server.");
				GUILayout.Label ("Hint: This demo uses a settings file and logs the server address to the console.");
				GUILayout.EndHorizontal ();
				GUILayout.EndArea ();
		}

		PhotonHashtable InitRoomSettings ()
		{
				//set the table with players and their charactes
				customSettings = new PhotonHashtable ();
				foreach (string name in playerPrefabs) {
						customSettings.Add (name, false);
				}
				return customSettings;
		}
}
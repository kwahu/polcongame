using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetwork : Photon.MonoBehaviour, ICoinCollector
{
		PlayerCamera cameraScript;
		PlayerController controllerScript;
		private bool appliedInitialUpdate;

	static public GameObject camera;

		void Awake ()
		{
		cameraScript = GetComponent<PlayerCamera> ();
		controllerScript = GetComponent<PlayerController> ();
		}

		void Start ()
		{

				var h = new Hashtable ();
				h.Add ("coinsWithPlayer", CollectedCoins);

				//TODO: Bugfix to allow .isMine and .owner from AWAKE!
				if (photonView.isMine) {

					Renderer child_renderer;
					foreach (Transform child in GetComponentsInChildren<Transform>()) {
						child_renderer = child.GetComponent<Renderer>();
						if(child_renderer){
							child_renderer.enabled = false;		
						}
						
					}
						//MINE: local player, simply enable the local scripts
					//	cameraScript.enabled = true;
						controllerScript.enabled = true;
						//GameObject obj = GameObject.FindGameObjectWithTag("oculus");//GameObject.Find ("CAME");//FindGameObjectWithTag("oculus");
						camera.transform.parent = transform;
						camera.transform.localPosition = new Vector3 (0, 1.5f, 0);
						camera.transform.localEulerAngles = new Vector3 (0, 0, 0);
				} else {           
					//	cameraScript.enabled = false;
						controllerScript.enabled = false;
				}
				controllerScript.SetIsRemotePlayer (!photonView.isMine);

				gameObject.name = gameObject.name + photonView.viewID + photonView.isMine;
		}

		void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
		{
				if (stream.isWriting) {
						//We own this player: send the others our data
						// stream.SendNext((int)controllerScript._characterState);
						stream.SendNext (transform.position);
						stream.SendNext (transform.rotation);
						stream.SendNext (rigidbody.velocity); 
						stream.SendNext (CollectedCoins);

				} else {
						//Network player, receive data
						//controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
						correctPlayerPos = (Vector3)stream.ReceiveNext ();
						correctPlayerRot = (Quaternion)stream.ReceiveNext ();
						rigidbody.velocity = (Vector3)stream.ReceiveNext ();
						collectedCoins = (int)stream.ReceiveNext ();

						if (!appliedInitialUpdate) {
								appliedInitialUpdate = true;
								transform.position = correctPlayerPos;
								transform.rotation = correctPlayerRot;
								rigidbody.velocity = Vector3.zero;
						}
				}
		}

		private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
		private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

		void Update ()
		{
				//Debug.Log ("isMessageQueueRunning"+PhotonNetwork.isMessageQueueRunning);
				if (!photonView.isMine) {
						//Debug.Log("Update" + correctPlayerPos);
						//Update remote player (smooth this, this looks good, at the cost of some accuracy)
						transform.position = Vector3.Lerp (transform.position, correctPlayerPos, Time.deltaTime * 5);
						transform.rotation = Quaternion.Lerp (transform.rotation, correctPlayerRot, Time.deltaTime * 5);
				}
		}

		void OnPhotonInstantiate (PhotonMessageInfo info)
		{
				//We know there should be instantiation data..get our bools from this PhotonView!
				object[] objs = photonView.instantiationData; //The instantiate data..
				bool[] mybools = (bool[])objs [0];   //Our bools!

				//disable the axe and shield meshrenderers based on the instantiate data
				MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer> ();
//        rens[0].enabled = mybools[0];//Axe
				//    rens[1].enabled = mybools[1];//Shield
		}

	#region ICoinCollector implementation

		public void GiveCoins (int coins)
		{
				CollectedCoins += coins;

				UpdatePlayerCoinsInfo ();
		}

		public int TakeCoins ()
		{
				int coins = CollectedCoins;

				UpdatePlayerCoinsInfo ();

				CollectedCoins = 0;

				return coins;
		}

		int collectedCoins = 0;

		public int CollectedCoins {
				get {
						return collectedCoins;
				}
				set {
						collectedCoins = value;
				}
		}

	#endregion

		int coinsInChest = 0;

		public void MoveCoinsToChest ()
		{
				coinsInChest += TakeCoins ();
		
				UpdatePlayerCoinsInfo ();
		}

		void UpdatePlayerCoinsInfo ()
		{
				var h = new ExitGames.Client.Photon.Hashtable ();

				h.Add ("coinsWithPlayer", CollectedCoins);
				h.Add ("coinsWithPlayerInChest", coinsInChest);

				photonView.owner.SetCustomProperties (h);
		}
}
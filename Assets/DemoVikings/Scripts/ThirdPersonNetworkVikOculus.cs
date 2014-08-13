using UnityEngine;
using System.Collections;


public class ThirdPersonNetworkVikOculus : Photon.MonoBehaviour
{
	ThirdPersonCameraNETOculus cameraScript;
    ThirdPersonControllerNET controllerScript;
    private bool appliedInitialUpdate;

    void Awake()
    {
       // cameraScript = GetComponent<ThirdPersonCameraNETOculus>();
       // controllerScript = GetComponent<ThirdPersonControllerNET>();

    }
    void Start()
    {
        //TODO: Bugfix to allow .isMine and .owner from AWAKE!
        if (photonView.isMine)
        {
            //MINE: local player, simply enable the local scripts
          //  cameraScript.enabled = true;
         //   controllerScript.enabled = true;
			GameObject obj = GameObject.Find("PC camera");//FindGameObjectWithTag("oculus");
			Debug.Log (obj);
			obj.transform.parent = transform;
			obj.transform.localPosition = new Vector3(0, 2, 0);
			obj.transform.localEulerAngles = new Vector3(0, 0, 0);

        }
        else
        {           
           // cameraScript.enabled = false;
            controllerScript.enabled = true;

        }
        controllerScript.SetIsRemotePlayer(!photonView.isMine);

		gameObject.name = gameObject.name + photonView.viewID + photonView.isMine;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            //We own this player: send the others our data
           // stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rigidbody.velocity); 

        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            rigidbody.velocity = (Vector3)stream.ReceiveNext();

            if (!appliedInitialUpdate)
            {
                appliedInitialUpdate = true;
                transform.position = correctPlayerPos;
                transform.rotation = correctPlayerRot;
                rigidbody.velocity = Vector3.zero;
            }
        }
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Update()
    {
		//Debug.Log ("isMessageQueueRunning"+PhotonNetwork.isMessageQueueRunning);
        if (!photonView.isMine)
        {
			//Debug.Log("Update" + correctPlayerPos);
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //We know there should be instantiation data..get our bools from this PhotonView!
        object[] objs = photonView.instantiationData; //The instantiate data..
        bool[] mybools = (bool[])objs[0];   //Our bools!

        //disable the axe and shield meshrenderers based on the instantiate data
        MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer>();
//        rens[0].enabled = mybools[0];//Axe
    //    rens[1].enabled = mybools[1];//Shield
    }
}
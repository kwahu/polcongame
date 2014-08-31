using UnityEngine;
using System.Collections;

public class GamePadNetwork : Photon.MonoBehaviour {

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log ("coconut");
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (rigidbody.velocity); 
			Debug.Log ("writing");
		} else {
			transform.position = (Vector3)stream.ReceiveNext ();
			transform.rotation = (Quaternion)stream.ReceiveNext ();
			rigidbody.velocity = (Vector3)stream.ReceiveNext ();

			Debug.Log ("reading");
		}
	}
}

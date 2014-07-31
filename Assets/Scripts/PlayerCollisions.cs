using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{

		public float forceFactor = 10000;
	
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetButtonDown ("Fire1"))
						Hit ();
		}

		void Hit ()
		{
				RaycastHit hit;

				Physics.Raycast (transform.position, transform.forward, out hit, 10);
				Debug.DrawLine (transform.position, hit.point, Color.green, 2, false);

				if (hit.collider != null) {
						Debug.Log ("hit:" + hit.collider.name);
						GameObject obj = hit.collider.gameObject;
				
						if (obj.tag == "enemy") {
								obj.rigidbody.AddForce (forceFactor * transform.forward);
						}
				}
		}
}
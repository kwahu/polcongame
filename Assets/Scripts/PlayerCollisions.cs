using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{

		public float forceFactor = 10000;
		public bool PowerUsed = false;
		public float CheckTimer;

		// Update is called once per frame
		void Update ()
		{

				if (Input.GetButtonDown ("Fire1") && PowerUsed == false)
						Hit ();
				if (PowerUsed == true) {
						CheckTimer = Time.deltaTime + CheckTimer;
				}
				if (CheckTimer >= 15.0f) {
						PowerUsed = false;
						CheckTimer = 0.0f;
				}
						
		}
		


		void Hit ()
		{
				RaycastHit hit;

				Physics.Raycast (transform.position, transform.forward, out hit, 2);
				//Debug.DrawLine (transform.position, hit.point, Color.green, 2, false);

				if (hit.collider != null) {
						//Debug.Log ("hit:" + hit.collider.name);
						GameObject obj = hit.collider.gameObject;
						
						
				
						if (obj.tag == "enemy") {
								obj.rigidbody.AddForce (forceFactor * transform.forward);
								PowerUsed = true;
						}
						if (obj.tag == "door") {
								Debug.Log (obj);
								obj.transform.parent.SendMessage ("NextState");
								PowerUsed = true;

						}
			
			
				}
		}
}

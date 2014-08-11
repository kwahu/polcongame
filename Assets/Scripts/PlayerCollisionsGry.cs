using UnityEngine;
using System.Collections;

public class PlayerCollisionsGry : MonoBehaviour
{

		public float forceFactor = 10000;
		public AudioClip Stop;
		public AudioClip Switch;

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
								audio.PlayOneShot (Stop);
								
						}
						if (obj.tag == "switch") {
								
								audio.PlayOneShot (Switch);
								
						}
			
			
				}
		}
}

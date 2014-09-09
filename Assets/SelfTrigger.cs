using UnityEngine;
using System.Collections;

public class SelfTrigger : MonoBehaviour
{
		bool triggered = false;

		void OnTriggerEnter (Collider col)
		{
				if (!triggered && col.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			triggered = true;
						SendMessage ("NextState");
				}
		}
}

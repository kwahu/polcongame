using UnityEngine;
using System.Collections;

public class ComputerPuzzleTrigger : MonoBehaviour
{

		public GameObject puzzle;
		bool done = false;

		void OnTriggerEnter (Collider col)
		{
				if (!done && col.gameObject.layer == LayerMask.NameToLayer("Player")) {
						puzzle.GetComponent<ComputerPuzzle> ().count += 1;
						done = true;
			this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
				}
		}
}

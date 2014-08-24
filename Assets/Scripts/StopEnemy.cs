using UnityEngine;
using System.Collections;

public class StopEnemy : MonoBehaviour {

	// Use this for initialization
	void StopIt () {
		StartCoroutine ("Stop");
	}
	IEnumerator Stop(){
		GetComponent<Animator> ().enabled = false;
		yield return new WaitForSeconds (5.0f);
		GetComponent<Animator> ().enabled = true;
	}
	// Update is called once per frame
	void Update () {

	}
}

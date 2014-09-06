using UnityEngine;
using System.Collections;

public class SelfTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("door collision");
		SendMessage("NextState");
	}
}

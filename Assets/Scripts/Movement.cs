using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision theObject){
	if(theObject.gameObject.name=="coconut")
			SendMessage("Stop");
	}
}

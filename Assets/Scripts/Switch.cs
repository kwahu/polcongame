using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
	/* 
	 * Simple swich tat allow us to open/close Doors, Bridges etc.
	 * One Switch might trigger multiple targets and many targets might be connected to one switch
     */

	public GameObject[] targets; // any StateObject (Bridge, Doors, etc.)
	public bool debugTrigger = false; // usefull in development as a trigger button in inspector

	private bool is_on;
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void FixedUpdate() {
		if (debugTrigger == true) {
			debugTrigger = false;
			Trigger ();
		}
	}

	void OnCollisionEnter(Collision collision) {
		/* Call Trigger() any way you want - I just didn't want to mess with player;
		 * hence, such a simple example - this OnCollisionEnter() is to be removed. 
		 */ 
		Trigger ();
	}

	public void Trigger(){
		animator.SetBool("trigger", true);
		
		foreach (GameObject traget in targets){
			traget.SendMessage("NextState");
		}
	}
}

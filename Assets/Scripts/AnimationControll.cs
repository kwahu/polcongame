using UnityEngine;
using System.Collections;

public class AnimationControll : MonoBehaviour {

	private Animator animator;
	
	void Start () {
		animator = GetComponent<Animator>();
	}

	void FixedUpdate () {
		transform.localPosition = Vector3.zero;

		float sqrmag = transform.parent.rigidbody.velocity.sqrMagnitude;

		if(sqrmag < 0.01){
			sqrmag = 0.0f;
		}

		animator.SetFloat("Speed",  sqrmag);
	}
}

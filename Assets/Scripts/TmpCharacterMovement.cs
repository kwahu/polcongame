using UnityEngine;
using System.Collections;

/* I just use it to test animators - do not use it
 */

public class TmpCharacterMovement : MonoBehaviour {

	protected Animator animator;
	
	
	float speed = 4.5f;
	float rot_speed = 180.0f;

	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update () {

		if (Input.GetKey ("w")) {
			transform.position += (transform.forward * speed * Time.deltaTime);
				animator.SetFloat ("Speed", 1.0f);
				animator.speed = 1.0f;
		} else {
			animator.SetFloat ("Speed", 0.0f);
		}

		if (Input.GetKey ("a")) {
			transform.Rotate (transform.up * -rot_speed * Time.deltaTime);
		}

		if (Input.GetKey ("d")) {
			transform.Rotate (transform.up * rot_speed * Time.deltaTime);
		} 
	}
}

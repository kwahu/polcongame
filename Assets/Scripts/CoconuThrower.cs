using UnityEngine;
using System.Collections;

public class CoconuThrower : MonoBehaviour {
	public AudioClip throwSound;
	public Rigidbody coconutPrefab;
	public float throwSpeed = 30.0f;
	public float timer=0.0f;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetButtonDown ("Fire1")) {
			audio.PlayOneShot(throwSound);
			Rigidbody newCoconut = Instantiate(coconutPrefab, transform.position, transform.rotation) as Rigidbody;
			newCoconut.name="coconut";
			newCoconut.velocity= transform.forward *throwSpeed;
			Physics.IgnoreCollision(transform.root.collider, newCoconut.collider,true);

				}

	}
}

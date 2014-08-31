using UnityEngine;
using System.Collections;

public class CoinRotator : MonoBehaviour {

	private float rotation_speed = 2.0f;
	private float shake_fac = 2.0f;
	private float shake_speed_fac = 10.0f;

	void FixedUpdate () {
		transform.Rotate (Vector3.up, rotation_speed);
		transform.Rotate (Vector3.left, Mathf.Sin (Time.time * shake_speed_fac) / shake_fac );
	}
}

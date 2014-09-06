using UnityEngine;
using System.Collections;

public class OnSightTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		
		Physics.Raycast (transform.position + transform.up * 1, transform.forward, out hit, 5);
		Debug.DrawLine (transform.position, hit.point, Color.green, 5, false);
	
		//Debug.DrawLine (transform.position, hit.point, Color.green, 2, false);
		
		if (hit.collider != null && hit.collider.name == "PaintingTrigger") {
						Debug.Log ("hit:" + hit.collider.name);
						GameObject obj = hit.collider.gameObject;
						obj.GetComponent<Switch>().Trigger();
				}
	}
}

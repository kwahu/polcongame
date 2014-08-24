using UnityEngine;
using System.Collections;

public class tidyObject : MonoBehaviour {

	public float removeTime = 1.0f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, removeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

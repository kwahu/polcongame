﻿using UnityEngine;
using System.Collections;

public class CoconuThrower : MonoBehaviour {
	public AudioClip throwSound;
	public Rigidbody coconutPrefab;
	public float throwSpeed = 30.0f;
	public float timer=0.0f;

	public Transform Origin;
	public Transform Destination;

	public DrawLine lineDrawer;
	public bool alreadyShot= false;



	private LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		lineDrawer = GetComponent<DrawLine> ();
		lineRenderer = GetComponent<LineRenderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetButtonDown ("Fire1")&& alreadyShot==false) {
			audio.PlayOneShot(throwSound);
			Rigidbody newCoconut = Instantiate(coconutPrefab, transform.position, transform.rotation) as Rigidbody;
			newCoconut.name="coconut";
			newCoconut.velocity= transform.forward *throwSpeed;
			Physics.IgnoreCollision(transform.root.collider, newCoconut.collider,true);

			lineDrawer.Origin = newCoconut.gameObject;

			lineDrawer.Destination = gameObject;
			//lineRenderer.enabled=true ;
			StartCoroutine("lineoff");
			StartCoroutine("drawoff");
			alreadyShot=true;
			StartCoroutine("canShot");
		//	initline(newCoconut , );
		//	drawline();
				}


		//if()drawline;

	}
	IEnumerator lineoff(){

		lineRenderer.enabled=true;
		yield return new WaitForSeconds(1.0f);
		lineRenderer.enabled=false;


	}
	IEnumerator drawoff(){
		lineDrawer.enabled = true;

		yield return new WaitForSeconds(0.9f);

		lineDrawer.enabled = false;
		
	}
	IEnumerator canShot(){

		
		yield return new WaitForSeconds(5.0f);
		
		alreadyShot = false;
		
	}


	// Use this for initialization
	/*void initline  (GameObject newCoconut ) {
		
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, Origin.position);
		lineRenderer.SetWidth (.45f, .45f);
		
		//			dist=Vector3.Distance(Origin.position, Destination.position);
		
		
	}
	
	// Update is called once per frame
	void drawline () {
		
		
		
		lineRenderer.SetPosition (1, Destination.position);
		
	}*/
}

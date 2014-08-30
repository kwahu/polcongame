using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{
		private LineRenderer lineRenderer;
		private float dist;
		public GameObject Origin;
		public GameObject Destination;

		// Use this for initialization
		void Start ()
		{
				lineRenderer = GetComponent<LineRenderer> ();
				//lineRenderer.SetWidth (.01f, .01f);
		}

		// Update is called once per frame
		void Update ()
		{
				if (Destination == null) 
						lineRenderer.enabled = false;
				else {
						draw ();
				}
		}

		void draw ()
		{
				lineRenderer.SetPosition (0, Origin.transform.position);
				lineRenderer.SetPosition (1, Destination.transform.position);
		}
}

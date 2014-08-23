using UnityEngine;
using System.Collections;

public class PlayerCollisionsFilm : MonoBehaviour
{

		public float forceFactor = 1000;
		public bool PowerUsed = false;
		public float CheckTimer;
		public  int clickCounter;
	public bool szybowanie = false;

		// Update is called once per frame
		void FixedUpdate ()
		{
		if(Input.GetButtonDown ("Fire1")&& clickCounter==0) {
			
			Fly ();
			clickCounter++;
		}
		if(Input.GetButtonDown ("Fire1") && clickCounter == 1) {
			szybowanie=true;
			
			clickCounter++;
		}

				
				if(szybowanie==true){
			rigidbody.AddForce(Vector3.up *5.0f);	
			}
		}
	void Start(){

		}

		
		void szybuj ()
		{

		}

		void Fly ()
		{
				
				rigidbody.AddForce (Vector3.up * forceFactor);
				
		}

		
			
			
}
		


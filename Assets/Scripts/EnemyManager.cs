using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	float standUpTimer = 0.0f;
	bool contact=false;
	public float standUpTime = 3.0f;
	public AudioClip downSound;
	public AudioClip upSound;
	// Use this for initialization
	void Start () {
		standUpTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (contact) {
			standUpTimer += Time.deltaTime;
			if (standUpTimer > standUpTime) {
				Capsule (upSound, false);
				standUpTimer = 0.0f;
			}
		}
	
	}
	void EnemyCheck(){
		if(!contact){
			Capsule(downSound, true);
		}
	}
	void Capsule(AudioClip aClip, bool standCheck){
		audio.PlayOneShot (aClip);
		contact = standCheck;

	}
}

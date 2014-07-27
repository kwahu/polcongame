using UnityEngine;
using System.Collections;

public class StateObject : Photon.MonoBehaviour {	
	/* 
	 * ment to be use with Switch
	 * localPosition is applied before localEulerAngles!
	 * 
	 */
	
	static int sendRate = 4; // photon - (times per second) - default is 20
	static int sendRateOnSerialize = 4; // photon - (times per second) - default is 10

	[System.Serializable]
	public class State
	{
		public Vector3 localPosition;
		public Vector3 localEulerAngles;
	}

	public int initStateIndex = 0; // on starting the game it will be this
	public State[] states; // there must be at least one state but as it makes no sense we need at least 2
	public float animationDuration = 5.0f; // in seconds
	public int defaultStateIndex = -1; // if >=0 it will come backt to this state after nonDefaultStateDuration   
	public float nonDefaultStateDuration = 5.0f; // how long it will tolerate not being in nondefault state

	// private
	int currStateIndex = 0;
	int nextStateIndex = 0;
	float startTime;

	void Start () {
		SetState (initStateIndex);
		PhotonNetwork.sendRate = sendRate; 
		PhotonNetwork.sendRateOnSerialize = sendRateOnSerialize;
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext( nextStateIndex );
		} 
		else {
			nextStateIndex = (int)stream.ReceiveNext();
			ChangeState(currStateIndex, nextStateIndex);
		}
	}
	
	void FixedUpdate () {
		if (currStateIndex != nextStateIndex) {
			float frac = ease( (Time.time - startTime) / animationDuration );

			foreach (Transform child in transform){
				child.transform.localPosition = Vector3.Lerp(
					states[currStateIndex].localPosition, 
					states[nextStateIndex].localPosition, frac);

				
				child.transform.localEulerAngles = Vector3.Lerp(
					states[currStateIndex].localEulerAngles, 
					states[nextStateIndex].localEulerAngles, frac);
			}

			if(Time.time - startTime >= animationDuration){
				SetState (nextStateIndex);
			}
		}

		if (defaultStateIndex >= 0 && (Time.time - startTime) > nonDefaultStateDuration + animationDuration) {
			ChangeState(currStateIndex, defaultStateIndex);
		}
	}
	
	void NextState () {
		if (nextStateIndex == currStateIndex) { // you cannot change state during animation
			nextStateIndex = (currStateIndex + 1) % states.Length;
			ChangeState(currStateIndex, nextStateIndex);
		}
	}

	private void SetState (int stateIndex) {
		foreach (Transform child in transform){
			child.transform.localPosition = states[stateIndex].localPosition;
            child.transform.localEulerAngles = states[stateIndex].localEulerAngles;	
		}	
		currStateIndex = stateIndex;
		nextStateIndex = stateIndex;
	}

	private void ChangeState (int from, int to) {
		startTime = Time.time;
		currStateIndex = from;
		nextStateIndex = to;
	}

	private float ease(float p){ // [0,1] -> [0,1]
		float a = 3.5f;
		float pow = Mathf.Pow (p, a);
		return Mathf.Min (pow / (pow + Mathf.Pow (1 - p, a)), 1.0f);
	}
}

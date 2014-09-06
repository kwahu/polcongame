using UnityEngine;
using System.Collections;

public class ComputerPuzzle : MonoBehaviour {

	public int count;
	bool open = false;

	void Update()
	{
		if (count == 8 && !open) {
			SendMessage("NextState");
			open = true;
				}
	}

}

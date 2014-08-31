using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHud : Photon.MonoBehaviour
{

		float part;
		float height;
		public float left = 0.2f;
		public float right = 0.615f;

		string printStats ()
		{
		return "Twoje punkty " + this.GetComponent<PlayerNetwork>().CollectedCoins + " Twoje punkty w skrzyni " + this.GetComponent<PlayerNetwork>().CoinsInChest;
		}

		void OnGUI ()
		{
				part = Screen.width / 6;
				height = Screen.height / 5.5f;

				if (GameManager.isOculus ()) {
						GUILayout.BeginArea (new Rect (Screen.width * left, height * 3, part, height));
						Draw ();
						GUILayout.EndArea ();

						GUILayout.BeginArea (new Rect (Screen.width * right, height * 3, part, height));
						Draw ();
						GUILayout.EndArea ();
				} else {
						GUILayout.BeginArea (new Rect (Screen.width / 2.3f, Screen.height / 2f, part, height));
						Draw ();
						GUILayout.EndArea ();
				}
		}

		void Draw ()
		{
		GUILayout.Box ("Twoje punkty " + this.GetComponent<PlayerNetwork>().CollectedCoins );
		GUILayout.Box ("Twoje punkty w skrzyni " + this.GetComponent<PlayerNetwork>().CoinsInChest);
		}
}

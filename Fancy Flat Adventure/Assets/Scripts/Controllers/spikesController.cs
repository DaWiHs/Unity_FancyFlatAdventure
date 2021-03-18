using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesController : MonoBehaviour {


	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (PlayerPrefs.GetString ("2PMode") == "False") {
				GameObject.FindGameObjectWithTag ("gameManager").SendMessage ("Death");
			} else {
				Debug.Log ("P1 Death");
				GameObject.FindGameObjectWithTag ("gameManager").SendMessage ("P1Death");

			}
		}
		if (other.CompareTag("Player2")) {
			Debug.Log ("P2 Death");
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("P2Death");
		}
	
	}
}

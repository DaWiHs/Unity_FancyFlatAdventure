using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLvl : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("LvlComplete");
		}
		if (other.CompareTag("Player2")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("LvlComplete");
		}
	}

}

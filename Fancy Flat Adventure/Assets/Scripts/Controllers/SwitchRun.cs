using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRun : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			GameObject.FindWithTag ("gameManager").SendMessage ("SwitchSide");
		}
	}
}

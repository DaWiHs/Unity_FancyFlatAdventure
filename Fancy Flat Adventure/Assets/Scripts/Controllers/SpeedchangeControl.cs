using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedchangeControl : MonoBehaviour {
	
	public bool SpeedUp;
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (SpeedUp == true) {
				GameObject.FindWithTag ("gameManager").SendMessage ("SpeedUp");
			} else {
				GameObject.FindWithTag ("gameManager").SendMessage ("SlowDown");
			}
		}
	}

}

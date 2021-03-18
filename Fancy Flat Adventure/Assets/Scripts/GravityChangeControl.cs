using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeControl : MonoBehaviour {

	public bool LowerGravity;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			if (LowerGravity == true) {
				other.GetComponent<Rigidbody2D> ().gravityScale -= 1;
			} else {
				other.GetComponent<Rigidbody2D> ().gravityScale += 1;
			}
		}
	}

}

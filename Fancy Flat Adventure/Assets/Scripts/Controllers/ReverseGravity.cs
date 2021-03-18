using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			other.GetComponent<Rigidbody2D> ().gravityScale *= -1;
			other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y * -1, other.transform.localScale.z);
			GameObject.FindWithTag ("gameManager").SendMessage ("ReverseGravity");
		}
	}
}

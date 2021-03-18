using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin5Controller : MonoBehaviour {

	public float rotationSpeed;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("CoinPickup5");
			Destroy (gameObject);
		}
		if(other.CompareTag("Player2")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("P2CoinPickup5");
			Destroy (gameObject);
		}
	}
	void Update () {
		transform.Rotate (new Vector3 (0, rotationSpeed, 0)); 
	}
}
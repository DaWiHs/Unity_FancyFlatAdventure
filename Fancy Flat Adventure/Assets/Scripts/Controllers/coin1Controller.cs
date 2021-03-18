using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin1Controller : MonoBehaviour {

	public float rotationSpeed;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("CoinPickup1");
			Destroy (gameObject);
		}
		if(other.CompareTag("Player2")) {
			GameObject.FindGameObjectWithTag("gameManager").SendMessage("P2CoinPickup1");
			Destroy (gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, rotationSpeed, 0)); 
	}
}

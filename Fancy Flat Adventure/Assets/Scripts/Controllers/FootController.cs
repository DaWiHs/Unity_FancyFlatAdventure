using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour {

	GameObject gameManager;
	string gOName;

	void Start () {
		gameManager = GameObject.FindWithTag ("gameManager");
		gOName = gameObject.name;
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Ground")) {
			gameManager.SendMessage (gOName + "ControlT");
		}
	}
	void OnTriggerExit2D (Collider2D other) {
		if (other.CompareTag("Ground")) {
			gameManager.SendMessage (gOName + "ControlF");
		}
	}
}

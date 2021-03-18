using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

	public GameObject activeObj;

	void Start () {
		
	}

	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			activeObj.SendMessage("Active");
		}
	}

}

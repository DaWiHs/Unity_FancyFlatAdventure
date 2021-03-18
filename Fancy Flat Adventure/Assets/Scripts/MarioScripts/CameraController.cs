using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject player;
	Vector3 lastPlayerPosition;
	float distanceToMove;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x > transform.position.x - 3) {
			transform.position = new Vector3 (player.transform.position.x + 3.1f  , transform.position.y, transform.position.z);
		} 
	}
}

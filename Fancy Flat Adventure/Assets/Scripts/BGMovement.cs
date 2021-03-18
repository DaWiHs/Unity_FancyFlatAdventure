using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour {

	private Vector3 lastPlayerPosition;
	public GameObject player;
	private float distanceToMove;
	public GameObject  mainCamera;
	GameObject BGEnd;
	GameObject BGBegin;

	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");
		lastPlayerPosition = player.transform.position;
		BGEnd = GameObject.Find ("BGEnd");
		BGBegin = GameObject.Find ("BGBegin");
	}

	void Update () {
		distanceToMove = player.transform.position.x - lastPlayerPosition.x;
		gameObject.transform.position += new Vector3 (distanceToMove - 0.02f,0,0);
		lastPlayerPosition = player.transform.position;
		if (gameObject.transform.position.x <= BGEnd.transform.position.x) {
			gameObject.transform.position = new Vector3 (BGBegin.transform.position.x, BGBegin.transform.position.y, 15);
		}
	}
}

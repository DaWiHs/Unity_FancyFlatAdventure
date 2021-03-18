using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warstwa3BGM : MonoBehaviour {
	private Vector3 lastPlayerPosition;
	private Vector3 lastPlayer2Position;
	GameObject player;
	GameObject player2;
	private float distanceToMove;
	GameObject BGEnd;
	GameObject BGBegin;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
		lastPlayerPosition = player.transform.position;
		BGEnd = GameObject.Find ("BGEnd");
		BGBegin = GameObject.Find ("BGBegin");
	}

	void Update () {

		if (PlayerPrefs.GetString("2PMode") == "False") {
			distanceToMove = player.transform.position.x - lastPlayerPosition.x;
			lastPlayerPosition = player.transform.position;
			if (gameObject.transform.position.x <= BGEnd.transform.position.x) {
				gameObject.transform.position = new Vector3 (BGBegin.transform.position.x, BGBegin.transform.position.y, gameObject.transform.position.z);
			}
		} else {
			if (player.transform.position.x >= player2.transform.position.x + 1 || PlayerPrefs.GetString("P2Death") == "true") {//P1 is further
				distanceToMove = player.transform.position.x - lastPlayerPosition.x;
				lastPlayerPosition = player.transform.position;
				lastPlayer2Position = player2.transform.position;
			} else {
				distanceToMove = player2.transform.position.x - lastPlayer2Position.x;
				lastPlayer2Position = player2.transform.position;
				lastPlayerPosition = player.transform.position;
			}
		}
		gameObject.transform.position += new Vector3 (distanceToMove - 0.02f,0,0);
		if (gameObject.transform.position.x <= BGEnd.transform.position.x) {
			gameObject.transform.position = new Vector3 (BGBegin.transform.position.x, BGBegin.transform.position.y, gameObject.transform.position.z);
		}
	}
}
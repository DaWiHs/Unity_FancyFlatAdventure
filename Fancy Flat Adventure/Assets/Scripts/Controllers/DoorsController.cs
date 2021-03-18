using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour {

	bool entr;
	bool activated;
	GameObject exit;
	float distanceToMoveH;
	float distanceToMoveV;
	GameObject player;
	GameObject player2;
	GameObject gameManager;


	void Start () {
		if (gameObject.name == "Entrance") {
			entr = true;
			activated = false;
		}
		player = GameObject.Find ("Player");
		player2 = GameObject.Find ("Player2");
		exit = GameObject.Find ("Exit");
		gameManager = GameObject.FindWithTag ("gameManager");
		if (entr == true) {
			distanceToMoveH = gameObject.transform.position.x - exit.transform.position.x;
			distanceToMoveV = gameObject.transform.position.y - exit.transform.position.y;
		}
	}

	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 50);

			gameManager.SendMessage("Freeze1");
			if (activated == false) {
				SendMessage ("Delay");
				activated = true;
			}
		}

		if (other.CompareTag ("Player2")) {
			gameManager.SendMessage ("Freeze2");
			player2.transform.position = new Vector3 (player2.transform.position.x, player2.transform.position.y, player2.transform.position.z + 50);
			if (activated == false) {
				SendMessage ("Delay");
				activated = true;
			}
		}
	}

	IEnumerator Delay () {
		yield return new WaitForSecondsRealtime (2);
		player.transform.position = new Vector3 (player.transform.position.x - distanceToMoveH, player.transform.position.y - distanceToMoveV, player.transform.position.z);
		if (PlayerPrefs.GetString ("2PMode") == "true" || PlayerPrefs.GetString ("2PMode") == "True") {
			player2.transform.position = new Vector3 (player2.transform.position.x - distanceToMoveH, player2.transform.position.y - distanceToMoveV, player2.transform.position.z);
		}
		yield return new WaitForSecondsRealtime (1);

			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 50);

		if (PlayerPrefs.GetString ("2PMode") == "true" || PlayerPrefs.GetString ("2PMode") == "True") {
			player2.transform.position = new Vector3 (player2.transform.position.x, player2.transform.position.y, player2.transform.position.z - 50);
		}
		gameManager.SendMessage("Unfreeze1");
		gameManager.SendMessage("Unfreeze2");
	}

}

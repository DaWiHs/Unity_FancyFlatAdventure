using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvirometFunction : MonoBehaviour {

	public bool[] Function;
	//0: Boulder 1: ObstacleShowUp
	public GameObject[] objects;
	//0: Boulder

	public bool activeOnEnter;
	public float operateObjMove;
	public bool operateToVertical;

	GameObject gameManager;
	GameObject player;
	GameObject player2;
	GameObject spawnPoint;
	int integrer;
	bool operate;

	void Start () {
		
		gameManager = GameObject.FindWithTag ("gameManager");
		player = GameObject.FindWithTag ("Player");
		player2 = GameObject.FindWithTag ("Player2");
		integrer = 0;
		operate = false;

		if (Function [0] == true) {
			SendMessage ("BoulderFunction");
			spawnPoint = GameObject.Find ("boulder_generator");
		}


	}

	void Update () {
		if (integrer <= 30 && operate == true) {
			if (operateToVertical == true) {
				objects [1].transform.position = new Vector3 (objects [1].transform.position.x, objects [1].transform.position.y + operateObjMove / 30f, objects [1].transform.position.z);
			} else {	
				objects [1].transform.position = new Vector3 (objects [1].transform.position.x + operateObjMove / 30f, objects [1].transform.position.y, objects [1].transform.position.z);
			}
			integrer += 1;
		}
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (activeOnEnter == true && Function [1] == true) {
			if (other.CompareTag("Player")) {
				operate = true;
			}
		}
	}

	void Active () {
	}



	IEnumerator BoulderFunction () {
		yield return new WaitForSecondsRealtime (0.75f);
		Instantiate (objects [0], spawnPoint.transform);
		yield return new WaitForSecondsRealtime (2.25f);
		SendMessage ("BoulderFunction");
	}
}
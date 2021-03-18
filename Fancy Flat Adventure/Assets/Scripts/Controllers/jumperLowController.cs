using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumperLowController : MonoBehaviour {

	public Sprite JumLActive;
	public Sprite JumLDeActive;
	public SpriteRenderer mySpriteComponent;
	public GameObject gameManager;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("gameManager");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			gameManager.SendMessage("JumperLow");
			SendMessage ("onActivation");
		}
	}

	IEnumerator onActivation () {
		mySpriteComponent.sprite = JumLActive;
		yield return new WaitForSecondsRealtime (1);
		mySpriteComponent.sprite = JumLDeActive;
	}
}

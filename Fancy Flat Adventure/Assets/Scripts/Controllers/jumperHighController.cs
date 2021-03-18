using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumperHighController : MonoBehaviour {

	public Sprite JumHActive;
	public Sprite JumHDeActive;
	public SpriteRenderer mySpriteComponent;
	public GameObject gameManager;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("gameManager");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player")) {
			gameManager.SendMessage("JumperHigh");
			SendMessage ("onActivation");
		}
	}

	IEnumerator onActivation () {
		mySpriteComponent.sprite = JumHActive;
		yield return new WaitForSecondsRealtime (1);
		mySpriteComponent.sprite = JumHDeActive;
	}
}

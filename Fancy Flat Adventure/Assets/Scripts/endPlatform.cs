using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPlatform : MonoBehaviour {

	SpriteRenderer spriteRen;
	public float selectedColor;
	public Sprite endSprite1;
	public Sprite endSprite2;
	public Sprite endSprite3;
	public Sprite endSprite4;

	void Start () {
		selectedColor = PlayerPrefs.GetFloat ("PickedColor");
		spriteRen = GetComponent<SpriteRenderer> ();
		SendMessage ("Delay");
	}
	IEnumerator Delay () {
		yield return new WaitForSecondsRealtime (Random.Range(0.5f,1.5f));
		selectedColor = Random.Range(0.5f,4.5f);
		if (selectedColor < 1.5f) {
			spriteRen.sprite = endSprite1;
		} else if (selectedColor < 2.5f ) {
			spriteRen.sprite = endSprite2;
		} else if (selectedColor < 3.5f) {
			spriteRen.sprite = endSprite3;
		} else {
			spriteRen.sprite = endSprite4;
		}
		SendMessage ("Delay");
	}
}

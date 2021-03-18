using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

	SpriteRenderer spriteRender;
	float i;
	bool fading;
	Color hieroglify;


	void Start () {
		spriteRender = gameObject.GetComponent<SpriteRenderer> ();
		hieroglify = new Color (0.95f, 0.95f, 0.95f,0 );
		spriteRender.color = hieroglify;
		fading = false;
	}
	void Update () {
		hieroglify = new Color (0.95f, 0.95f,0.95f, i/255);
		if (fading == false && i < 255) {
			i = i + 2;
		}
		if (i >= 255) {
			SendMessage ("FadeIn");
		}
		if (fading == true && i > 0) {
			i = i - 2;
		}
		spriteRender.color = hieroglify;
	}
	IEnumerator FadeIn () {
		fading = true;
		yield return new WaitForSecondsRealtime (4);
		fading = false;
	}
}

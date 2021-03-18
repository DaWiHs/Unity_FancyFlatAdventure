using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour {
	
	GameObject destroyPoint;

	// Use this for initialization
	void Start () {
		destroyPoint = GameObject.Find ("PlatformGeneratorEnd");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x < destroyPoint.transform.position.x) {
			Destroy (gameObject);
		}
	}
}

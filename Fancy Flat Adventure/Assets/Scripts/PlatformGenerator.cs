using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

	GameObject thePlatform;
	GameObject theCoin;
	int selectedPlatform;
	int currTerrain;
	//int currArray;
	string array;
	float verticalPosition;
	float verticalPositionRandom;
	float coinSelector;
	bool coinToGenerate;
	float randomCoinPosV;
	float randomCoinPosH;
	Vector3 coinPos;
	Vector3 p2coinPos;

	public GameObject[] coins;
	public GameObject[] platformsT1;
	public GameObject[] platformsT2;
	public GameObject[] startPlatforms;
	public GameObject[] startPlatforms2P;

	Transform generationPoint;
	public float distanceBetween;
	public float spaceBetweenMin;
	public float spaceBetweenMax;
	public float verticalPositionMin;
	public float verticalPositionMax;

	private float platformWidth;

	void Start () {
		generationPoint = GameObject.Find ("PlatformGeneratorBegin").GetComponent<Transform> ();
		currTerrain = PlayerPrefs.GetInt ("CurrentTerrain");
		startPlatforms [currTerrain - 1].SetActive (true);

		if (PlayerPrefs.GetString ("2PMode") == "True") {
			startPlatforms2P [currTerrain - 1].SetActive (true);
		}

		if (currTerrain == 1) {
			thePlatform = platformsT1 [7];
			//currArray = 1;
		}
		if (currTerrain == 2) {
			thePlatform = platformsT2 [9];
			//currArray = 2;				
		}
	}

	void Update () {
		platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
		if (transform.position.x < generationPoint.position.x) {

			// Defining platform to generate

			//For Terrain1
			if (currTerrain == 1) {
				selectedPlatform = Mathf.RoundToInt (Random.Range (0, platformsT1.Length));
				if (selectedPlatform <= 2) {
					thePlatform = platformsT1 [selectedPlatform];
					verticalPosition = -0.5f;
				} else if (selectedPlatform <= 9) {
					thePlatform = platformsT1 [selectedPlatform];
					verticalPosition = 0;
				}
				if ( selectedPlatform >= 9) {
					thePlatform = platformsT1[selectedPlatform];
					verticalPosition = 1;
				}
			}

			//For Terrain2
			if (currTerrain == 2) {
				selectedPlatform = Mathf.RoundToInt (Random.Range (0, platformsT2.Length));
				if (selectedPlatform <= 5) {
					thePlatform = platformsT2 [selectedPlatform];
					verticalPosition = -0.5f;
				} else if (selectedPlatform <= 11) {
					thePlatform = platformsT2 [selectedPlatform];
					verticalPosition = 0;
				}
				if ( selectedPlatform >= 12) {
					thePlatform = platformsT2[selectedPlatform];
					verticalPosition = 1;
				}
			}

			// Defining platform ranges
			verticalPositionRandom = Random.Range (verticalPositionMin, verticalPositionMax);
			distanceBetween = Random.Range (spaceBetweenMin, spaceBetweenMax);

			// Defining Coin
			coinSelector = Random.Range(0f, 10f);
			if (coinSelector >= 9.5f) {
				theCoin = coins[2];
			} else if (coinSelector >= 8.5f) {
				theCoin = coins[1];
			} else {
				theCoin = coins[0];
			}
			if (coinSelector >= 7f) {
				coinToGenerate = true;
			} else {
				coinToGenerate = false;
			}

			// Generating
			if (transform.position.y + verticalPosition + verticalPositionRandom < -3) {
				transform.position = new Vector3 (transform.position.x + platformWidth + distanceBetween, -3, transform.position.z);
			} else if (transform.position.y + verticalPosition + verticalPositionRandom > 3) {
				transform.position = new Vector3 (transform.position.x + platformWidth + distanceBetween, 3, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x + platformWidth + distanceBetween, transform.position.y + verticalPosition + verticalPositionRandom, transform.position.z);
			}

			randomCoinPosH = Random.Range (-1f, 1f);
			randomCoinPosV = Random.Range (1f, 1.5f);
			coinPos = new Vector3 (transform.position.x + randomCoinPosH, transform.position.y + verticalPosition + randomCoinPosV, transform.position.z);

			Instantiate (thePlatform, transform.position, transform.rotation);

			if (PlayerPrefs.GetString("2PMode") == "True") { 
				p2coinPos = new Vector3 (transform.position.x + randomCoinPosH, transform.position.y + verticalPosition + randomCoinPosV - 20, transform.position.z);
				Instantiate (theCoin, p2coinPos, transform.rotation);
				Instantiate (thePlatform, new Vector3 (transform.position.x , transform.position.y -20 , transform.position.z), transform.rotation); 
			}
			if (coinToGenerate == true) { Instantiate (theCoin, coinPos, transform.rotation); }
		}
	}
}

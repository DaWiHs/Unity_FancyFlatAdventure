using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System.Timers;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
	
	public float jumpSpeed;
	public float runSpeed;
	private Vector3 lastPlayerPosition;
	private Vector3 lastPlayer2Position;
	private float distanceToMove;
	public float dashLenght;
	public float jetLenght;
	public float pickedColor;
	public GameObject pauseObject;
	public Sprite[] flags;
	public Sprite[] flagWave;
	public GameObject[] loadLevel;
	public GameObject[] loadLevel2P;
	public GameObject[] loadBackground;
	public GameObject[] loadLevelWalkthrough;
	public AudioClip[] loadAudio;

	AudioSource audioSource;
	SpriteRenderer endFlagSpriteRen;
	GameObject player;
	GameObject player2;
	GameObject mainCamera;
	GameObject onDeathScreen;
	GameObject onCompleteScreen;
	GameObject loadMap;
	GameObject walkBlockade;
	float multiplerScore;
	Text completeScore;
	Text completeHiScore;
	Text completeAvaibleCoins;
	Text deathHiScore;
	Text textScore;
	Text deathScore;
	Text deathCoins;
	Text passedLvl;
	int coinsAvaible;
	int selectedFlag;
	int endCoinScore;
	bool doubleJumpActive;
	bool dashActive;
	bool jetBootsActive;
	bool frozen1;
	bool frozen2;
	bool switchedSide;
	Vector2 pausedPlayerVelocity;
	float endScore;
	float p2EndScore;
	int positionScore;
	int p2PositionScore;
	int coinScore;
	int p2coinScore;
	public int speedUpScore;
	float overallScore;
	RigidbodyConstraints2D originalConstraits;
	bool p2Mode;
	bool death;
	bool p1Death;
	public static bool p2Death;
	bool paused;
	bool grounded;
	bool p2Grounded;
	bool doublejumped;
	bool p2DoubleJumped;
	bool dashed;
	bool jeted;
	bool dashing;
	bool jeting;
	Rigidbody2D playerRigidbody2D;
	Rigidbody2D player2Rigidbody2D;
	Animator player1Animator;
	Animator player2Animator;
	GameObject buttonsController;

	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.Find ("Player");
		player2 = GameObject.Find ("Player2");
		player2.SetActive (false);
		//PlayerPrefs.SetString ("WalkActive", "False");
		if (PlayerPrefs.GetInt ("CurrentLevel") != 4) {
			walkBlockade = GameObject.Find ("WalkObject");
			walkBlockade.SetActive (false);
		}
		playerRigidbody2D = player.GetComponent<Rigidbody2D> ();
		player2Rigidbody2D = player2.GetComponent<Rigidbody2D> ();
		player1Animator = player.GetComponent<Animator> ();
		player2Animator = player2.GetComponent<Animator> ();
		lastPlayerPosition = player.transform.position;
		lastPlayer2Position = player2.transform.position;
		originalConstraits = playerRigidbody2D.constraints;
		multiplerScore = menuManager.multipler;
		buttonsController = GameObject.Find ("ButtonsController");
		pauseObject = GameObject.Find ("Pause");
		pauseObject.SetActive (false);
		audioSource = GameObject.Find ("AudioSource").GetComponent<AudioSource> ();
		p2Mode = false;
		frozen1 = false;
		frozen2 = false;
		if (PlayerPrefs.GetString ("2PMode") == "True") {
			p2Mode = true;
			p2Death = false;
			player2.SetActive (true);
			Debug.Log ("Active 2P Mode");
		}

		speedUpScore = 500;

		onDeathScreen = GameObject.FindWithTag ("DeathScreen");
		deathHiScore = GameObject.Find ("DSTextHiScore").GetComponent<Text> ();
		textScore = GameObject.Find ("TextScore").GetComponent<Text> ();
		deathScore = GameObject.Find ("DSTextEndScore").GetComponent<Text> ();
		deathCoins = GameObject.Find ("DSTextEndCoins").GetComponent<Text> ();
		onDeathScreen.SetActive (false);

		onCompleteScreen = GameObject.FindWithTag ("CompleteScreen");
		passedLvl = GameObject.Find ("CSPassedLevel").GetComponent<Text> ();
		completeScore = GameObject.Find ("CSTotalScore").GetComponent<Text> ();
		completeHiScore = GameObject.Find ("CSHiScore").GetComponent<Text> ();
		completeAvaibleCoins = GameObject.Find ("CSAvaibleCoins").GetComponent<Text> ();
		onCompleteScreen.SetActive (false);

		audioSource.clip = loadAudio [PlayerPrefs.GetInt ("CurrentTerrain") - 1];
		audioSource.Play ();
		loadBackground [PlayerPrefs.GetInt ("CurrentTerrain") - 1].SetActive (true);

		if (PlayerPrefs.GetInt ("CurrentLevel") < 4) {
			endFlagSpriteRen = GameObject.Find ("FlagEnd").GetComponent<SpriteRenderer> ();
			loadLevel [(PlayerPrefs.GetInt("CurrentTerrain") -1) * 3 + PlayerPrefs.GetInt("CurrentLevel") -1 ].SetActive (true);
			if (PlayerPrefs.GetString("WalkActive") == "True") {
				walkBlockade.SetActive (true);
				loadLevelWalkthrough [(PlayerPrefs.GetInt ("CurrentTerrain") - 1) * 3 + PlayerPrefs.GetInt ("CurrentLevel") - 1].SetActive (true);
			}
			if (p2Mode == true) {
				loadLevel2P [(PlayerPrefs.GetInt ("CurrentTerrain") - 1) * 3 + PlayerPrefs.GetInt ("CurrentLevel") - 1].SetActive (true);
			}
			Debug.Log ((PlayerPrefs.GetInt ("CurrentTerrain") - 1) * 3 + PlayerPrefs.GetInt ("CurrentLevel") -1 );
		}

		//Bought things
		if (PlayerPrefs.GetString ("DoubleJump") == "True") {
			doubleJumpActive = true;
		} else {
			doubleJumpActive = false;
		}
		if (PlayerPrefs.GetString ("Dash") == "True") {
			dashActive = true;
		} else {
			dashActive = false;
		}
		if (PlayerPrefs.GetString ("JetBoots") == "True") {
			jetBootsActive = true;
		} else {
			jetBootsActive = false;
		}
		Debug.Log ("DblJump Active" + PlayerPrefs.GetString ("DoubleJump"));
		Debug.Log ("Dash Active" + PlayerPrefs.GetString ("Dash"));
		Debug.Log ("JetBoots Active" + PlayerPrefs.GetString ("JetBoots"));

		if (PlayerPrefs.GetInt ("CurrentLevel") != 4) {
			//End Platform Colors
			pickedColor = Random.Range (0.6f, 4.4f);
			selectedFlag = Mathf.RoundToInt (Random.Range (0, 3));
			PlayerPrefs.SetFloat ("PickedColor", pickedColor);
			endFlagSpriteRen.sprite = flags [selectedFlag];
		}
		if (PlayerPrefs.GetInt ("CurrentLevel") == 4) {
			runSpeed = runSpeed + ((PlayerPrefs.GetInt ("CurrentTerrain") - 1) / 5f);
			SendMessage ("SpeedingUp");
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			SendMessage("CheckVariables");
		}
		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			walkBlockade.SetActive(false);
		}

		if (grounded) {
			doublejumped = false;
			dashed = false;
			jeted = false;
		}
		if (p2Grounded) {
			p2DoubleJumped = false;
		}
		//Player Run
		if (paused == false && death == false && p1Death == false && frozen1 == false) {
			if (dashing == true) {
				playerRigidbody2D.velocity = new Vector2 (3 * runSpeed, playerRigidbody2D.velocity.y);
				playerRigidbody2D.constraints = originalConstraits;
			} else if (jeting == true) {
				playerRigidbody2D.velocity = new Vector2 (1.5f * runSpeed, playerRigidbody2D.velocity.y);
				playerRigidbody2D.constraints = originalConstraits;
			} else {
				playerRigidbody2D.velocity = new Vector2 (1 * runSpeed, playerRigidbody2D.velocity.y);
				playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; 
			}
		} else { 
				playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		//Player2 Run
		if (paused == false && death == false && p2Death == false && frozen2 == false) {
				player2Rigidbody2D.velocity = new Vector2 (1 * runSpeed, player2Rigidbody2D.velocity.y);
				player2Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation; 
		} else { 
			player2Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		//Player1 Movement
		//		Jump
		if (Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.JoystickButton5)) { //High
			if (grounded) {
				playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 1 * jumpSpeed);
			}
			//		Double Jump
			if (grounded == false && doubleJumpActive && doublejumped == false) { 
				playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 1 * jumpSpeed);
				doublejumped = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.JoystickButton4)){ //Low
				if (grounded) {
					playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.6f * jumpSpeed);
				}
				if (grounded == false && doubleJumpActive && doublejumped == false) { 
					playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.6f * jumpSpeed);
					doublejumped = true;
				}
			}
		if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.JoystickButton2)){ //Mid
				if (grounded) {
					playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.8f * jumpSpeed);
				}
				if (grounded == false && doubleJumpActive && doublejumped == false) { 
					playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.8f * jumpSpeed);
					doublejumped = true;
				}
			}
		
		//		Dash
		if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
			if (dashActive == true && dashed == false && jeting == false && dashing == false && grounded == false && p2Mode == false) {
				SendMessage ("Dash");
				dashed = true;
				Debug.Log ("Dash");
			}
		}
		//		Jet Boots
		if (Input.GetKeyDown (KeyCode.T) || Input.GetKeyDown(KeyCode.JoystickButton3)) {
			if (jetBootsActive == true && jeted == false && jeting == false && dashing == false && grounded == false && p2Mode == false) {
				SendMessage ("Jets");
				jeted = true;
				Debug.Log ("JetBoots");
			}
		}
		//Player2 Movement
		//		Jump																											
		if (Input.GetKeyDown (KeyCode.Alpha0)) { //High
			if (p2Grounded) {
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 1 * jumpSpeed);
			}
			//		Double Jump
			if (p2Grounded == false && doubleJumpActive && p2DoubleJumped == false) { 
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 1 * jumpSpeed);
				p2DoubleJumped = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)){ //Low
			if (p2Grounded) {
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 0.6f * jumpSpeed);
			}
			if (p2Grounded == false && doubleJumpActive && p2DoubleJumped == false) { 
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 0.6f * jumpSpeed);
				p2DoubleJumped = true;
			}
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)){ //Mid
			if (p2Grounded) {
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 0.8f * jumpSpeed);
			}
			if (p2Grounded == false && doubleJumpActive && p2DoubleJumped == false) { 
				player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, 0.8f * jumpSpeed);
				p2DoubleJumped = true;
			}
		}
		//Escape Control
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton8)){
			if (death == false) {
				if (paused == true) {
					SendMessage ("Resumed");
				} else {
					SendMessage ("Paused");
				}
			}
			if (death == true) {
				SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
			}
		}
		//Return Control
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.JoystickButton9)) {
			if (paused == true && onCompleteScreen.activeSelf == false) {
				SendMessage ("Resumed");
			}
			if (paused == true && onCompleteScreen.activeSelf == true) {
				buttonsController.SendMessage ("NextLevel");
			}
			if (death == true) {
				SceneManager.LoadScene (PlayerPrefs.GetString ("LoadedScene"), LoadSceneMode.Single);
			}
		}


		if (Input.GetKeyDown (KeyCode.Keypad4)) {
			if (doubleJumpActive == false) {
				doubleJumpActive = true;
			} else {
				doubleJumpActive = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad5)) {
			if (dashActive == false) {
				dashActive = true;
			} else {
				dashActive = false;
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad6)) {
			if (jetBootsActive == false) {
				jetBootsActive = true;
			} else {
				jetBootsActive = false;
			}
		}
		//Player Animation
		player1Animator.SetFloat ("Speed", playerRigidbody2D.velocity.x);
		player1Animator.SetBool ("Grounded", grounded);
		if (p2Mode == true) {
			player2Animator.SetFloat ("Speed", player2Rigidbody2D.velocity.x);
			player2Animator.SetBool ("Grounded", p2Grounded);
		}

		//Camera Controller
		if (p2Mode == false) {
			distanceToMove = player.transform.position.x - lastPlayerPosition.x;
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + distanceToMove, mainCamera.transform.position.y, mainCamera.transform.position.z);
			lastPlayerPosition = player.transform.position;
		} else {
			if (player.transform.position.x >= player2.transform.position.x + 1 || p2Death == true) {//P1 is further
				distanceToMove = player.transform.position.x - lastPlayerPosition.x;
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + distanceToMove, mainCamera.transform.position.y, mainCamera.transform.position.z);
				lastPlayerPosition = player.transform.position;
				lastPlayer2Position = player2.transform.position;
			} else {
				distanceToMove = player2.transform.position.x - lastPlayer2Position.x;
				mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + distanceToMove, mainCamera.transform.position.y, mainCamera.transform.position.z);
				lastPlayer2Position = player2.transform.position;
				lastPlayerPosition = player.transform.position;

			}


		}

		//Scoring
		if (PlayerPrefs.GetString ("WalkActive") == "False") {
			positionScore = Mathf.RoundToInt (player.transform.position.x);
			p2PositionScore = Mathf.RoundToInt (player2.transform.position.x - 1);
			if (p2Mode == true) {
				if (coinScore * 10 + positionScore > p2coinScore * 10 + p2PositionScore) { //P1 Has more points
					overallScore = coinScore * 10 + positionScore;
					textScore.text = "Score: " + overallScore;
				} else {
					overallScore = p2coinScore * 10 + p2PositionScore;
					textScore.text = "Score: " + overallScore;
				}
			} else {
				overallScore = coinScore * 10 + positionScore;
				textScore.text = "Score: " + overallScore;
			}
		} else {
			positionScore = 0;
			overallScore = 0;
			coinScore = 0;
		}
	}
	void P1FootControlT() {
		grounded = true;
		jeting = false;
		dashing = false;
	}
	void P1FootControlF() {
		grounded = false;
	}
	void P2FootControlT() {
		p2Grounded = true;
	}
	void P2FootControlF() {
		p2Grounded = false;
	}
	void CheckVariables () {
		Debug.Log ("CoinScore: " + coinScore);
		Debug.Log ("PlayerPrefs: " + PlayerPrefs.GetInt("AvaibleCoins"));
		Debug.Log ("EndCoinScore: " + endCoinScore);
		if (PlayerPrefs.GetString ("DoubleJump") == "true") {
			Debug.Log ("DoubleJump");
		}
	}

	void JumperHigh () { //Jumper High Controller
		playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, jumpSpeed * 3 / 2);
	}
	void JumperLow () { //Jumper Low Controller
		playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, jumpSpeed * 3 / 4);
	}
	void P2JumperHigh () { //P2 Jumper High Controller
		player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, jumpSpeed * 3 / 2);
	}
	void P2JumperLow () { //P2 Jumper Low Controller
		player2Rigidbody2D.velocity = new Vector2 (player2Rigidbody2D.velocity.x, jumpSpeed * 3 / 4);
	}
	void LvlComplete () {
		SendMessage ("FlagWave");
		paused = true;
		onCompleteScreen.SetActive (true);


		endScore = overallScore * multiplerScore;
		if (coinScore > p2coinScore) {
			endCoinScore = Mathf.RoundToInt (coinScore * multiplerScore);
		} else {
			endCoinScore = Mathf.RoundToInt (p2coinScore * multiplerScore);
		}
		completeAvaibleCoins.text = "Coins: " + (PlayerPrefs.GetInt("AvaibleCoins") + endCoinScore) +" (+ " + endCoinScore + " )";
		completeScore.text = "Total Score: \n" + Mathf.RoundToInt (endScore);
		passedLvl.text = "You passed \nTerrain " + PlayerPrefs.GetInt ("CurrentTerrain") + " Level " + PlayerPrefs.GetInt ("CurrentLevel") + "!";

		if (PlayerPrefs.HasKey ("AvaibleCoins")) {
			PlayerPrefs.SetInt("AvaibleCoins", PlayerPrefs.GetInt("AvaibleCoins") + endCoinScore);
			PlayerPrefs.Save();
		} else {
			PlayerPrefs.SetInt("AvaibleCoins", endCoinScore);
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.HasKey(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + ".HiScore")) {
			if (PlayerPrefs.GetInt(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + ".HiScore") < endScore) {
				PlayerPrefs.SetInt(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + ".HiScore", Mathf.RoundToInt(endScore));
				PlayerPrefs.Save ();
				completeHiScore.text = "HiScore: \n" + Mathf.RoundToInt(endScore);
			} else {
				completeHiScore.text = "HiScore: \n" + PlayerPrefs.GetInt(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + ".HiScore");
				PlayerPrefs.Save ();
			}
		} else {
			PlayerPrefs.SetInt(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + ".HiScore", Mathf.RoundToInt(endScore));
			PlayerPrefs.Save ();
			completeHiScore.text = "HiScore: \n" + Mathf.RoundToInt(endScore);
		}
	}
	void Freeze1 () {
		frozen1 = true;
	}
	void Unfreeze1 () {
		frozen1 = false;
	}
	void Freeze2 () {
		frozen2 = true;
	}
	void Unfreeze2 () {
		frozen2 = false;
	}
	void Paused () { //Pause Controller
		pausedPlayerVelocity = playerRigidbody2D.velocity;
		pauseObject.SetActive(true);
		paused = true;
		Debug.Log ("Paused");
	}
	void Resumed () { //Pause Resume
		Debug.Log ("Resumed");
		pauseObject.SetActive (false);
		paused = false;
		playerRigidbody2D.velocity = pausedPlayerVelocity;
		if (p2Mode == true) {
			player2Rigidbody2D.velocity = pausedPlayerVelocity;
		}
	}
	void P1Death () {
		p1Death = true;
		PlayerPrefs.SetString ("P1Death", "" + p1Death);
		if (p2Death == true) {
			SendMessage ("Death");

		}
	}
	void P2Death () {
		p2Death = true;
		PlayerPrefs.SetString ("P2Death", "" + p2Death);
		if (p1Death == true) {
			SendMessage ("Death");
		}
	}
	void Death () { //Player Death
		endScore = overallScore * multiplerScore;
		if (p2Mode == true && p2coinScore > coinScore) { //P2 has more coins
			endCoinScore = Mathf.RoundToInt (p2coinScore * multiplerScore);
		} else {
			endCoinScore = Mathf.RoundToInt (coinScore * multiplerScore);
		}
		deathCoins.text = "Coins: " + endCoinScore;
		deathScore.text = "Total Score: " + Mathf.RoundToInt (endScore);
		if (Mathf.RoundToInt (endScore) > PlayerPrefs.GetInt(PlayerPrefs.GetInt("CurrentTerrain") + "." + PlayerPrefs.GetInt("CurrentLevel") + "HiScore")) {
			PlayerPrefs.SetInt (PlayerPrefs.GetInt ("CurrentTerrain") + "." + PlayerPrefs.GetInt ("CurrentLevel") + "HiScore", Mathf.RoundToInt (endScore));
			PlayerPrefs.Save ();
			deathHiScore.text = "HiScore: " + Mathf.RoundToInt (endScore);
		} else {
			deathHiScore.text = "HiScore: " + PlayerPrefs.GetInt (PlayerPrefs.GetInt ("CurrentTerrain") + "." + PlayerPrefs.GetInt ("CurrentLevel") + "HiScore");
		}
		death = true;
		player1Animator.SetBool ("Death", death);
		player2Animator.SetBool ("Death", death);
		onDeathScreen.SetActive (true);
		Debug.Log ("Deathhhhh");
		if (PlayerPrefs.HasKey ("AvaibleCoins")) {
			PlayerPrefs.SetInt("AvaibleCoins", PlayerPrefs.GetInt("AvaibleCoins") + endCoinScore);
			PlayerPrefs.Save();
		} else {
			PlayerPrefs.SetInt("AvaibleCoins", endCoinScore);
			PlayerPrefs.Save ();
		}

	}
	IEnumerator Dash () {
		playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
		dashing = true;
		yield return new WaitForSecondsRealtime (dashLenght);
		dashing = false;
		playerRigidbody2D.constraints = originalConstraits;
	}
	IEnumerator Jets () {
		playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
		jeting = true;
		yield return new WaitForSecondsRealtime (jetLenght);
		jeting = false;
		playerRigidbody2D.constraints = originalConstraits;
	}
	IEnumerator FlagWave () {
		endFlagSpriteRen.sprite = flagWave [selectedFlag];
		yield return new WaitForSecondsRealtime (Random.Range(0.1f,0.5f));
		endFlagSpriteRen.sprite = flagWave [selectedFlag + 4];
		yield return new WaitForSecondsRealtime (Random.Range(0.1f,0.5f));
		SendMessage("FlagWave");
	}
	IEnumerator SpeedingUp () {
		yield return new WaitForSecondsRealtime (1);
		if (speedUpScore < overallScore) {
			speedUpScore += Mathf.RoundToInt(runSpeed) * 2;
			runSpeed += PlayerPrefs.GetInt ("CurrentTerrain") / 50f;
		}
		SendMessage ("SpeedingUp");
	}
	// Coins PickUp
	void CoinPickup1 () { //Coin 1
		coinScore = coinScore + 1;
	}
	void CoinPickup2 () { //Coin 2
		coinScore = coinScore + 2;
	}
	void CoinPickup5 () { //Coin 5
		coinScore = coinScore + 5;
	}
	void P2CoinPickup1 () { //Coin 1
		p2coinScore = p2coinScore + 1;
	}
	void P2CoinPickup2 () { //Coin 2
		p2coinScore = p2coinScore + 2;
	}
	void P2CoinPickup5 () { //Coin 5
		p2coinScore = p2coinScore + 5;
	}

	// Jumps
	void JumpLow () {
		if (grounded) {
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.6f * jumpSpeed);
		}
		if (grounded == false && doubleJumpActive && doublejumped == false) { 
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.6f * jumpSpeed);
			doublejumped = true;
		}
	}
	void JumpMid () {
		if (grounded) {
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.8f * jumpSpeed);
		}
		if (grounded == false && doubleJumpActive && doublejumped == false) { 
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.8f * jumpSpeed);
			doublejumped = true;
		}
	}
	void JumpHigh () {
		if (grounded) {
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 1 * jumpSpeed);
		}
	}

	// Auto Jumps
	void autoJumpLow () { //Low
			if (grounded) {
				playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.6f * jumpSpeed);
			}
	}
	void autoJumpMid () { //Mid
			if (grounded) {
				playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 0.8f * jumpSpeed);
			}
	}
	void autoJumpHigh () { //High
		if (grounded) {
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 1 * jumpSpeed);
		}
		if (grounded == false && doubleJumpActive && doublejumped == false) { 
			playerRigidbody2D.velocity = new Vector2 (playerRigidbody2D.velocity.x, 1 * jumpSpeed);
			doublejumped = true;
		}
	}
	// Power Ups
	void ReverseGravity () {
		jumpSpeed *= -1;
	}
	void SpeedUp () {
		runSpeed += 3;
	}
	void SlowDown () {
		runSpeed -= 3;
	}
	void SwitchSide () {
		runSpeed *= -1;
		player.transform.localScale = new Vector3 (player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
		if (switchedSide == false) {
			switchedSide = true;
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x - 12, mainCamera.transform.position.y, mainCamera.transform.position.z);
		} else {
			switchedSide = false;
			mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x + 12, mainCamera.transform.position.y, mainCamera.transform.position.z);
		}
	}
}

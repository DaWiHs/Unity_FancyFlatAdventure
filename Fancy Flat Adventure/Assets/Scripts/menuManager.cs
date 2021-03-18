using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour {

	public int doubleJumpCost;
	public int jetBootsCost;
	public int dashCost;
	public int multiplerCost;
	public Text shopDoubleJump;
	public Text shopDash;
	public Text shopJetBoots;
	public Text shopMultipler;
	public Text actualCoins;
	public Button doubleJumpBtn;
	public Button dashBtn;
	public Button jetBootsBtn;
	public Button multiplerBtn;
	public Sprite[] Buttons;


	int lvlPointCurrent;
	int lvlPointAfter;
	int unlockedLvl;
	Transform lvlPointAfterTransf;
	Transform lvlPointCurrentTransf;
	Vector3 movingCharacter;
	GameObject mapCanvas;
	GameObject menuCanvas;
	GameObject shopCanvas;
	GameObject h2PCanvas;
	GameObject theCamera;
	GameObject levelSelect;
	GameObject selectBttn;
	GameObject walkBttn;
	GameObject P2ModeBttn;
	bool moving;
	string walkActive;
	string p2ModeActive;
	int i;

	public static bool doubleJumpActive = false;
	public static bool jetBootsActive = false;
	public static bool dashActive = false;
	public static float multipler = 1.05f;
	int coinsAvaible;

	void OnButtonStart () {
		PlayerPrefs.SetString ("DoubleJump", "" + doubleJumpActive);
		PlayerPrefs.SetString ("Dash", "" + dashActive);
		PlayerPrefs.SetString ("JetBoots", "" + jetBootsActive);
		PlayerPrefs.SetFloat ("MultiplerValue", multipler);
		PlayerPrefs.SetInt ("AvaibleCoins", coinsAvaible);
		PlayerPrefs.SetInt ("MultiplerCost", multiplerCost);
		PlayerPrefs.SetInt ("UnlockedLvl", unlockedLvl);
		PlayerPrefs.SetString ("WalkActive", "False");
		PlayerPrefs.SetString ("2PMode", "" + p2ModeActive);
		PlayerPrefs.Save ();

		shopCanvas.SetActive (false);
		mapCanvas.SetActive (true);
		menuCanvas.SetActive (false);
		//SceneManager.LoadScene ("LevelTest", LoadSceneMode.Single);
	}
	void WalkChange () {

		if (walkActive == "True") {
			walkActive = "False";
			Debug.Log("Walk Deactive");
			walkBttn.GetComponent<Image>().sprite = Buttons [5];
		} else {
			walkActive = "True";
			Debug.Log ("Walk Active");
			walkBttn.GetComponent<Image> ().sprite = Buttons [1];
		}
	}
	void P2Mode () {
		PlayerPrefs.SetString ("2PMode", "True");
		if ( p2ModeActive == "True") {
			p2ModeActive = "False";
			Debug.Log("P2Mode Deactive");
			P2ModeBttn.GetComponent<Image>().sprite = Buttons [5];
		} else {
			p2ModeActive = "True";
			Debug.Log ("P2Mode Active");
			P2ModeBttn.GetComponent<Image> ().sprite = Buttons [1];
		}
	}
	void OnButtonShop () {
		mapCanvas.SetActive (false);
		shopCanvas.SetActive (true);
		menuCanvas.SetActive (false);
	}
	void OnButtonShopBack () {
		mapCanvas.SetActive (false);
		shopCanvas.SetActive (false);
		menuCanvas.SetActive (true);
	}
	void OnButtonShopDoubleJump () {
		if (PlayerPrefs.GetString ("DoubleJumpBought") == "False") {
			if (coinsAvaible >= doubleJumpCost) {
				coinsAvaible = coinsAvaible - doubleJumpCost;
				doubleJumpActive = true;
				PlayerPrefs.SetString ("DoubleJumpBought", "True");
				PlayerPrefs.Save ();
			}
		} else if (PlayerPrefs.GetString ("DoubleJumpBought") == "True" ){
			if (doubleJumpActive == true) {
				doubleJumpBtn.GetComponent<Image>().sprite = Buttons [1];
				doubleJumpBtn.GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1f);
				doubleJumpActive = false;
			} else {
				doubleJumpBtn.GetComponent<Image>().sprite = Buttons [0];
				doubleJumpBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
				doubleJumpActive = true;
			}
		}
	}
	void OnButtonShopDash () {
		if (PlayerPrefs.GetString ("DashBought") == "False") {
			if (coinsAvaible >= dashCost) {
				coinsAvaible = coinsAvaible - dashCost;
				dashActive = true;
				PlayerPrefs.SetString ("DashBought", "True");
				PlayerPrefs.Save ();
			}
		} else if (PlayerPrefs.GetString ("DashBought") == "True" ){
			if (dashActive == true) {
				dashBtn.GetComponent<Image>().sprite = Buttons [1];
				dashBtn.GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1f);
				dashActive = false;
			} else {
				dashBtn.GetComponent<Image>().sprite = Buttons [0];
				dashBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
				dashActive = true;
			}
		
		}
	}
	void OnButtonShopJetBoots () {
		if (PlayerPrefs.GetString ("JetBootsBought") == "False") {
			if (coinsAvaible >= jetBootsCost) {
				coinsAvaible = coinsAvaible - jetBootsCost;
				jetBootsActive = true;
				PlayerPrefs.SetString ("JetBootsBought", "True");
				PlayerPrefs.Save ();
			}
		} else if (PlayerPrefs.GetString ("JetBootsBought") == "True" ){
			if (jetBootsActive == true) {
				jetBootsBtn.GetComponent<Image>().sprite = Buttons [1];
				jetBootsBtn.GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1f);
				jetBootsActive = false;
			} else {
				jetBootsBtn.GetComponent<Image>().sprite = Buttons [0];
				jetBootsBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
				jetBootsActive = true;
			}
		}
	}
	void OnButtonShopMultipler () {
		coinsAvaible = coinsAvaible - multiplerCost;
		multipler = multipler + 0.05f;
		multiplerCost = multiplerCost + 50;
	}

	void CheckVariables () {
		Debug.Log ("AvaibleCoins: " + coinsAvaible);
		Debug.Log ("PlayerPrefs Coins: " + PlayerPrefs.GetInt("AvaibleCoins"));
		Debug.Log ("Dbl Jump Boought: " + PlayerPrefs.GetString ("DoubleJumpBought"));
		Debug.Log ("Dash Boought: " + PlayerPrefs.GetString ("DashBought"));
		Debug.Log ("Jet Boots Boought: " + PlayerPrefs.GetString ("JetBootsBought"));
		Debug.Log ("Multipler Value: " + PlayerPrefs.GetFloat ("MultiplerValue"));
		Debug.Log ("Dbl Jump Active: " + doubleJumpActive);
	}
	void OnButtonQuit () {
		//PlayerPrefs.DeleteAll ();
		Application.Quit();
	}
	void MapLvlSelect() {
			mapCanvas.SetActive (true);
			menuCanvas.SetActive (false);
			selectBttn.SetActive (true);
	}
	void How2Play () {
		h2PCanvas.SetActive (true);
		mapCanvas.SetActive (false);
	}
	void Back () {
		if (h2PCanvas.activeSelf == true) {
			h2PCanvas.SetActive (false);
			mapCanvas.SetActive (true);
		} else {
			if (levelSelect.activeSelf == true) {
				levelSelect.SetActive (false);
				selectBttn.SetActive (true);
			} else {
				menuCanvas.SetActive (true);
				mapCanvas.SetActive (false);
				selectBttn.SetActive (false);
			}
		}
	}
	void Select () {
		levelSelect.SetActive (true);
		selectBttn.SetActive (false);
	}
	void Level1 () {
		PlayerPrefs.SetString ("WalkActive", "" + walkActive); 
		PlayerPrefs.SetString ("2PMode", "" + p2ModeActive);
		Debug.Log(PlayerPrefs.GetString ("2PMode"));
		PlayerPrefs.SetInt ("CurrentTerrain", lvlPointCurrent);
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		PlayerPrefs.SetString ("LoadedScene", "LevelAdventure");
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("LevelAdventure", LoadSceneMode.Single);
        
	}
	void Level2 () {
		PlayerPrefs.SetString ("WalkActive", "" + walkActive);
		PlayerPrefs.SetString ("2PMode", "" + p2ModeActive);
		PlayerPrefs.SetInt ("CurrentTerrain", lvlPointCurrent);
		PlayerPrefs.SetInt ("CurrentLevel", 2);
		PlayerPrefs.SetString ("LoadedScene", "LevelAdventure");
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("LevelAdventure", LoadSceneMode.Single);
	}
	void Level3 () {
		PlayerPrefs.SetString ("WalkActive", "" + walkActive);
		PlayerPrefs.SetString ("2PMode", "" + p2ModeActive);
		PlayerPrefs.SetInt ("CurrentTerrain", lvlPointCurrent);
		PlayerPrefs.SetInt ("CurrentLevel", 3);
		PlayerPrefs.SetString ("LoadedScene", "LevelAdventure");
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("LevelAdventure", LoadSceneMode.Single);
	}
	void LevelE () {
		PlayerPrefs.SetInt ("CurrentTerrain", lvlPointCurrent);
		PlayerPrefs.SetString ("2PMode", "" + p2ModeActive);
		PlayerPrefs.SetInt ("CurrentLevel", 4);
		PlayerPrefs.SetString ("LoadedScene", "LevelTest");
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("LevelTest", LoadSceneMode.Single);

	}
	void MapMove () {
		moving = true;
		lvlPointAfterTransf = GameObject.Find ("LvlPoint" + lvlPointAfter).transform;
		movingCharacter = new Vector3 ((lvlPointAfterTransf.position.x - lvlPointCurrentTransf.position.x)/45f, (lvlPointAfterTransf.position.y - lvlPointCurrentTransf.position.y)/45f, 0f);
		i = 0;
	}
	void Start () {
        Debug.Log(SceneManager.GetActiveScene().name.Split('n')[0]);
        Debug.Log(SceneManager.GetActiveScene().name.Split('n')[1]);
        Debug.Log(SceneManager.GetActiveScene().name.Split('n')[2]);
        if (PlayerPrefs.HasKey ("MultiplerValue")) {
			multipler = PlayerPrefs.GetFloat ("MultiplerValue");}
		if (PlayerPrefs.HasKey ("AvaibleCoins")) {
			coinsAvaible = PlayerPrefs.GetInt("AvaibleCoins");}
		if (PlayerPrefs.GetString ("DoubleJumpBought") == "True") {
			doubleJumpBtn.GetComponent<Image> ().sprite = Buttons [0];
			doubleJumpBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			doubleJumpActive = true;
		} else {
			PlayerPrefs.SetString ("DoubleJumpBought", "False");
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.GetString ("DashBought") == "True") {
			dashBtn.GetComponent<Image> ().sprite = Buttons [0];
			dashBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			dashActive = true;
		} else {
			PlayerPrefs.SetString ("DashBought", "False");
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.GetString ("JetBootsBought") == "True") {
			jetBootsBtn.GetComponent<Image> ().sprite = Buttons [0];
			jetBootsBtn.GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			jetBootsActive = true;
		} else {
			PlayerPrefs.SetString ("JetBootsBought", "False");
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.HasKey ("MultiplerCost")) {
			multiplerCost = PlayerPrefs.GetInt ("MultiplerCost");}

		P2ModeBttn = GameObject.Find ("2PModeButton");
		walkBttn = GameObject.Find ("WalkChange");
		mapCanvas = GameObject.Find ("MapCanvas");
		menuCanvas = GameObject.Find ("MenuCanvas");
		shopCanvas = GameObject.Find ("ShopCanvas");
		h2PCanvas = GameObject.Find ("How2PlayCanvas");
		theCamera = GameObject.FindWithTag ("MainCamera");
		levelSelect = GameObject.Find ("LevelSelect");
		selectBttn = GameObject.Find ("Select");
		selectBttn.SetActive (false);
		h2PCanvas.SetActive (false);
		mapCanvas.SetActive (false);
		levelSelect.SetActive (false);
		walkActive = "False";
		p2ModeActive = "False";

		if (PlayerPrefs.GetInt ("UnlockedLvl") > 1) {
			theCamera.transform.position = GameObject.Find ("LvlPoint" + PlayerPrefs.GetInt ("UnlockedLvl")).transform.position;
			unlockedLvl = PlayerPrefs.GetInt("UnlockedLvl");
			lvlPointCurrentTransf = GameObject.Find ("LvlPoint" + unlockedLvl).transform;
			lvlPointCurrent = unlockedLvl;
		} else {
			theCamera.transform.position = GameObject.Find ("LvlPoint1").transform.position;
			lvlPointCurrentTransf = GameObject.Find ("LvlPoint1").transform;
			lvlPointCurrent = 1;
			unlockedLvl = 1;
			PlayerPrefs.SetInt ("UnlockedLvl", 1);
			PlayerPrefs.Save ();
		}
	}
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			SendMessage ("CheckVariables");
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}
			//Buttons Interactables
		
		if (coinsAvaible >= doubleJumpCost || PlayerPrefs.GetString("DoubleJumpBought") == "True") {
			doubleJumpBtn.interactable = true;
		} else {
			doubleJumpBtn.interactable = false;
		}
		if (coinsAvaible >= jetBootsCost || PlayerPrefs.GetString("JetBootsBought") == "True") {
			jetBootsBtn.interactable = true;
		} else {
			jetBootsBtn.interactable = false;
		}
		if (coinsAvaible >= dashCost || PlayerPrefs.GetString("DashBought") == "True") {
			dashBtn.interactable = true;
		} else {
			dashBtn.interactable = false;
		}
		if (coinsAvaible >= multiplerCost) {
			multiplerBtn.interactable = true;
		} else {
			multiplerBtn.interactable = false;
		}

		//Map Moving
		if (Input.GetKeyDown (KeyCode.D) && lvlPointCurrent + 1 <= unlockedLvl && moving == false) {
			lvlPointAfter = lvlPointCurrent + 1;
			Debug.Log ("Next");
			gameObject.SendMessage("MapMove");
		}
		if (Input.GetKeyDown (KeyCode.A) && lvlPointCurrent - 1 > 0 && moving == false) {
			lvlPointAfter = lvlPointCurrent - 1;
			Debug.Log ("Previous");
			gameObject.SendMessage("MapMove");
		}

		if (moving == true) {
			theCamera.transform.position += movingCharacter;
			i++;
			if (i >= 45) {
				moving = false;
				theCamera.transform.position = lvlPointAfterTransf.position;
				lvlPointCurrentTransf = lvlPointAfterTransf;
				lvlPointCurrent = lvlPointAfter;
			}
		}
		if (Input.GetKeyDown (KeyCode.Keypad8)) {
			unlockedLvl += 1;
		}
		if (Input.GetKeyDown (KeyCode.Keypad2)) {
			unlockedLvl -= 1;
		}
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			coinsAvaible += 10;
		}
		if (Input.GetKeyDown(KeyCode.Escape)  || Input.GetKeyDown (KeyCode.JoystickButton8)) {
			gameObject.SendMessage("Back");
		}
		if (Input.GetKeyDown(KeyCode.Return)  || Input.GetKeyDown (KeyCode.JoystickButton9)) {
			if (menuCanvas.activeSelf == false) {
				gameObject.SendMessage ("Select");
			}
			if (menuCanvas.activeSelf == true) {
				gameObject.SendMessage ("OnButtonStart");
				selectBttn.SetActive (true);
			}
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			SendMessage ("P2Mode");
		}
			if (Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.JoystickButton0)) {
				if (mapCanvas.activeSelf == true) {
					gameObject.SendMessage ("Level1");
				} 
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.JoystickButton1)) {
				if (mapCanvas.activeSelf == true) {
					SendMessage ("Level2");
				} 
			}
			if (Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.JoystickButton2)) {
				if (mapCanvas.activeSelf == true) {
					SendMessage ("Level3");
				} 
			}
			if (Input.GetKeyDown (KeyCode.Alpha4) || Input.GetKeyDown (KeyCode.JoystickButton3)) {
				if (mapCanvas.activeSelf == true) {
					SendMessage ("LevelE");
				} 
			}
		shopMultipler.text = "Multipler: " + multipler + "\n" + "(" + multiplerCost + " Coins)";
		shopDoubleJump.text = "Double Jump" + "\n" + "(" + doubleJumpCost + " Coins)";
		shopDash.text = "Dash" + "\n" + "(" + dashCost + " Coins)";
		shopJetBoots.text = "Jet Boots" + "\n" + "(" + jetBootsCost + " Coins)";
		actualCoins.text = "Coins: " + coinsAvaible;
	}

}

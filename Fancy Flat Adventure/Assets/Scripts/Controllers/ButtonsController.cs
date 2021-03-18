using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour {

	public GameObject pauseObject;
	GameObject gameManager;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("gameManager");
	}
	void Pause () {
		pauseObject.SetActive (true);
		gameManager.SendMessage ("Paused");
	}
	void Resume () {
		pauseObject.SetActive (false);
		GameObject.FindWithTag("gameManager").SendMessage("Resumed");
	}
	void Restart () {
		SceneManager.LoadScene (PlayerPrefs.GetString ("LoadedScene"), LoadSceneMode.Single);
	}
	void NextLevel () {
		if (PlayerPrefs.GetInt ("CurrentLevel") < 3) {
			PlayerPrefs.SetInt (PlayerPrefs.GetInt ("CurrentLevel") + "TerrainUnlockedLvl", PlayerPrefs.GetInt ("CurrentLevel"));
			PlayerPrefs.SetInt ("CurrentLevel", PlayerPrefs.GetInt ("CurrentLevel") + 1);
			SceneManager.LoadScene (PlayerPrefs.GetString ("LoadedScene"));
		} else {
			PlayerPrefs.SetInt ("CurrentTerrain", PlayerPrefs.GetInt ("CurrentTerrain") + 1);
			PlayerPrefs.SetInt ("CurrentLevel", 1);
			PlayerPrefs.SetInt ("UnlockedLvl", PlayerPrefs.GetInt ("UnlockedLvl") + 1);
			SceneManager.LoadScene (PlayerPrefs.GetString ("LoadedScene"));
		}
	}
	void MainMenu () {
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}

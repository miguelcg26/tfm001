/* CREADO POR ANTONIO VILLENA */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour {

	public EventSystem ES;
	private GameObject storeSelected;
	public GameObject firstSelected;

	public bool menuPauseActive;
	public bool menuEndActive;

	public GameObject persistentMenu;
	public GameObject playMenu;
	public GameObject pauseMenu;
	public GameObject endMenu;
	public bool lockCursor;

	public bool mouseIsMoving;
	public bool stickIsMoving;

	void Start () {
		//		storeSelected = ES.firstSelectedGameObject;
		//Inicialmente bloqueamos el cursor al cargar el objeto que tenga asignado este script
		Cursor.lockState = CursorLockMode.Locked;
		lockCursor = true;
		persistentMenu = GameObject.Find("PersistentCanvas");
	}

	void Update () {

		if (menuEndActive) {
			persistentMenu.SetActive (false);
			pauseMenu.SetActive (false);
			playMenu.SetActive (false);
			endMenu.SetActive (true);
			ManageButtons ();
		}

		Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
		Cursor.visible = !lockCursor;

		if (SceneController.SC.blockAction) {
			return;
		}

		//Al pulsar la tecla asignada al botón Pause, liberamos el cursor
		if (Input.GetButtonDown ("Pause")) {

			if (Time.timeScale > 0) {
			AudioManager.AM.PlaySound2D ("Rune");
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}

			menuPauseActive = !menuPauseActive;

			lockCursor = true;

			Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
			Cursor.visible = !lockCursor;

			storeSelected = ES.firstSelectedGameObject;

			ES.SetSelectedGameObject (firstSelected, null);

		}

		Menu ();

	}

	void Menu(){

		if (menuPauseActive) {
			persistentMenu.SetActive (false);
			pauseMenu.SetActive (true);
			playMenu.SetActive (false);
			endMenu.SetActive (false);
			ManageButtons ();
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			stickIsMoving = false;
			mouseIsMoving = false;
			pauseMenu.SetActive (false);
			persistentMenu.SetActive (true);
			playMenu.SetActive (true);
		}
	}

	void ManageButtons() {

			if (ES.currentSelectedGameObject != storeSelected) {
				if (ES.currentSelectedGameObject == null) {
					ES.SetSelectedGameObject (storeSelected);
				} else {
					storeSelected = ES.currentSelectedGameObject;
				}
			}

			if (Input.GetAxis ("Mouse Menu X") != 0 || Input.GetAxis ("Mouse Menu Y") != 0 ) {
				mouseIsMoving = true;
				stickIsMoving = false;
			}

			if (Input.GetButtonDown ("Controller Menu X") //|| Input.GetAxis ("Controller Menu X") != 0
				|| Input.GetButtonDown ("Controller Menu Y") //|| Input.GetAxis ("Controller Menu Y") != 0 
				|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)
				|| Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
				stickIsMoving = true;
				mouseIsMoving = false;
			}

			if (mouseIsMoving) {
				lockCursor = false;
			} else if (stickIsMoving) {
				lockCursor = true;
			}


	}

	public void ChangeControllerHover(GameObject newHover){
		AudioManager.AM.PlaySound2D ("MenuSelect");
		ES.SetSelectedGameObject (newHover);
	}

	public void Pause() {
		//pausa la escena
		persistentMenu.SetActive (false);
		playMenu.SetActive (false);
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void Resume() {
		//reanuda la escena
		AudioManager.AM.PlaySound2D ("MenuButton");
		lockCursor = true;
		stickIsMoving = false;
		mouseIsMoving = false;
		menuPauseActive = false;
		persistentMenu.SetActive (true);
		playMenu.SetActive (true);
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void Retry() {
		//recarga la escena
		AudioManager.AM.PlaySound2D ("MenuButton");
		Time.timeScale = 1;

		playMenu.SetActive (false);
		StartCoroutine(SceneController.SC.Fade (1, 2));
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//introGame = false;
		//fadeCanvasGroup.alpha = 1f;

	}

	public void Quit() {
		AudioManager.AM.PlaySound2D ("MenuButton");
		Application.Quit();
	}
}

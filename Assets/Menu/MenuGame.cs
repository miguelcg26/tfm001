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

//	public CanvasGroup mainCanvas;
	public GameObject playCanvas;
	public GameObject pauseCanvas;
//	public GameObject persistentCanvas;
	public bool lockCursor;

	public bool mouseIsMoving;
	public bool stickIsMoving;

	void Start () {

		//		storeSelected = ES.firstSelectedGameObject;
		//Inicialmente bloqueamos el cursor al cargar el objeto que tenga asignado este script
		Cursor.lockState = CursorLockMode.Locked;
		lockCursor = true;
	}

	void Update () {
		
		if (SceneController.SC.blockAction) {
			return;
		}

		//Al pulsar la tecla asignada al botón Pause, liberamos el cursor
		if (Input.GetButtonDown ("Pause")) {

			if (Time.timeScale > 0) {
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




		Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
		Cursor.visible = !lockCursor;
	}

	void Menu(){

		if (menuPauseActive) {

			pauseCanvas.SetActive (true);
			playCanvas.SetActive (false);
		//	mainCanvas.alpha = 0;
		//	persistentCanvas.SetActive (false);

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




		} else {
			Cursor.lockState = CursorLockMode.Locked;
			stickIsMoving = false;
			mouseIsMoving = false;
			pauseCanvas.SetActive (false);
			playCanvas.SetActive (true);
		//	mainCanvas.alpha = 1;
		//	persistentCanvas.SetActive (true);

		}

	}

	public void ChangeControllerHover(GameObject newHover){
		ES.SetSelectedGameObject (newHover);
	}

	public void Pause() {
		//pausa la escena
		playCanvas.SetActive (false);
		pauseCanvas.SetActive (true);
		Time.timeScale = 0;
	}

	public void Resume() {
		//reanuda la escena
		lockCursor = true;
		stickIsMoving = false;
		mouseIsMoving = false;
		menuPauseActive = false;
		playCanvas.SetActive (true);
		pauseCanvas.SetActive (false);
		Time.timeScale = 1;
	}

	public void Retry() {
		//recarga la escena
		Time.timeScale = 1;

		playCanvas.SetActive (false);
		StartCoroutine(SceneController.SC.Fade (1));
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//introGame = false;
		//fadeCanvasGroup.alpha = 1f;

	}

	public void Quit() {
		Application.Quit();
	}
}

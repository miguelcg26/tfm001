using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour {

	[System.Serializable]
	public class MenuTexts {
		public Text menuText;
		public string textID;
	}

	public EventSystem ES;
	private GameObject storeSelected;
	public GameObject firstSelected;


	public GameObject menuCanvas;
	public GameObject optionCanvas;
	public bool lockCursor;

	public bool mouseIsMoving;
	public bool stickIsMoving;

	public MenuTexts[] menuTexts;

	void Start () {

		for (int i = 0; i < menuTexts.Length; i++) {
			menuTexts [i].menuText.text = TranslateManager.TM.GetString (menuTexts [i].textID);
		}

		//		storeSelected = ES.firstSelectedGameObject;
		//Inicialmente bloqueamos el cursor al cargar el objeto que tenga asignado este script

		Cursor.lockState = CursorLockMode.Locked;
		lockCursor = true;


		Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
		Cursor.visible = !lockCursor;

		storeSelected = ES.firstSelectedGameObject;

		ES.SetSelectedGameObject (firstSelected, null);
	}

	void Update () {
		
		if (SceneController.SC.blockAction) {
			return;
		}


		Menu ();

		Cursor.lockState = lockCursor?CursorLockMode.Locked:CursorLockMode.None;
		Cursor.visible = !lockCursor;
	}

	void Menu(){


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

			if (Input.GetButtonDown ("Controller Menu X") || Input.GetAxis ("Controller Menu X") != 0
				|| Input.GetButtonDown ("Controller Menu Y") || Input.GetAxis ("Controller Menu Y") != 0 
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
		ES.SetSelectedGameObject (newHover);
	}

	public void StartGame(){
		AudioManager.AM.PlaySound2D ("MenuButton");
		SceneController.SC.FadeAndLoadScene ("Level00", DataManager.DM.data.startPosition);
	}

	public void Quit() {
		Application.Quit();
	}
}

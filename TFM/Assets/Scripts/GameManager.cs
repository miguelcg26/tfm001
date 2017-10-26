/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager GM;

	public bool blockPlayer;

	public bool charWalkingUp;

	public bool charWalkingDown;
	//[Header("Character")]
	public enum CharPos { TOP, RIGHT_TOP, LEFT_TOP, BOTTOM, RIGHT_BOTTOM, LEFT_BOTTOM, VERTICAL }
	public CharPos charPos;

	public enum CharDir { RIGHT, LEFT } // LEFT es a la izq DEL personaje, no de lo que ves, same for RIGHT
	public CharDir charDir;

	//[Header("Camera")]
	public enum CamPosX { BACK, FRONT, RIGHT, LEFT }
	public CamPosX camPosX;

	public enum CamPosY { NORMAL, UP, DOWN }
	public CamPosY camPosY;

	public enum CamPosZ { TOP, BOTTOM }
	public CamPosZ camPosZ;


	//public GameObject[] attractors;

	//public GameObject currentAttractor;

	void Awake () {
		//DontDestroyOnLoad(transform.gameObject);


		if (GM == null)
			GM = this;	
		else
			Destroy (gameObject);

	}

	/*
	public GameObject gameMenu;
	public GameObject pauseMenu;



	public void Pause() {
		//pausa la escena
		gameMenu.SetActive (false);
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void Resume() {
		//reanuda la escena
		gameMenu.SetActive (true);
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void Retry() {
		//recarga la escena
		Time.timeScale = 1;

		pauseMenu.SetActive (false);
		//Hacemos un Face In
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//introGame = false;
		//fadeCanvasGroup.alpha = 1f;

	}

	public void Quit() {
		Application.Quit();
	}

	*/



}

/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip menuMusic;
	public AudioClip levelMusic;

	void Update () {
		int y = SceneManager.GetActiveScene().buildIndex;

		if (y == 1) {
			AudioManager.AM.PlayMusic (menuMusic, 2);
		} else if (y > 1) {
			AudioManager.AM.PlayMusic (levelMusic, 2);
		}


	}
}

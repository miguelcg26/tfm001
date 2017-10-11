using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public static AudioManager AM;

	public AudioSource music;
	Transform mainCam;

	void Awake(){
		if (AM == null) {
			AM = GetComponent<AudioManager> ();
		}
	}

	public void ChangeMusic (AudioClip _music){

		if (music.clip.name == _music.name)
			return;

		music.Stop ();
		music.clip = _music;
		music.Play ();
	}

	void Update (){

		int y = SceneManager.GetActiveScene().buildIndex;

		if (y > 1) {

			mainCam = GameObject.Find("PlayerCamera").transform;
		}

		if (mainCam != null) {
			transform.position = mainCam.position;
			transform.rotation = mainCam.rotation;
		}
	}


}

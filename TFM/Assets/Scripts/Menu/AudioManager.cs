/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

	public static AudioManager AM;

	public enum AudioChannel {MASTER, SFX, MUSIC};
	Transform player;
	Transform audioListener;

	float masterVolumePercent = 1;
	float sfxVolumePercent = 1;
	float musicVolumePercent = 1;

	AudioSource sfx2DSource;
	AudioSource[] musicSources;
	int activeMusicSourceIndex;

	SoundLibrary library;

	void Awake(){
		if (AM == null) {
			AM = GetComponent<AudioManager> ();		

			library = GetComponent<SoundLibrary> ();

			musicSources = new AudioSource[2];
			for (int i = 0; i < 2; i++) {
				GameObject newMusicSource = new GameObject ("MusicSource" + (i + 1));
				musicSources [i] = newMusicSource.AddComponent<AudioSource> ();
				newMusicSource.transform.parent = transform;
				musicSources[i].GetComponent<AudioSource>().loop = true;
			}

			GameObject newSfx2DSource = new GameObject ("Sfx2DSource");
			sfx2DSource = newSfx2DSource.AddComponent<AudioSource> ();
			newSfx2DSource.transform.parent = transform;

			audioListener = FindObjectOfType<AudioListener> ().transform;

			masterVolumePercent = PlayerPrefs.GetFloat ("MasterVolume", masterVolumePercent);
			sfxVolumePercent = PlayerPrefs.GetFloat ("SfxVolume", sfxVolumePercent);
			musicVolumePercent = PlayerPrefs.GetFloat ("MusicVolume", musicVolumePercent);
		} else {
			Destroy (gameObject);
		}
	}


	public void SetVolume(float volumePercent, AudioChannel channel) {
		switch(channel) {
		case AudioChannel.MASTER:
			masterVolumePercent = volumePercent;
			break;
		case AudioChannel.SFX:
			sfxVolumePercent = volumePercent;
			break;
		case AudioChannel.MUSIC:
			musicVolumePercent = volumePercent;
			break;
		}

		musicSources [0].volume = musicVolumePercent * masterVolumePercent;
		musicSources [1].volume = musicVolumePercent * masterVolumePercent;

		PlayerPrefs.SetFloat ("MasterVolume", masterVolumePercent);
		PlayerPrefs.SetFloat ("SfxVolume", sfxVolumePercent);
		PlayerPrefs.SetFloat ("MusicVolume", musicVolumePercent);
	}

	public void PlayMusic(AudioClip clip, float fadeDuration = 1) {
		if (musicSources [activeMusicSourceIndex].clip != null){
			if (clip.name == musicSources [activeMusicSourceIndex].clip.name) {
				return;
			}
		}
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		musicSources [activeMusicSourceIndex].clip = clip;
		musicSources [activeMusicSourceIndex].Play ();

		StartCoroutine (AnimateMusicCrossfade (fadeDuration));
	}

	public void PlaySound(AudioClip clip, Vector3 pos) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}

	public void PlaySound(string soundName, Vector3 pos) {
		PlaySound(library.GetClipFromName(soundName), pos);
	}

	public void PlaySound2D(string soundName) {
		sfx2DSource.PlayOneShot (library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
	}

	IEnumerator AnimateMusicCrossfade(float duration) {
	
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * 1 / duration;
			musicSources [activeMusicSourceIndex].volume = Mathf.Lerp (0, musicVolumePercent * masterVolumePercent, percent);
			musicSources [1 - activeMusicSourceIndex].volume = Mathf.Lerp (musicVolumePercent * masterVolumePercent, 0, percent);
			yield return null;
		}
	}

	void Update (){

		int y = SceneManager.GetActiveScene().buildIndex;

		if (y > 1) {
			player = FindObjectOfType<PlayerController> ().transform;
		}

		if (player != null) {
			audioListener.position = player.position;
			audioListener.rotation = player.rotation;
		}
	}


}

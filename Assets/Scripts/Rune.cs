using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {

	public Material checker;
	public GameObject nextRune;
	public bool endRune;

	bool check;

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			if (!check) {
				GetComponent<Renderer> ().material = checker;

				if (!endRune) {
					ActivateRune (nextRune);
					GameObject.Find ("CenterBlip").GetComponent<BlipObject> ().Target = nextRune.transform;
					check = true;
				} else {
					GameObject.Find ("CenterBlip").GetComponent<BlipObject> ().Target = null;
					GameObject.Find ("CenterBlip").SetActive (false);
					SceneController.SC.blockAction = true;
					GameObject.Find ("PlayerCamera").transform.parent.parent.parent.parent.GetComponent<CameraController> ().endCamera = true;
					Transform reactions = transform.FindChild ("Reactions");
					check = true;

					for (int i = 0; i < reactions.childCount; i++) {
						reactions.GetChild (i).gameObject.GetComponent<Reaction> ().ExecuteReaction ();
					}
				}
			}
		}
	}

	void ActivateRune(GameObject rune) {
		rune.SetActive (true);
	}
}

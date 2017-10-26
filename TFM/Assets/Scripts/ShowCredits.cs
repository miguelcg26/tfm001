/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Transform reactions = transform.FindChild ("Reactions");
			for (int i = 0; i < reactions.childCount; i++) {
				reactions.GetChild (i).gameObject.GetComponent<Reaction> ().ExecuteReaction ();
			}
	}

}

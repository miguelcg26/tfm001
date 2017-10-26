/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour {

	public static Persistent persistent;

	void Awake () {
		if (persistent != null) {
			Destroy (gameObject);
		} else {
			persistent = GetComponent<Persistent> ();
		}

	}
}

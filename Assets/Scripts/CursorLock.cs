using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour {

	void Start () {
		// Incialmente bloqueamos el cursor al cargar el objeto que tenga asignado este script
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		// al pusar la tecla asignada al botón Cancel, liberamos el cursor
		if (Input.GetButtonDown ("Cancel")) {
			Cursor.lockState = CursorLockMode.None;
		}

		// bloqueamos el cursor con el keyup de la tecla izquierda del ratón
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}

/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Reaction : MonoBehaviour {

	public string description;						//Descripción de la reacción, para uso en el editor
	public float delay;								//Retardo en realizar la reacción


	/// <summary>
	/// Método genérico que ejecutará la reacción, será extendido a todas las clases que lo hereden
	/// Lo hacemos de tipo virtual para poder sobreescribirlo
	/// </summary>
	public virtual void ExecuteReaction(){
		StartCoroutine (React ());
	}

	//Corutina que será ejecutada como reacción
	protected virtual IEnumerator React(){
		//Retardo de la reacción
		yield return new WaitForSeconds (delay);
		//Aquí irá el código de cada reacción específica
	}
}
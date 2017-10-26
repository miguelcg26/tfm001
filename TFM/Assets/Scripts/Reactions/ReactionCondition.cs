/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCondition : Reaction {

	public string conditionName;							//Nombre de la condición a modificar
	public bool conditionStatus;							//Estado al que queremos cambiar la confición

	/// <summary>
	/// Método que ejecuta la reacción
	/// </summary>
	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);

		//Recorremos la lista de todas las condiciones existentes
		foreach (Condition condition in DataManager.DM.data.allCondition) {
			//Si localizamos el nombre de la condición que buscamos
			if (condition.name == conditionName) {
				//Asignamos el status solicitado
				condition.done = conditionStatus;
				//Si lo hemos encontrado, salimos del bucle
				break;
			}


		}
	}
}

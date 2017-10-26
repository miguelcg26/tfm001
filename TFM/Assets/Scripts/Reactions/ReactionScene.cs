/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionScene : Reaction {

	public string nextScene;							//Nombre de la escena a cambiar
	public string nextStartPoint;						//Nombre del punto de entrada a la escena a la que cambiamos
	//public AudioClip nextSceneMusic;

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada
	/// </summary>
	protected override IEnumerator React(){
		//Tiempo de espera
		yield return new WaitForSeconds (delay);

		//Llamamos al método de sceneController que llevará a cabo la tarea del cambio de escena
		SceneController.SC.FadeAndLoadScene (nextScene, nextStartPoint); //, nextSceneMusic);
	}
}

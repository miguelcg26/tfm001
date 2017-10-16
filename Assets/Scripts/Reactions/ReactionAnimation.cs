using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAnimation : Reaction {

	public GameObject target;						//Objeto que será animado
	public string triggerName;						//Nombre del trigger del animator a disparar


	protected override IEnumerator React(){
		Debug.Log ("Animation Reaction - " + description);
		//Tiempo de espera antes de iniciar la animación
		yield return new WaitForSeconds (delay);
		//Disparo el trigger de la animación
		target.GetComponent<Animator> ().SetTrigger (triggerName);
	}
}
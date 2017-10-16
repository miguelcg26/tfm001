using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSound : Reaction {

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada
	/// </summary>
	protected override IEnumerator React(){
		//Realizamos la espera
		yield return new WaitForSeconds (delay);

		AudioManager.AM.PlaySound2D ("MenuButton");
	}
}

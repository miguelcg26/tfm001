/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionActivation : Reaction {

	public Objs[] targetObjs;					//Objeto a ser activado/desactivado


	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);

		for (int i = 0; i < targetObjs.Length; i++) {
			targetObjs[i].obj.SetActive (targetObjs[i].active);			//Activa o desactiva el objeto objetivo, según el parámetro active
		}

		if (GameObject.Find ("EndMenu").activeSelf == true) {
			GameObject.Find ("MANAGER").GetComponent<MenuGame> ().menuEndActive = true;
		}

	}

	[System.Serializable]
	public class Objs {
		public string name;							//Nombre
		public GameObject obj;						//Objeto en cuestión
		public bool active;							//Bool para indicar si activamos o desactivamos el objeto
	}

}

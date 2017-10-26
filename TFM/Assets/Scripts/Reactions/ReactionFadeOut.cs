/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionFadeOut : Reaction {

	public float delayOut;		
	public bool isFading;
	public CanvasGroup fadeCanvasGroup;
	public string canvasGroupName;
	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada
	/// </summary>
	protected override IEnumerator React(){

		fadeCanvasGroup = GameObject.Find (canvasGroupName).GetComponent<CanvasGroup>();


		//Tiempo de espera
		yield return new WaitForSeconds (delay);

		StartCoroutine (FadeOut ());

	}

	private IEnumerator FadeOut (){

		float fadeSpeed;

		//Indicamos que se está realizando un fade
		isFading = true;

		//Bloqueamos todo raycast para evitar que se interactúe durante el fade
		fadeCanvasGroup.blocksRaycasts = true;

		//Calculamos la velocidad del fade
		fadeSpeed = Mathf.Abs (fadeCanvasGroup.alpha - 0.0f) / delayOut;

		//Mientras el alpha no sea SIMILAR al alpha objetivo, ejecutamos acciones
		while (!Mathf.Approximately (fadeCanvasGroup.alpha, 0.0f)) {
			//Aumentamos el valor del alpha mediante MoveTowards
			fadeCanvasGroup.alpha = Mathf.MoveTowards (fadeCanvasGroup.alpha, 0.0f, fadeSpeed * Time.deltaTime);

			//Hacemos que la corrutina se ejecute en cada frame
			yield return null;
		}

		//Si llega hasta este punto, es que el fade ha terminado
		isFading = false;

		//Volvemos a permitir nuevamente los raycasts
		fadeCanvasGroup.blocksRaycasts = false;
	}

}

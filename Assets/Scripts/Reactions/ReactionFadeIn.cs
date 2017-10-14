using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionFadeIn : Reaction {

	public float delayIn;		
	public bool isFading;
	public CanvasGroup fadeCanvasGroup;
	public Image fadeImage;
	public Color fadeColor;

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada
	/// </summary>
	protected override IEnumerator React(){


		fadeImage.color = fadeColor;

		//Tiempo de espera
		yield return new WaitForSeconds (delay);

		StartCoroutine (FadeIn ());

	}

	private IEnumerator FadeIn (){

		float fadeSpeed;

		//Indicamos que se está realizando un fade
		isFading = true;

		//Bloqueamos todo raycast para evitar que se interactúe durante el fade
		fadeCanvasGroup.blocksRaycasts = true;

		//Calculamos la velocidad del fade
		fadeSpeed = Mathf.Abs (fadeCanvasGroup.alpha - 1.0f) / delayIn;

		//Mientras el alpha no sea SIMILAR al alpha objetivo, ejecutamos acciones
		while (!Mathf.Approximately (fadeCanvasGroup.alpha, 1.0f)) {
			//Aumentamos el valor del alpha mediante MoveTowards
			fadeCanvasGroup.alpha = Mathf.MoveTowards (fadeCanvasGroup.alpha, 1.0f, fadeSpeed * Time.deltaTime);

			//Hacemos que la corrutina se ejecute en cada frame
			yield return null;
		}

		//Si llega hasta este punto, es que el fade ha terminado
		isFading = false;

		//Volvemos a permitir nuevamente los raycasts
		fadeCanvasGroup.blocksRaycasts = true;
	}

}

/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;										//Para poder hacer uso de los elementos UI

public class TextManager : MonoBehaviour {

	public Text textTop;	
	public Text textMiddle;	
	public Text textBottom;	
	public RectTransform textCredits;	
	public Text textCreditsTitle;	
	public Text textCreditsName01;	
	public Text textCreditsName02;	
	CanvasGroup fadeCanvasGroup;							//Referencia al text del canvas
	public float displayTimePerCharacter = 0.05f;			//Tiempo mínimo de mostrado de cada carácter
	public float additionalDisplayTime = 0.5f;				//Tiempo adicional extra de mostrado de cada frase
	private float clearTime;								//Tiempo en el que se borrará el texto
	public static TextManager TM;							//Referencia estática


	void Awake () {
		if (TM == null) {
			TM = GetComponent<TextManager> ();
		}
		
		fadeCanvasGroup = GameObject.Find ("TextCredits").GetComponent<CanvasGroup> ();
	}

	// Update is called once per frame
	void Update () {
		//Si el tiempo actual es mayor que el fijado para el borrado realizamos el borrado del texto en la caja de texto
		if (Time.time >= clearTime) {
			textTop.text = "";
			textMiddle.text = "";
			textBottom.text = "";
		}
	}

	/// <summary>
	/// Muestra el texto por pantalla, indicando texto a mostrar y color
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="textColor">Text color.</param>
	public void DisplayMessageTop(string message, Color textColor){
		//Calculando la duración en función del número de caracteres del mensaje (mas un extra adicional)
		float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

		//Calculamos el tiempo exacto para el borrado
		clearTime = Time.time + displayDuration;

		//Asignamos el texto del mensaje
		textTop.text = message;

		//Cambiamos el color del texto
		textTop.color = textColor;
	}

	/// <summary>
	/// Muestra el texto por pantalla, indicando texto a mostrar y color
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="textColor">Text color.</param>
	public void DisplayMessageMiddle(string message, Color textColor){
		//Calculando la duración en función del número de caracteres del mensaje (mas un extra adicional)
		float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

		//Calculamos el tiempo exacto para el borrado
		clearTime = Time.time + displayDuration;

		//Asignamos el texto del mensaje
		textMiddle.text = message;

		//Cambiamos el color del texto
		textMiddle.color = textColor;
	}

	/// <summary>
	/// Muestra el texto por pantalla, indicando texto a mostrar y color
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="textColor">Text color.</param>
	public void DisplayMessageBottom(string message, Color textColor){
		//Calculando la duración en función del número de caracteres del mensaje (mas un extra adicional)
		float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

		//Calculamos el tiempo exacto para el borrado
		clearTime = Time.time + displayDuration;

		//Asignamos el texto del mensaje
		textBottom.text = message;

		//Cambiamos el color del texto
		textBottom.color = textColor;
	}

		/// <summary>
	/// Muestra el texto por pantalla, indicando texto a mostrar, color, tamaño de fuente, posición y duración
	/// </summary>
	public void DisplayMessageCredits(string title, string name01, string name02, Color textColor, int sizeUpperCase, int sizeLowerCase, Vector2 position, float duration){
		
		//Asignamos el texto del mensaje
		textCreditsTitle.text = title;

		//Cambiamos el color del texto
		textCreditsTitle.color = textColor;

		//Cambiamos el color del texto
		textCreditsTitle.fontSize = sizeLowerCase;

		//Asignamos el texto del mensaje
		textCreditsName01.text = name01;
		textCreditsName02.text = name02;

		//Cambiamos el color del texto
		textCreditsName01.color = textColor;
		textCreditsName02.color = textColor;

		//Cambiamos el color del texto
		textCreditsName01.fontSize = sizeUpperCase;
		textCreditsName02.fontSize = sizeUpperCase;

		//Posicionamos el texto
		textCredits.position = new Vector2 (position.x * Screen.height, position.y * Screen.height);

		StartCoroutine(ShowCredits(duration));

	}

	
	private IEnumerator ShowCredits (float duration){

		float fadeSpeed;

		//Calculamos la velocidad del fade
		fadeSpeed = Mathf.Abs (fadeCanvasGroup.alpha - 1.0f) / 1;

		//Mientras el alpha no sea SIMILAR al alpha objetivo, ejecutamos acciones
		while (!Mathf.Approximately (fadeCanvasGroup.alpha, 1.0f)) {
			//Aumentamos el valor del alpha mediante MoveTowards
			fadeCanvasGroup.alpha = Mathf.MoveTowards (fadeCanvasGroup.alpha, 1.0f, fadeSpeed * Time.deltaTime);

			//Hacemos que la corrutina se ejecute en cada frame
			yield return null;
		}

		yield return new WaitForSeconds(duration);

		
		//Calculamos la velocidad del fade
		fadeSpeed = Mathf.Abs (fadeCanvasGroup.alpha - 0.0f) / 1;

		//Mientras el alpha no sea SIMILAR al alpha objetivo, ejecutamos acciones
		while (!Mathf.Approximately (fadeCanvasGroup.alpha, 0.0f)) {
			//Aumentamos el valor del alpha mediante MoveTowards
			fadeCanvasGroup.alpha = Mathf.MoveTowards (fadeCanvasGroup.alpha, 0.0f, fadeSpeed * Time.deltaTime);

			//Hacemos que la corrutina se ejecute en cada frame
			yield return null;
		}
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;										//Para poder hacer uso de los elementos UI

public class TextManager : MonoBehaviour {

	public Text textTop;	
	public Text textMiddle;	
	public Text textBottom;									//Referencia al text del canvas
	public float displayTimePerCharacter = 0.05f;			//Tiempo mínimo de mostrado de cada carácter
	public float additionalDisplayTime = 0.5f;				//Tiempo adicional extra de mostrado de cada frase
	private float clearTime;								//Tiempo en el que se borrará el texto
	public static TextManager TM;							//Referencia estática


	void Awake () {
		if (TM == null) {
			TM = GetComponent<TextManager> ();
		}
	}

	// Update is called once per frame
	void Update () {
		//Si el tiempo actual es mayor que el fijado para el borrado realizamos el borrado del texto en la caja de texto
		if (Time.time >= clearTime) {
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

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionText : Reaction {

	public string text;
	public Color textColor;
	public float additionalDisplayTime;

	public enum TextPlace
	{
		TOP,
		MIDDLE,
		DOWN
	}

	public TextPlace textPlace;

	/// <summary>
	/// Método que ejecuta la reacción, con override para que pise la corrutina heredada
	/// </summary>
	protected override IEnumerator React(){
		yield return new WaitForSeconds (delay);
		TextManager.TM.additionalDisplayTime = additionalDisplayTime;

		switch (textPlace) {
		case TextPlace.TOP:
			TextManager.TM.DisplayMessageTop (TranslateManager.TM.GetString(text), textColor);				//Llamada al TextManager para que se haga cargo del dibujado del texto
			break;
		case TextPlace.MIDDLE:
			TextManager.TM.DisplayMessageMiddle (TranslateManager.TM.GetString(text), textColor);			//Llamada al TextManager para que se haga cargo del dibujado del texto
			break;
		case TextPlace.DOWN:
			TextManager.TM.DisplayMessageBottom (TranslateManager.TM.GetString(text), textColor);			//Llamada al TextManager para que se haga cargo del dibujado del texto
			break;
		}
	}

}
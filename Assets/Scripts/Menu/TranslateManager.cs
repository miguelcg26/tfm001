using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;												//Para poder hacer uso

public class TranslateManager : MonoBehaviour {


	public string defaultLanguage = "English";					//Idioma por defecto
	private Hashtable strings;									//Listado de ítems tipo array, pero que permite índices ALFANUMÉRICOS. Aquí guardaremos los textos del juego
	public static TranslateManager TM;


	void Awake () {
		if (TM == null) {
			TM = GetComponent<TranslateManager> ();
		}
	}


	void Start(){
		//Recuperamos el lenguaje del sistema operativo en un string
		string language = Application.systemLanguage.ToString ();

		//Recuperamos el fichero como asset de tipo texto
		TextAsset textAsset = (TextAsset)Resources.Load ("Lang", typeof(TextAsset));

		//Creamos una variable de tipo XmlDocument, para hacer la gestión del Xml
		XmlDocument xml = new XmlDocument ();

		//Volcamos el contenido recuperado del fichero de texto, a nuestro objeto Xml
		xml.LoadXml (textAsset.text);

		//Verificamos si no existe el idioma
		if (xml.DocumentElement [language] == null) {
			//Si no existe el idioma en el Xml, usaremos el definido como default
			language = defaultLanguage;
		}

		SetLanguage (xml, language);
	}


	/// <summary>
	/// Carga el idioma seleccionado del Xml y lo almacena en el hashtable
	/// </summary>
	/// <param name="xml">Xml.</param>
	/// <param name="language">Language.</param>
	public void SetLanguage(XmlDocument xml, string language){
		//Inicializamos el hashtable
		strings = new Hashtable ();

		//Recuperamos el bloque con el idioma seleccionado
		XmlElement element = xml.DocumentElement [language];

		if (element != null) {
			//Mediante este método recuperamos un tipo enumerador, que nos permite recorrer el Xml como si fuera un foreach
			var elemEnum = element.GetEnumerator ();

			//Mientras queden elementos, recuperamos elementos
			while (elemEnum.MoveNext ()) {
				//Recuperamos el elemento que contiene el literal actual
				var xmlItem = (XmlElement)elemEnum.Current;

				//Añadimos el literal actual al hashtable, utilizando como índice el valor de name
				strings.Add (xmlItem.GetAttribute ("name"), xmlItem.InnerText);
			}
		} else {
			//En caso de no recuperar nada, significa que el idioma no existe
			Debug.LogWarning ("El idioma especificado no existe: " + language);
		}
	}

	/// <summary>
	/// Recupera un literal de texto de la hashtable mediante un índice
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="name">Name.</param>
	public string GetString(string name){
		//Verificamos si NO existe el índice solicitado
		if (!strings.ContainsKey (name)) {
			Debug.LogWarning ("El índice no existe: " + name);

			//Si no existe, devolvemos una cadena vacía
			return "";
		}

		//Si llegamos hasta aquí, es que existe, así que devolvemos el valor de ese índice
		return (string)strings [name];
	}
}
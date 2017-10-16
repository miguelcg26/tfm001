using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;					//Para serializar en binario
using System.IO;														//Para gestión de ficheros

public class DataManager : MonoBehaviour {

	public Data data;													//Objeto que contendrá toda la información de la partida
	public string fileName = "data.dat";								//Nombre que tendrá el fichero al guardar
	public static DataManager DM;										

	void Awake(){
		//En caso de que no exista la instancia
		if (DM == null) {
			//Creamos la instancia principal del componente de este objeto
			DM = GetComponent<DataManager> ();

			//Recuperamos la información almacenada en disco al inicio
			Load ();
		}
	}

	/// <summary>
	/// Realiza el guardado de la información en disco
	/// </summary>
	public void Save(){
		//Objeto utilizado para serializar/deserializar
		BinaryFormatter bf = new BinaryFormatter ();

		//Creamos/sobreescribimos el fichero con los datos
		FileStream file = File.Create (Application.persistentDataPath + "/" + fileName);

		//Serializamos el contenido de nuestro objeto de datos
		bf.Serialize (file, data);

		//Cerramos el fichero
		file.Close ();
	}

	/// <summary>
	/// Realiza la carga de la información desde el disco
	/// </summary>
	public void Load (){
		//Debug Log con la ruta de guardado persistente (para poder locarlizarlo)
		Debug.Log (Application.persistentDataPath);

		//Antes de abrir el fichero verificamos si existe
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {
			//Objeto para serializar/deserializar
			BinaryFormatter bf = new BinaryFormatter ();

			//Abrimos el fichero
			FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);

			//Deserializamos el fichero
			data = (Data)bf.Deserialize (file);

			//Cerramos el fichero
			file.Close ();
		}
	}

	/// <summary>
	/// Devuelve el estado en el que se encuentra una condición global
	/// </summary>
	/// <returns><c>true</c>, if condition was checked, <c>false</c> otherwise.</returns>
	/// <param name="conditionName">Condition name.</param>
	public bool CheckCondition(string conditionName){
		//Buscamos la confición en la lista
		foreach (var cond in data.allCondition) {
			//Si la encuentro, devuelvo el valor de su  estado
			if (cond.name == conditionName) {				
				return cond.done;
			}
		}

		//Si termina el foreach y llega hasta aquí, significa que no ha encontrado nada, mandamos el debug y devolvemos un false
		Debug.Log (conditionName + " - son los padres (no existe)");
		return false;
	}

	/// <summary>
	/// Cambia el estado de una condición
	/// </summary>
	/// <param name="conditionName">Condition name.</param>
	/// <param name="done">If set to <c>true</c> done.</param>
	public void SetCondition (string conditionName, bool done){
		//Busco la condición en la lista
		foreach (var cond in data.allCondition) {
			//Si la encuentro, le asigno el valor del estado
			if (cond.name == conditionName) {
				cond.done = done;
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Condition {

	public string name;						//Nombre de la condición, que debe ser único, ya que lo usaremos como identificador
	public string description;				//Descripción de apoyo, solo visible en desarrollo
	public bool done;						//Indica si la condición ha sido cumplida o no

}
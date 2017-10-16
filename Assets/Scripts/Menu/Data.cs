using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Data {

	public string actualScene;						//Nombre de la escena actual
	public string startPosition;					//Nombre del punto de entrada de la escena
	public Condition[] allCondition;				//Array con el estado de todas las condiciones
	//public ObjectActive[] allObjectsActivated;
	//public Item[] allItems;							//Array con el estado de todos los objetos de juego
	//public Item[] inventory = new Item[4];			//Array con el inventario actual del jugador

}
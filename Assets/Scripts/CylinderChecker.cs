using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderChecker : MonoBehaviour {

	public Material checker;


	void OnTriggerEnter(){
		GetComponent<Renderer> ().material = checker;		
	}
}

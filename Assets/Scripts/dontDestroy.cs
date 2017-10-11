using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour {

	public static dontDestroy Canvas;
	// Use this for initialization
	void Awake () {
		
		if (Canvas == null)
			Canvas = this;	
		else
			Destroy (gameObject);
		
		DontDestroyOnLoad(transform.gameObject);
	}

}

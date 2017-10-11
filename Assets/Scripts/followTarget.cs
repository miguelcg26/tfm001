using UnityEngine;
using System;
using System.Collections;

public class followTarget : MonoBehaviour
{
    public GameObject target;
    public float smoothLerp = 5f;
    public bool smoothON;

    //cameraController cam;
    Vector3 camPos;
    public float distance;
    RaycastHit hit;


    void Awake() {
		//cam = GetComponent<cameraController>();
        //target = GameObject.FindWithTag("Player");
    }


    void FixedUpdate() {
     	if (smoothON)
			transform.position = Vector3.Lerp(transform.position, target.transform.position + target.transform.up * 0.5f, smoothLerp * Time.deltaTime);
        else
			transform.position = target.transform.position + target.transform.up * 0.5f;
            
    }

}


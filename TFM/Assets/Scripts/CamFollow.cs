/* CREADO POR MIGUEL CASADO */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	public GameObject camRig;
	public GameObject pivotX2;

	PlayerController player;
	public float turnSmooth = 5;

	public float turnSpeedX = 4f;
	public float turnSpeedY = 3f;
	public float tiltMax = 65f;
	public float tiltMin = 50f;
	float XAngle;	


	public float x;
	public float y;		

	void Awake() {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
	}
	void Start() {		
		XAngle = 0;
	}	

	void Update() {		

		if (XAngle > 360)
			XAngle = 0;
		else if (XAngle < -360)
			XAngle = 0;	

	}

	void FixedUpdate () {
		RotationMovement();
	}


	public void RotationMovement() {

		x = Input.GetAxis("Mouse X");
		y = Input.GetAxis("Mouse Y");

	
		if (SceneController.SC.blockAction) {
			return;
		}
				XAngle += x * turnSpeedX;


				pivotX2.transform.localRotation = Quaternion.Slerp (pivotX2.transform.localRotation, Quaternion.Euler (0, XAngle, 0), turnSmooth * Time.deltaTime);

				
		if (player.grounded) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, player.normalUp) * transform.rotation, 50f * Time.deltaTime);
			transform.rotation = Quaternion.Slerp (transform.rotation, camRig.transform.rotation, 1f * Time.deltaTime);
		}	
		else
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (transform.rotation.x, transform.rotation.y, transform.rotation.z /* cc*/), 1 * Time.deltaTime);
				


		}


}

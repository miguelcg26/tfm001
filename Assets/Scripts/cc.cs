using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cc : MonoBehaviour {

	public GameObject pivotX;
	public GameObject pivotX2;
	public GameObject pivotY;
	public GameObject pivotZ;
	public Vector3 offset;

	public playerController player;
	public float turnSmooth = 5;

	public float turnSpeedX = 4f;
	public float turnSpeedY = 3f;
	public float turnMax;
	public float turnMin;
	public float tiltMax = 65f;
	public float tiltMin = 50f;
	public float XAngle;	
	public float YAngle;
	public float ZAngle;
	public float XAngle2;	
	public float YAngle2;
	public float ZAngle2;


	public float x;
	public float y;		

	void Awake() {
		player = GameObject.FindWithTag ("Player").GetComponent<playerController> ();
	}
	void Start() {		
		XAngle = 0;
		YAngle = 0;
		// transform.position = Vector3.zero;
	}	

	void Update() {		

		if (XAngle > 360)
			XAngle = 0;
		else if (XAngle < -360)
			XAngle = 0;	

		CheckPosition ();
	}

	void FixedUpdate () {
		RotationMovement();
	}

	void LateUpdate() {   


		pivotY.transform.localPosition = Vector3.Lerp (pivotY.transform.localPosition, offset, turnSmooth * Time.deltaTime);


	}

	public void RotationMovement() {

		x = Input.GetAxis("Mouse X");
		y = Input.GetAxis("Mouse Y");

		/*
		if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM) {
			turnMin = 170;
			turnMax = -10;
			XAngle = Mathf.Clamp (XAngle, -turnMin, turnMax);
		} else if (GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM) {
			turnMin = -10;
			turnMax = 170;
			XAngle = Mathf.Clamp (XAngle, -turnMin, turnMax);
		} 
		else {
		}

		if (GameManager.GM.charPos != GameManager.CharPos.VERTICAL) {

			if (player.bodyUp.y < 0.0f) {
				offset.y = player.bodyUp.y * 0.5f;

				if (player.bodyUp.x > 0) {

					ZAngle = player.bodyUp.y * -180;
					ZAngle = Mathf.Clamp (ZAngle, 0, 180);
				} else {

					ZAngle = player.bodyUp.y * 180;
					ZAngle = Mathf.Clamp (ZAngle, -180, 0);
				}
			} else {
			
				if (player.bodyUp.y < 0.75f) {
					offset.y = player.bodyUp.y * 0.9f;

			
				} else {

					offset.y = 1;
					ZAngle = 0;
				}
			}

			if (GameManager.GM.charWalkingUp) {

				if (GameManager.GM.camPosX == GameManager.CamPosX.BACK || GameManager.GM.camPosX == GameManager.CamPosX.FRONT) {
					//YAngle2 = 0;
					if (GameManager.GM.charPos == GameManager.CharPos.TOP || GameManager.GM.charPos == GameManager.CharPos.BOTTOM) {
						XAngle2 = 0;
					}
					if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM) {
						XAngle2 -= 0.5f;
						XAngle2 = Mathf.Clamp (XAngle2, -12, 12);
					}
					if (GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM) {
						XAngle2 += 0.5f;
						XAngle2 = Mathf.Clamp (XAngle2, -12, 12);
					}
				}

				if (GameManager.GM.camPosX == GameManager.CamPosX.RIGHT) {
					XAngle2 = 0;

					if (GameManager.GM.charDir == GameManager.CharDir.RIGHT) {
						if (GameManager.GM.charPos == GameManager.CharPos.BOTTOM) {
							YAngle2 -= 5f;
							YAngle2 = Mathf.Clamp (YAngle2, -60, 360);

						} else if (GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM) {
							YAngle2 -= 5f;
							YAngle2 = Mathf.Clamp (YAngle2, -150, 360);
						} else if (GameManager.GM.charPos == GameManager.CharPos.LEFT_TOP) {
							YAngle2 -= 5f;
							YAngle2 = Mathf.Clamp (YAngle2, -360, 360);
						} 
					} else {
						/*
					if (GameManager.GM.charPos == GameManager.CharPos.BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 160);

					} else if (GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 260);
					} else if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 360);
					}

					}
				} 

				/*
			if (GameManager.GM.camPosX == GameManager.CamPosX.LEFT) {
				XAngle2 = 0;

				if (GameManager.GM.charDir == GameManager.CharDir.LEFT) {
					if (GameManager.GM.charPos == GameManager.CharPos.BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 60);

					} else if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 150);
					} else if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_TOP) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 360);
					} 
				} else {
					/*
					if (GameManager.GM.charPos == GameManager.CharPos.BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 160);

					} else if (GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 260);
					} else if (GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM) {
						YAngle2 += 5f;
						YAngle2 = Mathf.Clamp (YAngle2, -360, 360);
					}

				}
		
			}  

			} else {
				XAngle2 = 0;
				YAngle2 = 0;
			}
		} else {
			XAngle2 = 0;
			YAngle2 = 0;
		}

		*/
	
		if (SceneController.SC.blockAction) {
			return;
		}
				XAngle += x * turnSpeedX;

				YAngle += y * turnSpeedY;
				YAngle = Mathf.Clamp (YAngle, -tiltMin, tiltMax);

				pivotX.transform.localRotation = Quaternion.Slerp (pivotX.transform.localRotation, Quaternion.Euler (0, XAngle, 0), turnSmooth * Time.deltaTime);
				pivotX2.transform.localRotation = Quaternion.Slerp (pivotX2.transform.localRotation, Quaternion.Euler (0, XAngle, 0), turnSmooth * Time.deltaTime);

				if (player.grounded)
					pivotY.transform.localRotation = Quaternion.Slerp (pivotY.transform.localRotation, Quaternion.Euler (YAngle + YAngle2, 0, 0), turnSmooth * Time.deltaTime);
				else
					pivotY.transform.localRotation = Quaternion.Slerp (pivotY.transform.localRotation, Quaternion.Euler (YAngle + YAngle2 + 20, 0, 0), turnSmooth * Time.deltaTime);

				//if (!player.grounded)
				pivotZ.transform.localRotation = Quaternion.Slerp (pivotZ.transform.localRotation, Quaternion.Euler (0, 0, 0), turnSmooth * Time.deltaTime);

				//pivotX2.transform.rotation = new Quaternion(cam.transform.rotation.x, cam.transform.rotation.y, cam.transform.rotation.z, 0);

				if (player.grounded)
					transform.rotation =  Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, player.normalUp) * transform.rotation, 50f * Time.deltaTime);
				else
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (transform.rotation.x, transform.rotation.y, transform.rotation.z /* cc*/), 1 * Time.deltaTime);
				


		}


	void CheckPosition() {

		if ((XAngle > -90 && XAngle < 90) || (XAngle < -270 || XAngle > 270)) {	
			if ((XAngle > -45 && XAngle < 45) || (XAngle < -315 || XAngle > 315)) {	
				GameManager.GM.camPosX = GameManager.CamPosX.BACK;
			} else {
				if (XAngle < -45 && XAngle > -135)
					GameManager.GM.camPosX = GameManager.CamPosX.RIGHT;
				else if (XAngle > 45 && XAngle < 135)
					GameManager.GM.camPosX = GameManager.CamPosX.LEFT;
				else if (XAngle < -270 && XAngle > -315)
					GameManager.GM.camPosX = GameManager.CamPosX.LEFT;
			}
		} else {								
			if ((XAngle < -135 && XAngle > -225) || (XAngle > 135 && XAngle < 225)) {	
				GameManager.GM.camPosX = GameManager.CamPosX.FRONT;
			} else {
				if (XAngle > 225 && XAngle > -135)
					GameManager.GM.camPosX = GameManager.CamPosX.RIGHT;
				else if (XAngle > -135 && XAngle < -90)
					GameManager.GM.camPosX = GameManager.CamPosX.RIGHT;
				else if (XAngle < -225  && XAngle > -315)
					GameManager.GM.camPosX = GameManager.CamPosX.LEFT;
				else if (XAngle < 135  && XAngle > 90)
					GameManager.GM.camPosX = GameManager.CamPosX.LEFT;
			}
		} 
	}

}

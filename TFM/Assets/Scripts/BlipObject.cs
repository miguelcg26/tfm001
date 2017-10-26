/* CREADO POR MIGUEL CASADO */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BlipObject : MonoBehaviour
{
	public Transform Target;
	//Vector2 XRotation = Vector2.right;
	//Vector2 YRotation = Vector2.up;
	public float minScale = 1f;
	Vector3 blipPos;
	RectTransform myRectTransform;
	//public RectTransform parentRectTransform;

	//public Vector2 pointDebug;

	//public float offset;


	public void Start() {
		myRectTransform = GetComponent<RectTransform>();
	}

	public void LateUpdate() {


		//myRectTransform.position = MoveInside (new Vector2(blipPos.x, blipPos.y));
		//Vector2 newPosition = TransformPosition (Target.position);
		ClampToWindow();

		Vector3 rot = Vector3.zero - myRectTransform.localPosition;
		rot.Normalize ();
		float rot_z = Mathf.Atan2 (rot.y, rot.x) * Mathf.Rad2Deg;

		myRectTransform.localRotation = Quaternion.Euler (0f, 0f, rot_z + 90);
	}

	// Clamp panel to area of parent
	void ClampToWindow()
	{

		//Vector3 minPosition = parentRectTransform.rect.min - myRectTransform.rect.min;
		//Vector3 maxPosition = parentRectTransform.rect.max - myRectTransform.rect.max;

		float dot = Vector3.Dot( (Target.position - GameObject.Find ("PlayerCamera").GetComponent<Camera> ().transform.position).normalized, GameObject.Find ("PlayerCamera").GetComponent<Camera> ().transform.forward );
		if (dot <= 0) {// don't draw ui target marker 
			blipPos = manualWorldToScreenPointInverse (Target.position);
		} else {
			blipPos = manualWorldToScreenPoint (Target.position);
		}
			
		//Vector3 blipPos = GameObject.Find ("PlayerCamera").GetComponent<Camera> ().WorldToScreenPoint (Target.position);
		//Vector3 blipPos = manualWorldToScreenPoint (Target.position);



		//blipPos = new Vector3 (Mathf.Clamp (blipPos.x, -1800, 1800), Mathf.Clamp (blipPos.y, -1000, 1000), blipPos.z);

		blipPos.x = Mathf.Clamp(blipPos.x, 0.1f * Screen.width, 0.9f * Screen.width);
		blipPos.y = Mathf.Clamp(blipPos.y, 0.1f * Screen.height, 0.9f * Screen.height);


		//blipPos = Vector3.ClampMagnitude(blipPos, 0, 1);
		myRectTransform.position = blipPos;
		//blipPos.y = Mathf.Clamp(blipPos.y, -1000, 1000);
	}

	Vector3 manualWorldToScreenPoint(Vector3 wp) {
		// calculate view-projection matrix
		Matrix4x4 mat = GameObject.Find ("PlayerCamera").GetComponent<Camera> ().projectionMatrix * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().worldToCameraMatrix;

		// multiply world point by VP matrix
		Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

		if (temp.w == 0f) {
			// point is exactly on camera focus point, screen point is undefined
			// unity handles this by returning 0,0,0
			return Vector3.zero;
		} else {
			// convert x and y from clip space to window coordinates
			temp.x = (temp.x/temp.w + 1f)*.5f * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().pixelWidth;
			temp.y = (temp.y/temp.w + 1f)*.5f * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().pixelHeight;
			return new Vector3(temp.x, temp.y, wp.z);
		}
	}

	Vector3 manualWorldToScreenPointInverse(Vector3 wp) {
		// calculate view-projection matrix
		Matrix4x4 mat = GameObject.Find ("PlayerCamera").GetComponent<Camera> ().projectionMatrix * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().worldToCameraMatrix;

		// multiply world point by VP matrix
		Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

		if (temp.w == 0f) {
			// point is exactly on camera focus point, screen point is undefined
			// unity handles this by returning 0,0,0
			return Vector3.zero;
		} else {
			// convert x and y from clip space to window coordinates
			temp.x = (temp.x/temp.w + 1f)*.5f * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().pixelWidth;
			temp.y = (temp.y/temp.w + 1f)*.5f * GameObject.Find ("PlayerCamera").GetComponent<Camera> ().pixelHeight;
			return new Vector3(-temp.x, -temp.y, wp.z);
		}
	}
			//myRectTransform.localPosition = MoveInside (blipPos);

		

		//transform.position = new Vector3 (Mathf.Clamp (GameObject.Find("PlayerCamera").GetComponent<Camera>().WorldToScreenPoint(Target.position)));
	
		/*

		if (myRectTransform.position.x > Screen.width - offset && myRectTransform.position.y < Screen.height  && myRectTransform.position.y > 0 + offset) {
			
			myRectTransform.position = new Vector2 (Screen.width - offset, myRectTransform.position.y);

		} if (myRectTransform.position.y > Screen.height - offset && myRectTransform.position.x < Screen.width  && myRectTransform.position.x > 0 + offset) {
			
			myRectTransform.position = new Vector2 (myRectTransform.position.x, Screen.height - offset);

		} if (myRectTransform.position.x > Screen.width - offset && myRectTransform.position.y > Screen.height - offset) {
		
			myRectTransform.position = new Vector2 (Screen.width - offset, Screen.height - offset);

		} if (myRectTransform.position.x < 0 + offset && myRectTransform.position.y < Screen.height  && myRectTransform.position.y > 0 + offset) {

			myRectTransform.position = new Vector2 (0 + offset, myRectTransform.position.y);

		} if (myRectTransform.position.y < 0 + offset && myRectTransform.position.x < Screen.width  && myRectTransform.position.x > 0 + offset) {
			
			myRectTransform.position = new Vector2 (myRectTransform.position.x, 0 + offset);

		} if (myRectTransform.position.x < 0 + offset && myRectTransform.position.y < 0 + offset) {

			myRectTransform.position = new Vector2 (0 + offset, 0 + offset);
		}



	}

	public Vector2 TransformPosition(Vector3 position) {
		Vector3 offset = position - target.position;
		Vector2 newPosition = offset.x * XRotation;
		newPosition += offset.z * YRotation;


		return newPosition;
	}

	public Vector2 MoveInside(Vector2 point) {
		Rect mapRect = GetComponentInParent<RectTransform>().rect;
		point = Vector2.Max(point, mapRect.max);
		point = Vector2.Min(point, mapRect.min);

		//point = Vector2.Max(point, target.transform.position);

		//if (point.magnitude >= (mapRect.width / 2))
			//point = point.normalized * (mapRect.width / 2);


		pointDebug = point;

		return point;
	}
	*/

}

/* CREADO POR MIGUEL CASADO */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class blipCenterGUI : MonoBehaviour {
	
	public Transform Target;
    public bool keepInBounds = true;
    public bool lockScale;
    public bool lockRotation;
    public float minScale = 1f;

	MapCenterGUI map;
    RectTransform myRectTransform;
	public RectTransform rectTransformCenter;


	public Vector3 rot;

    public void Start() {
		map = GetComponentInParent<MapCenterGUI>();
        myRectTransform = GetComponent<RectTransform>();
    }

    public void LateUpdate() {
		if (Target != null) {
			Vector2 newPosition = map.TransformPosition (Target.position);

			if (keepInBounds) {
				newPosition = map.MoveInside (newPosition);
			}

			if (!lockScale) {
				float scale = Mathf.Max (minScale, map.zoomLevel);
				myRectTransform.localScale = new Vector3 (scale, scale, 1);
			}

			if (!lockRotation) {
				// myRectTransform.localEulerAngles = map.TransformRotation(Target.eulerAngles);

				Vector3 rot = rectTransformCenter.position - myRectTransform.position;
				rot.Normalize ();
				float rot_z = Mathf.Atan2 (rot.y, rot.x) * Mathf.Rad2Deg;

				myRectTransform.localRotation = Quaternion.Euler (0f, 0f, rot_z - 90);
			}

			myRectTransform.localPosition = newPosition;

		}
    }

}

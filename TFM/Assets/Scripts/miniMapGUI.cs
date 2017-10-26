/* CREADO POR MIGUEL CASADO */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class miniMapGUI : MonoBehaviour
{
    public Transform target;
    public float zoomLevel = 2f;
    public bool lockMapRotation;
    public Vector2 pointDebug;

	public float offset3D;

    Vector2 XRotation = Vector2.right;
    Vector2 YRotation = Vector2.up;

	public void LateUpdate () {

		if (target != null) {
			if (!lockMapRotation) {
				XRotation = new Vector2 (target.right.x, -target.right.z);
				YRotation = new Vector2 (-target.forward.x, target.forward.z);
			}
		}
    }


    public Vector2 TransformPosition(Vector3 position) {
		Vector3 offset = position - target.position;
        Vector2 newPosition = offset.x * XRotation;
        newPosition += offset.z * YRotation;

        newPosition *= zoomLevel;

        return newPosition;
    }

    public Vector3 TransformRotation(Vector3 rotation) {
        if (lockMapRotation)
            return new Vector3(0, 0, -rotation.y);
        else
            return new Vector3(0, 0, target.eulerAngles.y - rotation.y);

        
    }


    public Vector2 MoveInside(Vector2 point) {
        Rect mapRect = GetComponent<RectTransform>().rect;
        point = Vector2.Max(point, mapRect.min);
        point = Vector2.Min(point, mapRect.max);

		/*
        if (point.magnitude >= (mapRect.width / 2))
            point = point.normalized * (mapRect.width / 2);
		*/

        pointDebug = point;

        return point;
    }
}

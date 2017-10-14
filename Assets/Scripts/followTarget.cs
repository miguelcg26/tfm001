using UnityEngine;
using System;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float smoothLerp = 5f;
    public bool smoothON;

	[HideInInspector] public bool unfollow = false;

	public PlayerController plyCont;


    void FixedUpdate() {
		if (!unfollow) {
			if (smoothON) {
				if (plyCont.grounded) {
					transform.position = Vector3.Lerp (transform.position, target.transform.position + target.transform.up * 0.5f, smoothLerp * Time.deltaTime);
				} else {
					transform.position = Vector3.Lerp (transform.position, target.transform.position, 50 * Time.deltaTime);
				}
			} else {
				transform.position = target.transform.position + target.transform.up * 0.5f;
			}
		} else {
			transform.position = Vector3.Lerp (transform.position, target.transform.position, 0.5f * Time.deltaTime);
			GameObject.Find("PlayerCamera").transform.LookAt (target.transform.position);
		}
    }

}


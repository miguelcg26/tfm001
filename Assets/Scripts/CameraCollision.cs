using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CameraCollision : MonoBehaviour {
	public GameObject playerMesh;
		Transform cam;                          // the transform of the camera
		Transform pivot;                        // the point at which the camera pivots around
		public float dist;
		private float moveVelocity;             // the velocity at which the camera moved
		public float camVelocity;  //usado como velocidad de lerp de la camara, moveVelocity no tengo ni puta idea de que hace
		public float currentDist;               // the current distance from the camera to the target


		private Ray Ray;                        // the ray used in the lateupdate for casting between the camera and the target
		private RaycastHit[] Hits;              // the hits between the camera and the target
		// public ArrayList myArrayList = new ArrayList();
		private RayHitComparer rayHit;          // variable to compare raycast hit distances

		public float clipMoveTime = 0.05f;              // time taken to move when avoiding cliping (low value = fast, which it should be)
		public float returnTime = 0.4f;                 // time taken to move back towards desired position, when not clipping (typically should be a higher value than clipMoveTime)
		public float sphereCastRadius = 0.1f;           // the radius of the sphere used to test for object between camera and target
		public bool visualiseInEditor;                  // toggle for visualising the algorithm through lines for the raycast in the editor
		public float closestDistance = 0.5f;            // the closest distance the camera can be from the target
		public bool protecting { get; private set; }    // used for determining if there is an object between the target and the camera
		public string dontClipTag = "Player";           // don't clip against objects with this tag (useful for not clipping against the targeted object)
		public LayerMask collisionMask;
		public bool camColliding;

	public Shader transShader;
	public Shader normalShader;
	public float transparencyWeight;
	public float transparencyWeight2;

	public Color matColor;

	public Color matStandart;


		private void Start()
		{
			// find the camera in the object hierarchy
			cam = GetComponentInChildren<Camera>().transform;
			pivot = cam.parent;
		dist = 5.5f;
			currentDist = dist;

			// create a new RayHitComparer
			rayHit = new RayHitComparer();
		}


		private void FixedUpdate()
		{
			dist = GetComponent<cameraController> ().camDistance;
			// initially set the target distance
			float targetDist = dist;

			Ray.origin = pivot.position + pivot.forward*sphereCastRadius;
			Ray.direction = -pivot.forward;



			// initial check to see if start of spherecast intersects anything
			var cols = Physics.OverlapSphere(Ray.origin, sphereCastRadius);

			bool initialIntersect = false;
			bool hitSomething = false;


			// loop through all the collisions to check if something we care about
			for (int i = 0; i < cols.Length; i++)
			{
				if ((!cols[i].isTrigger) &&
					!(cols[i].attachedRigidbody != null && cols[i].attachedRigidbody.CompareTag(dontClipTag)))
				{
					initialIntersect = true;
					break;
				}
			}

			// if there is a collision
			if (initialIntersect)
			{
				Ray.origin += pivot.forward*sphereCastRadius;

				// do a raycast and gather all the intersections
				Hits = Physics.RaycastAll(Ray, dist - sphereCastRadius);
			}
			else
			{
				// if there was no collision do a sphere cast to see if there were any other collisions
				Hits = Physics.SphereCastAll(Ray, sphereCastRadius, dist + sphereCastRadius);
			}

			// sort the collisions by distance
			Array.Sort(Hits, rayHit);

			// set the variable used for storing the closest to be as far as possible
			float nearest = Mathf.Infinity;

			// loop through all the collisions
			for (int i = 0; i < Hits.Length; i++)
			{
				// only deal with the collision if it was closer than the previous one, not a trigger, and not tagged with the dontClipTag //AND not attached to a rigidbody 
				// Y A袮DO la condicion de la OpaqueCol
				if (Hits[i].distance < nearest && (!Hits[i].collider.isTrigger) &&
					//Hits[i].collider.gameObject.CompareTag(OpaqueCol)
					Hits[i].collider.gameObject.layer == collisionMask)
					//!Hits[i].collider.attachedRigidbody.CompareTag(dontClipTag))
					//!(Hits[i].collider.attachedRigidbody != null &&
				{
					// change the nearest collision to latest
					nearest = Hits[i].distance;
					targetDist = -pivot.InverseTransformPoint(Hits[i].point).z;
					hitSomething = true;
					// hitTransparentCol = false;

				}


			}


			// visualise the cam clip effect in the editor
		if (hitSomething)
		{
			if (currentDist < 2f) {
				playerMesh.GetComponent<Renderer> ().material.shader = transShader;
				transparencyWeight = Mathf.Lerp (transparencyWeight, 0f, 3.5f * Time.deltaTime);
			} else {
				transparencyWeight = Mathf.Lerp(transparencyWeight, 1f, 3.5f * Time.deltaTime);
			}
				
			Debug.DrawRay(Ray.origin, -pivot.forward*(targetDist + sphereCastRadius), Color.red);
			camColliding = true;

		}
		else
		{
			playerMesh.GetComponent<Renderer> ().material.shader = normalShader;
			transparencyWeight = Mathf.Lerp(transparencyWeight, 1f, 3.5f * Time.deltaTime);

			camColliding = false;


		}


		matColor.a = transparencyWeight;
		playerMesh.GetComponent<Renderer> ().material.color = matColor;

			// hit something so move the camera to a better position
			protecting = hitSomething;
			currentDist = Mathf.SmoothDamp(currentDist, targetDist, ref moveVelocity,
				currentDist > targetDist ? clipMoveTime : returnTime);
			currentDist = Mathf.Clamp(currentDist, closestDistance, dist);
			cam.localPosition = Vector3.Lerp(cam.localPosition,- Vector3.forward * currentDist, camVelocity * Time.deltaTime);

		}


		// comparer for check distances in ray cast hits
		public class RayHitComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				return ((RaycastHit) x).distance.CompareTo(((RaycastHit) y).distance);
			}
		}
	}



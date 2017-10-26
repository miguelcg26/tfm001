/* CREADO POR MIGUEL CASADO */

using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	//public GameObject attractorJump;
	[HideInInspector]
	public Rigidbody rigidBody;
	[HideInInspector]
	public Animator anim;

	public CameraController camController;
	
	public Transform camRig;
	public Transform character;
	//[SerializeField]
	public float moveSpeed = 40f;
	//[SerializeField]
	public float turnSpeed = 4.5f;
	//[SerializeField]
	public float moveSpeedAir = 2f;
	//[SerializeField]
	public float turnSpeedAir = 0.5f;
	//float sprintInput;

	
	public float horizontal;
	public float vertical;
		
	public float speed;
	public float direction;
	public float angle;
	public float angleForward;
	//public float animSpeed;
	//public float animAngle;
	public int posture;
	public float animAngle;

	bool sprinting;

	public Vector3 dir;
	[HideInInspector]
	public Vector3 dirPos;
	public Vector3 bodyUp;
	public Vector3 bodyForward;
	public Vector3 objectDir;
	public Vector3 normalDwn;
	public Vector3 normalUp;
	public Vector3 normalFwd;
	public bool onTrunk;
	public bool onBranch;
	public bool onSubBranch;
	public bool isVertical;
	public bool isJumping;
	public bool obstacleFwd;
	
	Vector3 currentRot;
	Vector3 targetRot;
	public float sign;
	//bool onGround;

	public Vector3 v;
	public Vector3 h;
	public Vector3 vTemp;
	public Vector3 hTemp;

	[Header ("Ground Check")]
	public bool grounded = false;
	public LayerMask groundCheckLayer;
	public float rayDownDistance = 0.5f;
	public CapsuleCollider capCol;
	public PhysicMaterial noFriction;
	public PhysicMaterial friction;
	public bool freeFall;
	public float distanceDwn;


	void Awake()
	{
		//attractorJump = GameObject.Find ("Attractor_Branch1");
		//GameManager.GM.currentAttractor = attractorJump;


		rigidBody = GetComponent<Rigidbody>();
		//camRig = GameObject.FindWithTag("MainCamera").transform.parent.parent;
		//cam = GameObject.FindWithTag("MainCamera").transform.parent.parent.GetComponent<cameraController>();
		//capCol = GetComponent<CapsuleCollider>();
		anim = GetComponent<Animator>();

		SetupAnimator();
	}
	
	void Start(){

		//Localizamos el punto definido como entrada de la escena
		GameObject startPosition = GameObject.Find (DataManager.DM.data.startPosition);

		//Si ha encontrado la posición de inicio
		if (startPosition) {
			//Posicionamos el jugador en ese punto
			transform.position = startPosition.transform.position;

			//Rotamos al jugador en la dirección a la que apunta la posición inicial
			transform.rotation = startPosition.transform.rotation;
		} else {
			//En caso contrario, avisamos de que no existe el punto
			Debug.LogWarning ("No se ha localizado la posición de inicio: " + DataManager.DM.data.startPosition);
		}
	}
	
	
	void Update()
	{
		bodyUp = transform.up;
		bodyForward = transform.forward;

		//transform.position = new Vector3 (transform.position.x, transform.position.y + (bodyUp.y * 0.1f), transform.position.z);

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");


				
		if (GameManager.GM.charPos != GameManager.CharPos.VERTICAL) {

			if (GameManager.GM.charPos != GameManager.CharPos.TOP && (GameManager.GM.camPosX == GameManager.CamPosX.BACK || GameManager.GM.camPosX == GameManager.CamPosX.FRONT)) {

				if (!GameManager.GM.charWalkingUp) {
					v = camRig.forward * vertical;
					h = camRig.right * horizontal;	 //	h = -camRig.right * horizontal;
				} else {
					v = camRig.forward * vertical;
					h = camRig.right * horizontal;	
				}
			} else if (GameManager.GM.charPos == GameManager.CharPos.TOP) {
				
				v = camRig.forward * vertical;
				h = camRig.right * horizontal;	

			} else {
				v = camRig.forward * vertical;
				h = camRig.right * horizontal;
			}
		}  else {
			v = camRig.forward * vertical;
			h = camRig.right * horizontal;
		}




		// Find the direction from that position
		dir = transform.position + (v + h).normalized - transform.position;
		//dir.y = 0;

		StartCoroutine (CharWalking ());

		if (Mathf.Abs (horizontal) > 0.0f || (Mathf.Abs (vertical) > 0.0f)) {
			capCol.material = noFriction;
			objectDir =	Vector3.Cross (transform.forward, dir);
			// And if it's not zero (so that we avoid a warning)
			if (bodyUp.y > 0)
				sign = (objectDir.y > 0) ? 1.0f : -1.0f;
			else
				sign = (objectDir.y > 0) ? -1.0f : 1.0f;
			//angle = Quaternion.Angle (transform.rotation, Quaternion.LookRotation (dir));

			angle = Vector3.Angle (transform.forward, dir) * sign;

			animAngle = angle;

			Vector3 dirForward = Vector3.Cross (Vector3.forward, dir);
			float signForward;
			if (bodyUp.y > 0)
				signForward = (dirForward.y > 0) ? 1.0f : -1.0f;
			else
				signForward = (dirForward.y > 0) ? -1.0f : 1.0f;
			
			angleForward = Vector3.Angle (Vector3.forward, dir) * signForward;

		} 
		else {
			capCol.material = friction;
			animAngle = 0;
			GameManager.GM.charWalkingDown = false;
			GameManager.GM.charWalkingUp = false;
		}



		//dirPos = new Vector3 (transform.forward.x * 0.5f + horizontal, 0, transform.forward.z * 0.5f + vertical);
		dirPos = transform.forward + (v + h);
		dirPos = (dirPos.magnitude > 1.0f) ? dirPos.normalized : dirPos;
		
		speed = dir.magnitude; // + sprintInput;
		speed = Mathf.Clamp(speed, 0f, 3);
		// animSpeed = Mathf.Clamp(speed, 0.4f, 1);





		/*
		if (Input.GetButtonDown("Jump2")) {
			Jump2 ();
		}
		*/

		CheckPosition ();
		Raycasting ();



		if (SceneController.SC.blockAction) {
			return;
		}

		anim.SetFloat ("Angle", animAngle);
		anim.SetInteger ("Posture", posture);

		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}

	}
	


	public void Jump() {   		
		if (grounded)	
		//if (GameManager.GM.charPos == GameManager.CharPos.TOP) {
		//rigidBody.AddForce(Vector3.up * jumpForce + transform.up * jumpForce, ForceMode.Impulse);
			anim.SetTrigger("Jump");	
		isJumping = true;

		//}
	}

	
	void FixedUpdate() {	

		//anim.SetBool("Grounded", grounded);	

		if (SceneController.SC.blockAction) {
			return;
		}
				if (grounded) {
			
					if (Mathf.Abs (horizontal) > 0.0f || (Mathf.Abs (vertical) > 0.0f)) {
							
						// MOVEMENT	
						//if (anim.GetCurrentAnimatorStateInfo (0).IsTag ("Run")){
						rigidBody.AddForce (new Vector3(dir.x, normalDwn.y, dir.z) * moveSpeed);						
						//}

						//ROTATION

					    //transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);

						//if (!cc)
						//character.transform.rotation =  Quaternion.Slerp (character.transform.rotation, Quaternion.FromToRotation (character.transform.up, normalUp) * character.transform.rotation, 50 * Time.deltaTime);
						
						//else
						//transform.rotation =  Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalFwd) * transform.rotation, 50 * Time.deltaTime);

					}
			character.localRotation = Quaternion.Slerp (character.localRotation, Quaternion.Euler (0, angle, 0), turnSpeed * Time.deltaTime);

			transform.rotation =  Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 10f * Time.deltaTime);


				} else {
					if (Mathf.Abs (horizontal) > 0.0f || (Mathf.Abs (vertical) > 0.0f)) {

						// MOVEMENT						
						rigidBody.AddForce (dir * moveSpeedAir);						

						//ROTATION
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), turnSpeedAir * Time.deltaTime);
						character.localRotation = Quaternion.Slerp (character.localRotation, Quaternion.Euler (0, angle, 0), turnSpeedAir * Time.deltaTime);

					}

				
			}


		CheckGrounded ();
	}



	void SetupAnimator()
	{
		// This is a ref to the animator component on the root
		anim = GetComponent<Animator>();

		// We use the avatar from the first child if present
		foreach (var childAnimator in GetComponentsInChildren<Animator>())
		{

			if (childAnimator != anim)
			{
				anim.avatar = childAnimator.avatar;
				anim.runtimeAnimatorController = childAnimator.runtimeAnimatorController;
				Destroy(childAnimator);
				break; // if you find the first animator, stop searching
			}
		}

	}


	void CheckGrounded()
	{  
		
		if (Physics.Raycast (transform.position, -transform.up, rayDownDistance, groundCheckLayer)) {
			rigidBody.drag = 10;
			rigidBody.useGravity = false;
			grounded = true;
		} else {
			//rigidBody.drag = 0;
			//rigidBody.useGravity = true;
			//grounded = false;
		}

		if (grounded) {
			rigidBody.AddForce (-normalUp * 1000);
		} else {

			//rigidBody.AddForce (-normalUp * 1000);
			rigidBody.AddForce (-Vector3.up * 100);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (transform.rotation.x, transform.rotation.y, transform.rotation.z /* cc*/), 1 * Time.deltaTime);
		}



	}

	void Raycasting(){
		
		RaycastHit hitFwd;

		RaycastHit hitDwn;

		RaycastHit distDwn;

		if (Physics.Raycast (transform.position + transform.up * 0.2f, character.transform.forward, out hitFwd, 1.5f)) {
			//Debug.Log(hitDwn.transform.tag);

			if (hitFwd.transform.tag != ("Branch") && hitFwd.transform.tag != ("Trunk")) {
				obstacleFwd = true;
			} else {
				obstacleFwd = false;
				normalFwd = hitFwd.normal;
			}

		} else {
			obstacleFwd = false;
		}



			if (Physics.Raycast (transform.position, -transform.up, out hitDwn, rayDownDistance)) {
				//Debug.Log(hitDwn.transform.tag);
				if (grounded) {
					if (hitDwn.transform.tag == ("Branch01")) {
					//	GameManager.GM.currentAttractor = GameManager.GM.attractors [0];
//						GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;
					} else if (hitDwn.transform.tag == ("Branch02")) {
						//GameManager.GM.currentAttractor = GameManager.GM.attractors [1];
						//GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;
					} else if (hitDwn.transform.tag == ("Trunk")) {
						//GameManager.GM.currentAttractor = GameManager.GM.attractors [2];
						//GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;
					}

				normalDwn = hitDwn.normal;
				normalDwn = Quaternion.AngleAxis (90, character.transform.right) * normalDwn;
			//	Collision col = capCol.;
				//ContactPoint[] cont = col.contacts;
				//normalUp = cont [0].normal;
				normalUp = hitDwn.normal;
				//Debug.Log (hitDwn.collider.name);
				//Debug.DrawRay(transform.position + transform.up * 2, normalUp, Color.magenta);
				} 

		}

		Physics.Raycast (transform.position, -Vector3.up, out distDwn);
		distanceDwn = distDwn.distance;
		if (distanceDwn > 6) {
			freeFall = true;
		} else {
		}

	}
		

	void OnCollisionEnter(Collision col){
		
		if (col.collider.tag == "Branch") {

			freeFall = false;

		anim.SetBool("Grounded", true);
			//if (!cc) {
		StartCoroutine (AttractorChangerBranch());
		/*
			float countercc = 0;
			countercc += 1f * Time.deltaTime;
			if (countercc > 1) {
				cc = false;
				countercc = 0;
			}

			*/	
		isJumping = false;
		grounded = true;
				rigidBody.velocity = Vector3.zero;
				//GameManager.GM.currentAttractor = GameManager.GM.attractors [1];
				//GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;
				
				//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 50 * Time.deltaTime);


			//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 5 * Time.deltaTime);

			if (onTrunk && !onBranch || onSubBranch && !onBranch) {
				ContactPoint[] cont = col.contacts;
				normalUp = cont [0].normal;
				//Debug.Log (col.collider.tag);
				transform.rotation = Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation;
			}
			
				rigidBody.drag = 10;
				rigidBody.useGravity = false;

			}

		if (col.collider.tag == "SubBranch") {

			freeFall = false;
			anim.SetBool("Grounded", true);
			//if (!cc) {
			StartCoroutine (AttractorChangerSubBranch());
			/*
			float countercc = 0;
			countercc += 1f * Time.deltaTime;
			if (countercc > 1) {
				cc = false;
				countercc = 0;
			}

			*/	
			isJumping = false;
			grounded = true;
			rigidBody.velocity = Vector3.zero;
			//GameManager.GM.currentAttractor = GameManager.GM.attractors [1];
			//GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;

			//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 50 * Time.deltaTime);


			//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 5 * Time.deltaTime);

			if (onTrunk && !onSubBranch || onBranch && !onSubBranch) {
				ContactPoint[] cont = col.contacts;
				normalUp = cont [0].normal;
				//Debug.Log (col.collider.tag);
				transform.rotation = Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation;
			}

			rigidBody.drag = 10;
			rigidBody.useGravity = false;

		}

		if (col.collider.tag == "Trunk") {

			freeFall = false;
			anim.SetBool("Grounded", true);
			//if (!cc) {
			StartCoroutine (AttractorChangerTrunk());
			/*
			float countercc = 0;
			countercc += 1f * Time.deltaTime;
			if (countercc > 1) {
				cc = false;
				countercc = 0;
			}

			*/	
			isJumping = false;
			grounded = true;
			rigidBody.velocity = Vector3.zero;
			//GameManager.GM.currentAttractor = GameManager.GM.attractors [1];
			//GameManager.GM.currentAttractor.GetComponent<followTarget1> ().onBranch = true;

			//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 50 * Time.deltaTime);


			//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation, 5 * Time.deltaTime);

			if (!onTrunk && onBranch || !onTrunk && onSubBranch) {
				ContactPoint[] cont = col.contacts;
				normalUp = cont [0].normal;
				//Debug.Log (col.collider.tag);
				transform.rotation = Quaternion.FromToRotation (transform.up, normalUp) * transform.rotation;

			}

			rigidBody.drag = 10;
			rigidBody.useGravity = false;

		}
	}

	void OnCollisionExit(Collision col){

		if (col.collider.tag == "Branch" || col.collider.tag == "Trunk" || col.collider.tag == "SubBranch") {
			if (isJumping) {
				anim.SetBool ("Grounded", false);	
				//GameManager.GM.currentAttractor = null;
				rigidBody.useGravity = true;
				grounded = false;
				rigidBody.drag = 2;
				//Debug.Log ("vv");

			}
		}

	}

	void CheckPosition () {
		if (!isVertical) {
			if (bodyUp.y > -0.09f) {	//TOP
				if (bodyUp.x < 0.4f && bodyUp.x > -0.4f) {
					GameManager.GM.charPos = GameManager.CharPos.TOP;
					posture = 0;
				} else {
					if (bodyUp.x > 0.6f) {
						GameManager.GM.charPos = GameManager.CharPos.RIGHT_TOP;
						posture = 1;
					} else if (bodyUp.x < -0.6f) {
						GameManager.GM.charPos = GameManager.CharPos.LEFT_TOP;
						posture = 6;
					}			
				}

				if (bodyForward.x > 0) {
					GameManager.GM.charDir = GameManager.CharDir.RIGHT;
				} else {
					GameManager.GM.charDir = GameManager.CharDir.LEFT;
				}

			} else {			//BOTTOM
				if (bodyUp.x < 0.55f && bodyUp.x > -0.55f) {
					GameManager.GM.charPos = GameManager.CharPos.BOTTOM;
					posture = 4;
				} else {
					if (bodyUp.x > 0.55f) {
						GameManager.GM.charPos = GameManager.CharPos.RIGHT_BOTTOM;
						posture = 3;
					} else if (bodyUp.x < -0.55f) {
						GameManager.GM.charPos = GameManager.CharPos.LEFT_BOTTOM;
						posture = 5;
					}			
				}

				if (bodyForward.x < 0) {
					GameManager.GM.charDir = GameManager.CharDir.RIGHT;
				} else {
					GameManager.GM.charDir = GameManager.CharDir.LEFT;
				}
			}
		}else {
			GameManager.GM.charPos = GameManager.CharPos.VERTICAL;
			posture = 8;
		}
	}

	void OnAnimatorMove()
	{
		if (SceneController.SC.blockAction) {
			anim.SetFloat ("Speed", 0);
			return;
		}
		if (!obstacleFwd) {
			anim.SetFloat ("Speed", speed, 0.25f * Time.deltaTime, 0.25f * Time.deltaTime);
		} else {
			anim.SetFloat ("Speed", 0);
		}
		anim.SetFloat ("Horizontal", horizontal, Time.deltaTime, Time.deltaTime);
		anim.SetFloat("Vertical", vertical, Time.deltaTime, Time.deltaTime);
		anim.SetFloat("Direction", direction, Time.deltaTime, Time.deltaTime);
		anim.SetBool("FreeFall", freeFall);
	}

	void OnDrawGizmos()
	{
		Debug.DrawRay(transform.position + transform.up * 2.5f, dir, Color.red);
		Debug.DrawRay(transform.position, -transform.up * 0.2f, Color.red);
		Debug.DrawRay(transform.position + transform.up * 2.5f, transform.forward, Color.blue);
		Debug.DrawRay(transform.position + transform.up * 2, normalDwn, Color.magenta);
		Debug.DrawRay(transform.position + transform.up * 2, normalUp, Color.yellow);

	}



	IEnumerator CharWalking() {
		
		if (GameManager.GM.charPos == GameManager.CharPos.TOP || GameManager.GM.charPos == GameManager.CharPos.LEFT_TOP || GameManager.GM.charPos == GameManager.CharPos.RIGHT_TOP || GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM || GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM && (GameManager.GM.camPosX == GameManager.CamPosX.BACK || GameManager.GM.camPosX == GameManager.CamPosX.FRONT)) {
			yield return new WaitForSeconds (1f);
			if (horizontal != 0 || vertical != 0)
			GameManager.GM.charWalkingUp = true;

		}

		if (GameManager.GM.charPos == GameManager.CharPos.BOTTOM  || GameManager.GM.charPos == GameManager.CharPos.LEFT_BOTTOM || GameManager.GM.charPos == GameManager.CharPos.RIGHT_BOTTOM && (GameManager.GM.camPosX == GameManager.CamPosX.BACK || GameManager.GM.camPosX == GameManager.CamPosX.FRONT)) {
			yield return new WaitForSeconds (0.0f);	
			if (horizontal == 0 && vertical == 0)
				GameManager.GM.charWalkingUp = false;

		}
	}
	IEnumerator AttractorChangerBranch(){		
		isVertical = false;
		yield return new WaitForSeconds (1f);
		onTrunk = false;
		onBranch = true;
		onSubBranch = false;
	}

	IEnumerator AttractorChangerSubBranch(){
		isVertical = false;
		yield return new WaitForSeconds (1f);
		onTrunk = false;
		onBranch = false;
		onSubBranch = true;
	}

	IEnumerator AttractorChangerTrunk(){
		isVertical = true;
		yield return new WaitForSeconds (1f);
		onTrunk = true;
		onBranch = false;
		onSubBranch = false;
	}

}
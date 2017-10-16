using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundResetLevel : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			GameObject.Find ("PlayerCamera").transform.parent.parent.parent.parent.GetComponent<FollowTarget> ().unfollow = true;
			SceneController.SC.FadeAndLoadSameScene (DataManager.DM.data.startPosition);
		}
	}
}

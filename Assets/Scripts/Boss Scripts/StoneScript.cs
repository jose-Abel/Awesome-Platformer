using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Deactivate", 4f);
	}

	void Deactivate() {
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == MyTags.PLAYER_TAG) {

			target.GetComponent<PlayerDamage> ().DealDamage ();

			gameObject.SetActive (false);
		}
	}
}

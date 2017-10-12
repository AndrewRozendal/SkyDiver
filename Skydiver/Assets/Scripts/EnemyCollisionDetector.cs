using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetector : MonoBehaviour {

	public bool debugMode = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Detect collisions
	void OnTriggerEnter(Collider other){
		PlayerCharacter player = other.GetComponent<PlayerCharacter> ();
		if (player != null) {
			player.hit ();
			if (debugMode) {
				Debug.Log ("I hit a player");
			}
		}
		Debug.Log ("dying");
		die ();
	}

	//Destroys the GameObject
	public void die (){
		Destroy (this.gameObject);
		if (debugMode) {
			Debug.Log ("Dead");
		}
	}
}

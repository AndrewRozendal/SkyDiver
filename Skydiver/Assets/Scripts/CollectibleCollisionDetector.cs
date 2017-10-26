using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollisionDetector : MonoBehaviour {

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
			Messenger.Broadcast(GameEvent.COIN_COLLECTED);
			if (debugMode) {
				Debug.Log ("Player collected coin");
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

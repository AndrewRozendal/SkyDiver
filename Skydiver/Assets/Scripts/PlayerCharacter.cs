using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
	private int health;
	public int maxHealth = 1;
	public bool healthOn = true;
	public bool debugMode = true;

	void Start(){
		health = maxHealth;
	}

	public void hit(){
		if (debugMode) {
			Debug.Log ("player hit, updating health");
		}
		if(healthOn){
			health -= 1;
			if (debugMode) {
				Debug.Log ("health: " + health);
			}

			if(health <= 0) {
				// Player is dead
				Messenger.Broadcast(GameEvent.PLAYER_DEAD);
			} else {
				// Update UI
			}
		}
	}

	public void firstAid(){
		if (health < maxHealth) {
			health++;
			if (health > maxHealth) {
				health = maxHealth;
			}
		}
	}
}
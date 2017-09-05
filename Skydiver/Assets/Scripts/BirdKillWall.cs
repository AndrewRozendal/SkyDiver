using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKillWall : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		BirdMove bird = other.GetComponent<BirdMove>();
		bird.Die ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKillWall : MonoBehaviour {

	[SerializeField] private GameObject birdEnemy;

	void OnTriggerEnter (Collider other){
		BirdMove bird = birdEnemy.GetComponent<BirdMove>();
		bird.Die ();

	}
}

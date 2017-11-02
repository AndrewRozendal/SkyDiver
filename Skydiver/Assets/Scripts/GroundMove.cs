using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour {
	private Vector3 destination;
	private float groundSpeed = 1f;
	// Use this for initialization
	void Start () {
		destination = new Vector3 (0, 0, -10);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(destination * groundSpeed * Time.deltaTime);
	}
}

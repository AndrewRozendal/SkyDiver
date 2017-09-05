using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState {Dead, Alive};

public class BirdMove : MonoBehaviour {

	public float birdSpeed;
	public float pushForce = 5.0f;
	private BirdState state;

	// Use this for initialization
	void Start () {
		state = BirdState.Alive;
		birdSpeed = Random.Range (1f, 20f);
	}
	
	// Update is called once per frame
	void Update () {
		if (state == BirdState.Alive){
			transform.Translate (Vector3.back * birdSpeed * Time.deltaTime);

		}
		if (state == BirdState.Dead) {
			Die ();
		}
	}

	public void ChangeState(BirdState state){
		this.state = state;
		Debug.Log ("State= " + state);
	}
		


	public void Die (){
		Destroy (this.gameObject);
		Debug.Log ("Dead");

	}
	void OnControllerColliderHit(ControllerColliderHit hit){
		Rigidbody body = hit.collider.attachedRigidbody;

		if (body != null && !body.isKinematic) {
			body.velocity = hit.moveDirection * pushForce;
		}

	}
}

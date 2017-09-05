using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {

    private CharacterController charController;
    public float speed = 10.0f;
	public float pushForce = 5.0f;
    // Use this for initialization

    void Start () {
        charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, deltaY, 0);
        //clamp magnitude for diagonal movement
        movement = Vector3.ClampMagnitude(movement, speed);

        // movement code Frame rate independent

        movement *= Time.deltaTime;

        //convert local to global coordinates
        movement = transform.TransformDirection(movement);

        charController.Move(movement);
    }
	void OnControllerColliderHit(ControllerColliderHit hit){
		Rigidbody body = hit.collider.attachedRigidbody;

		if (body != null && !body.isKinematic) {
			body.velocity = hit.moveDirection * pushForce;
		}

	}
}

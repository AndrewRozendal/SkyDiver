using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour {
    public bool debugMode = true;
    private CharacterController charController;
    public float speed = 5.0f;
	public float pushForce = 5.0f;

    //Screen boundaries
    public float maxXPosition = 9.5f;
    public float minXPosition = -9.5f;
    public float maxYPosition = 9.5f;
    public float minYPosition = -9.5f;

    void Start () {
        charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //where are we
        Transform currentLocation = this.GetComponent<Transform>();
        if (debugMode) { Debug.Log("Current character location: " + currentLocation.position); }

        //grab how far player want to move
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaY = Input.GetAxis("Vertical") * speed;

        if (debugMode)
        {
            Debug.Log("Horizontal movement requested: " + deltaX);
            Debug.Log("Vertical movement requested: " + deltaY);
        }

        float actualMovementX, actualMovementY;

        //limit player movement to prevent getting out of bounds

        //horizontal checks
        if((deltaX + currentLocation.position.x) > maxXPosition)
        {
            if (debugMode) { Debug.Log("requested x movement is too large(+), clamping"); }
            actualMovementX = maxXPosition - currentLocation.position.x;
        } else if((deltaX + currentLocation.position.x) < minXPosition)
        {
            if (debugMode) { Debug.Log("requested x movement is too large(-), clamping"); }
            actualMovementX = minXPosition - currentLocation.position.x;
        } else
        {
            if (debugMode) { Debug.Log("requested x movement is inbounds"); }
            actualMovementX = deltaX;
        }
        if (debugMode) { Debug.Log("Moving character horizontally " + actualMovementX); }

        //vertical checks
        if ((deltaY + currentLocation.position.y) > maxYPosition)
        {
            actualMovementY = maxYPosition - currentLocation.position.y;
        }
        else if ((deltaY + currentLocation.position.y) < minYPosition)
        {
            actualMovementY = minYPosition - currentLocation.position.y;
        }
        else
        {
            actualMovementY = deltaY;
        }
        if (debugMode) { Debug.Log("Moving character vertically " + actualMovementY); }

        float actualMovementZ = 0;
        Vector3 actualMovement = new Vector3(actualMovementX, actualMovementY, actualMovementZ);

        //clamp magnitude for diagonal movement
        actualMovement = Vector3.ClampMagnitude(actualMovement, speed);

        // movement code Frame rate independent
        actualMovement *= Time.deltaTime;

        //convert local to global coordinates
        actualMovement = transform.TransformDirection(actualMovement);
        charController.Move(actualMovement);
    }

    /*
    void OnControllerColliderHit(ControllerColliderHit hit){
		Rigidbody body = hit.collider.attachedRigidbody;

		if (body != null && !body.isKinematic) {
			body.velocity = hit.moveDirection * pushForce;
		}

	}
    */
}

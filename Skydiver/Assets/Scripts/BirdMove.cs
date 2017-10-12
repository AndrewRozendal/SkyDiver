using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState {Dead, Alive};

public class BirdMove : MonoBehaviour {

    public bool debugMode = true;
    public float maxDistance = 10f;
	private float birdSpeed;
    public float maxSpeed = 2f;
    public float minSpeed = 1f;
	public float pushForce = 5.0f;
	private BirdState state;
    private Vector3 destination;

	public int quad { get; set; }

	// Use this for initialization
	public void Start () {
		state = BirdState.Alive;
		birdSpeed = Random.Range (minSpeed, maxSpeed);
        destination = getDestination();
	}
	
	// Update is called once per frame
	public void Update () {
		if (state == BirdState.Alive) {
            //are we in bounds?
            bool inbounds = true;
            Transform location = this.GetComponent<Transform>();
            if(location.position.x > maxDistance || location.position.x < maxDistance * -1)
            {
                if (debugMode) {
                    Debug.Log("Bird was too far on x");
                    Debug.Log("Position is: " + location.position);
                }
                inbounds = false;
            } else if (location.position.y > maxDistance || location.position.y < maxDistance * -1)
            {
                if (debugMode) {
                    Debug.Log("Bird was too far on y");
                    Debug.Log("Position is: " + location.position);
                }
                inbounds = false;
            }
            else if (location.position.z > maxDistance || location.position.z < maxDistance * -1)
            {
                if (debugMode) {
                    Debug.Log("Bird was too far on z");
                    Debug.Log("Position is: " + location.position);
                    Debug.Log("location.position.z > maxDistance evaluates to " + (location.position.z > maxDistance));
                    Debug.Log("location.position.z < maxDistance * -1 evaluates to " + (location.position.z < maxDistance * -1));
                    Debug.Log("maxDistance is " + maxDistance);
                }
                inbounds = false;
            }

            if (!inbounds)
            {
                state = BirdState.Dead;
            } else
            {
                Move();
            }	
		}

		if (state == BirdState.Dead) {
			this.GetComponent<EnemyCollisionDetector> ().die();
		}
	}

    //Changes the state of the bird
	private void ChangeState(BirdState state){
		this.state = state;
		Debug.Log ("State= " + state);
	}

    private void Move()
    {
        if (debugMode) { Debug.Log("Moving bird to " + destination + " at speed " + birdSpeed); }
        transform.Translate(destination * birdSpeed * Time.deltaTime);
    }


    //Generate random destination coordinates
    private Vector3 getDestination()
    {
		//Pick the appropriate quadrant
		float x = 0f;
		float y = 0f;
		switch (quad) {
		case 1:
			x = Random.Range (-10f, -5f);
			y = Random.Range (-10f, -5f);
			break;

		case 2:
			x = Random.Range (-10f, -5f);
			y = Random.Range (5f, 10f);
			break;

		case 3:
			x = Random.Range (5f, 10f);
			y = Random.Range (5f, 10f);
			break;
		case 4:
			x = Random.Range (5f, 10f);
			y = Random.Range (-10f, -5f);
			break;
		}
			
        float z = (maxDistance + 1) * -1;
        return new Vector3(x, y, z);
    }
}

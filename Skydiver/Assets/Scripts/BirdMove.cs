using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState {Dead, Alive};

public class BirdMove : MonoBehaviour {
    public bool debugMode = true;
    public float maxDistance = 100f;
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

            if (inbounds)
            {
                transform.Translate(Vector3.back * birdSpeed * Time.deltaTime);
            } else
            {
                state = BirdState.Dead;
            }	
		}

		if (state == BirdState.Dead) {
			Die ();
		}
	}

    //Changes the state of the bird
	public void ChangeState(BirdState state){
		this.state = state;
		Debug.Log ("State= " + state);
	}

    //Destroys the GameObject
	public void Die (){
		Destroy (this.gameObject);
		Debug.Log ("Dead");
	}
}

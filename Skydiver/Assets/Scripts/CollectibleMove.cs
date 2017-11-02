using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinState {Inactive, Active};

public class CollectibleMove : MonoBehaviour {
	public bool debugMode;
	private CoinState state;
	private float coinSpeed;
    public float maxDistance { get; set; }
    private float maxSpeed;
    private float minSpeed;
	private Vector3 destination;
	private Transform location;
	
	// Use this for initialization
	void Start () {
        maxSpeed = 0.2f;
        minSpeed = 0.01f;
		state = CoinState.Active;
		coinSpeed = Random.Range(minSpeed, maxSpeed);
		if (debugMode) {
			Debug.Log ("coinSpeed: " + coinSpeed);
		}
		location = this.GetComponent<Transform>();
		destination = new Vector3 (location.position.x, location.position.y, (maxDistance * -1 )- 1);
	}
	
	// Update is called once per frame
	void Update () {
		 if(state == CoinState.Active){
			 bool inbounds = true;
			 if (location.position.z > maxDistance || location.position.z < maxDistance * -1)
             {
                 if (debugMode) {
                     Debug.Log("Coin was too far on z");
                     Debug.Log("Position is: " + location.position);
                     Debug.Log("location.position.z > maxDistance evaluates to " + (location.position.z > maxDistance));
                     Debug.Log("location.position.z < maxDistance * -1 evaluates to " + (location.position.z < maxDistance * -1));
                     Debug.Log("maxDistance is " + maxDistance);
                 }
                 inbounds = false;
             }

             if (!inbounds)
             {
                 state = CoinState.Inactive;
             } else
             {
                 Move();
             }	
		 } else {
			 this.GetComponent<CollectibleCollisionDetector>().die();
		 }
	}
	
	private void Move(){
		if (debugMode) { Debug.Log("Moving coin to " + destination + " at speed " + coinSpeed); }
		transform.Translate(destination * coinSpeed * Time.deltaTime);
	}
}

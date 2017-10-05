using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public class Coordinates
    {
        public Coordinates(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float x { get; private set; }
        public float y { get; private set; }
    }

    [SerializeField] private GameObject _BirdPrefab;
	private GameObject Bird;
	public int maxNumBirds = 5;
    public float enemySpawnPlane = 30f;
	private int quad;
	public int frequency = 15;
	public bool debugMode = true;

    //Array to hold the birds
    private static GameObject[] _numBirds;

	public void Start () {
		_numBirds = new GameObject[maxNumBirds];
		quad = 1;
	}
	

	public void Update () {
		calculateCurrentQuad ();
		for (int i = 0; i < maxNumBirds; i++) {
			wait ();
			if (_numBirds [i] == null) {
                //Birds starting and ending coordinates
                Coordinates origin = getCoordinates();
				Bird = Instantiate (_BirdPrefab) as GameObject;
				transform.Rotate (180, 0, 0);
				_numBirds [i] = Bird;
				Bird.transform.position = new Vector3 (origin.x, origin.y, enemySpawnPlane);
				Bird.GetComponent<BirdMove> ().quad = quad;
			}
		}
    }


	// Calclulates the quadrent that we should spawn enemies in
	private void calculateCurrentQuad() {
		int result = (int)Time.time % 60;
		if (debugMode) {
			Debug.Log ("Game time:" + result);
		}
		if (result > 45) {
			quad = 4;
		} else if (result > 30) {
			quad = 3;
		} else if (result > 15) {
			quad = 2;
		} else {
			quad = 1;
		}
	}

	private IEnumerator wait(){
		yield return new WaitForSeconds (10);
	}

    //generates coordinates for the enemies to spawn on
	private Coordinates getCoordinates(){
		//What quadrant to spawn in?
		float x = 0;
		float y = 0;

		switch (quad) {

		case 1:
			x = Random.Range (0f, 10f);
			y = Random.Range (0f, 10f);
			break;

		case 2:
			x = Random.Range (0f, 10f);
			y = Random.Range (-10f, 0f);
			break;

		case 3:
			x = Random.Range (-10f, 0f);
			y = Random.Range (-10f, 0f);
			break;
		case 4:
			x = Random.Range (-10f, 0f);
			y = Random.Range (0f, 10f);
			break;
		}
			
        Coordinates origin = new Coordinates(x, y);
        return origin;
	}
		
}
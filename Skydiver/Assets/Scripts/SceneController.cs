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

    //Array to hold the birds
    private static GameObject[] _numBirds;

	public void Start () {
		_numBirds = new GameObject[maxNumBirds]; 
	}
	

	public void Update () {
		for (int i = 0; i < maxNumBirds; i++) {
			wait ();
			if (_numBirds [i] == null) {
                //Birds starting and ending coordinates
                Coordinates origin = getCoordinates();
				Bird = Instantiate (_BirdPrefab) as GameObject;
				transform.Rotate (180, 0, 0);
				_numBirds [i] = Bird;
				Bird.transform.position = new Vector3 (origin.x, origin.y, enemySpawnPlane);
			}
		}
    }

	private IEnumerator wait(){
		yield return new WaitForSeconds (10);
	}

    //generates coordinates for the enemies to spawn on
	private Coordinates getCoordinates(){
        
		float x = Random.Range (-10f, 10f);
		float y = Random.Range (-10f, 10f);
        Coordinates origin = new Coordinates(x, y);
        return origin;
	}
		
}
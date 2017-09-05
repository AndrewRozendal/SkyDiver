using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject _BirdPrefab;
	private GameObject Bird;
	private int maxNumBirds = 20;

	//Birds starting and ending coordinates
	private float coordinateXStart;
	private float coordinateYStart;




	//Array to hold the birds
	static GameObject[] _numBirds;



	void Start () {
		_numBirds = new GameObject[maxNumBirds]; 
	}
	

	void Update () {
		for (int i = 0; i < maxNumBirds; i++) {
			wait ();
			if (_numBirds [i] == null) {
				getCoordinates ();
				Bird = Instantiate (_BirdPrefab) as GameObject;
				transform.Rotate (180, 0, 0);
				_numBirds [i] = Bird;
				Bird.transform.position = new Vector3 (coordinateXStart, coordinateYStart, 30);
			}

		}
}


	IEnumerator wait(){
		yield return new WaitForSeconds (5f);
	}

	void getCoordinates(){
		coordinateXStart = Random.Range (-6.6f, 6.6f);
		coordinateYStart = Random.Range (-3.7f, 3.7f);

	}
		
}
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

	// Birds
    [SerializeField] private GameObject _BirdPrefab;
	private GameObject Bird;
	public int maxNumBirds = 5;
	private GameObject[] _birdList;

	// V3BirdFormations
	[SerializeField] private GameObject _V3BirdFormationPrefab;
	private GameObject V3BirdFormation;
	public int maxNumV3BirdFormations = 5;
	private GameObject[] _V3BirdFormationList;

    public float enemySpawnPlane = 30f;
	private int quad;
	public int frequency = 15;
	public bool debugMode = true;

	public void Start () {
		//Order matters
		quad = 1;
		Spawn (out _birdList, maxNumBirds, _BirdPrefab);  //Bird
		Spawn (out _V3BirdFormationList, maxNumV3BirdFormations, _V3BirdFormationPrefab, true);  //V3BirdFormation
	}


	public void Update () {
		calculateCurrentQuad ();
		CheckEntityStatus (ref _birdList, maxNumBirds, _BirdPrefab);
		CheckEntityStatus (ref _V3BirdFormationList, maxNumV3BirdFormations, _V3BirdFormationPrefab, true);
    }

	public void Spawn(out GameObject[] list, int numEntities, GameObject prefab, bool isGroup = false){
		list = new GameObject[numEntities];
		for (int i = 0; i < numEntities; i++) {
			InstantiatePrefab(ref list, prefab, i, isGroup);
		}
	}

	private void CheckEntityStatus(ref GameObject[] list, int numEntities, GameObject prefab, bool isGroup = false){
		for (int i = 0; i < numEntities; i++) {
			if (list[i] == null) {
				InstantiatePrefab (ref list, prefab, i, isGroup);
			}
		}
	}

	private void InstantiatePrefab(ref GameObject[] list, GameObject prefab, int i, bool isGroup){
		StartCoroutine(wait ());
		//Objects starting and ending coordinates
		Coordinates origin = getCoordinates ();
		GameObject entity = Instantiate (prefab) as GameObject;
		transform.Rotate (180, 0, 0);
		list [i] = entity;
		entity.transform.position = new Vector3 (origin.x, origin.y, enemySpawnPlane);
		entity.GetComponent<BirdMove> ().quad = quad;
		if (isGroup) {
			//foreach child gameobject, set self flight to false.
			foreach (Transform child in entity.transform) {
				Destroy(child.GetComponent<BirdMove>());
				if (debugMode) {
					Debug.Log ("Destroying child BirdMove script");
				}
			}
			//Destroy(entity.GetComponentInChildren<BirdMove>());
		}
	}

//	private void DestroyCurrentEntities(ref GameObject[] list, int numEntities){
//		for (int i = 0; i < numEntities; i++) {
//			Destroy (list [i]);
//		}
//	}


	// Calclulates the quadrent that we should spawn enemies in
	private void calculateCurrentQuad() {
		int result = (int)Time.time % 60;
		if (debugMode) {
			//Debug.Log ("Game time:" + result);
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
		Debug.Log (Time.time);
		yield return new WaitForSeconds (1);
		print (Time.time);
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
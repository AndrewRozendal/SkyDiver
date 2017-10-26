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
	//private GameObject Bird;
	public int maxNumBirds = 5;
	private GameObject[] _birdList;
	private float nextBirdSpawnTime;

	// V3BirdFormations
	[SerializeField] private GameObject _V3BirdFormationPrefab;
	//private GameObject V3BirdFormation;
	public int maxNumV3BirdFormations = 5;
	private GameObject[] _V3BirdFormationList;
	private float nextV3BirdSpawnTime;
	
	//Coins
	[SerializeField] private GameObject _CoinPrefab;
	//private GameObject Coin;
	public int maxNumCoins = 5;
	private GameObject[] _coinList;
	private float nextCoinSpawnTime;
	
	

    public float enemySpawnPlane = 30f;
	private int quad;
	public float frequency = 4;
	public bool debugMode = true;
	public float delayBetweenInstantiate = 1;

	public void Start () {
		//Order matters
		nextBirdSpawnTime = 0;
		nextV3BirdSpawnTime = 1;
		nextCoinSpawnTime = 2;
		quad = 1;
		Spawn (out _birdList, maxNumBirds, _BirdPrefab, ref nextBirdSpawnTime);  //Bird
		Spawn (out _V3BirdFormationList, maxNumV3BirdFormations, _V3BirdFormationPrefab, ref nextV3BirdSpawnTime, true);  //V3BirdFormation
		Spawn (out _coinList, maxNumCoins, _CoinPrefab, ref nextCoinSpawnTime);  //Coins
	}


	public void Update () {
		calculateCurrentQuad ();
		if (debugMode) {
			Debug.Log ("nextBirdSpawnTime: " + nextBirdSpawnTime);
		}
		CheckEntityStatus (ref _birdList, maxNumBirds, _BirdPrefab, ref nextBirdSpawnTime);
		CheckEntityStatus (ref _V3BirdFormationList, maxNumV3BirdFormations, _V3BirdFormationPrefab, ref nextV3BirdSpawnTime, true);
		CheckEntityStatus (ref _coinList, maxNumCoins, _CoinPrefab, ref nextCoinSpawnTime);
    }

	public void Spawn(out GameObject[] list, int numEntities, GameObject prefab, ref float nextSpawnTime, bool isGroup = false){
		if (debugMode) {
			Debug.Log ("Creating array with " + numEntities + "objects");
		}

		list = new GameObject[numEntities];

		// Spread the instantiations out
		for (int i = 0; i < numEntities; i++) {
			if (Time.time > nextSpawnTime) {
				nextSpawnTime = Time.time + delayBetweenInstantiate;
				InstantiatePrefab (ref list, prefab, i, isGroup);
				calculateCurrentQuad ();
			}
		}
	}

	private void CheckEntityStatus(ref GameObject[] list, int numEntities, GameObject prefab, ref float nextSpawnTime, bool isGroup = false){
		// Spread the instantiations out
		for (int i = 0; i < numEntities; i++) {
			if (list[i] == null) {
				if (debugMode) {
					Debug.Log ("currentTime: " + Time.time + " nextSpawnTime for current object: " + nextSpawnTime);
				}
				if (Time.time > nextSpawnTime) {
					nextSpawnTime = Time.time + delayBetweenInstantiate;
					InstantiatePrefab (ref list, prefab, i, isGroup);
					calculateCurrentQuad ();
					if (debugMode) {
						Debug.Log ("nextSpawnTime is now set to: " + nextSpawnTime);
					}
				}
			}
		}
	}

	private void InstantiatePrefab(ref GameObject[] list, GameObject prefab, int i, bool isGroup){
		// Delay so that instantiations are not instantaneous
		// yield return new WaitForSeconds (delayTime);

		//Objects starting and ending coordinates
		Coordinates origin = getCoordinates ();
		GameObject entity = Instantiate (prefab) as GameObject;
		transform.Rotate (180, 0, 0);
		list [i] = entity;
		entity.transform.position = new Vector3 (origin.x, origin.y, enemySpawnPlane);
		if (entity.GetComponent<BirdMove> () != null) {
			entity.GetComponent<BirdMove> ().quad = quad;
		} else if (entity.GetComponent<CollectibleMove> () != null) {
			// do nothing for now
		} else {
			Debug.LogError ("unexpected entity with neither BirdMove or CollectibleMove");
		}
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
		quad += 1;
		if (quad > 4) {
			quad = 1;
		}
//		if (Time.time > lastQuadrantChange) {
//			lastQuadrantChange = Time.time + delayBetweenQuadrantChange;
//			quad += 1;
//			if (quad > 4) {
//				quad = 1;
//			}
//		}
	}

	private IEnumerator wait(){
		Debug.Log ("wait start time: " + Time.time);
		yield return new WaitForSeconds (10);
		print ("wait end time: " + Time.time);
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

	private void gameOver(){
		Time.timeScale = 0;
	}

	void Awake() {
		Messenger.AddListener (GameEvent.PLAYER_DEAD, gameOver);
	}

	void Destroy(){
		Messenger.RemoveListener (GameEvent.PLAYER_DEAD, gameOver);
	}
		
}
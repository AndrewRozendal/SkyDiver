using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour {

	public bool won = false;
	private float score;
	private float time;
	private int coinsCollected;
    [SerializeField]private Text timerValue;
	[SerializeField] private Text coinValue;
   

    void start() {
		time = Time.time;
		score = 0f;
		coinsCollected = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (!won) {
			time = Time.timeSinceLevelLoad;
			FormatTime ();
		}
		displayCoinsCollected();
	}

	private void FormatTime(){
		int minutes = ((int)time / 60);
		int seconds = ((int)time % 60);
		int ms = (int)(time * 1000) % 1000;
		string currentTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms);
		timerValue.text = "Time: " + currentTime;
	}
	
	private void displayCoinsCollected(){
		coinValue.text = "Coins Collected: " + coinsCollected;
	}
	
	private void OnCoinCollected(){
		coinsCollected++;
	}
	
	void Awake(){
		Messenger.AddListener (GameEvent.COIN_COLLECTED, OnCoinCollected);
	}
	
	void OnDestroy(){
		Messenger.RemoveListener (GameEvent.COIN_COLLECTED, OnCoinCollected);
	}
}

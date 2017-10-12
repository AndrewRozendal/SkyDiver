using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour {

	public bool won = false;
	private float time;
    [SerializeField]private Text timerValue;
   

    void start() {
		time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		if (!won) {
			time = Time.timeSinceLevelLoad;
			FormatTime ();
		}
	}

	private void FormatTime(){
		int minutes = ((int)time / 60);
		int seconds = ((int)time % 60);
		int ms = (int)(time * 1000) % 1000;
		string currentTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms);
		timerValue.text = "Time: " + currentTime;
	}
}

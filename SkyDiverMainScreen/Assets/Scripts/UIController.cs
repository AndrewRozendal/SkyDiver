using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField] private StartScreen startScreen;
	[SerializeField] private SettingsPopup settingsPopup;
	[SerializeField] private ControlsPopup controlsPopup;


	// Use this for initialization
	void Start () {
		settingsPopup.Close ();
		controlsPopup.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

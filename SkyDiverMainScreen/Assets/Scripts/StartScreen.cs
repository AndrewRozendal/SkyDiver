using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {
	[SerializeField] private SettingsPopup settingsPopup;
	[SerializeField] private ControlsPopup controlsPopup;


	void StartButtonPressed(){
		Messenger.Broadcast("START_GAME");
	}

	public void OnSettingsButton(){
		settingsPopup.Open ();
		this.gameObject.SetActive (false);
	}

	public void onSettingsOkButton() {
		settingsPopup.Close ();
	}

	public void onControlsButton() {
		controlsPopup.Open ();
	}

	public void onControlsOkButton() {
		controlsPopup.Close ();
	}

}

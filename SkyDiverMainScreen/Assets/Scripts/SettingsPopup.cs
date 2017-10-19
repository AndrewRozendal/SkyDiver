using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour {
	[SerializeField] private Slider volumeSlider;
	[SerializeField] private StartScreen startScreen;

	void Start(){
	}

	public void Open() {
		this.gameObject.SetActive (true);
	}

	public void Close () {
		this.gameObject.SetActive (false);
		startScreen.gameObject.SetActive (true);
	}

	public void OnOKButton(){
		Close ();
	}
	public void OnReturnButton () {
		Close ();
	}

}

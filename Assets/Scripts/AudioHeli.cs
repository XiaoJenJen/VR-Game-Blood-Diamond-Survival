using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHeli : MonoBehaviour {

	AudioSource HeliSource; //An audio source variable

	bool MinionGuide;
	
	// Use this for initialization
	void Start () {
		
		HeliSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//used to turn of the sound in case the game is over.
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && HeliSource.isPlaying) HeliSource.Stop();
		
	}
	
}
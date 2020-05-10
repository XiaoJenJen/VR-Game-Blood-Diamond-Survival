using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioThirst : MonoBehaviour {

	AudioSource thirstSource; //An audio source variable

	// Use this for initialization
	void Start () {
		
		thirstSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if( GeneralController.Instance.ThristNumber > 0 && GeneralController.Instance.ThristNumber <= 4 && !thirstSource.isPlaying) thirstSource.Play();
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && thirstSource.isPlaying) thirstSource.Stop();
		
	}
}

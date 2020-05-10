using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHunger : MonoBehaviour {

	AudioSource hungerSource; //An audio source variable

	// Use this for initialization
	void Start () {
		
		hungerSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if( GeneralController.Instance.HungryNumber > 0 && GeneralController.Instance.HungryNumber <= 4 && !hungerSource.isPlaying) hungerSource.Play();
		
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && hungerSource.isPlaying) hungerSource.Stop();
		
	}
}

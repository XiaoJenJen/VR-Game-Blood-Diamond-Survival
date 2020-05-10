using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMineBackGround : MonoBehaviour {

	AudioSource BackgroundSource; //An audio source variable

	// Use this for initialization
	void Start () {
		
		BackgroundSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!BackgroundSource.isPlaying && GeneralController.Instance.SlaveDeath == false) BackgroundSource.Play();
		
		if (BackgroundSource.isPlaying && GeneralController.Instance.SlaveDeath == true) BackgroundSource.Stop();
		
	}
}

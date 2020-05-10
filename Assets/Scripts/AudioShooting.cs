using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShooting : MonoBehaviour {

	AudioSource shootingSource; //An audio source variable

	// Use this for initialization
	void Start () {
		
		shootingSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GeneralController.Instance.gameTimer >= GeneralController.Instance.DeathTimer && !shootingSource.isPlaying) shootingSource.Play();
		
	}
}

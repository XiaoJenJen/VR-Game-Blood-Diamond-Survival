using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFire : MonoBehaviour {

	AudioSource fireSource; //An audio source variable

	bool MinionGuide;
	
	// Use this for initialization
	void Start () {
		
		fireSource = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
    {		
		
		if(other.tag == "Player" && !fireSource.isPlaying) fireSource.Play();

		
    }
	
	void OnTriggerExit(Collider other)
    {		
		
		if(other.tag == "Player" && fireSource.isPlaying) fireSource.Stop();

		
    }
}
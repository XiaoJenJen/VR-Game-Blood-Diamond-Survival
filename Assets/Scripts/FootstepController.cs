using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour {
    AudioSource footstepSource; //An audio source variable

    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
		
        //If the user is walking and the footstep sound is not playing, play the footstep sound
        if (SamplePlayerController.isWalking && !footstepSource.isPlaying) footstepSource.Play();

        if (SamplePlayerController.isRunning && footstepSource.pitch == 1.0f) footstepSource.pitch = 1.4f;

        if (!SamplePlayerController.isRunning && footstepSource.pitch != 1.0f) footstepSource.pitch = 1.0f;

        //If the user is not walking and the footstep sound is playing, stop the footstep sound
        if (!SamplePlayerController.isWalking && footstepSource.isPlaying) footstepSource.Stop();
		
		//audio stops if game ends
		if ( (GeneralController.Instance.GameOver == true || GeneralController.Instance.GameWin == true) && footstepSource.isPlaying) footstepSource.Stop();

        //isWalking comes from the SamplePlayerController script (first person continuous steering movement)
    }
}



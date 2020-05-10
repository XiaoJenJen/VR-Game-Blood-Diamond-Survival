using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMinionFollow : MonoBehaviour {

	AudioSource footstepSource;
	
	public GameObject Player;
	private Animator animator;
	bool startMove, move, PlayerDied;

	public Rigidbody minionMove;
	
	private Vector3 minionPosition;
	
	float waitTime;
	
	
	
	// Use this for initialization
	void Start () {
		
		footstepSource = GetComponent<AudioSource>();
		
		animator = GetComponent<Animator>();
		minionMove = GetComponent<Rigidbody>();
		
		startMove = false;
		move = true;
		PlayerDied = false;
		
		animator.SetBool("IsTimeUp", false);
	}
	
	// Update is called once per frame
	void Update () {

		//The minions will start following you after 180 seconds.
		if(GeneralController.Instance.gameTimer >= 180 && startMove == false){ 
			
			//disables kinematics so that you cannot move the minions before they chase you
			GetComponent<Rigidbody>().isKinematic = false;
			
			startMove = true;
		}
		if(startMove == true && Time.time < GeneralController.Instance.DeathTimer){
			
			// This is will determine whether or not the minions are chasing you
			if(move == true){
				
				//Switch animation to run
				animator.SetBool("Follow", true);
				
				//plays footstep sound
				
				if (!footstepSource.isPlaying) footstepSource.Play();
				
				//The turning code is based on Unity Documentation - Vector3.RotateTowards
				//*************************************************************************
				//Make it turn towards the player. Save Vector3 relationship of positions 
				Vector3 playerDirection = Player.transform.position - transform.position;
				
				//Use RotateTowards to identify the rotation angle
				Vector3 minionTurn = Vector3.RotateTowards(transform.forward, playerDirection, 4.0f * Time.deltaTime, 0.0f);
				
				//Rotate using quaternion
				transform.rotation = Quaternion.LookRotation(minionTurn);
				
				//Command to chase Vector3.MoveTowards(transform.position, Player.transform.position, speed);
				transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, .06f);

			
				//Command to stop chasing if they're too close
				if (Vector3.Distance(transform.position, Player.transform.position) < 4){
					move = false;
					
					//Save time as base for waiting
					waitTime = Time.time;
					
					//Switch animation to idle
					animator.SetBool("Follow", false);
					
					//Stops footstep sound
					if (footstepSource.isPlaying) footstepSource.Stop();
					
				}
		
			}
			
			// Waiting time to start chasing you again
			if(waitTime + 3 <= Time.time){
				move = true;
			}

		}
		
		if(Time.time >= GeneralController.Instance.DeathTimer && PlayerDied == false){
			
			animator.SetBool("IsTimeUp", true);
			PlayerDied = true;
		}
		
		
	}
}

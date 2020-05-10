using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovement : MonoBehaviour {

	bool Move;
	int xMove, yMove, zMove;
	float timeMove;
	
	public Rigidbody bugMove;
	
	// Use this for initialization
	void Start () {
		xMove = 0;
		yMove = 0;
		zMove = 0;
		timeMove = 0.0f;
		Move = false;
		
		bugMove = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(Move == true){
			
			//Since it's a Rigidbody, the MovePosition needs to be used 
			//and Quaternion.Euler ensures the bugs to upwards always. 
			transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			bugMove.MovePosition(transform.position + new Vector3(xMove, yMove, zMove) * Time.deltaTime);
			
			if(timeMove + 1.5 <= Time.time)
				Move = false;
		}
		
		//If the bugs go below ground level, they get destroyed. Assume the bugs hide in the ground.
		if(transform.position.y <= 15)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider other)
    {	
		
		//If the user approaches the bug it will move randomly 
        if (other.tag == "Player"){
			//if(timeMove + 1 <= Time.time){
				timeMove = Time.time;
				xMove = Random.Range(-4, 4);
				yMove = Random.Range(6, 8);
				zMove = Random.Range(-4, 4);
				Move = true;
			//}
			
		}
		
    }
	
	// void OnTriggerExit(Collider other)
    // {	
		
		//If the user approaches the bug it will move randomly 
        // if (other.tag == "Player"){
		
			// Move = false;
	
		// }
		
    // }
	
}

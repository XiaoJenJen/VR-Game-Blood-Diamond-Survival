using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionShoot : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		
		animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("Minion"))
        {
            animator.SetBool("IsNear", true);
        }
    }
}

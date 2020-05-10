using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHit_S : MonoBehaviour {

    public Color originalColorVar;

	void Start ()
    {
        //We are storing the original color of the gameobject to which this script is attached
        originalColorVar = transform.GetComponent<Renderer>().material.color;
    }
}


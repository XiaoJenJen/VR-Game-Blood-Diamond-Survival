using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadowRotation : MonoBehaviour {

	void Update ()
    {
        //Updating the forward (blue axis) of the blob shadow projector
        //to down. So that when the game object rotates, the shadow is
        //projected on the correct plane (downwards)
        transform.rotation = Quaternion.LookRotation(Vector3.down);
	}
}


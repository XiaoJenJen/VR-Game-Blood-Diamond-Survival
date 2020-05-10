using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    //This script destroys the prefab to which it is attached
    //after 2 seconds or when any key is pressed.
    void Update()
    {
       Destroy(gameObject, 2.0f);
    }
}

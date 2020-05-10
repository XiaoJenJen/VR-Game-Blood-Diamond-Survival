using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGesture : MonoBehaviour
{
    //A variable to keep track of if the user is looking down or not
    public bool isFacingDown = false;

    void Update()
    {
        isFacingDown = DetectFacingDown();
    }

    //We check whether the camera's rotation angle is below 60 degrees
    private bool DetectFacingDown()
    {
        return (CameraAngleFromGround() < 60.0f);
    }

    //We get the angle of the current camera relative to the down axis
    //and we return an angle between 0 and 180 degrees
    private float CameraAngleFromGround()
    {
        return Vector3.Angle(Vector3.down,
                             Camera.main.transform.rotation * Vector3.forward);
    }
}


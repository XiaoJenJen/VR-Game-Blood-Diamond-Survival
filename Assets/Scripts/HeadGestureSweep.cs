using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGestureSweep : MonoBehaviour
{
    public bool isFacingDown = false;
    public bool isRotatingDown = false;

    //We give a threshold for a quick enough head rotation
    //to trigger the UI element (80 degrees per second)
    private float sweepRate = 80.0f;
    private float previousCameraAngle;

    void Start()
    {
        //We store the previous camera angle to compare
        previousCameraAngle = CameraAngleFromGround();
    }

    void Update()
    {
        isFacingDown = DetectFacingDown();
        isRotatingDown = DetectRotatingDown();
    }

    private bool DetectFacingDown()
    {
        return (CameraAngleFromGround() < 60.0f);
    }

    private float CameraAngleFromGround()
    {
        return Vector3.Angle(Vector3.down,
                             Camera.main.transform.rotation * Vector3.forward);
    }

    //We compare the camera's X rotation angle with the previousCameraAngle 
    //from the previous frame. We calculate the rotational rate in seconds
    //and check whether the rate exceeds the threshold(sweepRate).
    private bool DetectRotatingDown()
    {
        float angle = CameraAngleFromGround();
        float deltaAngle = previousCameraAngle - angle;
        float rate = deltaAngle / Time.deltaTime;
        previousCameraAngle = angle;
        return (rate >= sweepRate);
    }
}

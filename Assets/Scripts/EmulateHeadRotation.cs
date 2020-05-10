using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//********************************************************************************//
//**REMOVE THIS SCRIPT FROM THE OVRCameraRig BEFORE TAKING A BUILD FOR OCULUS GO**//
//********************************************************************************//

//Press Q while the Game View is active, rotate the camera with the left mouse button

public class EmulateHeadRotation : MonoBehaviour
{
    float headSpeedHorizontal = 1.5f;
    float headSpeedVertical = 1.5f;
    float headYaw = 0.0f;
    float headPitch = 0.0f;
    public static bool isLimited = false; //A variable to limit head rotation 
                                          //emulation during the controller rotation

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            headYaw += headSpeedHorizontal * Input.GetAxis("Mouse X");
            headPitch += headSpeedVertical * Input.GetAxis("Mouse Y") * -1.0f;

            if (!isLimited)
                transform.eulerAngles = new Vector3(headPitch, headYaw, 0.0f);
        }
    }
}


/************************************************************************************

Copyright   :   Copyright 2017 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.4.1 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

https://developer.oculus.com/licenses/sdk-3.4.1


Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls the player's movement in virtual reality.
/// </summary>
[RequireComponent(typeof(CharacterController))]

public class SamplePlayerController : OVRPlayerController
{
    public OVRInput.Button runButton;
    public OVRInput.Button alternateRunButton;

    public float walkScale = 1.0f;
    float runScaleMultiplier = 1.0f;
    public bool rotationSnap = false;
    float PendingRotation = 0;
    float SimulationRate_ = 60f;
    private bool prevHatLeft_;
    private bool prevHatRight_;
    private OVRPose? InitialPose_;
    private Vector3 MoveThrottle_ = Vector3.zero;
    private float FallSpeed_ = 0.0f;
    private float InitialYRotation_ = 0.0f;

    public float axisDeadZone = 0.1f;

    float rotationAnimation = 0;
    float targetYaw = 0;
    bool animating;

    //We've created a variable to understand if the player is walking
    public static bool isWalking = false;
    public static bool isRunning = false;

    void Awake()
    {
        Controller = gameObject.GetComponent<CharacterController>();

        if (Controller == null)
            Debug.LogWarning("OVRPlayerController: No CharacterController attached.");

        // We use OVRCameraRig to set rotations to cameras, and to be influenced by rotation
        List<OVRCameraRig> cameraRigs = new List<OVRCameraRig>();
        foreach (Transform child in transform)
        {
            OVRCameraRig childCameraRig = child.gameObject.GetComponent<OVRCameraRig>();
            if (childCameraRig != null)
            {
                cameraRigs.Add(childCameraRig);
            }
        }

        if (cameraRigs.Count == 0)
            Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
        else if (cameraRigs.Count > 1)
            Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
        else
            CameraRig = cameraRigs[0];

        InitialYRotation_ = transform.rotation.eulerAngles.y;
    }

    protected void Update()
    {
        if (useProfileData)
        {

        }
        else if (InitialPose_ != null)
        {
            CameraRig.transform.localPosition = InitialPose_.Value.position;
            CameraRig.transform.localRotation = InitialPose_.Value.orientation;
            InitialPose_ = null;
        }

        //****************************************************COMMENT OUT TO DISABLE JUMP (START)*********************************************//
        //We are making the user Jump when they pull the trigger while they were pressing the One (Touchpad) button on the Oculus Go Controller
        //if ((OVRInput.Get(OVRInput.Button.One) && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) || Input.GetKeyDown(KeyCode.Space))
        //    MoveThrottle_ += new Vector3(0, transform.lossyScale.y * JumpForce, 0);
        //****************************************************COMMENT OUT TO DISABLE JUMP (END)**********************************************//


        UpdateMovement();

        Vector3 moveDirection = Vector3.zero;

        float motorDamp = (1.0f + (Damping * SimulationRate_ * Time.deltaTime));

        MoveThrottle_.x /= motorDamp;
        MoveThrottle_.y = (MoveThrottle_.y > 0.0f) ? (MoveThrottle_.y / motorDamp) : MoveThrottle_.y;
        MoveThrottle_.z /= motorDamp;

        moveDirection += MoveThrottle_ * SimulationRate_ * Time.deltaTime;

        // Gravity
        if (Controller.isGrounded && FallSpeed_ <= 0)
            FallSpeed_ = ((Physics.gravity.y * (GravityModifier * 0.002f)));
        else
            FallSpeed_ += ((Physics.gravity.y * (GravityModifier * 0.002f)) * SimulationRate_ * Time.deltaTime);

        moveDirection.y += FallSpeed_ * SimulationRate_ * Time.deltaTime;

        // Offset correction for uneven ground
        float bumpUpOffset = 0.0f;

        if (Controller.isGrounded && MoveThrottle_.y <= 0.001f)
        {
            bumpUpOffset = Mathf.Max(Controller.stepOffset, new Vector3(moveDirection.x, 0, moveDirection.z).magnitude);
            moveDirection -= bumpUpOffset * Vector3.up;
        }

        Vector3 predictedXZ = Vector3.Scale((Controller.transform.localPosition + moveDirection), new Vector3(1, 0, 1));

        // Move contoller
        Controller.Move(moveDirection);

        Vector3 actualXZ = Vector3.Scale(Controller.transform.localPosition, new Vector3(1, 0, 1));

        if (predictedXZ != actualXZ)
            MoveThrottle_ += (actualXZ - predictedXZ) / (SimulationRate_ * Time.deltaTime);
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    float AngleDifference(float a, float b)
    {
        float diff = (360 + a - b) % 360;
        if (diff > 180)
            diff -= 360;
        return diff;
    }

    public override void UpdateMovement()
    {
        bool HaltUpdateMovement = false;
        GetHaltUpdateMovement(ref HaltUpdateMovement);
        if (HaltUpdateMovement)
            return;

        float MoveScaleMultiplier = 1;

        float RotationScaleMultiplier = 1;

        float MoveScale = 1.0f;

        MoveScale *= SimulationRate_ * Time.deltaTime;

        Quaternion playerDirection = ((HmdRotatesY) ? CameraRig.centerEyeAnchor.rotation : transform.rotation);
        
        //remove any pitch + yaw components
        playerDirection = Quaternion.Euler(0, playerDirection.eulerAngles.y, 0);

        Vector3 euler = transform.rotation.eulerAngles;

        Vector2 touchDir = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        bool stepLeft = false;
        bool stepRight = false;
        stepLeft = Input.GetKeyDown(KeyCode.V);
        stepRight = Input.GetKeyDown(KeyCode.B);

        OVRInput.Controller activeController = OVRInput.GetActiveController();

        if ((activeController == OVRInput.Controller.LTrackedRemote) || (activeController == OVRInput.Controller.RTrackedRemote))
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {
                if ((touchDir.magnitude > 0.3f) && (Mathf.Abs(touchDir.x) > Mathf.Abs(touchDir.y)))
                {
                    stepLeft |= (touchDir.x < 0.0f);
                    stepRight |= (touchDir.x > 0.0f);
                }
            }
        }

        float rotateInfluence = SimulationRate_ * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

        float rightAxisX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
        if (Mathf.Abs(rightAxisX) < axisDeadZone)
            rightAxisX = 0;

        PendingRotation += rightAxisX * rotateInfluence;

        if (rotationSnap)
        {
            if (Mathf.Abs(PendingRotation) > RotationRatchet)
            {
                if (PendingRotation > 0)
                    stepRight = true;
                else
                    stepLeft = true;
                PendingRotation -= Mathf.Sign(PendingRotation) * RotationRatchet;
            }
        }
        else
        {
            euler.y += PendingRotation;
            PendingRotation = 0;
        }

        if (rotationAnimation > 0 && animating)
        {
            float speed = Mathf.Max(rotationAnimation, 3);

            float diff = AngleDifference(targetYaw, euler.y);

            euler.y += Mathf.Sign(diff) * speed * Time.deltaTime;

            if ((AngleDifference(targetYaw, euler.y) < 0) != (diff < 0))
            {
                animating = false;
                euler.y = targetYaw;
            }
        }
        if (stepLeft ^ stepRight)
        {
            float change = stepRight ? RotationRatchet : -RotationRatchet;

            if (rotationAnimation > 0)
            {
                targetYaw = (euler.y + change) % 360;
                animating = true;
            }
            else
            {
                euler.y += change;
            }
        }

        float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier * runScaleMultiplier;


        //***************************************RUNNING (START)*****************************************//
        if (OVRInput.Get(runButton) || OVRInput.Get(alternateRunButton) || Input.GetKey(KeyCode.LeftShift))
        {
            runScaleMultiplier = 3.0f;
            isRunning = true;
        }
        else
        {
            runScaleMultiplier = 1.0f;
            isRunning = false;
        }
        //***************************************RUNNING (END)*******************************************//

        
        //**************************************************MOVEMENT (START)***************************************************//

        float leftAxisX = 0;
        float leftAxisY = 0;

        if ((activeController == OVRInput.Controller.LTrackedRemote) || (activeController == OVRInput.Controller.RTrackedRemote))
        {
            if (OVRInput.Get(OVRInput.Touch.One))
            {
                if ((touchDir.magnitude > 0.3f) && (Mathf.Abs(touchDir.y) > Mathf.Abs(touchDir.x)))
                {
                    leftAxisX = 0;
                    leftAxisY = (touchDir.y > 0.0f) ? (1 * walkScale) : (-1 * walkScale);
                }
                if (touchDir.magnitude <= 0.3f)
                { 
                    leftAxisY = 0;
                    leftAxisX = 0;
                }
            }
        }
        //**************************************************MOVEMENT (END)*****************************************************//


        if (Mathf.Abs(leftAxisX) < axisDeadZone)
            leftAxisX = 0;
        if (Mathf.Abs(leftAxisY) < axisDeadZone)
            leftAxisY = 0;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            leftAxisY = 1 * walkScale;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            leftAxisY = -1 * walkScale;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            leftAxisX = -1 * walkScale;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            leftAxisX = 1 * walkScale;

        //We are holding if the user is walking or not
        isWalking = Mathf.Abs(leftAxisY) > 0.0f ? true : false;


        if (leftAxisY > 0.0f)
            MoveThrottle_ += leftAxisY * (playerDirection * (Vector3.forward * moveInfluence));

        if (leftAxisY < 0.0f)
            MoveThrottle_ += Mathf.Abs(leftAxisY) * (playerDirection * (Vector3.back * moveInfluence));

        if (leftAxisX < 0.0f)
            MoveThrottle_ += Mathf.Abs(leftAxisX)
            * (playerDirection * (Vector3.left * moveInfluence));

        if (leftAxisX > 0.0f)
            MoveThrottle_ += leftAxisX
            * (playerDirection * (Vector3.right * moveInfluence));

        transform.rotation = Quaternion.Euler(euler);
    }

    public void SetRotationSnap(bool value)
    {
        rotationSnap = value;
        PendingRotation = 0;
    }

    public void SetRotationAnimation(float value)
    {
        rotationAnimation = value;
        PendingRotation = 0;
    }

    /// <summary>
    /// Resets the player look rotation when the device orientation is reset.
    /// </summary>
    public new void ResetOrientation()
    {
        if (HmdResetsY)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.y = InitialYRotation_;
            transform.rotation = Quaternion.Euler(euler);
        }
    }

    void Reset()
    {
        // Prefer to not reset Y when HMD position reset
        HmdResetsY = false;
    }
}
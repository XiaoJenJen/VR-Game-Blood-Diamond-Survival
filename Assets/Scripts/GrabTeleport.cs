using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTeleport : MonoBehaviour
{
    public GameObject playerController, targetVisual;
    private bool isGrabbing = false;
    private Transform grabbedTransform;
    public float zSpeed = 4.0f;
    private Transform hitTransform;
    private Material targetMaterial;
    private Vector3 hitPos;
    private bool waitTeleportation; //A control variable to prevent
                                    //multiple coroutine instantiation

    void Start()
    {
        targetMaterial = targetVisual.GetComponent<Renderer>().material;
        waitTeleportation = false;
        targetVisual.transform.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        transform.localPosition = OVRInput.GetLocalControllerPosition(activeController);
        transform.localRotation = OVRInput.GetLocalControllerRotation(activeController);

        RaycastHit hitInfo2;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo2))
        {
            if (hitInfo2.transform.tag == "Grabbable" && !isGrabbing)
            {
                if (hitTransform != null)
                    SetHighlight(hitTransform, false);

                hitTransform = hitInfo2.transform;
                SetHighlight(hitTransform, true);
            }
            else
            {
                if (hitTransform != null && !isGrabbing)
                    SetHighlight(hitTransform, false);
            }
        }
        else
        {
            if (hitTransform != null && !isGrabbing)
            {
                SetHighlight(hitTransform, false);
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo))
            {
                if (hitInfo.transform.tag == "Grabbable")
                {
                    isGrabbing = true;
                    grabbedTransform = hitInfo.transform;
                    grabbedTransform.GetComponent<Rigidbody>().isKinematic = true;
                    grabbedTransform.GetComponent<Rigidbody>().useGravity = false;
                    grabbedTransform.parent = transform;
                }
            }
        }

        if (isGrabbing && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (grabbedTransform != null)
            {
                grabbedTransform.GetComponent<Rigidbody>().isKinematic = false;
                grabbedTransform.GetComponent<Rigidbody>().useGravity = true;
                grabbedTransform.parent = null;
            }
            isGrabbing = false;
        }

        if (isGrabbing && !OVRInput.Get(OVRInput.Button.One))
        {
            float distance = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).y;
            grabbedTransform.position += distance * Time.deltaTime * zSpeed * transform.forward;
        }

        //If the user touches the touchpad, we enable the target visual at the hit position
        //with the terrain's rotation (Quaternion.FromToRotation(Vector3.up, hitInfo3.normal))
        if (OVRInput.Get(OVRInput.Touch.One))
        {
            targetVisual.transform.GetComponent<Renderer>().enabled = true;

            RaycastHit hitInfo3;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hitInfo3))
            {
                //We check if the tag of the hit collider is ground, to exclude other objects
                if (hitInfo3.transform.tag == "Ground")
                {
                    targetVisual.transform.position = new Vector3(hitInfo3.point.x,
                                                      hitInfo3.point.y + 0.01f, hitInfo3.point.z);
                    targetVisual.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo3.normal);

                    //If the user presses button one, we start teleportation coroutine
                    if (OVRInput.GetDown(OVRInput.Button.One))
                    {
                        //We are storing the hit position for use in the coroutine
                        hitPos = new Vector3(hitInfo3.point.x, hitInfo3.point.y + 1.02f, hitInfo3.point.z);

                        //If there is no ongoing coroutine, we call the teleportation coroutine
                        if (!waitTeleportation)
                            StartCoroutine(Teleportation());
                    }
                }
            }
        }

        //If the user isn't touching the touchpad, we disable the target visual
        if (OVRInput.GetUp(OVRInput.Touch.One))
        {
            targetVisual.transform.GetComponent<Renderer>().enabled = false;
        }
    }

    //The teleportation coroutine
    IEnumerator Teleportation()
    {
        //We set the control variable as true
        waitTeleportation = true;
        targetMaterial.color = new Color(0.0f, 1.0f, 0.0f, 0.7f);
        targetVisual.GetComponent<AudioSource>().Play();

        //We wait for a fraction of a second for the user to see the
        //color change (from yellow to green) on the target visual
        yield return new WaitForSeconds(0.35f);

        //We move the player instantaneously to the hit position
        playerController.transform.position = hitPos;
        targetMaterial.color = new Color(1.0f, 1.0f, 0.0f, 0.7f);
        targetVisual.transform.GetComponent<Renderer>().enabled = false;
        waitTeleportation = false;
    }

    void SetHighlight(Transform t, bool highlight)
    {
        if (highlight)
        {
            hitTransform.GetComponent<Renderer>().material.color = Color.cyan;
            hitTransform.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            t.GetComponent<Renderer>().material.color = t.GetComponent<IsHit_S>().originalColorVar;
            t.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineHidden;
            transform.GetComponent<LineRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 0.6f);
        }
    }
}

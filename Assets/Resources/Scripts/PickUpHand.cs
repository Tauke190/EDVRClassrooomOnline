using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HAND_MODE { SKELETONBUILD,MENU}
public class PickUpHand : MonoBehaviour
{
    public float speed = 100f;
    public Transform holder;
    public HAND_MODE handMode = HAND_MODE.SKELETONBUILD;
    public GameObject VrCanvasPointer;

    [HideInInspector]
    public Bone currentBone;
    [HideInInspector]
    public PortalObjectMover currentMenuObject;
    //private SteamVR_Action_Boolean pickUp;

    //private void Start()
    //{
    //    pickUp = FindObjectOfType<VRPointer>().Trigger;
    //}

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (currentBone)
            {
                currentBone.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand);
                currentBone.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RHand);
                currentBone.Drop();
            }
            else if (currentMenuObject)
            {
                if (currentMenuObject.labelObject)
                {
                    Destroy(currentMenuObject.labelObject);
                }
                Destroy(currentMenuObject.gameObject);
                VrCanvasPointer.SetActive(true);
            }
        }
    }
}

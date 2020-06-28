using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Events;


public class Player_VR : MonoBehaviour
{


    public static Player_VR instance;
    public Transform t_left;
    public Transform t_right;
    public GameObject VRCanvasPointer; 
   // public SteamVR_Action_Boolean Triggerclick;
   // public static SteamVR_Input_Sources ActiveHand;
    public bool onrighthand;
  

    

    private void Awake()
    {
        onrighthand = true;
    }
    private void Update()
    {   
          if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))// if(Triggerclick.GetStateDown(SteamVR_Input_Sources.RightHand))   
        {
            if(!onrighthand)
            {
              

                VRCanvasPointer.transform.SetParent(null);
                VRCanvasPointer.transform.SetParent(t_right);
                VRCanvasPointer.transform.localPosition = Vector3.zero;
                VRCanvasPointer.transform.localEulerAngles = Vector3.zero;
                onrighthand = true;
             
            }     
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))//      if (Triggerclick.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if (onrighthand)
            {     
                VRCanvasPointer.transform.SetParent(null);
                VRCanvasPointer.transform.SetParent(t_left);
                VRCanvasPointer.transform.localPosition = Vector3.zero;
                VRCanvasPointer.transform.localEulerAngles = Vector3.zero;
                onrighthand = false;
            }
        }
    }

}

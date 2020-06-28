using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class Player : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate Playerevent;
    public static Player instance;
    public Transform CameraVR;

    //public SteamVR_Action_Boolean trackpadforward;
    //public SteamVR_Action_Boolean trackpadbackward;
    //public SteamVR_Action_Boolean DoubleGrap;
    //public SteamVR_Action_Boolean trackpadcenter;
    //public SteamVR_Action_Vector2 trackpadpos;
   

    public  bool TeleportMode;
    
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationalspeed = 1;
    
                  
    void Start()
    {
        TeleportMode = false;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Playerevent += PlayerTranslation;//subscribed to the playertranslationevent
        Playerevent += PlayerRotation;
    }
    private void FixedUpdate()
    {
        if(!TeleportMode)
        {
            Playerevent();
        } 
    }
    private void PlayerTranslation()
    {
        Quaternion cameraAngle = Quaternion.Euler(new Vector3(CameraVR.eulerAngles.x, CameraVR.eulerAngles.y, 0f));
        Vector3 cameraDirection = cameraAngle * Vector3.forward;
        //references the  camera child gameobjects parented to the player
        if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))  //oculus quest left controller//if (trackpadforward.GetState(SteamVR_Input_Sources.RightHand))
        {
            transform.Translate(cameraDirection * speed * Time.deltaTime);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))  //oculus quest left  controller//if (trackpadforward.GetState(SteamVR_Input_Sources.RightHand))
        {
            transform.Translate(-cameraDirection * speed * Time.deltaTime);
        }
    }

    private void PlayerRotation()
    {

        Vector2 rotationvalue = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        transform.Rotate(0.0f, rotationvalue.x, 0.0f);//rotational functionality just for exploration
       if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)) //if (trackpadcenter.GetLastStateDown(SteamVR_Input_Sources.Any))
        {
            transform.rotation = Quaternion.identity;//Orients the player to the right postion and direction
        } 
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.One))  //if(DoubleGrap.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if(!TeleportMode)
            {
                TeleportMode = true;
                Debug.Log(TeleportMode);
            }
            else
            {
                TeleportMode = false;
                transform.SetParent(null);
                Debug.Log(TeleportMode);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


[RequireComponent(typeof(LineRenderer))]
public class VRPointer : MonoBehaviour
{


  //  private SteamVR_Input_Sources SteamVR_Input;
  //  public SteamVR_Action_Boolean Trigger;
    public VRInputModule inputmodule;
    public GameObject DotPrefab;
    public float DefaultLength = 5f;
    private LineRenderer lineRenderer;
    GameObject Dot;
    private bool isDrawing;
    public Vector3 endposition = new Vector3();
    GameObject previoushitobject;
    Vector3 ControllerAxis;
    Vector3 direction;
    public float axis;

    Vector3 spawnPosition;
    GameObject spawnObject;

    bool onenter;
  
    private int VertexIndex = 1;

   private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Dot = Instantiate(DotPrefab);
        Dot.transform.SetParent(gameObject.transform);
      //  SteamVR_Input = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
    }

   

    private void Update()
    {
        //SteamVR_Input = GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource;
        UpdateLine();
    }
    private void UpdateLine( )
    {
       
     
       
        PointerEventData data = inputmodule.GetData();
        
        //Set Default Length 
        float targetlength = data.pointerCurrentRaycast.distance == 0 ?DefaultLength:data.pointerCurrentRaycast.distance;
        //Return a hit information 
        RaycastHit hit = CreateRaycast(targetlength);

        //Find the endpositon
     
       
        endposition = transform.position + (transform.forward * targetlength);
        ControllerAxis = Vector3.zero;

        //if hits something 
        if (hit.collider != null)
        {

           if(OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))// if (Trigger.GetState(SteamVR_Input_Sources.RightHand))
            {
                Vector3 startpos = hit.point;
                direction = hit.point = startpos;
                if (direction.x > 0.1)
                {
                    ControllerAxis = direction;  
                }
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))// if (Trigger.GetState(SteamVR_Input_Sources.LeftHand))
                {
                Vector3 startpos = hit.point;
                direction = hit.point = startpos;
                if (direction.x > 0.1)
                {
                    ControllerAxis = -direction;
                     axis = ControllerAxis.magnitude;
                }
            }
            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                ControllerAxis = Vector3.zero;
            }

            GameObject hitobject = hit.transform.gameObject;
            previoushitobject = hit.transform.gameObject;

            switch (hitobject.transform.tag)
            {
                case "Point":
                    PointerEnter("Point", hitobject);
                    if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))// if (Trigger.GetState(SteamVR_Input))
                    {
                        PointerTriggerStay("Point", hitobject);
                    }
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//if (Trigger.GetStateDown(SteamVR_Input))
                    {
                        hitobject.GetComponent<Point3D>().OnPointerDown();
                    }
                    break;
                case "Skeleton":
                    PointerEnter("Skeleton", hitobject);
                    onenter = true;
                    if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        PointerTriggerStay("Skeleton", hitobject);
                    }
                    break;

                case "Building":
                   if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))// if (Trigger.GetStateDown(SteamVR_Input))
                    {
                        PointerDown("Building", hitobject, hit.point);
                    }
                    break;
                case "MenuObject":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        GameObject go = Instantiate(hitobject.GetComponent<MenuObject>().objectPrefab, hitobject.transform.position, Quaternion.identity);
                        go.GetComponent<PortalObjectMover>().PickUp(transform.parent);
                        GetComponentInParent<PickUpHand>().currentMenuObject = go.GetComponent<PortalObjectMover>();
                        gameObject.SetActive(false);
                    }
                    break;
                case "Button":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        if (hitobject.GetComponent<BoneSpawner>())
                        {
                            Debug.Log("called");
                            if (!hitobject.GetComponent<BoneSpawner>().isSpawned)
                            {
                                hitobject.GetComponent<BoneSpawner>().isSpawned = true;
                                spawnPosition = hitobject.GetComponent<BoneSpawner>().spawnPosition;
                                Instantiate(hitobject.GetComponent<BoneSpawner>().bonePrefab, spawnPosition, Quaternion.identity);
                                spawnObject = hitobject.GetComponent<BoneSpawner>().boneHolderPrefab;
                                Invoke("InstantiateHolder", 1f);
                            }
                        }
                        else
                        {
                            
                        }
                    }
                    break;
                case "Bone":
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    {
                        hitobject.GetComponent<Bone>().Drop();
                        GetComponentInParent<PickUpHand>().currentBone = hitobject.GetComponent<Bone>();
                        hitobject.GetComponent<Bone>().PickUp(transform.parent);
                    }

                    break;
            }
            endposition = hit.point;

        }
        else
        {

            if (onenter)
            {
                PointerExit(previoushitobject.transform.tag, previoushitobject);
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))//   if (Trigger.GetStateUp(SteamVR_Input_Sources.RightHand))
            {
            PointerUp(previoushitobject.transform.tag);
            
        }

        Dot.transform.position = endposition;
        //Draw Line
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endposition);
    }

    void InstantiateHolder()
    {
        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }

    private  RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, DefaultLength);
        return hit;
    }

    private void PointerEnter(string name,GameObject hitobject)
    {
        switch(name)
        {
            case "Point":
                hitobject.GetComponent<Point3D>().OnPointerEnter();
                onenter = true;
                break;
            case "Skeleton":
                break;
               
        }
       
    }

    private void PointerTriggerStay(string name,GameObject hitobject)
    {
       
        switch (name)
        {
            case "Point":
                hitobject.GetComponent<Point3D>().OnTriggerPointerEnter();

                break;
            case "Skeleton":
                break;

              
        }

    }

    private void PointerExit( string name,GameObject hitobject)
    {
        switch (name)
        {
            case "Point":
                hitobject.GetComponent<Point3D>().OnPointerExit();
                onenter = false;
                break;
            case "Skeleton":
                break;
               
        }

    }
    private void PointerUp(string name)
    {
        switch (name)
        {
            case "Point":
                VR_LineDrawer.instance.Reset();
                break;
            case "Skeleton":
                break;
        }

    }
    private void PointerDown(string name, GameObject hitobject,Vector3 endpos)
    {
        switch (name)
        {
            case "Point":
                break;
            case "Skeleton":
                break;
            case "Building":
                break;
        }

    }
}

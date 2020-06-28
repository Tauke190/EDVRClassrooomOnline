using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using OVR;

public class VRInputModule : BaseInputModule
{

    public Camera cam;
   // public  SteamVR_Action_Boolean Pointer_click;
  //  public SteamVR_Input_Sources TargetSource;

    private PointerEventData data = null;
    private GameObject pointedobj;


    protected override void Awake()
    {
        base.Awake();
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //reset and set camera
        data.Reset();
        data.position = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        //get the object intersecting raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        pointedobj = data.pointerCurrentRaycast.gameObject;

        //clear raycast cache
        m_RaycastResultCache.Clear();

        //handle hover state
        HandlePointerExitAndEnter(data, pointedobj);


        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))//if(Pointer_click.GetStateDown(TargetSource))
        {
            ProcessPress(data);
         
        }
        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))//if(Pointer_click.GetStateup(TargetSource))
        {
            
            ProcessRelase(data);
        }


    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {

        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject pointerpressobj = ExecuteEvents.ExecuteHierarchy(pointedobj, data, ExecuteEvents.pointerDownHandler);

        if(pointerpressobj == null)
        {
            pointerpressobj = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointedobj);
        }

        data.pressPosition = data.position;
        data.pointerPress = pointerpressobj;
        data.rawPointerPress = pointedobj;

    }
    private void ProcessRelase(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress,data, ExecuteEvents.pointerUpHandler);

        GameObject PointerUpdhandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointedobj);

        if(data.pointerPress = PointerUpdhandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector3.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }



}

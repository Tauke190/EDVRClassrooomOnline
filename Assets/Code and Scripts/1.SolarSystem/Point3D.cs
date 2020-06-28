using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Point3D : MonoBehaviour
{

    private bool isDrawing;

    public  void OnPointerDown()
    {
        isDrawing = true;
        VR_LineDrawer.instance.InstantiateLinePrefab(transform.position);
    }
    public void OnPointerUp()
    {
        isDrawing = false;
        VR_LineDrawer.instance.FinishLine();
    }
    public void OnTriggerPointerEnter()
    {
        VR_LineDrawer.instance.UpdatePoint(transform.position);
        Debug.Log("On TriggerPointerEnter");
    }

    public void OnPointerExit()
    {
        Debug.Log("On Pointer Exit");
    }

    public void OnPointerEnter()
    {
        Debug.Log("On Pointer Enter");
    }
}



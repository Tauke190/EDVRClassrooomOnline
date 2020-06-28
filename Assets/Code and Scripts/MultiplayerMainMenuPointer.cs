using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiplayerMainMenuPointer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lr;

    [SerializeField]
    public float lineLength = 20f;

    private void Start()
    {
        lr.positionCount = 2;
    }


    private void Update()
    {
        lr.SetPosition(0, transform.position);

        Vector3 endPosition = transform.forward * lineLength;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray,out hit,lineLength))
        {
            endPosition = hit.point;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                Debug.Log("down");
                if (hit.transform.GetComponent<MenuObj>())
                {
                    hit.transform.GetComponent<MenuObj>().Clicked();
                }
            }
        }

        lr.SetPosition(1, endPosition);
    }
}

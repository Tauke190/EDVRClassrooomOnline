using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line_VR : MonoBehaviour
{
    [HideInInspector]
    public bool isDrawing;

    private LineRenderer _lineRenderer;

    private int currentPositionIndex = 1;

    private bool positionUpdating;

    VRPointer vrpointer;

    private void Start()
    {
        vrpointer = FindObjectOfType<VRPointer>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position);
    }


    public void DrawLine(Vector3 StartPoint,Vector3 EndPoint)
    {
        _lineRenderer.SetPosition(0, StartPoint);
        _lineRenderer.SetPosition(0, EndPoint);
    }
    void DrawLine()
    {
        Vector3 pos = vrpointer.endposition;
       _lineRenderer.SetPosition(currentPositionIndex, pos);
    }

    void Update()
    {
        if (isDrawing)
        {
            DrawLine();
        }
    }

    public void FinishDrawing()
    {
        if (_lineRenderer.positionCount <= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            isDrawing = false;
            _lineRenderer.positionCount--;
        }
    }

    public void PositionUpdate(Vector3 _position)
    {
        if (_lineRenderer == null)
            return;

        positionUpdating = true;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(currentPositionIndex, _position);
        currentPositionIndex++;
        positionUpdating = false;
    }

}

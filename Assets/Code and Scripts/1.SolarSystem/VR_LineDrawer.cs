using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_LineDrawer : MonoBehaviour
{
    public static VR_LineDrawer instance;
    [SerializeField]
    private GameObject linePrefab;

    GameObject currentLinePrefab;

    bool draw;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    public void InstantiateLinePrefab(Vector3 _position)
    {
        draw = true;
        currentLinePrefab = Instantiate(linePrefab, _position, Quaternion.identity,gameObject.transform) as GameObject;
        currentLinePrefab.GetComponent<Line_VR>().isDrawing = true;
    }

    public void FinishLine()
    {
        draw = false;
        currentLinePrefab.GetComponent<Line_VR>().FinishDrawing();
    }

    public void UpdatePoint(Vector3 _position)
    {
        if (draw)
        {
            if(currentLinePrefab)
            {
                currentLinePrefab.GetComponent<Line_VR>().PositionUpdate(_position);
            }
           
        }
    }

    public void Reset()
    {
        for (int i = transform.childCount-1; i >=0; i --)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

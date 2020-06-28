using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public GameObject[] cubes;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;



    private void Start()
    {
        foreach(GameObject obj in cubes)
        {
            obj.SetActive(false);
        }
    }

    void  Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickUp))
        { 
            cubes[0].SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            cubes[1].SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            cubes[2].SetActive(true);
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            cubes[3].SetActive(true);
        }
        Vector3 laxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 raxis2 = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        text1.text = laxis.x.ToString();
        text2.text = laxis.y.ToString();
        text3.text = raxis2.x.ToString();
        text4.text = raxis2.y.ToString();
    }
}

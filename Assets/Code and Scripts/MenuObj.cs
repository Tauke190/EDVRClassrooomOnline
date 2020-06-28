using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuObj : MonoBehaviour
{
    public void Clicked()
    {
        Debug.Log("Clicked " + transform.name);
    }
}

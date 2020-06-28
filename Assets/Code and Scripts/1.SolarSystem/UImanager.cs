
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{

    public float Time = 20f;
    public static UImanager instance;
    public GameObject[] PlanetUI;
    private void Start()
    {
        foreach (GameObject palnetsimage in PlanetUI)
        {
            palnetsimage.gameObject.SetActive(false);
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void EnableHandProjector(int index,bool value)
    {
        PlanetUI[index].gameObject.SetActive(value);
    }
}

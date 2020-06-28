using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SolarConfig : MonoBehaviour
{

   


    
    [Range(1, 1000)]
    public float bodyScaleModifier = 1f;
    [Space(10)]
    [Range(0, 1)]
    public float bodyDistanceModifier = 1f;
    [Space(10)]
    public float kilometerSize = 0.0000007187005893344833f;

    public static float scaleMod;
    public static float distanceMod;

    public static float scaleRel;
    public static float distanceRel;

    public static float kilometerScale;
  

    private void Awake()
    {
        scaleMod = bodyScaleModifier;
        distanceMod = bodyDistanceModifier;

        scaleRel = 1f;
        distanceRel = 1f;

        kilometerScale = kilometerSize;
       
    }
}

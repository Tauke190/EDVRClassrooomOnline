using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrences : MonoBehaviour
{
    public static Refrences instance;

    public Material boneNormalMaterial;
    public Material boneHighLightMaterial;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;
    }
}

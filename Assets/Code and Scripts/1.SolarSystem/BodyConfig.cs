using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyConfig : MonoBehaviour
{
    public float bodyRadius;
    public bool highlightEnabled = false;
    
    [HideInInspector]
    public bool overrideSize;
    [HideInInspector]
    public float scaleModifier = 1f;

    [HideInInspector]
    public bool isSun;
    [HideInInspector]
    public Transform orbitingSun;

    public Transform orbitingAstroidBelt;
    public float astroidBeltSpeed;

    Renderer bodyRenderer;
    bool previousHighlightEnabled;

    private void OnAwake()
    {
        bodyRenderer = GetComponent<Renderer>();
        previousHighlightEnabled = highlightEnabled;
    }

    private void Update()
    {
        float scaleMod;

        if (overrideSize)
        {
            scaleMod = SolarConfig.kilometerScale * scaleModifier;
        }
        else
        {
            scaleMod = SolarConfig.kilometerScale * SolarConfig.scaleMod;
        }

        transform.localScale = new Vector3(bodyRadius, bodyRadius, bodyRadius) * 2f * scaleMod;


        if (highlightEnabled != previousHighlightEnabled)
        {
            ToggleHighlight();
        }

        previousHighlightEnabled = highlightEnabled;


        if (orbitingAstroidBelt)
        {
            orbitingAstroidBelt.Rotate(orbitingAstroidBelt.up * astroidBeltSpeed * Time.deltaTime);
        }
    }

    public void ToggleHighlight()
    {
        if (highlightEnabled)
        {
            
        }
    }
}
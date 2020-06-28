using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyConfig))]
public class OrbitController : MonoBehaviour
{
    public float orbitAphelion;
    public float orbitPerihelion;
    public float orbitEccentricity;
    public float orbitInclination;
    public float orbitLongitude;
    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    [HideInInspector]
    public Ellipse orbitPath;
    [HideInInspector]
    public Transform orbitOrigin;
    [HideInInspector]
    public SolarConfig solarConfig;
    [HideInInspector]
    public Vector2 helionAdjustment;

    private void Start()
    {
        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());

        transform.parent.eulerAngles = new Vector3(0f, orbitLongitude, orbitInclination);
    }

    void SetOrbitingObjectPosition()
    {
        float semiMajorAxis = (orbitAphelion + orbitPerihelion) / 2f;
        float semiMinorAxis = semiMajorAxis * Mathf.Sqrt(1f - Mathf.Pow(orbitEccentricity, 2f));
        //float fociDistance = Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 2f) - Mathf.Pow(semiMinorAxis, 2f));

        orbitPath.xAxis = semiMajorAxis;
        orbitPath.yAxis = semiMinorAxis;
        Vector2 pathPos = orbitPath.Evaluate(orbitProgress);
        helionAdjustment = new Vector3(semiMajorAxis - orbitPerihelion, 0f, 0f);
        Vector2 orbitPos = pathPos + helionAdjustment;

        transform.localPosition = ((orbitOrigin.localPosition + new Vector3(orbitPos.x, 0f, orbitPos.y)) * (SolarConfig.kilometerScale * SolarConfig.distanceMod)) * SolarConfig.distanceRel;
    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }

        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }

    private void OnValidate()
    {
        solarConfig = transform.parent.GetComponent<SolarConfig>();
        orbitOrigin = GetComponent<BodyConfig>().orbitingSun;
    }
}

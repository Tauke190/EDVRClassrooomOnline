using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    [Range(24, 124)]
    public int segments = 124;
    public float width = 0.03f;
    public Material trajectoryLineMaterial;
    public bool renderOrbit = true;

    LineRenderer lr;
    Ellipse ellipse;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
        lr.material = trajectoryLineMaterial;
        lr.startWidth = width;
        lr.endWidth = width;
    }

    private void Update()
    {
        if (renderOrbit && lr)
        {
            CalculateEllipse();
        }
    }

    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];

        for (int i = 0; i < segments; i++)
        {
            OrbitController orbitController = GetComponent<OrbitController>();
            float helionAdjustment = orbitController.helionAdjustment.x;

            Transform orbitOrigin = orbitController.orbitOrigin;
            ellipse = orbitController.orbitPath;
            float angle = ((float)i / (float)segments) * 360 * Mathf.Deg2Rad;
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments);
            Vector3 orbitPosition = new Vector3(position2D.x + helionAdjustment, 0f, position2D.y);
            points[i] = transform.parent.rotation * ((orbitOrigin.localPosition + orbitPosition) * (SolarConfig.kilometerScale * SolarConfig.distanceMod)) * SolarConfig.distanceRel;
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying && !lr)
        {
            lr = GetComponent<LineRenderer>();
            lr.enabled = false;
        }
    }
}

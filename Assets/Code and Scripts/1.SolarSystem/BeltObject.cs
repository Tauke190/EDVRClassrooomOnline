using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltObject : MonoBehaviour
{
    private float orbitSpeed;
    private GameObject parent;
    private bool rotationClockwise;
    private float rotationSpeed;
    private Vector3 rotationDirection;

    public void SetupBeltObject(float _speed, float _rotationSpeed, GameObject _parent, bool _rotateClockwise)
    {
        orbitSpeed = _speed;
        rotationSpeed = _rotationSpeed;
        parent = _parent;
        rotationClockwise = _rotateClockwise;
        rotationDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void Update()
    {
        if(rotationClockwise)
        {
            transform.RotateAround(parent.transform.position, parent.transform.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(parent.transform.position, -parent.transform.up, orbitSpeed * Time.deltaTime);
        }

        transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField]
    private float cameraSensitivity;

    float _rotationX;
    float _rotationY;

    void Update()
    {
        if (SpaceShip_Manager.isMoving)
        {
            return;
        }

        _rotationX -= Input.GetAxis("Mouse Y") * cameraSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -45, 45);

        float delta = Input.GetAxis("Mouse X") * cameraSensitivity;
        float rotationY = transform.localEulerAngles.y + delta;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.Translate(y,0, x);
    }
}

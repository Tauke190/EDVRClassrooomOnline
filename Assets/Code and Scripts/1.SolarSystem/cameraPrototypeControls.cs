using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPrototypeControls : MonoBehaviour
{
    public Transform prototypeCamera;
    public float zoomModifier = 1f;
    public float rotateModifier = 1f;
    public float traverseModifier = 1f;
    public Transform focusObject;

    private void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.position += transform.forward * zoomInput * zoomModifier;

        if (Input.GetMouseButton(2))
        {
            focusObject.localPosition += new Vector3(mouseInput.x, 0, mouseInput.y) * traverseModifier;
        }
        else
        {
            focusObject.localEulerAngles += Vector3.up * mouseInput.x * rotateModifier;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float speed;
    void Update()
    {
        transform.Rotate(0f, 1 * Time.deltaTime * speed, 0f);
    }
}

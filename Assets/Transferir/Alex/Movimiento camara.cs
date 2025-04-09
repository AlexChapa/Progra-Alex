using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientocamara : MonoBehaviour
{
    private float rotationY = 0.0f;
    private float rotationX = 0.0f;

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X");
        rotationY -= Input.GetAxis("Mouse Y");

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
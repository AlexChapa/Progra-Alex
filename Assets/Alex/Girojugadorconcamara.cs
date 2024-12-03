using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girojugadorconcamara : MonoBehaviour
{
    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        cameraForward.y = 0;
    }
}
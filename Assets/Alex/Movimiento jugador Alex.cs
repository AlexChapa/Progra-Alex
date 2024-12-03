using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientojugadorAlex : MonoBehaviour
{
    public float crouchSpeed = 3;
    public float walkSpeed = 5;
    public float runSpeed = 7;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        Vector3 moveDirection = (cameraForward * VerticalMove() + cameraRight * HorizontalMove()).normalized;

        rb.velocity = moveDirection * ActualSpeed();
    }

    private float ActualSpeed()
    {
        return IsRunning() ? runSpeed : IsCrouching() ? crouchSpeed : walkSpeed;
    }

    private float HorizontalMove()
    {
        return Input.GetAxis("Horizontal");
    }

    private float VerticalMove()
    {
        return Input.GetAxis("Vertical");
    }
    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsCrouching()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

}

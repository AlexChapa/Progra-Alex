using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 10f;
    public float crouchSpeed = 3f;
    public float walkSpeed = 5f;
    public float jumpForce = 7f; // Añadido: Fuerza del salto

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        Vector3 moveDirection = (cameraForward * VerticalMove() + cameraRight * HorizontalMove()).normalized;

        rb.velocity = new Vector3(moveDirection.x * ActualSpeed(), rb.velocity.y, moveDirection.z * ActualSpeed()); 
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
          rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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


    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}


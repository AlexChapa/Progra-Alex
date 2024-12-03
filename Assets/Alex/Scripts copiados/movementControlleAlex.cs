using UnityEngine;

/// <summary>
/// Un movimiento que requiere fisica se debe de hacer con rigidbody
/// 
/// Un movimiento que unicamente es lateral, salto, y ya, Usas CharacterController
/// 
/// Un movimiento con Transform puedes hacer de todo, pero requiere más trabajo
/// 
/// Inputs
/// Ridigbody
/// 3 Velocidades
/// 
/// </summary>
/// 
namespace Alex
{
    public class movementControlleAlex : MonoBehaviour
    {
        public float crouchSpeed = 3;
        public float walkSpeed = 5;
        public float runSpeed = 7;

        private Rigidbody rb;

        corrutinasAlex corrutinas;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            corrutinas = GetComponent<corrutinasAlex>();
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rb.velocity = transform.rotation * new Vector3(HorizontalMove(), 0, VerticalMove()) * ActualSpeed();
        }

        private float ActualSpeed()
        {
            return IsRunning() ? runSpeed : IsCrouching() ? crouchSpeed : walkSpeed; // Operador ternario
        }

        public float HorizontalMove()
        {
            return Input.GetAxis("Horizontal");
        }

        public float VerticalMove()
        {
            return Input.GetAxis("Vertical");
        }

        public bool IsMoving()
        {
            if (HorizontalMove() != 0 || VerticalMove() != 0)
            {
                Debug.Log("Me muevo");
                return true;
            }
            else
            {
                Debug.Log("No me muevo");
                return false;
            }
        }

        public bool IsRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        private bool IsCrouching()
        {
            return Input.GetKey(KeyCode.LeftControl);
        }

    }
}
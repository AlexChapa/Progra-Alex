using UnityEngine;

public class Player : MonoBehaviour
{
    private Quaternion targetRotation;
    private float rotationSpeed = 400f; // Velocidad de rotación en grados por segundo
    private float rotationStep = 45f; // Cantidad de rotación en grados por paso

    [SerializeField] private Transform checkGameOver;
    [SerializeField] private float range;

   [HideInInspector]  public bool isGameOver = false; // Variable para verificar si el juego ha terminado



    private void Start()
    {
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        RotateInput();
        SmoothRotate();
        GameOver();
    }

    private void RotateInput() // Se encarga de la rotación del Player
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation *= Quaternion.Euler(0, 0, rotationStep);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetRotation *= Quaternion.Euler(0, 0, -rotationStep);
        }
    }

    private void SmoothRotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void GameOver()
    {
        if (Detection())
        {
            isGameOver = true; // Cambia el estado del juego a Game Over
            Debug.Log("Game Over");
            Cursor.lockState = CursorLockMode.None; // Libera el cursor
            gameObject.SetActive(false); // Desactiva el objeto del jugador
        }
    }

    private  bool Detection()
    {
        return Physics.CheckSphere(checkGameOver.position, range, LayerMask.GetMask("Target"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkGameOver.position, range);
    }
}

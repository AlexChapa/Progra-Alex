using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject reset;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int wheelCount;
    private PlayFabManager playFabManager;
    private Player player; // Referencia al script Player
    private bool isCoroutineRunning = false; // Bandera para controlar la ejecución de la corrutina

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        playFabManager = FindFirstObjectByType<PlayFabManager>();
    }

    private void Update()
    {
        if (player.isGameOver && !isCoroutineRunning)
        {
            StartCoroutine(End());
        }

    }

    public void Reset()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IncrementWheelCount()
    {
        wheelCount++;
        scoreText.text = wheelCount.ToString();
        playFabManager.UpdateLeaderBoard(wheelCount); // Actualiza la tabla de clasificación con la puntuación del jugador
    }

    private IEnumerator End()
    {
        isCoroutineRunning = true; // Marcar la corrutina como en ejecución
        leaderboard.SetActive(true);
        Debug.Log("Download Leadboard...");
        yield return new WaitForSeconds(1.7f);
        playFabManager.RequestLeaderboard();
        isCoroutineRunning = false; // Marcar la corrutina como finalizada
        yield break; // Terminar la corrutina y evitar que se vuelva a ejecutar
    }
}

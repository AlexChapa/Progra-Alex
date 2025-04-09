using TMPro;
using UnityEngine;

public class RuedaPollo : MonoBehaviour
{
    private static int score = 0; // estático para que se comparta entre todas las instancias
    private TextMeshProUGUI scoreText;

    private GameOver gameOver; // Referencia al script GameOver

    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        gameOver = FindFirstObjectByType<GameOver>(); 
    }
    private void Update()
    {
        UpdateScoreText(); // Actualizar el texto al inicio
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            gameOver.IncrementWheelCount(); // Incrementa el contador de ruedas en el script GameOver
            Debug.Log("Score: " + score); 
            UpdateScoreText(); 
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;

        }
        else
        {
            Debug.LogWarning("scoreText no está asignado.");
        }

    }
}

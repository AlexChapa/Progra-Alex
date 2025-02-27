using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetctive : MonoBehaviour
{
     [SerializeField] private Score score;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            score.ScoreCount();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

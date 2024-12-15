using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrucciónLlave : MonoBehaviour
{
    public GameObject objetoADestruir;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (objetoADestruir != null)
        {
            Destroy(objetoADestruir);
        }
    }
}
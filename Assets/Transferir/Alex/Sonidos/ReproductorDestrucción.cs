using UnityEngine;

public class ReproductorDestrucción : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnDestroy()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

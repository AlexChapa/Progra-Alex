using System.Collections;
using UnityEngine;

public class MuertePlayer : MonoBehaviour
{
    [SerializeField] GameObject particulas;

    private void Start()
    {
        particulas.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        StartCoroutine(Explocion());
    }
}
    public IEnumerator Explocion()
    {
        Debug.Log("exploision");
        particulas.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(this);
        yield return null;
        particulas.SetActive(false);
    }

}

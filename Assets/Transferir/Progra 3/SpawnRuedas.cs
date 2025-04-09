using System.Collections;
using UnityEngine;
public class SpawnRuedas : MonoBehaviour
{
    [SerializeField] private GameObject prefabWheel;
    [SerializeField] private float spawnInterval = 3.0f;
    [SerializeField] private float speedWheel = 10f;

    private Rigidbody rb;

    private void Start()
    {
        StartCoroutine(SpawnPrefabCoroutine());
    }

    private IEnumerator SpawnPrefabCoroutine()
    {
        while (true)
        {
            // Generar un número aleatorio entre 0 y 7, y multiplicarlo por 45 para obtener un ángulo de rotación
            int randomMultiplier = Random.Range(0, 8);
            float randomRotation = randomMultiplier * 45f;
            Quaternion rotation = Quaternion.Euler(0, 0, randomRotation);

            // Instanciar la rueda con la rotación aleatoria
            GameObject wheelInstance = Instantiate(prefabWheel, transform.position, rotation);
            rb = wheelInstance.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.back * speedWheel, ForceMode.Impulse);
            Destroy(wheelInstance, 8f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

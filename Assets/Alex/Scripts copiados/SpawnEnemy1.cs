using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemigoPrefab;
    private Vector3 puntoInicio;
    public float velEnemigo;
    bool spawnActivo = true;

   
    [HideInInspector]
    public Vector3 rotacion;

    void Update()
    {
        SpawnEnemigo();
    }
    void SpawnEnemigo()
    {
        if (spawnActivo)
        {
            float posicionX= Random.Range(2f, 2f);
            puntoInicio = new Vector3(posicionX, 0, 0);

            Quaternion rotacionQuaternion = Quaternion.Euler(rotacion);
            GameObject clone = Instantiate(enemigoPrefab, puntoInicio, rotacionQuaternion);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * velEnemigo);
            Destroy(clone, 150f);
            StartCoroutine(ColdDown());
        }
    }

    IEnumerator ColdDown()
    {
        spawnActivo = false;
        yield return new WaitForSeconds(1f);
        spawnActivo = true;
    }
}



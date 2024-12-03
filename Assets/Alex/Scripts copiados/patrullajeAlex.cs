using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Alex
{
    /// <summary>
    /// EJERCICIO/TAREA
    /// 
    /// Al regresar a patrullar debe de ir al ultimo punto a donde se dirigia antes de detectar al objetivo. Y regresar a su velocidad de patrullaje.
    /// Y continuar patrullando
    /// 
    /// </summary>
    public class patrullajeAlex : MonoBehaviour
    {

        public Transform[] puntosDePatrullaje;
        public float tiempoDeVigilancia;

        private NavMeshAgent agent;


        private void Start()
        {

            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Patrullar());
        }

        private IEnumerator Patrullar()
        {
            Transform randomPos = RandomPos();

            agent.destination = randomPos.position;

            yield return new WaitUntil(() => Vector3.Distance(transform.position, randomPos.position) < 2);

            Debug.Log("Ya llegó al punto");

            yield return new WaitForSeconds(tiempoDeVigilancia);

            StartCoroutine(Patrullar());
        }

        public void DejarDePatrullar()
        {
            StopAllCoroutines();
        }

        private Transform RandomPos()
        {
            int randomPoint = Random.Range(0, puntosDePatrullaje.Length);
            return puntosDePatrullaje[randomPoint];
        }

    }
}

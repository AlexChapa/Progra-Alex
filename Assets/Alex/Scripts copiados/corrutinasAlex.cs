using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class corrutinasAlex : MonoBehaviour
    {
        public bool tiempoPausado;
        public bool canReload = true;

        public int balas = 0;
        public int municionMaxima = 10;

        private void Start()
        {
            StartCoroutine(Reload());
        }

        private void Update()
        {
            if (!canReload)
            {
                StopCoroutine(Reload());
                Debug.Log("Se interrumpio la recarga");
            }
        }

        private IEnumerator Reload()
        {

            for (int i = 0; i < municionMaxima; i++)
            {
                balas++;
                yield return new WaitForSeconds(2);
            }
        }

        private IEnumerator DebuggearWaitForSeconds()
        {
            Debug.Log("Hola");

            yield return new WaitForSeconds(5); // Se espera a que pasen 5 segundos en tiempo escalado

            Debug.Log("Adios"); // Y sigue
        }

        private IEnumerator DebuggearWaitForSecondsRealtime()
        {
            Debug.Log("Hola");

            yield return new WaitForSecondsRealtime(5); // Se espera a que pasen 5 segundos en tiempo real

            Debug.Log("Adios");
        }

        private IEnumerator DebuggearWaitUntil()
        {
            Debug.Log("Hola");

            yield return new WaitUntil(() => tiempoPausado == true); // Se espera hasta que la condicion se cumpla

            Debug.Log("Adios");
        }

        private IEnumerator DebuggearWaitWhile()
        {
            Debug.Log("Hola");

            yield return new WaitWhile(() => tiempoPausado == true); // Se espera hasta que la condicion se deje de cumplir

            Debug.Log("Adios");
        }


    }
}
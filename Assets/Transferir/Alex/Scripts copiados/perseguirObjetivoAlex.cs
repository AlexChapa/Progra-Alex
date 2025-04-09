using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Alex
{
    /// <summary>
    /// /// EJERCICIO/TAREA
    /// 
    /// Hacer que deje de perseguir al objetivo
    /// </summary>
    public class perseguirObjetivoAlex : MonoBehaviour
    {

        public Transform objetivo;
        public float velocidad;

        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Perseguir()
        {
            agent.speed = velocidad;
            agent.destination = objetivo.position;
        }

    }
}
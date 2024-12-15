using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public float radio;
    public LayerMask playerMask;
    private Vector3 origin;

    public IAHealth health;

    void Start()
    {
        origin = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
       Persecusion();   
    }
    void Persecusion()
    {
        Physics.CheckSphere(transform.position, radio, playerMask);
        if (Physics.CheckSphere(transform.position, radio, playerMask))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent.SetDestination(player.position);
            agent.stoppingDistance = 0;
            
        }
        else
        {
            agent.SetDestination(origin);
            agent.stoppingDistance = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}

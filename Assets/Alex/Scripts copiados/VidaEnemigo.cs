using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaEnemigo;
    public int dañoPelota;
    public GameObject player;
    
   
        public void Danio(int danio)
        {
            vidaEnemigo -= danio;

            if (vidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Bala"))
    //    {
    //        Danio(dañoPelota);
    //        gameObject.transform.GetComponent<VidaEnemigo>().Danio(dañoPelota);
    //        Destroy(this.gameObject);
    //    }

    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(player);
    //    }
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class Puertas : MonoBehaviour,Interactable
    {
        public bool tieneLlave = false;
        public InventoryHandler1 inventario;
        public int llaveRequerida;
        int numeroLlave;

        public void Interact()
        {
            inventario = FindObjectOfType<InventoryHandler1>();

            foreach (var llaves in inventario.inventory)
            {
                Llave numero = llaves.itemPrefab.GetComponent<Llave>();

                if (numero.numeroLlave == llaveRequerida)
                {
                    tieneLlave = true;
                }
            }

            if (tieneLlave)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No tienes llave unu");
            }
        }
    }
}
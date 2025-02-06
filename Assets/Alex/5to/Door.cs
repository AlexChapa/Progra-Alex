
using UnityEngine;
using System.Collections;

namespace Alex
{
public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private TipoDePuerta tipoDePuerta;
    [SerializeField] private bool eventoActivado;
    [SerializeField] private SOItem2 key;
    [SerializeField] private SOItem2[] keys;

    [SerializeField] private LayerMask playerLayer;

    private InventoryHandler1 inventoryHandler;

    private void Awake()
    {
        inventoryHandler = FindObjectOfType<InventoryHandler1>();
    }

    private void Update()
    {
        Automatica();
    }

    public void Interact()
    {
        switch (tipoDePuerta)  
        {
            case TipoDePuerta.Normal:
                {
                    Normal();
                    break;
                }

            case TipoDePuerta.DeLlave:
                {
                    DeLlave();
                    break;
                }

            case TipoDePuerta.Evento:
                {
                    Evento();
                    break;
                }

            case TipoDePuerta.MultiplesLlaves:
                {
                    MultiplesLlaves();
                    break;
                }
        }
    }

    private void Automatica()
    {
        if (tipoDePuerta == TipoDePuerta.Automatica && Detection())
        {
                Debug.Log("Se abre automatico");
                Destroy(gameObject);
        }
    }

    private void Normal()
    {
        Debug.Log("Se abre");
        Destroy(gameObject);
    }

    private void Evento()
    {
        if (eventoActivado)
        {
            Destroy(gameObject);
            Debug.Log("Se ha activado el evento");
        }
        else
        {
            Debug.Log("No se ha activado el evento");
        }
    }

    private void MultiplesLlaves()
    {
        bool allKeysPresent = true;
        foreach (SOItem2 item in keys)
        {
            if (!inventoryHandler.inventory.Contains(item))
            {
                allKeysPresent = false;
                Debug.Log("No tienes las llaves");
                break;
            }
        }
        if (allKeysPresent)
        {
            Destroy(gameObject);
            Debug.Log("Se abrio con multiples llaves");
        }
    }

    private void DeLlave()
    {
        if (inventoryHandler.inventory.Contains(key))
        {
            Destroy(gameObject);
            Debug.Log("Se abrio con 1 llave");
        }
        else
        {
            Debug.Log("No tienes la llave");
        }
    }

    bool Detection()
    {
        return Physics.CheckSphere(transform.position, 8f, playerLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}

public enum TipoDePuerta
{
    Automatica, Normal, DeLlave, Evento, MultiplesLlaves
}

}
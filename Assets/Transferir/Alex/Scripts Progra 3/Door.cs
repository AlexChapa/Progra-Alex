using System.Collections;
using UnityEngine;

namespace AlexDoor
{

    
    public class Door : MonoBehaviour, Interactable
    {
        [SerializeField] public TipoDePuerta tipoDePuerta;
        [SerializeField] public bool eventoActivado = false;
        [SerializeField] public bool showKeysNames;
        public SOItem2 key;
        public SOItem2[] keys;

        private InventoryHandler1 inventoryHandler;
        private Score score;
        internal Texture2D names;

      

        private void Awake()
        {
            inventoryHandler = FindFirstObjectByType<InventoryHandler1>();
            score = FindFirstObjectByType<Score>();
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
            if (tipoDePuerta == TipoDePuerta.Automatica && Touch())
            {
                StartCoroutine(OpenDoorAutomatic());
            }
        }

        private void Normal()
        {
            Debug.Log("Se abre");
            StartCoroutine(NormalOpen());
        }

        private void Evento()
        {
            if (score.isLevelComplete == true)
            {
                eventoActivado = true;
                Debug.Log("Se ha activado el evento");
                Destroy(gameObject);

            }
            else
            {
                Debug.Log("No se ha activado el evento");
            }
        }

        private void MultiplesLlaves()
        {
            foreach (SOItem2 item in keys)
            {
                if (inventoryHandler.inventory.Contains(item))
                {
                    Debug.Log("Se abrio con multiples llaves");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("No tienes las llaves");
                }
            }
        }

        private void DeLlave()
        {
            Debug.Log("Ver");
            if (inventoryHandler.inventory.Contains(key))
            {
                Debug.Log("Si tengo llave");
                Debug.Log("Se abrio con 1 llave");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No tienes la llave");
            }
        }

        [ContextMenu("Show | Hide keys Names")]
        public void ToggleBool()
        {
            showKeysNames = !showKeysNames;
        }

        bool Touch()
        {
            return Physics.CheckSphere(transform.position, 4f, LayerMask.GetMask("Player"));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 4f);
        }

        IEnumerator OpenDoorAutomatic()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 4f, 4f);
            Debug.Log("Se abre automaticamente");
            yield return new WaitForSeconds(2f);
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down * 4f, 4f);
        }

        IEnumerator NormalOpen()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 4f, 4f);
            yield return new WaitForSeconds(2f);
        }
    }

    public enum TipoDePuerta
    {
        Automatica, Normal, DeLlave, Evento, MultiplesLlaves
    }
}



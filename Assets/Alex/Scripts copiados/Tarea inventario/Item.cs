using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Alex
{
    public class Item : MonoBehaviour, Interactable
    {
        [SerializeField] private SOItem2 item;
        private InventoryHandler1 inventory;

        private void Start()
        {
            inventory = FindObjectOfType<InventoryHandler1>();
        }

        public void Interact()
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }

    }
}
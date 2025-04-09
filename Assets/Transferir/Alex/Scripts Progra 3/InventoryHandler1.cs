using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class InventoryHandler1 : MonoBehaviour
    {

        public List<SOItem2> inventory = new List<SOItem2>();
        [SerializeField] private Image newItemImage;

        public void AddItem(SOItem2 item)
        {
            inventory.Add(item);
            Debug.Log("Se ha añadido " + item.name + " a tu inventario");
            Debug.Log("Descripcion: " + item.description);
            newItemImage.sprite = item.sprite;
        }

    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alex
{

    public class InventoryHandler : MonoBehaviour
    {

        public List<SOItem> inventory = new List<SOItem>();
        [SerializeField] private Image newItemImage;

        public void AddItem(SOItem item)
        {
            inventory.Add(item);
            Debug.Log("Se ha añadido " + item.name + " a tu inventario");
            Debug.Log("Descripcion: " + item.description);
            newItemImage.sprite = item.sprite;
        }

    }

}
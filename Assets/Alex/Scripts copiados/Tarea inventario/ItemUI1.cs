using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Alex;


public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        [SerializeField] private Image itemImage;

        public void SetItemInfo(SOItem item)
        {
            itemName.text = item.name;
            itemDescription.text = item.description;
            itemImage.sprite = item.sprite;
        }

    } 


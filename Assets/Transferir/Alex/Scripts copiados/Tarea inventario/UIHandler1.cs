using System;
using UnityEngine;

namespace Alex
{
    public class UIHandler1 : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject uiItemPrefab;
        [SerializeField] private GameObject displayArea;
        [SerializeField] private Page[] pages = new Page[4];

        public int actualPage = 0;
        private int maxItemsPerPage = 2;
        private int actualItems = 0;
        [SerializeField] private InventoryHandler1 inventoryRef;

        public bool inventoryOpened = false;

        private void Awake()
        {
            inventoryRef = FindAnyObjectByType<InventoryHandler1>();

            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].items = new GameObject[maxItemsPerPage];
                pages[i].itemsDeployed = 0;
            }
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                OpenInventory();
            }

            if (inventoryOpened)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    NextPage();
                }

                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PreviousPage();
                }

            }

        }



        private void OpenInventory()
        {

            inventoryOpened = !inventoryOpened;
            inventoryCanvas.SetActive(inventoryOpened);

            if (inventoryOpened)
            {
                actualPage = 0;
                HideAllItems();
                ShowItems(actualPage);

                if (inventoryRef.inventory.Count <= 0)
                {
                    return;
                }

                else if (inventoryRef.inventory.Count > actualItems)
                {


                    for (int i = GetTotalItemsDeployed(); i < inventoryRef.inventory.Count; i++)
                    {
                        GameObject item = Instantiate(uiItemPrefab);
                        item.transform.SetParent(displayArea.transform);
                        item.transform.localScale = Vector3.one;
                        item.GetComponent<ItemUI>().SetItemInfo(inventoryRef.inventory[i]);
                        pages[actualPage].items[pages[actualPage].itemsDeployed] = item;
                        pages[actualPage].itemsDeployed++;

                        if (pages[actualPage].itemsDeployed >= maxItemsPerPage)
                        {
                            actualPage++;
                        }
                    }

                    actualItems = inventoryRef.inventory.Count;
                    HideAllItems();
                    ShowItems(actualPage);


                }

                else
                {
                    HideAllItems();
                    ShowItems(actualPage);
                }
            }

        }

        private void NextPage()
        {
            if (actualPage < pages.Length - 1)
            {
                HideItems(actualPage);
                actualPage++;
                ShowItems(actualPage);
            }
        }

        private void PreviousPage()
        {
            if (actualPage > 0)
            {
                HideItems(actualPage);
                actualPage--;
                ShowItems(actualPage);
            }
        }

        [ContextMenu("Show Items in Page")]
        private void ShowItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(true);
            }
        }

        [ContextMenu("Hide Items in Page")]
        private void HideItems()
        {
            for (int i = 0; i < pages[actualPage].itemsDeployed; i++)
            {
                pages[actualPage].items[i].SetActive(false);
            }
        }

        private void ShowItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(true);
            }
        }

        private void HideItems(int page)
        {
            for (int i = 0; i < pages[page].itemsDeployed; i++)
            {
                pages[page].items[i].SetActive(false);
            }
        }

        [ContextMenu("Hide All Items")]
        private void HideAllItems()
        {
            for (int page = 0; page <= actualPage; page++)
            {
                Debug.Log(page);
                for (int item = 0; item < pages[page].itemsDeployed; item++)
                {
                    Debug.Log(item);
                    pages[page].items[item].SetActive(false);
                }
                Debug.Log("Siguiente pagina");
            }
        }

        private int GetTotalItemsDeployed()
        {
            int items = 0;
            for (int i = 0; i < pages.Length; i++)
            {
                items += pages[i].itemsDeployed;
            }
            return items;
        }
    }

    [Serializable]
    public struct Page
    {
        public int itemsDeployed;
        public GameObject[] items;
    }
}





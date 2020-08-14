using UnityEngine;
using System;

namespace RPG.Inventory
{
    public class InventoryView : MonoBehaviour, InventoryViewInterface, IClickObserve
    {
        [SerializeField]
        private ItemDetailsView itemDetails = null;

        [SerializeField]
        private GameObject inventoryOrganizer = null, templateButton = null;

        private ItemButton[] itemButtons;

        public Func<int, DetailData> OnItemButtonClick { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

            for (int i = 0; i < itemButtons.Length; i++)
            {
                GameObject itemBtn = Instantiate(templateButton);
                itemBtn.transform.SetParent(inventoryOrganizer.transform);
                itemButtons[i] = itemBtn.GetComponent<ItemButton>();
                itemButtons[i].Init(this, i);
            }
        }

        public void Display(ItemHolder[] holders)
        {
            for (int i = 0; i < ItemManager.MAX_INVENTORY_SIZE; i++)
            {
                itemButtons[i].DisplayItem(holders[i]);
            }
        }

        public void OnButtonClick(int idx)
        {
            if (OnItemButtonClick == null) return;

            DetailData data = OnItemButtonClick(idx);
            itemDetails.Show(data.name, data.description);
        }
    }
}

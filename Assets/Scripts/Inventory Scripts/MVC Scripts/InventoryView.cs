using UnityEngine;
using System;

namespace RPG.Inventory
{
    public class InventoryView : MonoBehaviour, InventoryViewInterface, IClickObserve
    {
        #region SERIALIZE FIELD
        [SerializeField]
        private ItemDetailsView itemDetails = null;

        [SerializeField]
        private GameObject inventoryOrganizer = null, templateButton = null;
        #endregion

        private ItemButton[] itemButtons;

        #region DELEGATES
        public Func<int, DetailData> OnItemButtonClick { get; set; }
        #endregion

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

            if (!data.shouldShow) return;

            itemDetails.Show(data.name, data.description);
        }
    }
}

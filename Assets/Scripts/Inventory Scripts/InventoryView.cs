using UnityEngine;
using System;

namespace RPG.Inventory
{
    public class InventoryView : MonoBehaviour, InventoryViewInterface, IClickObserve
    {
        #region SERIALIZE FIELDS
        [SerializeField]
        private GameObject controllerObject = null;

        [SerializeField]
        private ItemDetailsView itemDetails = null;

        [SerializeField]
        private GameObject inventoryOrganizer = null, templateButton = null;

        [SerializeField]
        private GameObject itemFrame = null;
        #endregion

        #region DELEGATES
        public Func<int, DetailsData> OnItemButtonClick { get; set; }
        #endregion

        private InventoryControllerInterface controller;

        private ItemButton[] itemButtons;

        void Awake()
        {           
            controller = controllerObject.GetComponent<InventoryControllerInterface>();
            controller.OnItemMove += _ => itemDetails.Show(_.name, _.description, _.equippableSprites, _.itemType);
        }

        void Start()
        {
            itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

            for (int i = 0; i < itemButtons.Length; i++)
            {
                GameObject itemBtn = Instantiate(templateButton, inventoryOrganizer.transform);
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

            itemFrame.transform.position = itemButtons[idx].transform.position;

            DetailsData data = OnItemButtonClick(idx);

            if (!data.shouldShow) return;

            ShowItemDetails(data);
        }

        public void ShowItemDetails(DetailsData data)
        {
            itemDetails.Show(data.name, data.description, data.equippableSprites, data.itemType);
        }

        public void Reset()
        {
            itemDetails.Reset();
        }
    }
}

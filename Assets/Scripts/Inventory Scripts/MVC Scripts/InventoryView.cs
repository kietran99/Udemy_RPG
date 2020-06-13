using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class InventoryView : MonoBehaviour, InventoryViewInterface
    {
        #region
        public Text ItemNameText { get { return itemNameText; } }
        public Text ItemDescriptionText { get { return itemDescriptionText; } }
        public Text PrimaryActionText { get { return primaryActionText; } }
        #endregion

        [SerializeField]
        private Text itemNameText = null,
                        itemDescriptionText = null,
                        possessorText = null,
                        primaryActionText = null;

        [SerializeField]
        private GameObject inventoryOrganizer = null, templateButton = null;

        private ItemButton[] itemButtons;

        // Start is called before the first frame update
        void Start()
        {
            itemButtons = new ItemButton[ItemManager.MAX_INVENTORY_SIZE];

            for (int i = 0; i < itemButtons.Length; i++)
            {
                GameObject itemBtn = Instantiate(templateButton);
                itemBtn.transform.SetParent(inventoryOrganizer.transform);
                itemButtons[i] = itemBtn.GetComponent<ItemButton>();
                itemButtons[i].Init((IClickInvoker)this, i);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Display(ItemHolder[] holders)
        {
            throw new System.NotImplementedException();
        }
    }
}

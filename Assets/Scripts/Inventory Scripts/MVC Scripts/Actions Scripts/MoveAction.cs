using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class MoveAction : InventoryAction
    {
        [SerializeField]
        private GameObject promptContainer = null;

        [SerializeField]
        private Text promptText = null;

        private IAmountSelector amountSelector;
        private int idxToMove = -1;

        private bool boundWithAmountSelector;

        protected override void Start()
        {
            base.Start();
            inventoryController.OnHide += Cancel;
            amountSelector = actionController.AmountSelector;
        }

        public override void Invoke()
        {
            if (inventoryController.HasChosenEmptySlot()) return;

            boundWithAmountSelector = false;
            idxToMove = inventoryController.ChosenPosition;
            ShowPrompt();
            actionController.HideInteractButtons();
            inventoryController.View.GetComponent<InventoryViewInterface>().OnItemButtonClick += PickAmount;
        }

        public void Cancel()
        {
            HidePrompt();
            actionController.ShowInteractButtons();
            inventoryController.View.GetComponent<InventoryViewInterface>().OnItemButtonClick -= PickAmount;
            if (boundWithAmountSelector) amountSelector.OnAmountConfirm -= MoveItem;
        }
       
        private DetailData PickAmount(int idx)
        {
            var dummyData = new DetailData(string.Empty, string.Empty, false);
            
            if (!inventoryController.IsEmptySlot(idx) && !inventoryController.HasChosenSameItemAt(idxToMove))
            {
                promptText.GetComponent<Text>().text = "PLEASE CHOOSE A VALID SLOT!";
                return dummyData;
            }

            HidePrompt();
            amountSelector.OnAmountConfirm += MoveItem;
            boundWithAmountSelector = true;
            amountSelector.Activate(inventoryController.View, inventoryController.ChosenItemHolder.Amount);

            return dummyData;
        }

        void MoveItem(int amount)
        {
            inventoryController.MoveItem(idxToMove, amount, ItemPossessor.BAG);
        }

        private void ShowPrompt()
        {
            promptContainer.SetActive(true);
            promptText.text = "WHERE TO MOVE?";
        }

        private void HidePrompt()
        {
            promptContainer.SetActive(false);
        }
    }
}

using System;
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

        private int idxToMove;
        private ItemPossessor sender;
        private int originalAmount;
        private IAmountSelector amountSelector;
      
        protected override void Start()
        {
            base.Start();
            inventoryController.OnHide += Cancel;
        }

        void BindDelegates()
        {
            amountSelector.OnAmountConfirm += MoveItemThenCancel;
        }

        void UnbindDelegates()
        {
            amountSelector.OnAmountConfirm -= MoveItemThenCancel;
            amountSelector.OnActivate -= BindDelegates;
            amountSelector.OnDeactivate -= UnbindDelegates;
        }

        public override void Invoke()
        {
            if (inventoryController.HasChosenEmptySlot()) return;

            amountSelector = actionController.AmountSelector;
            amountSelector.OnActivate += BindDelegates;
            amountSelector.OnDeactivate += UnbindDelegates;

            idxToMove = inventoryController.ChosenPosition;
            originalAmount = inventoryController.ChosenItemHolder.Amount;
            sender = inventoryController.CharCycler.Current;
            ShowPrompt();
            actionController.HideInteractors();
            inventoryController.View.GetComponent<InventoryViewInterface>().OnItemButtonClick += PickAmount;
        }

        public void Cancel()
        {
            HidePrompt();
            actionController.ShowInteractors();
            inventoryController.View.GetComponent<InventoryViewInterface>().OnItemButtonClick -= PickAmount;
        }
       
        private DetailData PickAmount(int idx)
        {
            var dummyData = new DetailData(string.Empty, string.Empty, Array.Empty<Sprite>(), false);
            
            if (!inventoryController.IsEmptySlot(idx) && !inventoryController.HasChosenSameItemAt(idxToMove))
            {
                promptText.GetComponent<Text>().text = "PLEASE CHOOSE A VALID SLOT!";
                return dummyData;
            }

            HidePrompt();
            amountSelector.Activate(originalAmount);

            return dummyData;
        }

        private void MoveItemThenCancel(int amount)
        {
            inventoryController.MoveItem(idxToMove, sender, amount);
            Cancel();
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

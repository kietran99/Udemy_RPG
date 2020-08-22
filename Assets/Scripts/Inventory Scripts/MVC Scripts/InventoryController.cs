using UnityEngine;
using Cycler;
using System;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface
    {
        private const int NONE_CHOSEN = -1;

        #region PUBLIC
        public GameObject View { get { return invViewObject; } }
        public ItemHolder[] CurrentInv { get; private set; }
        public ICycler<ItemPossessor> CharCycler { get; private set; }
        public int ChosenPosition { get; private set; }
        public ItemHolder ChosenItemHolder { get { return CurrentInv[ChosenPosition]; } }
        #endregion

        #region DELEGATES
        public Action OnHide { get; set; }
        #endregion

        [SerializeField]
        private GameObject charCyclerObject = null;

        [SerializeField]
        private GameObject invViewObject = null;

        private InventoryViewInterface invView;

        public void BindController(InventoryViewInterface invView)
        {
            this.invView = invView;
            Init();
        }

        void Start()
        {
            if (invView == null)
            {
                invView = invViewObject.GetComponent<InventoryViewInterface>();
                invView.OnItemButtonClick += GetItemDetails;
            }

            Init();
            ShowInventory();
        }

        private void Init()
        {
            ChosenPosition = NONE_CHOSEN;
            CharCycler = charCyclerObject.GetComponent<ICycler<ItemPossessor>>();
            CharCycler.OnCycle += ShowNextInventory;
            CurrentInv = ItemManager.Instance.GetInventory(ItemPossessor.BAG);
        }

        void OnDisable()
        {
            OnHide?.Invoke();
        }

        public void ShowNextInventory(ItemPossessor possessor)
        {
            CurrentInv = ItemManager.Instance.GetInventory(possessor);
            invView.Display(CurrentInv);
        }

        private DetailData GetItemDetails(int idx)
        {
            ChosenPosition = idx;

            Item item = CurrentInv[idx].TheItem;
            return new DetailData(item.ItemName, item.Description);
        }

        public void ShowInventory()
        {
            invView.Display(CurrentInv);
        }

        public bool HasChosenEmptySlot()
        {
            return IsEmptySlot(ChosenPosition);
        }

        public bool IsEmptySlot(int idx)
        {
            if (idx == NONE_CHOSEN) return true;

            return CurrentInv[idx].IsEmpty();
        }

        public void DiscardItem(int amount)
        {
            ItemManager.Instance.RemoveItemAt(CharCycler.CurrPos, ChosenPosition, amount);
            ShowInventory();
        }

        public bool HasChosenSameItemAt(int idx)
        {
            return CurrentInv[idx].SameItem(CurrentInv[ChosenPosition]);
        }
    
        public void MoveItem(int from, int amount, ItemPossessor receivingInv)
        {
            var curInv = ItemManager.Instance.GetInvHolder(CharCycler.CurrPos);
            curInv.MoveItem(from, ChosenPosition, amount, null);
            ShowInventory();
        }
    }
}

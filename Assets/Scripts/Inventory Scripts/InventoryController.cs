﻿using UnityEngine;
using Cycler;
using System;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface
    {
        #region PUBLIC
        public InventoryViewInterface View { get { return view; } }
        public ItemHolder[] CurrentInv { get; private set; }
        public ICycler<ItemOwner> CharCycler { get; private set; }
        public int ChosenPosition { get; private set; }
        public ItemHolder ChosenItemHolder { get { return CurrentInv[ChosenPosition]; } }
        #endregion

        #region SERIALIZE FIELD
        [SerializeField]
        private GameObject charCyclerObject = null;

        [SerializeField]
        private GameObject invViewObject = null;
        #endregion

        #region DELEGATES
        public Action OnHide { get; set; }
        public Action<bool, bool> OnUsableItemClick { get; set; }
        public Action<DetailData> OnItemMove { get; set; }
        #endregion

        private InventoryViewInterface view;

        public void BindController(InventoryViewInterface invView)
        {
            view = invView;
            Init();
        }

        void Start()
        {
            if (view == null)
            {
                view = invViewObject.GetComponent<InventoryViewInterface>();
                view.OnItemButtonClick += GetItemDetails;
            }

            Init();
            ShowInventory();
        }

        private void Init()
        {
            ChosenPosition = Constants.INVALID;
            CharCycler = charCyclerObject.GetComponent<ICycler<ItemOwner>>();
            CharCycler.OnCycle += ShowNextInventory;
            CurrentInv = ItemManager.Instance.GetInventory(ItemOwner.BAG);
        }

        void OnEnable()
        {
            if (CurrentInv != null) ShowInventory();
        }

        void OnDisable()
        {
            OnHide?.Invoke();
        }

        public void ShowNextInventory(ItemOwner possessor)
        {
            CurrentInv = ItemManager.Instance.GetInventory(possessor);
            view.Display(CurrentInv);
        }

        private DetailData GetItemDetails(int idx)
        {           
            ChosenPosition = idx;
            bool isEquipment = CurrentInv[idx].TheItem.IsEquipment;
            OnUsableItemClick?.Invoke(!isEquipment, CurrentInv[idx].IsEquipped);

            Item item = CurrentInv[idx].TheItem;

            Sprite[] equippableSprites = GetEquippableCharsSprites(item);

            return new DetailData(item.ItemName, item.Description, equippableSprites);
        }

        private Sprite[] GetEquippableCharsSprites(Item item)
        {
            try
            {
                CharName[] equippableCharsNames = ((Equipment)item).EquippableChars;
                return equippableCharsNames.Map(_ => GameManager.Instance.GetCharacter(_.CharacterName).CharImage);
            }
            catch
            {
                return Array.Empty<Sprite>();
            }
        }

        public void ShowInventory()
        {
            view.Display(CurrentInv);
        }

        public bool HasChosenEmptySlot()
        {
            return IsEmptySlot(ChosenPosition);
        }

        public bool IsEmptySlot(int idx)
        {
            return idx == Constants.INVALID || CurrentInv[idx].IsEmpty();           
        }

        public void DiscardItem(int amount)
        {
            ItemManager.Instance.RemoveItemAt(CharCycler.Current, ChosenPosition, amount);
            ShowInventory();
        }

        public bool HasChosenSameItemAt(int idx)
        {
            return CurrentInv[idx].CompareItem(CurrentInv[ChosenPosition]);
        }
    
        public void MoveItem(int fromPos, int toPos, ItemOwner sender, ItemOwner receiver, int amount)
        {
            var sendingInventory = ItemManager.Instance.GetInvHolder(sender);
            if (sender.Equals(CharCycler.Current)) sendingInventory.MoveItem(fromPos, toPos, amount);
            else sendingInventory.MoveItem(fromPos, toPos, amount, ItemManager.Instance.GetInvHolder(receiver));
            OnItemMove?.Invoke(GetItemDetails(ChosenPosition));
            ShowInventory();
        }

        public void EquipItem(ItemOwner charToEquip)
        {
            var sendingInv = ItemManager.Instance.GetInvHolder(CharCycler.Current);
            var receivingInv = ItemManager.Instance.GetInvHolder(charToEquip);
            int firstEmptySlot = receivingInv.FindFirstEmptySlot();

            sendingInv.MoveItem(ChosenPosition, firstEmptySlot, 1, receivingInv);
            OnItemMove?.Invoke(GetItemDetails(ChosenPosition));
            receivingInv.ItemHolders[firstEmptySlot].IsEquipped = true;
            (receivingInv.ItemHolders[firstEmptySlot].TheItem as Equipment).ToggleEquipAbility(PossessorSearcher.GetStats(charToEquip));

            UnequipSameType(receivingInv, firstEmptySlot);

            ShowInventory();
        }
          
        private void UnequipSameType(InventoryHolder receivingInv, int posToCompare)
        {
            var sameEquippedPos = receivingInv.FindSameEquippedTypePos(posToCompare);
            if (!sameEquippedPos.Equals(Constants.INVALID)) receivingInv.ItemHolders[sameEquippedPos].IsEquipped = false;
        }

        public void UnequipItem()
        {
            CurrentInv[ChosenPosition].IsEquipped = false;
            (CurrentInv[ChosenPosition].TheItem as Equipment).ToggleEquipAbility(PossessorSearcher.GetStats(CharCycler.Current));
            ShowInventory();
        }
    }
}
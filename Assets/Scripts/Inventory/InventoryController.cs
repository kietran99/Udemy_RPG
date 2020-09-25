using UnityEngine;
using Cycler;
using System;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface
    {
        #region PROPERTIES
        public InventoryViewInterface View { get { return view; } }
        public ItemHolder[] CurrentInv { get; private set; }
        public ICycler<InventoryOwner> CharCycler { get; private set; }
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
        public Action<DetailsData> OnItemMove { get; set; }
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
            CharCycler = charCyclerObject.GetComponent<ICycler<InventoryOwner>>();
            CharCycler.OnCycle += ShowNextInventory;
            CurrentInv = ItemManager.Instance.GetInventory(InventoryOwner.BAG);
        }

        void OnEnable()
        {
            if (CurrentInv != null) ShowInventory();
        }

        void OnDisable()
        {
            OnHide?.Invoke();
        }

        public void ShowNextInventory(InventoryOwner possessor)
        {
            CurrentInv = ItemManager.Instance.GetInventory(possessor);
            view.Reset();
            view.Display(CurrentInv);
            if (!ChosenPosition.Equals(Constants.INVALID)) view.ShowItemDetails(GenerateDetailsData());
        }

        private DetailsData GetItemDetails(int idx)
        {           
            ChosenPosition = idx;
            bool isEquipment = CurrentInv[idx].TheItem.IsEquipment;
            OnUsableItemClick?.Invoke(!isEquipment, CurrentInv[idx].IsEquipped);

            return GenerateDetailsData();
        }

        private DetailsData GenerateDetailsData()
        {
            Item item = CurrentInv[ChosenPosition].TheItem;
            Sprite[] equippableSprites = GetEquippableCharsSprites(item);
            Type itemType = item.ItemName.Equals(string.Empty) ? GetType() : item.GetType();
            return new DetailsData(item.ItemName, item.Description, equippableSprites, itemType);
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

        public void Organize()
        {
            ItemManager.Instance.GetInvHolder(CharCycler.Current).Organize();
            ShowInventory();
        }

        public void Discard(int amount)
        {
            ItemManager.Instance.RemoveItemAt(CharCycler.Current, ChosenPosition, amount);
            ShowInventory();
        }

        public bool HasChosenSameItemAt(int idx)
        {
            return CurrentInv[idx].CompareItem(CurrentInv[ChosenPosition]);
        }
    
        public void MoveItem(int fromPos, int toPos, InventoryOwner sender, InventoryOwner receiver, int amount)
        {
            var sendingInventory = ItemManager.Instance.GetInvHolder(sender);
            if (sender.Equals(CharCycler.Current)) sendingInventory.MoveItem(fromPos, toPos, amount);
            else sendingInventory.MoveItem(fromPos, toPos, amount, ItemManager.Instance.GetInvHolder(receiver));
            OnItemMove?.Invoke(GetItemDetails(ChosenPosition));
            ShowInventory();
        }

        public void EquipItem(InventoryOwner charToEquip)
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

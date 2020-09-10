using UnityEngine;
using Cycler;
using System;
using Functional;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface
    {
        private const int NONE_CHOSEN = -1;

        #region PUBLIC
        public GameObject View { get { return invViewObject; } }
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
        public Action<bool> OnUsableItemClick { get; set; }
        #endregion

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
            CharCycler = charCyclerObject.GetComponent<ICycler<ItemOwner>>();
            CharCycler.OnCycle += ShowNextInventory;
            CurrentInv = ItemManager.Instance.GetInventory(ItemOwner.BAG);
        }

        void OnDisable()
        {
            OnHide?.Invoke();
        }

        public void ShowNextInventory(ItemOwner possessor)
        {
            CurrentInv = ItemManager.Instance.GetInventory(possessor);
            invView.Display(CurrentInv);
        }

        private DetailData GetItemDetails(int idx)
        {           
            ChosenPosition = idx;
            bool isEquipment = CurrentInv[idx].TheItem.IsEquipment;
            OnUsableItemClick?.Invoke(!isEquipment);

            Item item = CurrentInv[idx].TheItem;

            Sprite[] equippableSprites = GetEquippableCharsSprites(item);

            return new DetailData(item.ItemName, item.Description, equippableSprites);
        }

        private Sprite[] GetEquippableCharsSprites(Item item)
        {
            try
            {
                CharName[] equippableCharsNames = ((Equipment)item).EquippableChars;
                return HOF.Map(charName => GameManager.Instance.GetCharacter(charName.CharacterName).CharImage, equippableCharsNames);
            }
            catch
            {
                return Array.Empty<Sprite>();
            }
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
            ItemManager.Instance.RemoveItemAt(CharCycler.Current, ChosenPosition, amount);
            ShowInventory();
        }

        public bool HasChosenSameItemAt(int idx)
        {
            return CurrentInv[idx].SameItem(CurrentInv[ChosenPosition]);
        }
    
        public void MoveItem(int fromPos, int toPos, ItemOwner sender, ItemOwner receiver, int amount)
        {
            var sendingInventory = ItemManager.Instance.GetInvHolder(sender);
            if (sender.Equals(CharCycler.Current)) sendingInventory.MoveItem(fromPos, toPos, amount);
            else sendingInventory.MoveItem(fromPos, toPos, amount, ItemManager.Instance.GetInvHolder(receiver));
            ShowInventory();
        }

        public void EquipItem(CharStats charToEquip)
        {
            var sendingInv = ItemManager.Instance.GetInvHolder(CharCycler.Current);
            var receivingInv = ItemManager.Instance.GetInvHolder(PossessorSearcher.GetOwner(charToEquip.CharacterName));
            int firstEmptySlot = receivingInv.FindFirstEmptySlot();
            sendingInv.MoveItem(ChosenPosition, firstEmptySlot, 1, receivingInv);
            receivingInv.ItemHolders[firstEmptySlot].IsEquipped = true;
            (receivingInv.ItemHolders[firstEmptySlot].TheItem as Equipment).ToggleEquipAbility(charToEquip);
            ShowInventory();
        }
    }
}

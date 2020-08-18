using UnityEngine;
using Cycler;

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

        // Start is called before the first frame update
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
            if (ChosenPosition == NONE_CHOSEN) return true;

            return CurrentInv[ChosenPosition].IsEmpty();
        }

        public void DiscardItem(int amount)
        {
            ItemManager.Instance.RemoveItemAt(CharCycler.CurrPos, ChosenPosition, amount);
            ShowInventory();
        }
    }
}

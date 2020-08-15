using UnityEngine;
using Cycler;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface
    {
        #region PUBLIC
        public ItemHolder[] CurrentInv { get; private set; }
        public ICycler<ItemPossessor> CharCycler { get; private set; }
        public int ChosenPosition { get; private set; }
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
            invView = invViewObject.GetComponent<InventoryViewInterface>();
            invView.OnItemButtonClick += GetItemDetails;
            Init();
            ShowInventory();
        }

        private void Init()
        {
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
    }
}

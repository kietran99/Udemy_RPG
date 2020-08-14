using UnityEngine;
using Cycler;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface, ICycleObserver<ItemPossessor>
    {
        #region
        public ItemHolder[] CurrentInv { get { return currentInv; } set { currentInv = value; } }
        public CharCycler CharCycler { get { return charCycler; } set { charCycler = value; } }
        #endregion

        [SerializeField]
        private CharCycler charCycler = null;

        [SerializeField]
        private GameObject invViewObject = null;

        private InventoryViewInterface invView;

        private ItemHolder[] currentInv;     

        public void Init(InventoryViewInterface invView)
        {          
            this.invView = invView;
            charCycler.Activate(this);
            currentInv = ItemManager.Instance.GetInventory(ItemPossessor.BAG);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (invView == null)
            {
                invView = invViewObject.GetComponent<InventoryViewInterface>();
                invView.OnItemButtonClick += GetItemDetails;
            }

            charCycler.Activate(this);
            currentInv = ItemManager.Instance.GetInventory(ItemPossessor.BAG);
            invView.Display(currentInv);
        }

        public void OnCycle(ItemPossessor possessor)
        {
            currentInv = ItemManager.Instance.GetInventory(possessor);
            invView.Display(currentInv);
        }

        private DetailData GetItemDetails(int idx)
        {
            Item item = currentInv[idx].TheItem;

            return new DetailData(item.ItemName, item.Description);
        }
    }
}

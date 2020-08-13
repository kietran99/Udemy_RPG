using UnityEngine;
using Cycler;

namespace RPG.Inventory
{
    public class InventoryController : MonoBehaviour, InventoryControllerInterface, ICycleObserver<PossessorSearcher.ItemPossessor>
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
            currentInv = ItemManager.Instance.GetInventory(PossessorSearcher.ItemPossessor.BAG);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (invView == null) invView = invViewObject.GetComponent<InventoryViewInterface>();

            charCycler.Activate(this);
            currentInv = ItemManager.Instance.GetInventory(PossessorSearcher.ItemPossessor.BAG);
            invView.Display(currentInv);
        }

        public void OnCycle(PossessorSearcher.ItemPossessor possessor)
        {
            currentInv = ItemManager.Instance.GetInventory(possessor);
            invView.Display(currentInv);
        }
    }
}

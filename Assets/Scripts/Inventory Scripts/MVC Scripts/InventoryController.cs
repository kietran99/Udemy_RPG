using System.Collections;
using System.Collections.Generic;
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

        private ItemHolder[] currentInv;

        private InventoryViewInterface invView;

        public void Activate(InventoryViewInterface invView)
        {
            this.invView = invView;
            charCycler.Activate((ICycleObserver<PossessorSearcher.ItemPossessor>) this);
            currentInv = ItemManager.Instance.GetInventory(PossessorSearcher.ItemPossessor.BAG);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnCycle(PossessorSearcher.ItemPossessor possessor)
        {
            currentInv = ItemManager.Instance.GetInventory(possessor);
            invView.Display(currentInv);
        }
    }
}

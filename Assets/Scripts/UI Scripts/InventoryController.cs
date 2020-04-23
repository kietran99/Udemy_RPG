using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cycler;

namespace Inventory
{
    public interface IInventoryDisplay
    {
        void UpdateInv(ItemHolder[] holders);
    }

    public class InventoryController : MonoBehaviour, ICycleObserver<PossessorSearcher.ItemPossessor>
    {
        #region
        public ItemHolder[] CurrentInv { get { return currentInv; } set { currentInv = value; } }
        public CharCycler CharCycler { get { return charCycler; } set { charCycler = value; } }
        #endregion

        [SerializeField]
        private CharCycler charCycler = null;

        private ItemHolder[] currentInv;

        private IInventoryDisplay display;

        public void Activate(IInventoryDisplay display)
        {
            this.display = display;
            charCycler.Activate((ICycleObserver<PossessorSearcher.ItemPossessor>)this);
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
            display.UpdateInv(currentInv);
        }
    }
}

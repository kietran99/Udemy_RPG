using UnityEngine;

namespace RPG.Inventory
{
    public class InventoryModel : MonoBehaviour, InventoryModelInterface
    {        
        void Start()
        {

        }

        public ItemHolder[] GetInventory(ItemOwner possessor)
        {
            throw new System.NotImplementedException();
        }
    }
}

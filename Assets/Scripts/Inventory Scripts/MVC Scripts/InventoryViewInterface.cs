using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventory
{
    public interface InventoryViewInterface
    {
        void Display(ItemHolder[] holders);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryModelInterface
{
    ItemHolder[] GetInventory(PossessorSearcher.ItemPossessor possessor);
}

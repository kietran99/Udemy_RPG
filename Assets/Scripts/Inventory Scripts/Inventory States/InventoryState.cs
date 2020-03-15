using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InventoryState
{
    void OnItemSelected(int pos);
    void OnAmountConfirm(int changeAmount);
}

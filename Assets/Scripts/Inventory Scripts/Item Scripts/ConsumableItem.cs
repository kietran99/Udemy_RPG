using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "RPG Generator/Items/Consumable Item")]
public class ConsumableItem : Item
{
    public override string GetPrimaryAction()
    {
        return USE_ACTION;
    }

    public override void SetPrimaryAction(bool isEquipped) { }

    public override void InvokePrimaryAction(CharStats charStats)
    {
        Use(charStats);
    }

    public override string GetItemType()
    {
        return "Consumables";
    }
}

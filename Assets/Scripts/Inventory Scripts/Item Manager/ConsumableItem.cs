using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "RPG Generator/Items/Consumable Item", order = 51)]
public class ConsumableItem : Item
{
    protected const string primaryAction = "USE";

    public override string GetPrimaryAction()
    {
        return primaryAction;
    }
}

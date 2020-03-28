using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "RPG Generator/Items/Equipments/Shield")]
public class Shield : Protector
{
    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedSecondary.IsEqual(bareEquipment) || !stats.EquippedSecondary.IsEqual(this)) stats.EquippedSecondary = this;
        else stats.EquippedSecondary = bareEquipment;
    }
}

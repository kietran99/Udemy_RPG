using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Footwear", menuName = "RPG Generator/Items/Equipments/Footwear")]
public class Footwear : Equipment
{
    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedFootwear.IsEqual(bareEquipment) || !stats.EquippedFootwear.IsEqual(this)) stats.EquippedFootwear = this;
        else stats.EquippedFootwear = bareEquipment;
    }
}

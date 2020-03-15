using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "RPG Generator/Items/Equipments/Armor")]
public class Armor : Equipment
{
    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedArmor.IsEqual(bareEquipment) || !stats.EquippedArmor.IsEqual(this)) stats.EquippedArmor = this; 
        else stats.EquippedArmor = bareEquipment;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "RPG Generator/Items/Equipments/Sword")]
public class Sword : Equipment
{
    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedWeapon.IsEqual(bareEquipment) || !stats.EquippedWeapon.IsEqual(this)) stats.EquippedWeapon = this;
        else stats.EquippedWeapon = bareEquipment;
    }
}

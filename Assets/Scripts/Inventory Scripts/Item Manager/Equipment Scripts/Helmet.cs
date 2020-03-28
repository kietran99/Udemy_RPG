using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Helmet", menuName = "RPG Generator/Items/Equipments/Helmet")]
public class Helmet : Protector
{
    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedHelmet.IsEqual(bareEquipment) || !stats.EquippedHelmet.IsEqual(this)) stats.EquippedHelmet = this;
        else stats.EquippedHelmet = bareEquipment;
    }
}

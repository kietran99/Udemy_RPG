using UnityEngine;

[CreateAssetMenu(fileName = "Bare Equipment", menuName = "RPG Generator/Items/Equipments/Bare Equipment")]
public class BareEquipment : Equipment
{
    public override string GetStatBoostName()
    {
        return "";
    }
    public override void ToggleEquipAbility(CharStats stats) { }

}

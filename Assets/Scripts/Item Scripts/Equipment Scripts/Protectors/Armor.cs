using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "RPG Generator/Items/Equipments/Armor")]
public class Armor : Protector
{    
    private void Awake()
    {
        NullEquipment = Resources.Load<Armor>(NullEquipmentsRef.noArmor);
    }

    public override int GetLaterStat(CharStats stats)
    {
        return stats.Defense + statChange - stats.EquippedArmor.StatChange;
    }

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats) { defense = stats.Defense + statChange - stats.EquippedArmor.StatChange };
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        int laterStat = GetLaterStat(stats);
        stats.EquippedArmor = (Armor)ToggleCharEquipment(stats.EquippedArmor, out bool shouldEquip);
        stats.Defense = UpdateStat(laterStat, shouldEquip);
    }
}

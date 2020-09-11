using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "RPG Generator/Items/Equipments/Shield")]
public class Shield : Protector
{    
    private void Awake()
    {
        NullEquipment = Resources.Load<Shield>(NullEquipmentsRef.noShield);
    }

    public override int GetLaterStat(CharStats stats)
    {
        return stats.Defense + statChange - stats.EquippedSecondary.StatChange;
    }

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats)
        {
            defense = stats.Defense + statChange - stats.EquippedSecondary.StatChange
        };
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        int laterStat = GetLaterStat(stats);
        stats.EquippedSecondary = (Weapon)ToggleCharEquipment(stats.EquippedSecondary, out bool shouldEquip);
        stats.Defense = UpdateStat(laterStat, shouldEquip);
    }
}

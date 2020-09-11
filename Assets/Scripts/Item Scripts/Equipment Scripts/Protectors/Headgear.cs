using UnityEngine;

[CreateAssetMenu(fileName = "Headgear", menuName = "RPG Generator/Items/Equipments/Headgear")]
public class Headgear : Protector
{
    private void Awake()
    {
        NullEquipment = Resources.Load<Headgear>(NullEquipmentsRef.noHeadgear);
    }

    public override int GetLaterStat(CharStats stats) => stats.Defense + statChange - stats.EquippedHeadgear.StatChange;

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats) { defense = GetLaterStat(stats) };
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        int laterStat = GetLaterStat(stats);
        stats.EquippedHeadgear = (Weapon)ToggleCharEquipment(stats.EquippedHeadgear, out bool shouldEquip);
        stats.Defense = UpdateStat(laterStat, shouldEquip);
    }
}

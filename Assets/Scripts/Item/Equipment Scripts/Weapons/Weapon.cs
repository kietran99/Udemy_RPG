using UnityEngine;

public abstract class Weapon : Equipment
{
    private void Awake()
    {
        NullEquipment = Resources.Load<Weapon>(NullEquipmentsRef.noWeapon);
    }

    public override int GetLaterStat(CharStats stats) => stats.Strength + statChange - stats.EquippedWeapon.StatChange;

    public override AttributesData GetLaterChangeStat(CharStats stats) => new AttributesData(stats) { strength = GetLaterStat(stats)};

    public override string GetItemType() => "Strength";

    public override int GetCorresStat(CharStats stats) => stats.Strength;

    public override void ToggleEquipAbility(CharStats stats)
    {        
        int laterStat = GetLaterStat(stats);
        stats.EquippedWeapon = (Weapon)ToggleCharEquipment(stats.EquippedWeapon, out bool shouldEquip);
        stats.Strength = UpdateStat(laterStat, shouldEquip);
    }
}

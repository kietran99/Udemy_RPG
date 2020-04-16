using UnityEngine;

public abstract class Weapon : Equipment
{
    protected Weapon nullWeapon;

    private void Awake()
    {
        nullWeapon = Resources.Load<Weapon>(NullEquipmentsRef.noWeapon);
    }

    public override int GetPostChangeStat(CharStats stats)
    {
        return stats.Strength + statChange - stats.EquippedWeapon.StatChange;
    }

    public override string GetItemType()
    {
        return "Strength";
    }

    public override int GetCorresStat(CharStats stats)
    {
        return stats.Strength;
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Strength = GetPostChangeStat(stats);

        if (stats.EquippedWeapon.IsEqual(nullWeapon) || !stats.EquippedWeapon.IsEqual(this)) stats.EquippedWeapon = this;
        else stats.EquippedWeapon = nullWeapon;        
    }  
}

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

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats)
        {
            strength = stats.Strength + statChange - stats.EquippedWeapon.StatChange
        };
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

        if (stats.EquippedWeapon.Equals(nullWeapon) || !stats.EquippedWeapon.Equals(this)) stats.EquippedWeapon = this;
        else stats.EquippedWeapon = nullWeapon;        
    }  
}

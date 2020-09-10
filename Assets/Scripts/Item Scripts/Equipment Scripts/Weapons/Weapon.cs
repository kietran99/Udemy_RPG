using UnityEngine;

public abstract class Weapon : Equipment
{
    protected Weapon nullWeapon;

    private void Awake()
    {
        nullWeapon = Resources.Load<Weapon>(NullEquipmentsRef.noWeapon);
    }

    public override int GetPostChangeStat(CharStats stats) => stats.Strength + statChange - stats.EquippedWeapon.StatChange;

    public override AttributesData GetLaterChangeStat(CharStats stats) =>
        new AttributesData(stats)
        {
            strength = stats.Strength + statChange - stats.EquippedWeapon.StatChange
        };

    public override string GetItemType() => "Strength";

    public override int GetCorresStat(CharStats stats) => stats.Strength;

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Strength = GetPostChangeStat(stats);
        
        if (stats.EquippedWeapon.Equals(nullWeapon) || !ReferenceEquals(stats.EquippedWeapon, this)) stats.EquippedWeapon = this;
        else stats.EquippedWeapon = nullWeapon;        
    }  
}

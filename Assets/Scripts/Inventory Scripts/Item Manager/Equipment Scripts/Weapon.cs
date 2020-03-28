public class Weapon : Equipment
{
    public override string GetStatBoostName()
    {
        return "Strength";
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        if (stats.EquippedWeapon.IsEqual(bareEquipment) || !stats.EquippedWeapon.IsEqual(this)) stats.EquippedWeapon = this;
        else stats.EquippedWeapon = bareEquipment;
    }
}

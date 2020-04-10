using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "RPG Generator/Items/Equipments/Shield")]
public class Shield : Protector
{
    private Shield nullShield;

    private void Awake()
    {
        nullShield = Resources.Load<Shield>(NullEquipmentsRef.noShield);
    }

    public override int GetPostChangeStat(CharStats stats)
    {
        return stats.Defence + statChange - stats.EquippedSecondary.StatChange;
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Defence = GetPostChangeStat(stats);

        if (stats.EquippedSecondary.IsEqual(nullShield) || !stats.EquippedSecondary.IsEqual(this)) stats.EquippedSecondary = this;
        else stats.EquippedSecondary = nullShield;        
    }
}

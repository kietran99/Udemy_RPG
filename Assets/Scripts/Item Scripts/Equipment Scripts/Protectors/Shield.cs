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
        stats.Defense = GetPostChangeStat(stats);

        if (stats.EquippedSecondary.Equals(nullShield) || !stats.EquippedSecondary.Equals(this)) stats.EquippedSecondary = this;
        else stats.EquippedSecondary = nullShield;        
    }
}

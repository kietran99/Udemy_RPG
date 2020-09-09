using UnityEngine;

[CreateAssetMenu(fileName = "Headgear", menuName = "RPG Generator/Items/Equipments/Headgear")]
public class Headgear : Protector
{
    private Headgear nullHeadgear;

    private void Awake()
    {
        nullHeadgear = Resources.Load<Headgear>(NullEquipmentsRef.noHeadgear);
    }

    public override int GetPostChangeStat(CharStats stats)
    {
        return stats.Defense + statChange - stats.EquippedHeadgear.StatChange;
    }

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats) { defense = stats.Defense + statChange - stats.EquippedHeadgear.StatChange };
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Defense = GetPostChangeStat(stats);

        if (stats.EquippedHeadgear.Equals(nullHeadgear) || !stats.EquippedHeadgear.Equals(this)) stats.EquippedHeadgear = this;
        else stats.EquippedHeadgear = nullHeadgear;       
    }
}

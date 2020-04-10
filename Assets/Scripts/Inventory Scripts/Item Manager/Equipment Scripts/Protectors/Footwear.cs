using UnityEngine;

[CreateAssetMenu(fileName = "Footwear", menuName = "RPG Generator/Items/Equipments/Footwear")]
public class Footwear : Equipment
{
    private Footwear nullFootwear;

    private void Awake()
    {
        nullFootwear = Resources.Load<Footwear>(NullEquipmentsRef.noFootwear);
    }

    public override int GetCorreStat(CharStats stats)
    {
        return stats.Agility;
    }

    public override string GetItemType()
    {
        return "Agility";
    }

    public override int GetPostChangeStat(CharStats stats)
    {
        return stats.Agility + statChange - stats.EquippedFootwear.StatChange;
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Agility = GetPostChangeStat(stats);

        if (stats.EquippedFootwear.IsEqual(nullFootwear) || !stats.EquippedFootwear.IsEqual(this)) stats.EquippedFootwear = this;
        else stats.EquippedFootwear = nullFootwear;       
    }
}

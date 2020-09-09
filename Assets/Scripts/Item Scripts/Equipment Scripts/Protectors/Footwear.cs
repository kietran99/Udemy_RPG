using UnityEngine;

[CreateAssetMenu(fileName = "Footwear", menuName = "RPG Generator/Items/Equipments/Footwear")]
public class Footwear : Equipment
{
    private Footwear nullFootwear;

    private void Awake()
    {
        nullFootwear = Resources.Load<Footwear>(NullEquipmentsRef.noFootwear);
    }

    public override int GetCorresStat(CharStats stats)
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

    public override AttributesData GetLaterChangeStat(CharStats stats)
    {
        return new AttributesData(stats) { agility = stats.Agility + statChange - stats.EquippedFootwear.StatChange };
    }

    public override void ToggleEquipAbility(CharStats stats)
    {
        stats.Agility = GetPostChangeStat(stats);

        if (stats.EquippedFootwear.Equals(nullFootwear) || !stats.EquippedFootwear.Equals(this)) stats.EquippedFootwear = this;
        else stats.EquippedFootwear = nullFootwear;       
    }
}

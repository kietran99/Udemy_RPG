using UnityEngine;

[CreateAssetMenu(fileName = "Footwear", menuName = "RPG Generator/Items/Equipments/Footwear")]
public class Footwear : Equipment
{
    private void Awake()
    {
        NullEquipment = Resources.Load<Footwear>(NullEquipmentsRef.noFootwear);
    }

    public override int GetCorresStat(CharStats stats) => stats.Agility;

    public override string GetItemType() => "Agility";

    public override int GetLaterStat(CharStats stats) => stats.Agility + statChange - stats.EquippedFootwear.StatChange;

    public override AttributesData GetLaterChangeStat(CharStats stats) => new AttributesData(stats) { agility = GetLaterStat(stats) };

    public override void ToggleEquipAbility(CharStats stats)
    {        
        int laterStat = GetLaterStat(stats);
        stats.EquippedFootwear = (Footwear)ToggleCharEquipment(stats.EquippedFootwear, out bool shouldEquip);
        stats.Agility = UpdateStat(laterStat, shouldEquip);
    }   
}

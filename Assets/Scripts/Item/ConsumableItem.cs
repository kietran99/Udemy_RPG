using UnityEngine;

[CreateAssetMenu(fileName = "Consumables", menuName = "RPG Generator/Items/Consumables")]
public class ConsumableItem : Item
{
    public override bool IsEquipment { get => false; }
    
    public override string GetItemType() => "Consumables";
}

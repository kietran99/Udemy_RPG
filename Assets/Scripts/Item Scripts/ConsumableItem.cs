using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Item", menuName = "RPG Generator/Items/Consumable Item")]
public class ConsumableItem : Item
{
    public override bool IsEquipment { get => false; }

    public override string GetPrimaryAction()
    {
        return USE_ACTION;
    }

    public override void SetPrimaryAction(bool isEquipped) { }
    
    public override string GetItemType() => "Consumables";
}

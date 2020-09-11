using UnityEngine;

public abstract class Item : ScriptableObject
{
    #region
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public Sprite Image { get { return image; } }
    public int SellValue { get { return sellValue; } }
    public int BuyValue { get { return buyValue; } }
    public Effect[] Effects { get { return bonusEffects; } }
    public abstract bool IsEquipment { get; }
    #endregion

    public const string USE_ACTION = "USE";
    public const string EQUIP_ACTION = "EQUIP";
    public const string UNEQUIP_ACTION = "UNEQUIP";

    [SerializeField]
    protected string itemName = "", description = "";

    [SerializeField]
    protected Sprite image = null;

    [SerializeField]
    protected int buyValue = 0, sellValue = 0;

    [SerializeField]
    protected Effect[] bonusEffects;

    public abstract string GetItemType();

    public abstract string GetPrimaryAction();

    public abstract void SetPrimaryAction(bool isEquipped);

    public void Use(CharStats stats) => bonusEffects.Map(_ => _.Invoke(stats));
    public bool Equals(Item other) => itemName.Equals(other.ItemName);
}

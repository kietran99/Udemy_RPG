using UnityEngine;

public abstract class Item : ScriptableObject
{
    #region PROPERTIES
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public Sprite Image { get { return image; } }
    public int SellValue { get { return sellValue; } }
    public int BuyValue { get { return buyValue; } }
    public Effect[] Effects { get { return bonusEffects; } }
    public abstract bool IsEquipment { get; }
    #endregion

    #region SERIALIZE FIELDS
    [SerializeField]
    protected string itemName = "", description = "";

    [SerializeField]
    protected Sprite image = null;

    [SerializeField]
    protected int buyValue = 0, sellValue = 0;

    [SerializeField]
    protected Effect[] bonusEffects;
    #endregion

    public abstract string GetItemType();

    public void Use(CharStats stats) => bonusEffects.Map(_ => _.Invoke(stats));

    public bool Equals(Item other) => itemName.Equals(other.ItemName);

    public bool CompareType(Item other) => GetType().Equals(other.GetType());
}

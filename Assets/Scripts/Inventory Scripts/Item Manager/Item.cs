using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    #region
    public string ItemName { get { return itemName; } }
    public string Description { get { return description; } }
    public Sprite Image { get { return image; } }
    public int SellValue { get { return sellValue; } }
    public Effect[] Effects { get { return effects; } }
    #endregion

    public const string USE_ACTION = "USE";
    public const string EQUIP_ACTION = "EQUIP";
    public const string UNEQUIP_ACTION = "UNEQUIP";

    [SerializeField]
    protected string itemName = "", description = "";

    [SerializeField]
    protected Sprite image = null;

    [SerializeField]
    protected int sellValue = 0;

    [SerializeField]
    protected Effect[] effects;

    public abstract string GetPrimaryAction();

    public abstract void SetPrimaryAction(bool isEquipped);

    public abstract void InvokePrimaryAction(CharStats charStats);

    public void Use(CharStats charStats)
    {
        if (effects.Length == 0) return;

        foreach (Effect effect in effects)
        {
            effect.Invoke(charStats);
        }
    }

    public bool IsEqual(Item other)
    {
        return itemName == other.ItemName;
    }

}

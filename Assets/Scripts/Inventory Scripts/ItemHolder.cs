using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemHolder
{
    #region
    public Item TheItem { get { return theItem; } set { theItem = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public bool IsEquipped { get { return isEquipped; } set { isEquipped = value; } }
    #endregion

    public const int ITEM_CAPACITY = 99;

    private Item theItem;

    private int amount;

    private bool isEquipped;
        
    public ItemHolder(Item theItem, int amount, bool isEquipped = false)
    {
        this.theItem = theItem;
        this.amount = amount;
        this.isEquipped = isEquipped;
    }   

    public ItemHolder(ItemHolder itemHolder)
    {
        theItem = itemHolder.theItem;
        amount = itemHolder.amount;
        isEquipped = itemHolder.isEquipped;
    }

    public bool IdenticalItem(ItemHolder other)
    {
        return theItem.IsEqual(other.TheItem);
    }

    public bool IsEmpty()
    {
        return amount <= 0;
    }

    public void Use(CharStats stats)
    {
        theItem.Use(stats);
    }
}

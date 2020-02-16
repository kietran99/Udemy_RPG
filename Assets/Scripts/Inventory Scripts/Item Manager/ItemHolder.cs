using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemHolder
{
    public const int ITEM_CAPACITY = 99;

    [SerializeField]
    private Item theItem = null;

    [SerializeField]
    private int amount = 0;
   
    public Item TheItem { get { return theItem; } set { theItem = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    
    private static ItemHolder nullHolder;
    public static ItemHolder NullHolder { get { return nullHolder; } }    

    public ItemHolder(Item theItem, int amount)
    {
        this.theItem = theItem;
        this.amount = amount;
    }

    public static void InitNullHolder()
    {
        nullHolder = new ItemHolder(ItemManager.Instance.NullItem, -1);
    }

    public bool IdenticalItem(ItemHolder other)
    {
        return theItem.IsEqual(other.TheItem);
    }

    public bool IsEmpty()
    {
        return amount <= 0;
    }
}

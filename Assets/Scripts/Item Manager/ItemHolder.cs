using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemHolder
{
    [SerializeField]
    private Item theItem = null;

    [SerializeField]
    private int amount = 0;

    private int invPosition;

    private ItemManager.ItemPossessor possessor;

    public Item TheItem { get { return theItem; } set { theItem = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public int InvPosition { get { return invPosition; } set { invPosition = value; } }
    public ItemManager.ItemPossessor Possessor { get { return possessor; } set { possessor = value; } }

    public ItemHolder(Item theItem, int amount, int invPosition, ItemManager.ItemPossessor possessor)
    {
        this.theItem = theItem;
        this.amount = amount;
        this.invPosition = invPosition;
        this.possessor = possessor;
    }

}

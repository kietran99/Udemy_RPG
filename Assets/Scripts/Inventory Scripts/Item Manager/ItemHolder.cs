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

    private int invPosition;

    private PossessorSearcher.ItemPossessor possessor;

    public Item TheItem { get { return theItem; } set { theItem = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public int InvPosition { get { return invPosition; } set { invPosition = value; } }
    public PossessorSearcher.ItemPossessor Possessor { get { return possessor; } set { possessor = value; } }

    private static ItemHolder nullHolder;
    public static ItemHolder NullHolder { get { return nullHolder; } }

    public ItemHolder(Item theItem, int amount, int invPosition, PossessorSearcher.ItemPossessor possessor)
    {
        this.theItem = theItem;
        this.amount = amount;
        this.invPosition = invPosition;
        this.possessor = possessor;
    }

    public ItemHolder(Item theItem, int amount, PossessorSearcher.ItemPossessor possessor)
    {
        this.theItem = theItem;
        this.amount = amount;
        this.possessor = possessor;
    }

    public static void InitNullHolder()
    {
        nullHolder = new ItemHolder(ItemManager.Instance.NullItem, -1, -1, PossessorSearcher.ItemPossessor.NONE);
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

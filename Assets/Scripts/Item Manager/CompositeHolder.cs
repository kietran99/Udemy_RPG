using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeHolder
{   
    private ItemManager.ItemPossessor possessor;
    public ItemManager.ItemPossessor Possessor { get { return possessor; } }

    private ItemHolder[] itemHolders;
    public ItemHolder[] ItemHolders { get { return itemHolders; } set { itemHolders = value; } }
    
    public CompositeHolder(ItemManager.ItemPossessor possessor, int size)
    {
        this.possessor = possessor;
        itemHolders = new ItemHolder[size];
    }

    public void Add(ItemHolder holder)
    {
        itemHolders[itemHolders.Length] = holder;
    }

    public void Remove(ItemHolder holder)
    {

    }
}

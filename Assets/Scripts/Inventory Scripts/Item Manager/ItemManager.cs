using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public const int MAX_INVENTORY_SIZE = 16;

    private static ItemManager instance;
    public static ItemManager Instance { get { return instance; } set { instance = value; } }

    [SerializeField]
    private Item nullItem = null;
    public Item NullItem { get { return nullItem; } }

    [SerializeField]
    private Item[] itemLibrary = null;

    //private ItemHolder ItemHolder.NullHolder = null;

    private ItemHolder[] itemHolders;
    public ItemHolder[] ItemHolders { get { return itemHolders; } }

    private InventoryHolder[] invHolders;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitItemLibrary();
        TestItemData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitItemLibrary()
    {
        ItemHolder.InitNullHolder();
        itemHolders = new ItemHolder[MAX_INVENTORY_SIZE * (GameManager.NUM_OF_CHARS + 1)];

        int pos = 0;
        invHolders = new InventoryHolder[GameManager.NUM_OF_CHARS + 1];

        foreach (PossessorSearcher.ItemPossessor possessor in (PossessorSearcher.ItemPossessor[]) System.Enum.GetValues(typeof(PossessorSearcher.ItemPossessor)))
        {
            if (possessor == PossessorSearcher.ItemPossessor.NONE) continue;
            invHolders[pos] = new InventoryHolder(possessor, MAX_INVENTORY_SIZE);
            pos++;
        }
    }

    private void FillInvSlots(ItemHolder[] holders)
    {
        for (int i = 0; i < holders.Length; i++) holders[i] = ItemHolder.NullHolder;
    }

    private void TestItemData()
    {       
        // Inventory holder test
        foreach (InventoryHolder myHolder in invHolders)
        {
            FillInvSlots(myHolder.ItemHolders);
        }

        InventoryHolder holder = invHolders[0];

        holder.ItemHolders[0] = new ItemHolder(itemLibrary[2], 5);
        holder.ItemHolders[3] = new ItemHolder(itemLibrary[0], 1);
        holder.ItemHolders[6] = new ItemHolder(itemLibrary[1], 10);
        holder.ItemHolders[7] = new ItemHolder(itemLibrary[4], 9);
        holder.ItemHolders[9] = new ItemHolder(itemLibrary[3], 40);
        holder.ItemHolders[15] = new ItemHolder(itemLibrary[5], 6);

        holder = invHolders[1];
        holder.ItemHolders[1] = new ItemHolder(itemLibrary[6], 1);
        holder.ItemHolders[4] = new ItemHolder(itemLibrary[7], 2);
        holder.ItemHolders[7] = new ItemHolder(itemLibrary[8], 1);
        holder.ItemHolders[8] = new ItemHolder(itemLibrary[9], 2);
        holder.ItemHolders[13] = new ItemHolder(itemLibrary[10], 1);
    }   

    public void Organize(PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] holders = GetInventory(possessor);

        for (int i = 0; i < holders.Length; i++)
        {
            if (holders[i].IsEmpty())
            {
                for (int j = i + 1; j < holders.Length; j++)
                {
                    if (holders[j].IsEmpty()) continue;

                    holders[i] = holders[j];
                    holders[j] = ItemHolder.NullHolder;
                    break;
                }
            }
        }
    }

    public Item GetItemAt(int pos, PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] inv = GetInventory(possessor);

        if (inv == null) return nullItem;

        return inv[pos].TheItem;
    }

    public ItemHolder[] GetInventory(PossessorSearcher.ItemPossessor possessor)
    {       
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) return holder.ItemHolders;
        }

        return null;
    }

    public void AddItem(PossessorSearcher.ItemPossessor possessor, Item itemToAdd, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.Add(itemToAdd, amount);
        }
    }

    public void AddItemAt(PossessorSearcher.ItemPossessor possessor, ItemHolder itemToAdd, int posToAdd, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.AddAt(itemToAdd, posToAdd, amount);
        }
    }

    public void RemoveItem(PossessorSearcher.ItemPossessor possessor, Item itemToRemove, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.Remove(itemToRemove, amount);
        }
    }

    public void RemoveItemAt(PossessorSearcher.ItemPossessor possessor, int posToRemove, int amount)
    {
        foreach (InventoryHolder holder in invHolders)
        {
            if (holder.Possessor == possessor) holder.RemoveAt(posToRemove, amount);
        }
    }
}

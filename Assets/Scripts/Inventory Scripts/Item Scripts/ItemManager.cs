using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region
    public static ItemManager Instance { get { return instance; } set { instance = value; } }
    public int CurrentGold { get { return currentGold; } set { currentGold = value; } }
    #endregion

    public const int MAX_INVENTORY_SIZE = 16;

    private static ItemManager instance;

    [SerializeField]
    private int currentGold = 60;

    [SerializeField]
    private Item nullItem = null;
    
    [SerializeField]
    private Item[] itemLibrary = null;

    private InventoryHolder[] invHolders;

    private ItemHolder nullHolder;

    private void Awake()
    {
        InitInventoryHolders();
        TestItemData();
    }

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitInventoryHolders()
    {
        nullHolder = new ItemHolderFactory().CreateNullHolder(nullItem);

        int pos = 0;
        invHolders = new InventoryHolder[GameManager.MAX_PARTY_MEMBERS + 1]; // +1 for the bag inventory

        foreach (PossessorSearcher.ItemPossessor possessor in (PossessorSearcher.ItemPossessor[]) System.Enum.GetValues(typeof(PossessorSearcher.ItemPossessor)))
        {
            if (possessor == PossessorSearcher.ItemPossessor.NONE) continue;
            invHolders[pos] = new InventoryHolder(possessor, MAX_INVENTORY_SIZE, nullHolder);
            pos++;
        }
    }

    private void FillInventorySlots(ItemHolder[] holders)
    {
        for (int i = 0; i < holders.Length; i++) holders[i] = nullHolder;
    }

    private void TestItemData()
    {       
        foreach (InventoryHolder myHolder in invHolders)
        {
            FillInventorySlots(myHolder.ItemHolders);
        }

        InventoryHolder invHolder = invHolders[0];

        invHolder.ItemHolders[0] = new ItemHolder(itemLibrary[2], 5);
        invHolder.ItemHolders[3] = new ItemHolder(itemLibrary[0], 1);
        invHolder.ItemHolders[6] = new ItemHolder(itemLibrary[1], 10);
        invHolder.ItemHolders[7] = new ItemHolder(itemLibrary[4], 99);
        invHolder.ItemHolders[9] = new ItemHolder(itemLibrary[3], 40);
        invHolder.ItemHolders[15] = new ItemHolder(itemLibrary[5], 6);

        invHolder = invHolders[1];
        invHolder.ItemHolders[1] = new ItemHolder(itemLibrary[6], 1);
        invHolder.ItemHolders[4] = new ItemHolder(itemLibrary[7], 2);
        invHolder.ItemHolders[7] = new ItemHolder(itemLibrary[8], 1);
        invHolder.ItemHolders[8] = new ItemHolder(itemLibrary[9], 2);
        invHolder.ItemHolders[13] = new ItemHolder(itemLibrary[10], 1);
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
                    holders[j] = nullHolder;
                    break;
                }
            }
        }
    }

    public InventoryHolder GetInvHolder(PossessorSearcher.ItemPossessor possessor)
    {
        foreach (InventoryHolder invHolder in invHolders)
        {
            if (invHolder.Possessor == possessor) return invHolder;
        }

        return null;
    }

    public ItemHolder[] GetInventory(PossessorSearcher.ItemPossessor possessor)
    {
        return GetInvHolder(possessor).ItemHolders;
    }

    public Item GetItemAt(int pos, PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] inv = GetInventory(possessor);

        if (inv == null) return nullItem;

        return inv[pos].TheItem;
    }

    public int GetNumOfItemsAt(int pos, PossessorSearcher.ItemPossessor possessor)
    {
        ItemHolder[] inv = GetInventory(possessor);

        if (inv == null) return 0;

        return inv[pos].Amount;
    }   

    public int AddItem(PossessorSearcher.ItemPossessor possessor, ItemHolder itemToAdd)
    {
        return GetInvHolder(possessor).Add(itemToAdd);
    }

    public void AddItemAt(PossessorSearcher.ItemPossessor possessor, ItemHolder itemToAdd, int posToAdd)
    {
        GetInvHolder(possessor).AddAt(itemToAdd, posToAdd);
    }

    public void RemoveItemAt(PossessorSearcher.ItemPossessor possessor, int posToRemove, int amount)
    {
        GetInvHolder(possessor).RemoveAt(posToRemove, amount);         
    }

    public void UseItem(PossessorSearcher.ItemPossessor possessor, int pos, CharStats charToUse)
    {
        GetInvHolder(possessor).UseItem(pos, charToUse);       
    }   
}

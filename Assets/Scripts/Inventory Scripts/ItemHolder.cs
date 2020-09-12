[System.Serializable]
public class ItemHolder
{
    public const int ITEM_CAPACITY = 99;

    #region PROPERTIES
    public Item TheItem { get { return theItem; } set { theItem = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public bool IsEquipped { get { return isEquipped; } set { isEquipped = value; } }
    #endregion

    #region FIELDS
    private Item theItem;

    private int amount;

    private bool isEquipped;
    #endregion

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

    public bool CompareItem(ItemHolder other) => theItem.Equals(other.TheItem);

    public bool CompareType(ItemHolder other) => theItem.CompareType(other.theItem);

    public bool IsEmpty() => amount <= 0;
    
    public bool IsFull() => amount == ITEM_CAPACITY;

    public void Use(CharStats stats) => theItem.Use(stats);
}

public abstract class AbstractItemHolderFactory
{
    public abstract ItemHolder CreateRegularHolder(Item item, int amount);
    public abstract ItemHolder CreateEquipmentHolder(Item item);
    public abstract ItemHolder CreateItemToObtainHolder(Item item);
    public ItemHolder CreateNullHolder(Item nullItem)
    {
        return new ItemHolder(nullItem, 0);
    }
}

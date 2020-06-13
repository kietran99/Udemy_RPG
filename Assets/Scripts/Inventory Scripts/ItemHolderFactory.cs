public class ItemHolderFactory : AbstractItemHolderFactory
{
    public override ItemHolder CreateEquipmentHolder(Item item)
    {
        return new ItemHolder(item, 1, true);
    }

    public override ItemHolder CreateItemToObtainHolder(Item item)
    {
        return new ItemHolder(item, 1);
    }

    public override ItemHolder CreateRegularHolder(Item item, int amount)
    {
        return new ItemHolder(item, amount);
    }
}

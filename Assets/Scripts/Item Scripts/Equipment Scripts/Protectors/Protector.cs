public abstract class Protector : Equipment
{
    public override string GetItemType()
    {
        return "Defence";
    }

    public override int GetCorresStat(CharStats stats)
    {
        return stats.Defense;
    }

}

public abstract class Protector : Equipment
{
    public override string GetItemType()
    {
        return "Defence";
    }

    public override int GetCorreStat(CharStats stats)
    {
        return stats.Defence;
    }

}

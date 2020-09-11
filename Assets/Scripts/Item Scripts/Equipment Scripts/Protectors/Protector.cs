public abstract class Protector : Equipment
{
    public override string GetItemType() => "Defence";

    public override int GetCorresStat(CharStats stats) => stats.Defense;
}

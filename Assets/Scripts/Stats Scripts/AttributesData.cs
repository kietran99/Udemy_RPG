public struct AttributesData
{
    public int strength, defense, intellect, vitality, agility, luck;

    public AttributesData(CharStats stats)
    {
        strength = stats.Strength;
        defense = stats.Defense;
        intellect = stats.Intellect;
        vitality = stats.Vitality;
        agility = stats.Agility;
        luck = stats.Luck;
    }
}

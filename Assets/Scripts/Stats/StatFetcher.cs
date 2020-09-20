public class StatFetcher
{    
    private AbstractStatExtractor extractor;

    public StatFetcher(EntityStats.Attributes attr)
    {
        switch (attr)
        {
            case EntityStats.Attributes.STR: extractor = new StrengthExtractor(); break;
            case EntityStats.Attributes.DEF: extractor = new DefenceExtractor(); break;
            case EntityStats.Attributes.INT: extractor = new IntellectExtractor(); break;
            case EntityStats.Attributes.VIT: extractor = new VitalityExtractor(); break;
            case EntityStats.Attributes.AGI: extractor = new AgilityExtractor(); break;
            case EntityStats.Attributes.LCK: extractor = new LuckExtractor(); break;
            case EntityStats.Attributes.HP:
            case EntityStats.Attributes.MAX_HP: extractor = new HealthExtractor(); break;
            case EntityStats.Attributes.MP:
            case EntityStats.Attributes.MAX_MP: extractor = new ManaExtractor(); break;
            case EntityStats.Attributes.EXP: extractor = new ExpExtractor(); break;
            default: break;
        }
    }

    public string ExtractName() => extractor.ExtractName();

    public (int cur, int max) ExtractValues(CharStats stats) => extractor.ExtractValues(stats);
}

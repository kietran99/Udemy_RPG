﻿public abstract class AbstractStatExtractor
{    
    public abstract string ExtractName();
    public abstract (int cur, int max) ExtractValues(CharStats stats);
}

public class StrengthExtractor : AbstractStatExtractor
{   
    public override string ExtractName() => "STR";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Strength, Constants.INVALID);
}

public class DefenceExtractor : AbstractStatExtractor
{   
    public override string ExtractName() => "DEF";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Defense, Constants.INVALID);
}

public class IntellectExtractor : AbstractStatExtractor
{    
    public override string ExtractName() => "INT";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Intellect, Constants.INVALID);
}

public class VitalityExtractor : AbstractStatExtractor
{    
    public override string ExtractName() => "VIT";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Vitality, Constants.INVALID);
}

public class AgilityExtractor : AbstractStatExtractor
{
    public override string ExtractName() => "AGI";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Agility, Constants.INVALID);
}

public class LuckExtractor : AbstractStatExtractor
{
    public override string ExtractName() => "LCK";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.Luck, Constants.INVALID);
}

public class HealthExtractor : AbstractStatExtractor
{
    public override string ExtractName() => "HP";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.CurrentHP, stats.MaxHP);
}

public class ManaExtractor : AbstractStatExtractor
{
    public override string ExtractName() => "MP";

    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.CurrentMP, stats.MaxMP);
}

public class ExpExtractor : AbstractStatExtractor
{
    public override string ExtractName() => "EXP"; 
    public override (int cur, int max) ExtractValues(CharStats stats) => (stats.CurrentEXP, Constants.INVALID);
}


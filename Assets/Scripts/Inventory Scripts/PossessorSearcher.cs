public static class PossessorSearcher
{
    private const int BAG = -1;
    private const int NONE = -2;
    public const string bagPossessor = "Bag";

    public enum ItemPossessor
    {
        BAG,
        P1,
        P2,
        P3,
        P4,
        P5,
        NONE = 101
    }

    private static int GetCharPos(ItemPossessor possessor)
    {
        switch (possessor)
        {
            case ItemPossessor.BAG:
                return BAG;
            case ItemPossessor.P1:
                return 0;
            case ItemPossessor.P2:
                return 1;
            case ItemPossessor.P3:
                return 2;
            case ItemPossessor.P4:
                return 3;
            case ItemPossessor.P5:
                return 4;
            default:
                return NONE;
        }
    }

    public static CharStats GetPossessor(ItemPossessor possessor)
    {
        int converted = GetCharPos(possessor);
        //int converted = (int) possessor;

        switch (converted)
        {            
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                CharStats temp = GameManager.Instance.PlayerStats[converted];
                if (temp.gameObject.activeInHierarchy) return temp;
                else return null;
            default:
                return null;
        }
    }

    public static string GetPossessorName(ItemPossessor possessor)
    {
        int converted = GetCharPos(possessor);
        //int converted = (int) possessor;

        switch (converted)
        {
            case BAG:
                return bagPossessor;
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                CharStats temp = GameManager.Instance.PlayerStats[converted];
                if (temp.gameObject.activeInHierarchy) return temp.CharacterName;
                else return "";
            default:
                return "";
        }
    }

    public static void FillPossessorList(CircularLinkedList<ItemPossessor> list)
    {    
        foreach(ItemPossessor possessor in (ItemPossessor[])System.Enum.GetValues(typeof(ItemPossessor)))
        {
            if (possessor == ItemPossessor.NONE) continue;
            list.Append(possessor);
        }       
    }
}

namespace RPG.Inventory
{
    public enum ItemOwner
    {
        BAG,
        P0,
        P1,
        P2,
        P3,
        P4,
        NONE = 101
    }

    public static class PossessorSearcher
    {
        private const int BAG = -1;
        private const int NONE = -2;
        public const string BAG_OWNER = "Bag";

        private static int GetCharPos(ItemOwner possessor)
        {
            switch (possessor)
            {
                case ItemOwner.BAG:
                    return BAG;
                case ItemOwner.P0:
                    return 0;
                case ItemOwner.P1:
                    return 1;
                case ItemOwner.P2:
                    return 2;
                case ItemOwner.P3:
                    return 3;
                case ItemOwner.P4:
                    return 4;
                case ItemOwner.NONE:
                    return NONE;
                default:
                    return NONE;
            }
        }

        public static CharStats GetStats(ItemOwner possessor)
        {
            int converted = GetCharPos(possessor);

            switch (converted)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    CharStats temp = GameManager.Instance.PlayerStats[converted];
                    if (temp.gameObject.activeInHierarchy) return temp;
                    else return null;
                default:
                    return null;
            }
        }

        public static string GetPossessorName(ItemOwner possessor)
        {
            int converted = GetCharPos(possessor);

            switch (converted)
            {
                case BAG:
                    return BAG_OWNER;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    CharStats temp = GameManager.Instance.PlayerStats[converted];
                    if (temp.gameObject.activeInHierarchy) return temp.CharacterName;
                    else return "";
                default:
                    return "";
            }
        }

        public static ItemOwner GetOwner(string ownerName)
        {            
            int ownerIdx = GameManager.Instance.PlayerStats.LookUp(_ =>
            _.gameObject.activeInHierarchy && _.CharacterName.Equals(ownerName)).idx;

            switch (ownerIdx)
            {
                case 0:
                    return ItemOwner.P0;
                case 1:
                    return ItemOwner.P1;
                case 2:
                    return ItemOwner.P2;
                case 3:
                    return ItemOwner.P3;
                case 4:
                    return ItemOwner.P4;
                default:
                    return ItemOwner.NONE;
            }
        }

        public static void FillPossessorList(CircularLinkedList<ItemOwner> list)
        {
            foreach (ItemOwner possessor in (ItemOwner[])System.Enum.GetValues(typeof(ItemOwner)))
            {
                if (possessor == ItemOwner.NONE) continue;
                list.Append(possessor);
            }
        }
    }
}

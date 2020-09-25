namespace RPG.Inventory
{
    public enum InventoryOwner
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

        private static int GetCharPos(InventoryOwner possessor)
        {
            switch (possessor)
            {
                case InventoryOwner.BAG:
                    return BAG;
                case InventoryOwner.P0:
                    return 0;
                case InventoryOwner.P1:
                    return 1;
                case InventoryOwner.P2:
                    return 2;
                case InventoryOwner.P3:
                    return 3;
                case InventoryOwner.P4:
                    return 4;
                case InventoryOwner.NONE:
                    return NONE;
                default:
                    return NONE;
            }
        }

        public static CharStats GetStats(InventoryOwner possessor)
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

        public static string GetPossessorName(InventoryOwner possessor)
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

        public static InventoryOwner GetOwner(string ownerName)
        {            
            int ownerIdx = GameManager.Instance.PlayerStats.LookUp(_ =>
            _.gameObject.activeInHierarchy && _.CharacterName.Equals(ownerName)).idx;

            switch (ownerIdx)
            {
                case 0:
                    return InventoryOwner.P0;
                case 1:
                    return InventoryOwner.P1;
                case 2:
                    return InventoryOwner.P2;
                case 3:
                    return InventoryOwner.P3;
                case 4:
                    return InventoryOwner.P4;
                default:
                    return InventoryOwner.NONE;
            }
        }

        public static void FillPossessorList(CircularLinkedList<InventoryOwner> list)
        {
            foreach (InventoryOwner possessor in (InventoryOwner[])System.Enum.GetValues(typeof(InventoryOwner)))
            {
                if (possessor == InventoryOwner.NONE) continue;
                list.Append(possessor);
            }
        }
    }
}

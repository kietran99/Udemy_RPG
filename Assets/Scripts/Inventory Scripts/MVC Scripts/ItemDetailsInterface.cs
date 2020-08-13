namespace RPG.Inventory
{
    public struct DetailData
    {
        public string name;
        public string description;

        public DetailData(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }

    public interface ItemDetailsInterface
    {
        void Show(string name, string description);
    }
}

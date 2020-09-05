namespace RPG.Inventory
{
    public struct DetailData
    {
        public bool shouldShow;

        public string name;
        public string description;      
        
        public DetailData(string name, string description, bool shouldShow = true)
        {
            this.name = name;
            this.description = description;
            this.shouldShow = shouldShow;
        }
    }

    public interface ItemDetailsInterface
    {
        void Show(string name, string description);
    }
}

using UnityEngine;

namespace RPG.Inventory
{
    public struct DetailsData
    {
        public bool shouldShow;

        public string name;
        public string description;
        public System.Type itemType;
        public Sprite[] equippableSprites;
        
        public DetailsData(string name, string description, Sprite[] equippableSprites, System.Type itemType, bool shouldShow = true)
        {
            this.name = name;
            this.description = description;
            this.equippableSprites = equippableSprites;
            this.itemType = itemType;
            this.shouldShow = shouldShow;
        }
    }

    public interface ItemDetailsInterface
    {
        void Show(string name, string description, Sprite[] equippablesSprites, System.Type itemType);
        void Reset();
    }
}

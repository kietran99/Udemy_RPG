using UnityEngine;

namespace RPG.Inventory
{
    public struct DetailData
    {
        public bool shouldShow;

        public string name;
        public string description;
        public Sprite[] equippablesSprites;
        
        public DetailData(string name, string description, Sprite[] equippableCharactersSprites, bool shouldShow = true)
        {
            this.name = name;
            this.description = description;
            this.equippablesSprites = equippableCharactersSprites;
            this.shouldShow = shouldShow;
        }
    }

    public interface ItemDetailsInterface
    {
        void Show(string name, string description, Sprite[] equippablesSprites);
    }
}

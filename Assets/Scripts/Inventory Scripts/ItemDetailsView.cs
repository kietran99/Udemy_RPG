using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    [RequireComponent (typeof(ObjectPool))]
    public class ItemDetailsView : MonoBehaviour, ItemDetailsInterface
    {      
        [SerializeField]
        private Text nameText = null, descriptionText = null;

        [SerializeField]
        private ItemTypeIconFetcher itemTypeIcon = null;

        private IObjectPool spritesPool;

        void Awake()
        {
            spritesPool = GetComponent<IObjectPool>();
        }

        public void Show(string name, string description, Sprite[] equippablesSprites, System.Type itemType)
        {
            nameText.text = name;
            descriptionText.text = description;
            spritesPool.Reset(); 
            equippablesSprites.Map(sprite => spritesPool.Pop().GetComponent<Image>().sprite = sprite);
            itemTypeIcon.Show(itemType);
        }

        public void Reset()
        {
            nameText.text = string.Empty;
            descriptionText.text = string.Empty;
            spritesPool.Reset();
            itemTypeIcon.Hide();
        }
    }
}

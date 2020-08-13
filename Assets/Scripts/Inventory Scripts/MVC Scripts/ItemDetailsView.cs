using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class ItemDetailsView : MonoBehaviour, ItemDetailsInterface
    {
        #region PROPERTIES
        public Text ItemNameText { get { return nameText; } }
        public Text ItemDescriptionText { get { return descriptionText; } }
        #endregion

        [SerializeField]
        private Text nameText = null,
                     descriptionText = null;

        public void Show(string name, string description)
        {
            nameText.text = name;
            descriptionText.text = description;
        }
    }
}

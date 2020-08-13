using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class ItemDetailsView : MonoBehaviour, ItemDetailsInterface
    {      
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

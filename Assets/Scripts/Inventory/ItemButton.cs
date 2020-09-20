using UnityEngine;
using UnityEngine.UI;

namespace RPG.Inventory
{
    public class ItemButton : MonoBehaviour
    {
        #region PROPERTIES
        public Image ItemImage { get { return itemImage; } set { itemImage = value; } }
        public Text AmountText { get { return amountText; } set { amountText = value; } }
        public int ButtonPos { get { return buttonPos; } set { buttonPos = value; } }
        #endregion
        
        [SerializeField]
        private Image itemImage = null;

        [SerializeField]
        private Text amountText = null;

        private int buttonPos;

        private IClickObserve invoker;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => GetPos());
        }

        public void Init(IClickObserve invoker, int buttonPos)
        {
            this.invoker = invoker;
            this.buttonPos = buttonPos;
        }

        public void DisplayItem(ItemHolder holder)
        {
            if (holder.TheItem.Sprite == null)
            {
                itemImage.enabled = false;
                amountText.enabled = false;
                return;
            }
            
            itemImage.enabled = true;
            amountText.enabled = true;
            itemImage.sprite = holder.TheItem.Sprite;
            amountText.text = holder.IsEquipped ? "E" : holder.Amount.ToString();            
        }

        private void GetPos()
        {
            invoker.OnButtonClick(buttonPos);
        }
    }
}

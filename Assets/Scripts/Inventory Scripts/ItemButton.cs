using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    #region
    public Image ItemImage { get { return itemImage; } set { itemImage = value; } }
    public Text AmountText { get { return amountText; } set { amountText = value; } }
    public int ButtonPos { get { return buttonPos; } set { buttonPos = value; } }
    #endregion

    private IClickInvoker invoker;

    [SerializeField]
    private Image itemImage = null;
    
    [SerializeField]
    private Text amountText = null;

    private int buttonPos;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GetPos());
    }

    public void Init(IClickInvoker invoker, int buttonPos)
    {
        this.invoker = invoker;
        this.buttonPos = buttonPos;
    }

    public void DisplayItem(ItemHolder holder)
    {
        if (holder.TheItem.Image == null)
        {
            itemImage.enabled = false;
            amountText.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            amountText.enabled = true;
            itemImage.sprite = holder.TheItem.Image;
            if (holder.IsEquipped)
            {
                amountText.text = "E";
            }
            else
            {
                amountText.text = holder.Amount.ToString();
            }
        }
    }

    private void GetPos()
    {
        invoker.OnInvokeeClick(buttonPos);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MerchInfo : MonoBehaviour
{    
    public interface IClickable
    {
        void OnClick(int pos);
    }

    [SerializeField]
    private Image image = null;

    [SerializeField]
    private Text merchNameText = null, priceText = null;

    private int order;

    private IClickable clickable;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GetOrder());
    }

    public void SetData(IClickable clickable, Sprite sprite, string merchName, int price, int order)
    {
        this.clickable = clickable;
        this.image.sprite = sprite;
        this.merchNameText.text = merchName;
        this.priceText.text = price.ToString() + " G";
        this.order = order;
    }   
    
    private void GetOrder()
    {
        clickable.OnClick(order);
    }
}

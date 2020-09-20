using UnityEngine;
using UnityEngine.UI;

public class MerchInfo : MonoBehaviour
{    
    [SerializeField]
    private Image image = null;

    [SerializeField]
    private Text merchNameText = null, priceText = null;

    private int order;

    private IClickObserve invoker;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GetOrder());
    }

    public void SetData(IClickObserve clickable, Sprite sprite, string merchName, int price, int order)
    {
        this.invoker = clickable;
        this.image.sprite = sprite;
        this.merchNameText.text = merchName;
        this.priceText.text = price.ToString() + " G";
        this.order = order;
    }   
    
    private void GetOrder()
    {
        invoker.OnButtonClick(order);
    }
}

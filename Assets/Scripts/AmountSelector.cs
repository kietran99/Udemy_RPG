using UnityEngine;
using UnityEngine.UI;

public class AmountSelector : MonoBehaviour
{
    public interface IEnabler
    {
        void OnAmountConfirm(int changeAmount);
    }

    [SerializeField]
    private Text amountText = null;

    private const string STARTING_VAL = "01";   
    private int itemQuantity;
    
    private IEnabler enabler;
    private GameObject invoker;

    // Start is called before the first frame update
    void Start()
    {
        amountText.text = STARTING_VAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Input.GetKey(KeyCode.W)) IncreaseAmount();
        if (Input.GetKey(KeyCode.S)) DecreaseAmount();        
    }

    private void OnEnable()
    {
        enabler = null;
        amountText.text = STARTING_VAL;
    }

    private void OnDisable()
    {
        enabler = null;
        SetBtnsInteraction(true);
    }

    public void SetEnabler(IEnabler enabler, GameObject invoker, int itemQuantity)
    {
        this.enabler = enabler;
        this.itemQuantity = itemQuantity;
        this.invoker = invoker;
        SetBtnsInteraction();
    }

    private void SetBtnsInteraction(bool flag = false)
    {
        // Disable button click for every button in the display including this gameObject's buttons
        foreach (Button button in invoker.GetComponentsInChildren<Button>())
        {
            button.interactable = flag; 
        }

        // Enable button click for this gameObject's buttons
        foreach (Button button in gameObject.GetComponentsInChildren<Button>())
        {
            button.interactable = !flag;
        }  
    }

    private void IncreaseAmount()
    {       
        amountText.text = GetIncrement();
    }

    private void DecreaseAmount()
    {
        amountText.text = GetDecrement();
    }

    private int IntFastParse(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }
   
    private string GetIncrement()
    {
        int increaseVal = IntFastParse(amountText.text);
        if (increaseVal == itemQuantity) increaseVal = 1;
        else increaseVal++;
        string str = "";
        if (increaseVal < 10) str = "0";
        return str + increaseVal;
    }

    private string GetDecrement()
    {
        int decreaseVal = IntFastParse(amountText.text);
        if (decreaseVal == 1) decreaseVal = itemQuantity;
        else decreaseVal--;
        string str = "";
        if (decreaseVal < 10) str = "0";
        return str + decreaseVal;
    }  

    public void Confirm()
    {
        if (enabler != null)
        {
            enabler.OnAmountConfirm(IntFastParse(amountText.text));
            SetBtnsInteraction(true);
        }
    }

    public void Cancel()
    {
        if (enabler != null)
        {
            enabler.OnAmountConfirm(0);
            SetBtnsInteraction(true);
        }
    }
}

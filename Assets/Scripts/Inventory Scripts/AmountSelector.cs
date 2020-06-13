using UnityEngine;
using UnityEngine.UI;

public class AmountSelector : MonoBehaviour, IAmountSelector
{
    [SerializeField]
    private Text amountText = null;

    private const string STARTING_VAL = "01";   
    private int itemQuantity;
    
    private GameObject display, itemInteractor;
    private IAmountConfirmable confirmable;
    private ILiveAmountObserver amtObserver;

    [SerializeField]
    private float requiredHoldTime = 0f;

    private float startTime = 0f;
    private KeyCode lastKey;

    // Start is called before the first frame update
    void Start()
    {
        amountText.text = STARTING_VAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Cancel();
        else HoldKey();    
    }

    private void OnEnable()
    {
        amountText.text = STARTING_VAL;
    }

    private void HoldKey()
    {
        if (!gameObject.activeInHierarchy) return;
     
        if (Input.GetKey(KeyCode.W))
        {
            if (lastKey == KeyCode.S) Reset();

            lastKey = KeyCode.W;

            startTime += Time.deltaTime;

            if (startTime >= requiredHoldTime)
            {
                IncreaseAmount();
                Reset();
            }
            
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            if (lastKey == KeyCode.W) Reset();

            lastKey = KeyCode.S;

            startTime += Time.deltaTime;

            if (startTime >= requiredHoldTime)
            {
                DecreaseAmount();
                Reset();
            }
        }
    }

    private void Reset()
    {
        startTime = 0f;
    }

    public void Activate(IAmountConfirmable confirmable, GameObject display, GameObject itemInteractor, int itemQuantity)
    {
        this.confirmable = confirmable;
        this.display = display;
        this.itemQuantity = itemQuantity;
        if (itemInteractor != null) this.itemInteractor = itemInteractor;
        this.itemInteractor.SetActive(false);
        SetBtnsInteraction();
        gameObject.SetActive(true);
    }

    public void Activate(IAmountConfirmable confirmable, GameObject display, GameObject itemInteractor, ILiveAmountObserver observer, int itemQuantity)
    {
        Activate(confirmable, display, itemInteractor, itemQuantity);
        amtObserver = observer;
        amtObserver.OnValueChanged(1);
    }

    private void SetBtnsInteraction(bool flag = false)
    {
        // Disable button click for every button in the display including this gameObject's buttons
        foreach (Button button in display.GetComponentsInChildren<Button>())
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
        if (amtObserver != null) amtObserver.OnValueChanged(increaseVal);
        string str = "";
        if (increaseVal < 10) str = "0";
        return str + increaseVal;
    }

    private string GetDecrement()
    {
        int decreaseVal = IntFastParse(amountText.text);
        if (decreaseVal == 1) decreaseVal = itemQuantity;
        else decreaseVal--;
        if (amtObserver != null) amtObserver.OnValueChanged(decreaseVal);
        string str = "";
        if (decreaseVal < 10) str = "0";
        return str + decreaseVal;
    }  

    public void Confirm()
    {
        ReturnValue(IntFastParse(amountText.text));
    }

    public void Cancel()
    {
        ReturnValue(0);
    }

    private void ReturnValue(int val)
    {
        if (confirmable == null) return;
        
        confirmable.OnAmountConfirm(val);
        SetBtnsInteraction(true);
        if (itemInteractor != null) itemInteractor.SetActive(true);
        gameObject.SetActive(false);       
    }

}

using System;
using UnityEngine;
using UnityEngine.UI;

public class AmountSelector : MonoBehaviour, IAmountSelector
{
    [SerializeField]
    private Text amountText = null;

    private const string STARTING_VAL = "01";   
    private int itemQuantity;
    
    private GameObject display;

    [SerializeField]
    private float requiredHoldTime = 0f;

    private float startTime = 0f;
    private KeyCode lastKey;

    #region DELEGATES
    public Action<int> OnAmountConfirm { get; set; }
    public Action<int> OnValueChange { get; set; }
    public Action OnActivate { get ; set; }
    public Action OnDeactivate { get; set; }
    #endregion

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

    public void Activate(GameObject view, int itemQuantity)
    {
        this.display = view;
        this.itemQuantity = itemQuantity;
        OnActivate?.Invoke();
        SetButtonsInteractability();
        gameObject.SetActive(true);
        OnValueChange?.Invoke(1);
    }

    private void SetButtonsInteractability(bool flag = false)
    {
        foreach (Button button in display.GetComponentsInChildren<Button>())
        {
            button.interactable = flag; 
        }

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
        increaseVal = increaseVal == itemQuantity ? 1 : increaseVal + 1;
        OnValueChange?.Invoke(increaseVal);
        return FormatNumText(increaseVal);
    }

    private string GetDecrement()
    {
        int decreaseVal = IntFastParse(amountText.text);
        decreaseVal = decreaseVal == 1 ? itemQuantity : decreaseVal - 1;
        OnValueChange?.Invoke(decreaseVal);
        return FormatNumText(decreaseVal);
    }  

    private string FormatNumText(int num)
    {
        return (num >= 10 ? "" : "0") + num;
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
        if (OnAmountConfirm == null) return;

        OnAmountConfirm(val);
        SetButtonsInteractability(true);
        OnDeactivate?.Invoke();
        gameObject.SetActive(false);       
    }

}

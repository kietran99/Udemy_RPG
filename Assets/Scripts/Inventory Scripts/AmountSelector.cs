using RPG.Inventory;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(InteractDisabler))]
public class AmountSelector : MonoBehaviour, IAmountSelector
{
    private int CurrentAmount
    {
        get { return currentAmount; }
        set
        {
            currentAmount = value;
            amountText.text = currentAmount.FormatNum();

            OnValueChange?.Invoke(currentAmount);
        }
    }

    [SerializeField]
    private Text amountText = null;

    private int currentAmount;

    private int itemQuantity;

    private InteractDisablerInterface interactDisabler;

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

    void Awake()
    {
        interactDisabler = GetComponent<InteractDisablerInterface>();
    }
    
    void Update()
    {        
        if (Input.GetKeyDown(KeyboardControl.GlobalExit)) Cancel();
        else HoldKey();    
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

    public void Activate(int itemQuantity)
    {
        this.itemQuantity = itemQuantity;
        
        gameObject.SetActive(true);
        interactDisabler.Activate();

        OnActivate?.Invoke();
        CurrentAmount = 1;
    }
    
    private void IncreaseAmount()
    {
        CurrentAmount = CurrentAmount == itemQuantity ? 1 : CurrentAmount + 1;
    }

    private void DecreaseAmount()
    {
        CurrentAmount = CurrentAmount == 1 ? itemQuantity : CurrentAmount - 1;
    }    
          
    public void Confirm()
    {
        ReturnValue(CurrentAmount);
    }

    public void Cancel()
    {
        ReturnValue(0);
    }

    private void ReturnValue(int val)
    {
        if (OnAmountConfirm == null) return;

        OnAmountConfirm(val);
        interactDisabler?.Deactivate();
        OnDeactivate?.Invoke();
        gameObject.SetActive(false);       
    }
}

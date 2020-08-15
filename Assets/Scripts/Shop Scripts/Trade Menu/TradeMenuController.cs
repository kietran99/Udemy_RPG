using UnityEngine;

public abstract class TradeMenuController : MonoBehaviour, IClickObserve
{
    protected void SubscribeToDelegates()
    {
        amtSelector.OnAmountConfirm += OnAmountConfirm;
        amtSelector.OnValueChange += merchDescription.UpdateShownValue;
        amtSelector.OnActivate += () => defaultActions.SetActive(false);
        amtSelector.OnDeactivate += () => defaultActions.SetActive(true);
    }

    protected ShopDialog dialog;

    [SerializeField]
    protected MerchDescription merchDescription = null;

    [SerializeField]
    protected AmountSelector amtSelector = null;

    [SerializeField]
    protected GameObject defaultActions = null;

    public abstract void OnAmountConfirm(int changeAmount);
    public abstract void OnButtonClick(int pos);
}

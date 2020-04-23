using UnityEngine;

public abstract class TradeMenuController : MonoBehaviour, IClickInvoker, IAmountConfirmable
{
    protected ShopDialog dialog;

    [SerializeField]
    protected MerchDescription merchDescription = null;

    [SerializeField]
    protected AmountSelector amtSelector = null;

    [SerializeField]
    protected GameObject defaultActions = null;

    public abstract void OnAmountConfirm(int changeAmount);
    public abstract void OnInvokeeClick(int pos);
}

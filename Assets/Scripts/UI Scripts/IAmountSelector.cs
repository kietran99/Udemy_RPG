using UnityEngine;
public interface IAmountConfirmable
{
    void OnAmountConfirm(int changeAmount);
}

public interface IAmountSelector
{
    void Activate(IAmountConfirmable confirmable, GameObject display, GameObject itemInteractor, int itemQuantity);
    void Activate(IAmountConfirmable confirmable, GameObject display, GameObject itemInteractor, ILiveAmountObserver observer, int itemQuantity);
    void Confirm();
    void Cancel();
}


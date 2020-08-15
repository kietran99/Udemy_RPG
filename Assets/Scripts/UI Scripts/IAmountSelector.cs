using System;
using UnityEngine;

public interface IAmountSelector
{
    void Activate(GameObject display, int itemQuantity);
    void Confirm();
    void Cancel();

    Action OnActivate { get; set; }
    Action OnDeactivate { get; set; }
    Action<int> OnAmountConfirm { get; set; }
    Action<int> OnValueChange { get; set; }
}


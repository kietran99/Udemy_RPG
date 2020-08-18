using System;
using UnityEngine;

public interface IAmountSelector
{
    #region METHODS
    void Activate(GameObject display, int itemQuantity);
    void Confirm();
    void Cancel();
    #endregion

    #region DELEGATES
    Action OnActivate { get; set; }
    Action OnDeactivate { get; set; }
    Action<int> OnAmountConfirm { get; set; }
    Action<int> OnValueChange { get; set; }
    #endregion
}


public abstract class AmountConfirmableDisplay : UIDisplay, AmountSelector.IAmountConfirmable
{
    public abstract void OnAmountConfirm(int changeAmount);
}

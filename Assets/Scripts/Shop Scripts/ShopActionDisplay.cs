using UnityEngine;
using UnityEngine.UI;

public class ShopActionDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject actionCanvas = null, itemTypeCanvas = null;

    [SerializeField]
    private Button[] actionButtons = null;

    private ShopDialog dialog;
    
    public void ToggleActionCanvas(bool flag, ShopDialog dialog)
    {
        actionCanvas.SetActive(flag);
        if (dialog != null) this.dialog = dialog;
        ToggleActionButtons(flag);
    }

    public void ToggleItemTypeCanvas(bool flag)
    {
        itemTypeCanvas.SetActive(flag);
        ToggleActionButtons(!flag);
        if (!flag && dialog != null) dialog.Disable();
    }  

    public void ToggleMenuDisplay(bool flag)
    {
        actionCanvas.SetActive(flag);
        itemTypeCanvas.SetActive(flag);
    }

    private void ToggleActionButtons(bool flag)
    {
        actionButtons.Map(_ => _.interactable = flag);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ButtonsPicker : MonoBehaviour
{
    public Button[] Buttons 
    { 
        get 
        { 
            if (!autoPicking) return buttons;

            autoPickedButtons = autoPickedButtons ?? GetComponentsInChildren<Button>();
            return autoPickedButtons;
        } 
    }

    [SerializeField] bool autoPicking = false;

    [SerializeField] Button[] buttons = null;

    private Button[] autoPickedButtons = null;
}

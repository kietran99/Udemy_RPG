using UnityEngine;
using UnityEngine.UI;
using Functional;

namespace RPG.Inventory
{
    public class InteractDisabler : MonoBehaviour
    {
        [SerializeField] ButtonsPicker[] pickers = null;

        void OnEnable()
        {
            SetInteractability(false);
        }

        void OnDisable()
        {
            SetInteractability(true);
        }

        void SetInteractability(bool interactable)
        {
            HOF.Map((ButtonsPicker picker) => HOF.Map(
                (Button button) => button.interactable = interactable, picker.Buttons), 
            pickers);
        }
    }
}

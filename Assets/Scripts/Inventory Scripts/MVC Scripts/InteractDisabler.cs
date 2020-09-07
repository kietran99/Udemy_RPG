using UnityEngine;
using UnityEngine.UI;
using Functional;

namespace RPG.Inventory
{
    public class InteractDisabler : MonoBehaviour, InteractDisablerInterface
    {
        [SerializeField] ButtonsPicker[] pickers = null;

        public void Activate()
        {
            SetInteractability(false);
        }

        public void Deactivate()
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

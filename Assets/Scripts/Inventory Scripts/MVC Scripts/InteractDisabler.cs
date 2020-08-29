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
            HigherOrderFunc.Map((ButtonsPicker picker) => HigherOrderFunc.Map(
                (Button button) => button.interactable = interactable, picker.Buttons), 
            pickers);
        }
    }
}

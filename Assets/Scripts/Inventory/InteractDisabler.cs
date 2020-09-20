using UnityEngine;

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
            pickers.Map(picker => picker.Buttons.Map(_ => _.interactable = interactable));
        }
    }
}

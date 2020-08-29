using System;
using UnityEngine;

namespace RPG.Inventory
{
    [RequireComponent(typeof(InteractDisabler))]
    public class UserChooserController : MonoBehaviour, IUserChooserController
    {
        InteractDisablerInterface interactDisabler;

        [SerializeField] GameObject viewObject = null;

        IUserChooserView view;

        #region DELEGATES
        public Action OnActivate { get; set; }
        public Action OnDeactivate { get; set; }
        #endregion

        void Awake()
        {
            interactDisabler = GetComponent<InteractDisablerInterface>();
            view = viewObject.GetComponent<IUserChooserView>();
        }

        void Start()
        {
            interactDisabler.Activate();

            OnActivate?.Invoke();
        }

        void Update()
        {
            if (!gameObject.activeInHierarchy) return;

            if (Input.GetKeyDown(KeyboardControl.GlobalExit))
            {
                Deactivate();
                gameObject.SetActive(false);
            }
        }

        public void Deactivate()
        {
            view.Destruct();
            interactDisabler.Deactivate();
            OnDeactivate?.Invoke();
        }
    }
}

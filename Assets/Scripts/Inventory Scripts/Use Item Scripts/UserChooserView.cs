﻿using UnityEngine;
using UnityEngine.UI;
using Functional;

namespace RPG.Inventory
{
    public class UserChooserView : MonoBehaviour, IUserChooserView
    {
        [SerializeField]
        private GameObject userButtonPrefab = null, buttonsContainer = null;

        [SerializeField]
        private Text remainingText = null;

        private UserButton[] userButtons;

        public void Init()
        {
            int numOfActives = GameManager.Instance.GetNumActives();
            userButtons = new UserButton[numOfActives];

            for (int i = 0; i < numOfActives; i++)
            {
                GameObject instance = Instantiate(userButtonPrefab, buttonsContainer.transform);
                userButtons[i] = instance.GetComponent<UserButton>();
                int pos = i;
                //userButtons[i].GetComponent<Button>().onClick.AddListener(() => OnUserSelected(pos));
            }
        }

        public void Destruct()
        {            
            HigherOrderFunc.Map((UserButton button) => Destroy(button.gameObject), userButtons);
        }
    }
}

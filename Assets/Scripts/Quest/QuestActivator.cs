using UnityEngine;

namespace RPG.Quest
{
    public class QuestActivator : MonoBehaviour
    {
        [SerializeField]
        private QuestData data = null;

        [SerializeField]
        private GameObject questActivateUIObject = null;

        private QuestActivateUI questActivateUI;

        private bool canActivate, hasAccepted;

        private void Update()
        {
            if (hasAccepted) return;

            if (!canActivate) return;

            if (!Input.GetKeyDown(KeyCode.Q)) return;

            InitUI();
            questActivateUI.gameObject.SetActive(true);
        }

        private void InitUI()
        {
            if (questActivateUI == null)
            {
                questActivateUI = Instantiate(questActivateUIObject).GetComponent<QuestActivateUI>();
                questActivateUI.OnAccept += OnQuestAccept;
                questActivateUI.BindData(data.QuestName, data.QuestDescription, data.ItemReward, data.GoldReward);
            }
        }

        private void OnQuestAccept()
        {
            hasAccepted = true;
            
            questActivateUI.OnAccept -= OnQuestAccept;

            if (ServiceLocator.Resolve<IQuestManager>(out IQuestManager questManager))
            {
                questManager.AddTracker(data.QuestGoal.GenerateTracker(data.QuestName));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            canActivate = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            canActivate = false;
        }
    }
}

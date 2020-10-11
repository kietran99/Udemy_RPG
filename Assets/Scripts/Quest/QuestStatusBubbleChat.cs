using UnityEngine;

namespace RPG.Quest
{
    public class QuestStatusBubbleChat : MonoBehaviour
    {
        [SerializeField]
        private Color unacceptedColor = Color.white, ongoingColor = Color.white, completedColor = Color.white;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;
       
        private void Start()
        {
            EventSystems.EventManager.Instance.
                StartListening<QuestStatusChangeData>(ChangeSpriteColor);
        }

        private void OnDestroy()
        {
            EventSystems.EventManager.Instance.
                StopListening<QuestStatusChangeData>(ChangeSpriteColor);
        }

        private void ChangeSpriteColor(QuestStatusChangeData data) => UpdateSpriteColor(data.status);

        public void UpdateSpriteColor(QuestStatus status)
        {
            switch (status)
            {
                case QuestStatus.UNACCEPTED:
                    spriteRenderer.color = unacceptedColor;
                    break;
                case QuestStatus.ONGOING:
                    spriteRenderer.color = ongoingColor;
                    break;
                case QuestStatus.COMPLETED:
                    spriteRenderer.color = completedColor;
                    break;
            }
        }
    }
}
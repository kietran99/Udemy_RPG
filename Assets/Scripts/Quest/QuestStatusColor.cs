using UnityEngine;

namespace RPG.Quest
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class QuestStatusColor : MonoBehaviour
    {
        [SerializeField]
        private Color unacceptedColor = Color.white, ongoingColor = Color.white, completedColor = Color.white;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            EventSystems.EventManager.Instance.StartListening<QuestStatusChangeData>(_ => UpdateSpriteColor(_.status));
        }

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
using UnityEngine;

namespace RPG.Quest
{
    public class QuestProgressTracker : MonoBehaviour
    {
        private struct TestData : EventSystems.IEventData
        {
            public int x;

            public TestData(int x)
            {
                this.x = x;
            }
        }

        private void Awake()
        {
            EventSystems.EventManager.Instance.StartListening<TestData>(Foo);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V)) EventSystems.EventManager.Instance.TriggerEvent(new TestData(1));
        }  
        
        void Foo(TestData eventData)
        {
            Debug.Log(((TestData)eventData).x);
        }
    }
}

using EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Quest
{
    public class QuestManager : IQuestManager
    {
        private List<IQuestTracker> trackers;

        private Dictionary<string, List<IQuestTracker>> trackerDict;

        public QuestManager()
        {
            SceneManager.sceneLoaded += PublishTrackedQuestData;
            trackers = new List<IQuestTracker>();
            trackerDict = new Dictionary<string, List<IQuestTracker>>();
        }

        public void AddTracker(IQuestTracker tracker)
        {
            trackers.Add(tracker);
        }

        public void RemoveTracker(IQuestTracker tracker)
        {
            trackers.Remove(tracker);
        }

        public List<IQuestTracker> GetTrackers(string sceneName)
        {
            if (trackerDict.TryGetValue(sceneName, out List<IQuestTracker> trackers))
            {
                Debug.Log("Accepted quests in " + sceneName + " scene: ");
                trackers.Map(_ => Debug.Log(_.QuestName));
                return trackers;
            }

            Debug.Log("No accepted quest in scene: " + sceneName);
            return default;
        }

        public void AddTracker(string sceneName, IQuestTracker tracker)
        {
            if (trackerDict.TryGetValue(sceneName, out List<IQuestTracker> trackers))
            {
                trackers.Add(tracker);
                return;
            }

            trackerDict.Add(sceneName, new List<IQuestTracker>{ tracker });
        }

        public void RemoveTracker(string sceneName, IQuestTracker tracker)
        {
            if (trackerDict.TryGetValue(sceneName, out List<IQuestTracker> trackers))
            {
                trackers.Remove(tracker);
            }
        }

        private void PublishTrackedQuestData(Scene scene, LoadSceneMode mode)
        {
            var trackers = GetTrackers(scene.name);

            if (trackers == null) return;
            EventManager.Instance.TriggerEvent(new TrackedQuestsData(trackers));           
        }
    }
}
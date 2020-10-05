using UnityEngine;

public class Loader : MonoBehaviour
{
    #region SINGLETON
    private static Loader instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        ServiceLocator.Register<RPG.Quest.IQuestManager>(new RPG.Quest.QuestManager());
    }
}

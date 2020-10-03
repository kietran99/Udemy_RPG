using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance areaEntrance;

    void Start()
    {
        areaEntrance.AreaTransitionName = areaTransitionName;        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        UIFade.Instance.FadeToBlack();
        UIFade.Instance.OnFadeComplete += LoadSceneAfterFaded;

        GameManager.Instance.FadingBetweenAreas = true;

        PlayerController.Instance.AreaTransitionName = areaTransitionName;
    }

    public void LoadSceneAfterFaded()
    {
        SceneManager.LoadScene(areaToLoad);        
        UIFade.Instance.FadeFromBlack();
        UIFade.Instance.OnFadeComplete -= LoadSceneAfterFaded;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance areaEntrance;

    [SerializeField]
    private GameObject fadeScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        areaEntrance.AreaTransitionName = areaTransitionName;
        UIFade.OnFadeComplete += LoadSceneAfterFaded;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Instantiate(fadeScreen).GetComponent<UIFade>().FadeToBlack();

        GameManager.Instance.fadingBetweenAreas = true;

        PlayerController.Instance.AreaTransitionName = areaTransitionName;
    }

    public void LoadSceneAfterFaded()
    {
        SceneManager.LoadScene(areaToLoad);
    }
}

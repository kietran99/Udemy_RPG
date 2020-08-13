using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string AreaTransitionName { set { areaTransitionName = value; } }

    [SerializeField]
    private string fadeScreenTag;

    [SerializeField]
    private string areaTransitionName;

    private UIFade fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (areaTransitionName.Equals(PlayerController.Instance.AreaTransitionName))
        {
            PlayerController.Instance.transform.position = GetComponent<BoxCollider2D>().bounds.center;
        }
       
        fadeScreen = FindObjectOfType<UIFade>();
        if (fadeScreen != null) fadeScreen.FadeFromBlack();

        GameManager.Instance.fadingBetweenAreas = false;
    }

    public void DestroyFadeScreenAfterFaded()
    {
        Destroy(fadeScreen.gameObject);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour, UIFade.IFade
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {           
            Instantiate(fadeScreen).GetComponent<UIFade>().FadeToBlack(this);

            GameManager.Instance.fadingBetweenAreas = true;

            PlayerController.Instance.AreaTransitionName = areaTransitionName;
        }
    }

    public void OnCompleted()
    {
        SceneManager.LoadScene(areaToLoad);
    }
}

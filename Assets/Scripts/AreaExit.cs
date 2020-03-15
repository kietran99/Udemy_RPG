using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour, UIFade.IFade
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance areaEntrance;

    // Start is called before the first frame update
    void Start()
    {
        areaEntrance.areaTransitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIFade.instance.FadeToBlack(this);

            GameManager.Instance.fadingBetweenAreas = true;

            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

    public void OnCompleted()
    {
        SceneManager.LoadScene(areaToLoad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour, UIFade.IFade
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
        if (fadeScreen != null) fadeScreen.FadeFromBlack(this);

        GameManager.Instance.fadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCompleted()
    {
        Destroy(fadeScreen.gameObject);
    }
}

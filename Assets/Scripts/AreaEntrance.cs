using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour, UIFade.IFade
{
    public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (areaTransitionName.Equals(PlayerController.instance.areaTransitionName))
        {
            PlayerController.instance.transform.position = transform.position;
        }

        UIFade.instance.FadeFromBlack(this);

        GameManager.Instance.fadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCompleted()
    {

    }
}

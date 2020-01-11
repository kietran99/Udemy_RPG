using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject player, fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }

        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(fadeScreen).GetComponent<UIFade>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

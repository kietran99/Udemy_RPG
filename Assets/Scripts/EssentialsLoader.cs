using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject fadeScreen;
    public GameObject gameManager;

    [SerializeField]
    private GameObject itemManager = null;

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

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }

        if (ItemManager.Instance == null)
        {
            ItemManager.Instance = Instantiate(itemManager).GetComponent<ItemManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

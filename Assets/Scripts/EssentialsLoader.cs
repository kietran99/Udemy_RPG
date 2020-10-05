using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject player            = null,
                       fadeScreen        = null,
                       gameMenuCanvas    = null,
                       dialogCanvas      = null,
                       gameManager       = null, 
                       itemManager       = null;

    void Start()
    {
        if (PlayerController.Instance == null) PlayerController.Instance = Instantiate(player).GetComponent<PlayerController>();

        if (UIFade.Instance == null) UIFade.Instance = Instantiate(fadeScreen).GetComponent<UIFade>();

        Instantiate(gameMenuCanvas);

        if (DialogManager.Instance == null) DialogManager.Instance = Instantiate(dialogCanvas).GetComponent<DialogManager>();

        if (GameManager.Instance == null) GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();

        if (ItemManager.Instance == null) ItemManager.Instance = Instantiate(itemManager).GetComponent<ItemManager>();
    }    
}

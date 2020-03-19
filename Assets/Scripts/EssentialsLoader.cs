using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField]
    public GameObject player            = null,
                      gameMenuCanvas    = null,
                      dialogCanvas      = null,
                      gameManager       = null, 
                      itemManager       = null;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.Instance == null) PlayerController.Instance = Instantiate(player).GetComponent<PlayerController>(); 

        Instantiate(gameMenuCanvas);

        if (DialogManager.Instance == null) DialogManager.Instance = Instantiate(dialogCanvas).GetComponent<DialogManager>();

        if (GameManager.Instance == null) GameManager.Instance = Instantiate(gameManager).GetComponent<GameManager>();

        if (ItemManager.Instance == null) ItemManager.Instance = Instantiate(itemManager).GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

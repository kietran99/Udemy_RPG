using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const int NUM_OF_CHARS = 5;

    [SerializeField]
    private CharStats[] playerStats = null;
    public CharStats[] PlayerStats { get { return playerStats; } }

    [HideInInspector]
    public bool fadingBetweenAreas, dialogActive, openingGameMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingBetweenAreas || dialogActive || openingGameMenu) PlayerController.instance.canMove = false;
        else PlayerController.instance.canMove = true;
    }

    public CharStats GetCharacter(int pos)
    {
        return playerStats[pos];
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CharStatChange : MonoBehaviour
{
    [SerializeField]
    private GameObject statChange = null;

    [SerializeField]
    private GameObject alreadyEquipped = null;

    [SerializeField]
    private Text characterName = null, currStatText = null, postStatText = null, alreadyEquippedText = null;

    //[SerializeField]
    [ColorUsage(true)]
    public Color statIncColor, statDecColor, statUnchangeColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayChangeStat(string charName, int currStat, int postStat)
    {
        ToggleStatDisplay(true);
        characterName.text = charName;
        currStatText.text = currStat.ToString();
        postStatText.text = postStat.ToString();
        
        if (currStat < postStat)
        {
            currStatText.color = statIncColor;
            postStatText.color = statIncColor;
        }
        else if (currStat > postStat)
        {
            currStatText.color = statDecColor;
            postStatText.color = statDecColor;
        }
        else
        {
            currStatText.color = statUnchangeColor;
            postStatText.color = statUnchangeColor;
        }
    }

    public void DisplayUnchangeStat(string charName, int currStat)
    {
        ToggleStatDisplay(false);
        characterName.text = charName;
        alreadyEquippedText.text = "E " + currStat;
        alreadyEquippedText.color = statUnchangeColor;
    }

    private void ToggleStatDisplay(bool change)
    {
        statChange.SetActive(change);
        alreadyEquipped.SetActive(!change);
    }
}

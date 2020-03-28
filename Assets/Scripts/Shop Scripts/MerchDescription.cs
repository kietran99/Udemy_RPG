using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchDescription : MonoBehaviour
{
    [SerializeField]
    private Text changeStatText = null;

    private CharStatChange[] statChanges;

    // Start is called before the first frame update
    void Start()
    {
        InitStatChangeList();
    }

    private void InitStatChangeList()
    {
        statChanges = new CharStatChange[GameManager.Instance.GetNumOfActives()];
    }

    public void UpdateDesc(Item merch)
    {
        changeStatText.text = ((Equipment)merch).GetStatBoostName();
    }
}

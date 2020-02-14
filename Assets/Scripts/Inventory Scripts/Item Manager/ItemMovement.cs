using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMovement : MonoBehaviour
{
    public interface ISlotSelected
    {
        
    }

    [SerializeField]
    private GameObject promptText = null, amountSelector = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        promptText.SetActive(true);
        amountSelector.SetActive(false);
    }

    void OnDisable()
    {
        promptText.SetActive(false);
        amountSelector.SetActive(false);
    }
    
    public void OnSlotSelected(int pos)
    {
        //amountSelector.GetComponent<AmountSelector>().SetEnabler()
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
    [SerializeField]
    private float secsToWait = 1f;

    [SerializeField]
    private GameObject dialogCanvas = null;

    [SerializeField]
    private Text dialogText = null;

    [SerializeField]
    private string[] greetings = null;

    [SerializeField]
    private string[] buyDialogs = null;

    [SerializeField]
    private string insufficientFundDialog = null;

    [SerializeField]
    private string unavailSlotDialog = null;

    [SerializeField]
    private string[] tradeSuccessfulDialog = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Greetings()
    {
        DisplayDialog(RandomizeSpeech(greetings));
    }

    public void Buy()
    {
        DisplayDialog(RandomizeSpeech(buyDialogs));
    }

    //-----------When buying/selling items----------------------------------------
    public void InsufficientFund()
    {
        dialogCanvas.SetActive(true);
        StartCoroutine("DisplayDelayDialog", insufficientFundDialog);
    }

    public void UnavailSlot()
    {
        dialogCanvas.SetActive(true);
        StartCoroutine("DisplayDelayDialog", unavailSlotDialog);
    }

    public void TradeSuccessful()
    {
        dialogCanvas.SetActive(true);
        StartCoroutine("DisplayDelayDialog", RandomizeSpeech(tradeSuccessfulDialog));
    }
    //-------------------------------------------------------------------

    private string RandomizeSpeech(string[] texts)
    {
        return texts[Random.Range(0, texts.Length)];
    }

    private void DisplayDialog(string textToDisplay)
    {
        dialogCanvas.SetActive(true);
        dialogText.text = textToDisplay;
    }

    private IEnumerator DisplayDelayDialog(string textToDisplay)
    {
        dialogText.text = textToDisplay;
        yield return new WaitForSeconds(secsToWait);
        Disable();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

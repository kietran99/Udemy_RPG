using System.Collections;
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
        StartCoroutine(nameof(DisplayDelayDialog), insufficientFundDialog);
    }

    public void UnavailSlot()
    {
        dialogCanvas.SetActive(true);
        StartCoroutine(nameof(DisplayDelayDialog), unavailSlotDialog);
    }

    public void TradeSuccessful()
    {
        dialogCanvas.SetActive(true);
        StartCoroutine(nameof(DisplayDelayDialog), RandomizeSpeech(tradeSuccessfulDialog));
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

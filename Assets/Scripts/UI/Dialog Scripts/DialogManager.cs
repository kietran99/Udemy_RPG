using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    #region
    public static DialogManager Instance { get { return instance; } set { instance = value; } }
    public GameObject DialogBox { get { return dialogBox; } }
    #endregion

    private static DialogManager instance;

    [SerializeField]
    private GameObject dialogBox = null, nameBox = null;

    [SerializeField]
    private Text dialogText = null, nameText = null;

    private string[] dialogLines;

    private int currentLine;

    // Make each line be typed more fluidly
    [SerializeField]
    private WaitForSeconds charWaitTime = new WaitForSeconds(0.02f);
    private bool isTyping = false;

    // Seconds to next dialog display since closing current dialog
    [SerializeField]
    private const float dialogDelay = 1f;
    private float secsToNextSentence;

    void Awake()
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

    void Update()
    {
        if (secsToNextSentence >= 0f)
        {
            secsToNextSentence -= Time.deltaTime;
            return;
        }

        if (!dialogBox.activeInHierarchy) return;

        if (!Input.GetKeyDown(KeyboardControl.General.Interact)) return;

        if (isTyping)
        {
            isTyping = false;
            PrintWholeSentence();
            return;
        }

        if (++currentLine < dialogLines.Length)
        {
            StartCoroutine(TypeSentence());
            return;
        }

        dialogBox.SetActive(false);
        GameManager.Instance.DialogActive = false;
        secsToNextSentence = dialogDelay;
    }

    private void PrintWholeSentence()
    {
        StopCoroutine(TypeSentence());
        dialogText.text = dialogLines[currentLine];
    }

    public bool CanLoadNextSentence() => secsToNextSentence <= 0f;

    private IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in dialogLines[currentLine].ToCharArray())
        {
            dialogText.text += letter;
            yield return charWaitTime;
        }

        isTyping = false;
    }

    public void InitDialog(string speaker, string[] dialogLines, bool isPerson)
    {
        GameManager.Instance.DialogActive = true;

        this.dialogLines = dialogLines;

        currentLine = 0;
        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        if (isPerson)
        {
            nameBox.SetActive(true);
            nameText.text = speaker;
        }
        else
        {
            nameBox.SetActive(false);
        }
    }
}

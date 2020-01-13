using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogBox, nameBox;
    public Text dialogText, nameText;

    public string[] dialogLines;

    public int currentLine;
    private const float charWaitTime = 0.02f;
    private bool isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy && Input.GetKeyUp(KeyCode.Space))
        {    
            // if a dialog sentence is being typed, stop the current typing then show the whole sentence
            if (isTyping)
            {
                StopCoroutine("TypeSentence");
                isTyping = false;
                dialogText.text = dialogLines[currentLine];
                return;
            }

            // else move to next line of dialog
            currentLine++;

            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                StartCoroutine("TypeSentence");
            }
        }
    }

    public IEnumerator TypeSentence()
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in dialogLines[currentLine].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(charWaitTime);
        }

        isTyping = false;
    }

    public void InitDialog(string speaker, string[] dialogLines, bool isPerson)
    {
        this.dialogLines = dialogLines;

        currentLine = 0;
        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(true);

        if (isPerson)
        {
            nameText.text = speaker;
        }
        else
        {
            nameBox.SetActive(false);
        }
    }
}

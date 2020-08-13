using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] 
    private string speaker = "";

    [SerializeField]
    private string[] dialogLines = null;

    [SerializeField]
    private bool isPerson = true;

    private bool canActivate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canActivate && 
            !DialogManager.Instance.DialogBox.activeInHierarchy &&
            DialogManager.Instance.CanLoadNextSentence())
            DialogManager.Instance.InitDialog(speaker, dialogLines, isPerson); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canActivate = false;
    }
}

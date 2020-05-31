using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

public class LongClickInvoker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool mouseHold;
    private float ptrDownTimer = 0f;

    [SerializeField]
    private float requiredHoldTime = 0f;

    [SerializeField]
    private UnityEvent onLongClick = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        mouseHold = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseHold)
        {
            ptrDownTimer += Time.deltaTime;

            if (ptrDownTimer >= requiredHoldTime)
            {
                if (onLongClick != null)
                onLongClick.Invoke();

                Reset();
            }
        }
    }

    private void Reset()
    {
        ptrDownTimer = 0f;
    }
}

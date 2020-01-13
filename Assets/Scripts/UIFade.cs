using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public interface IFade
    {
        void OnCompleted();
    }

    public static UIFade instance;

    private IFade fadeCaller;

    public Image fadeScreen;

    public float fadeSpeed;

    private bool shouldFadeToBlack, shouldFadeFromBlack;

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
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
                                        Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
                fadeCaller.OnCompleted();
            }

            return;
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                                        Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
                fadeCaller.OnCompleted();
            }
        }
    }

    public void FadeToBlack(IFade fadeCaller)
    {
        this.fadeCaller = fadeCaller;
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack(IFade fadeCaller)
    {
        this.fadeCaller = fadeCaller;
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}

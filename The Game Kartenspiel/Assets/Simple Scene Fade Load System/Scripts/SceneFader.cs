using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : Fader
{
    public override float FadeDamp { get; set; }
    public override float LastTime { get; set; }
    public override float Alpha { get; set; }
    public override bool IsFadeIn { get; set; }
    public override bool Start { get; set; }
    public override bool StartedLoading { get; set; }
    public override string FadeScene { get; set; }
    public override Image BackgroundImage {get; set;}
    public override Color FadeColor { get; set; }
    public override CanvasGroup MyCanvas { get; set; }

    public override void StartFading()
    {
        DontDestroyOnLoad(gameObject);

        if (transform.GetComponent<CanvasGroup>())
            MyCanvas = transform.GetComponent<CanvasGroup>();

        if (transform.GetComponentInChildren<Image>())
        {
            BackgroundImage = transform.GetComponent<Image>();
            BackgroundImage.color = FadeColor;
        }
        
        if (MyCanvas && BackgroundImage)
        {
            MyCanvas.alpha = 0.0f;
            MyCanvas.GetComponent<Canvas>().sortingOrder = 1;
            StartCoroutine(FadeIt());
        }
        else
        {
            Debug.LogWarning("Canvas or Backgroundimage missing.");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public override IEnumerator FadeIt()
    {
        while (!Start)
        {
            yield return null;
        }
        
        LastTime = Time.time;
        float coDelta = LastTime;
        bool hasFadedIn = false;

        while (!hasFadedIn)
        {
            coDelta = Time.time - LastTime;
            if (!IsFadeIn)
            {
                Alpha = newAlpha(coDelta, 1, Alpha);
                if (Alpha == 1 && !StartedLoading)
                {
                    StartedLoading = true;
                    SceneManager.LoadScene(FadeScene);
                }
            }
            else
            {
                Alpha = newAlpha(coDelta, 0, Alpha);
                if (Alpha == 0)
                {
                    hasFadedIn = true;
                }
            }
            
            LastTime = Time.time;
            MyCanvas.alpha = Alpha;
            yield return null;
        }

        FadeItAll.DoneFading();

        Destroy(gameObject);

        yield return null;
    }

    float newAlpha(float delta, int to, float currAlpha)
    {
        switch (to)
        {
            case 0:
            {
                currAlpha -= FadeDamp * delta;
                if (currAlpha <= 0)
                    currAlpha = 0;
                break;
            }
            case 1:
            {
                currAlpha += FadeDamp * delta;
                if (currAlpha >= 1)
                    currAlpha = 1;
                break;
            }
        }

        return currAlpha;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIt());
        IsFadeIn = true;
    }
}

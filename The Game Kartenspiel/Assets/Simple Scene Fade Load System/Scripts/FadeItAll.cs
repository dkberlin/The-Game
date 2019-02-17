using UnityEngine;
using UnityEngine.UI;

public static class FadeItAll
{
    private static bool _isFadingScenes;
    private static bool _isFadingMusic;
    private static bool _isFadingCanvasGroup;

    public static void FadeSceneChange(string scene, Color col, float multiplier)
    {
        if (_isFadingScenes)
        {
            Debug.Log("Already Fading");
            return;
        }

        GameObject init = new GameObject();
        init.name = "SceneFader";
        Canvas myCanvas = init.AddComponent<Canvas>();
        var sceneFader = init.AddComponent<SceneFader>();
        
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        init.AddComponent<CanvasGroup>();
        init.AddComponent<Image>();

        sceneFader.FadeDamp = multiplier;
        sceneFader.FadeScene = scene;
        sceneFader.FadeColor = col;
        sceneFader.Start = true;
        _isFadingScenes = true;
        sceneFader.StartFading();
    }
    
    public static void FadeAudio(AudioSource source, float multiplier, bool shouldFadeIn)
    {
        // optional
//        if (_isFadingMusic)
//        {
//            Debug.Log("Already Fading");
//            return;
//        }
        
        GameObject init = new GameObject();
        init.name = "AudioFader";
        var audioFader = init.AddComponent<AudioFader>();
        
        audioFader.FadeDamp = multiplier;
        audioFader.MyAudioSource = source;
        audioFader.Start = true;
        // optional
//        _isFadingMusic = true;    
        audioFader.IsFadeIn = shouldFadeIn;
        audioFader.StartFading();
    }
   
    public static void FadeCanvasGroup(CanvasGroup group, float multiplier, bool shouldFadeIn)
    {
        // optional
//        if (_isFadingCanvasGroup)
//        {
//            Debug.Log("Already Fading");
//            return;
//        }
        
        GameObject init = new GameObject();
        init.name = "CanvasFader";
        var canvasFader = init.AddComponent<CanvasGroupFader>();
        
        canvasFader.MyCanvas = group;
        canvasFader.FadeDamp = multiplier;
        canvasFader.Start = true;
        // optional
//        _isFadingCanvasGroup = true;
        canvasFader.IsFadeIn = shouldFadeIn;
        canvasFader.StartFading();
    }
    
    public static void DoneFading() {
        _isFadingScenes = false;
        _isFadingCanvasGroup = false;
        _isFadingMusic = false;
    }
}

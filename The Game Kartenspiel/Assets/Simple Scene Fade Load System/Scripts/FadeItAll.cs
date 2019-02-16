using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
public static class FadeItAll
{
    static bool areWeFadingScenes = false;
    static bool areWeFadingMusic = false;
    static bool areWeFadingCanvas = false;

    //Create Fader object and assing the fade scripts and assign all the variables
    public static void FadeSceneChange(string scene, Color col, float multiplier)
    {
        if (areWeFadingScenes)
        {
            Debug.Log("Already Fading");
            return;
        }

        GameObject init = new GameObject();
        init.name = "SceneFader";
        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        init.AddComponent<CanvasGroup>();
        init.AddComponent<Image>();

        var sceneFader = init.AddComponent<SceneFader>();

        sceneFader.fadeDamp = multiplier;
        sceneFader.fadeScene = scene;
        sceneFader.fadeColor = col;
        sceneFader.start = true;
        areWeFadingScenes = true;
        sceneFader.StartFading();
    }

    public static void DoneFading() {
        areWeFadingScenes = false;
        areWeFadingCanvas = false;
        areWeFadingMusic = false;
    }
    
    public static void FadeAudio(AudioSource source, float multiplier, bool shouldFadeIn)
    {
        if (areWeFadingMusic)
        {
            Debug.Log("Already Fading");
            return;
        }
        
        GameObject init = new GameObject();
        init.name = "AudioFader";
        
        var audioFader = init.AddComponent<AudioFader>();
        
        audioFader.fadeDamp = multiplier;
        audioFader.myAudioSource = source;
        audioFader.start = true;
        areWeFadingMusic = true;
        audioFader.isFadeIn = shouldFadeIn;
        audioFader.StartFading();
    }
   
    public static void FadeCanvasGroup(CanvasGroup group, float multiplier, bool shouldFadeIn)
    {
        GameObject init = new GameObject();
        init.name = "CanvasFader";
        var audioFader = init.AddComponent<CanvasGroupFader>();
        
        audioFader.fadeDamp = multiplier;
        audioFader.start = true;
        areWeFadingCanvas = true;
        audioFader.StartFading();
        audioFader.isFadeIn = shouldFadeIn;
    }
}

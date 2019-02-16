using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Fader : MonoBehaviour
{
    public abstract bool start {get; set;} 
    public abstract float fadeDamp {get; set;}
    public abstract bool isFadeIn {get; set;}
    public abstract float lastTime {get; set;}
    public abstract bool startedLoading {get; set; }
    public abstract void StartFading();
    public abstract IEnumerator FadeIt();

    public virtual string fadeScene {get;set;}
    public virtual float alpha {get; set;}
    public virtual float volume {get; set;}
    public virtual Color fadeColor {get; set;}
    public virtual CanvasGroup myCanvas {get; set;}
    public virtual Image bg {get; set;}
    public virtual AudioSource myAudioSource {get; set;}
}

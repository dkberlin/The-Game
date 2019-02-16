using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Fader : MonoBehaviour
{
    public abstract bool Start {get; set;} 
    public abstract bool IsFadeIn {get; set;}
    public abstract bool StartedLoading {get; set; }
    public abstract float FadeDamp {get; set;}
    public abstract float LastTime {get; set;}
    public abstract void StartFading();
    public abstract IEnumerator FadeIt();

    public virtual string FadeScene {get;set;}
    public virtual float Alpha {get; set;}
    public virtual float MyAlpha {get; set;}
    public virtual float MyVolume {get; set;}
    public virtual Color FadeColor {get; set;}
    public virtual CanvasGroup MyCanvas {get; set;}
    public virtual Image BackgroundImage {get; set;}
    public virtual AudioSource MyAudioSource {get; set;}
}

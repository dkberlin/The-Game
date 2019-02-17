using UnityEngine;

public class MusicHandler : MonoBehaviour {

	public AudioSource myAudioSource;
	
	void Start () {
		FadeItAll.FadeAudio(myAudioSource, 1, true);
	}
	
	public void TriggerMusic()
	{
		FadeItAll.FadeAudio(myAudioSource,3,false);
	}
}

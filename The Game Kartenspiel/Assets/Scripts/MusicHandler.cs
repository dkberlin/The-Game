using UnityEngine;

public class MusicHandler : MonoBehaviour {

	public AudioSource myAudioSource;
	
	void Start () {
		FadeItAll.FadeAudio(myAudioSource, 1, true);
	}
}

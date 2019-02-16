using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour {

	public AudioSource myAudioSource;
	// Use this for initialization
	void Start () {
		FadeItAll.FadeAudio(myAudioSource, 1, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

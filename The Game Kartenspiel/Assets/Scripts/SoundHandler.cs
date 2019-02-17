using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour 
{
	public AudioSource audioSource;
	public AudioClip cardDeclineClip;

	private void Start()
	{
		GameEvents.OnCardDropDeclined += PlayDeclineSound;
	}

	private void OnDestroy()
	{
		GameEvents.OnCardDropDeclined -= PlayDeclineSound;
	}

	public void PlayDeclineSound()
	{
		audioSource.clip = cardDeclineClip;
		audioSource.Play();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour 
{
	public AudioSource audioSource;
	public AudioClip cardDeclineClip;

	public void PlayDeclineSound()
	{
		audioSource.clip = cardDeclineClip;
		audioSource.Play();
	}
}

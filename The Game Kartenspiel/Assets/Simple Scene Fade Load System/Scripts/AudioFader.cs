using System.Collections;
using UnityEngine;

public class AudioFader : Fader 
{
	public override float FadeDamp { get; set; }
	public override float LastTime { get; set; }
	public override bool Start { get; set; }
	public override bool IsFadeIn { get; set; }
	public override bool StartedLoading { get; set; }
	public override AudioSource MyAudioSource { get; set; }

	public override void StartFading()
	{
		if (MyAudioSource)
		{
			StartCoroutine(FadeIt());
		}
		else
		{
			Debug.LogWarning("Audiosource Missing.");
		}
	}

	public override IEnumerator FadeIt()
	{
		while (!Start)
		{
			yield return null;
		}
		
		LastTime = Time.time;
		float coDelta = LastTime;
		bool hasFaded = false;

		while (!hasFaded)
		{
			coDelta = Time.time - LastTime;
			if (IsFadeIn)
			{
				MyVolume = NewVolume(coDelta, 1, MyVolume);
				
				if (MyVolume == 1)
				{
					hasFaded = true;
				}
			}
			else
			{
				MyVolume = NewVolume(coDelta, 0, MyAudioSource.volume);
				
				if (MyVolume == 0)
				{
					hasFaded = true;
				}
			}
			
			LastTime = Time.time;
			MyAudioSource.volume = MyVolume;
			
			yield return null;
		}
		
		FadeItAll.DoneFading();

		Destroy(gameObject);

		yield return null;
	}
	
	float NewVolume(float delta, int to, float currVol)
	{
		switch (to)
		{
			case 0:
				currVol -= FadeDamp * delta;
				if (currVol <= 0)
					currVol = 0;

				break;
			case 1:
				currVol += FadeDamp * delta;
				if (currVol >= 1)
					currVol = 1;

				break;
		}

		return currVol;
	}
}
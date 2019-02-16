using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFader : Fader 
{
	public override bool start { get; set; }
	public override float fadeDamp { get; set; }
	public override bool isFadeIn { get; set; }
	public override float lastTime { get; set; }
	public override bool startedLoading { get; set; }
	public override AudioSource myAudioSource { get; set; }

	public override void StartFading()
	{
		DontDestroyOnLoad(gameObject);

		//Checking and starting the coroutine
		if (myAudioSource)
		{
			StartCoroutine(FadeIt());
		}
		else
			Debug.LogWarning("Something is missing please reimport the package.");
	}

	public override IEnumerator FadeIt()
	{
		while (!start)
		{
			//waiting to start
			yield return null;
		}
		lastTime = Time.time;
		float coDelta = lastTime;
		bool hasFaded = false;

		while (!hasFaded)
		{
			coDelta = Time.time - lastTime;
			if (isFadeIn)
			{
				//Fade in
				volume = NewVolume(coDelta, 1, volume);
				if (volume == 1)
				{
					hasFaded = true;
				}
			}
			else
			{
				//Fade out
				volume = NewVolume(coDelta, 0, myAudioSource.volume);
				
				if (volume == 0)
				{
					hasFaded = true;
				}

			}
			lastTime = Time.time;
			myAudioSource.volume = volume;
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
				currVol -= fadeDamp * delta;
				if (currVol <= 0)
					currVol = 0;

				break;
			case 1:
				currVol += fadeDamp * delta;
				if (currVol >= 1)
					currVol = 1;

				break;
		}

		return currVol;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFader : Fader 
{
	public override float LastTime { get; set; }
	public override float FadeDamp { get; set; }
	public override bool Start { get; set; }
	public override bool IsFadeIn { get; set; }
	public override bool StartedLoading { get; set; }
	public override CanvasGroup MyCanvas { get; set; }

	public override void StartFading()
	{
		if (MyCanvas)
		{
			StartCoroutine(FadeIt());
		}
		else
		{
			Debug.LogWarning("Canvasgroup Missing.");
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
				MyAlpha = NewAlpha(coDelta, 1, MyAlpha);
				
				if (MyAlpha == 1)
				{
					hasFaded = true;
					MyCanvas.interactable = true;
				}
			}
			else
			{
				MyAlpha = NewAlpha(coDelta, 0, MyCanvas.alpha);
				
				if (MyAlpha == 0)
				{
					hasFaded = true;
					MyCanvas.interactable = false;
				}
			}
			
			LastTime = Time.time;
			MyCanvas.alpha = MyAlpha;
			
			yield return null;
		}
		
		FadeItAll.DoneFading();

		Destroy(gameObject);

		yield return null;
	}
	
	float NewAlpha(float delta, int to, float currAlpha)
	{
		switch (to)
		{
			case 0:
				currAlpha -= FadeDamp * delta;
				if (currAlpha <= 0)
					currAlpha = 0;

				break;
			case 1:
				currAlpha += FadeDamp * delta;
				if (currAlpha >= 1)
					currAlpha = 1;

				break;
		}

		return currAlpha;
	}
}

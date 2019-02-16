using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFader : Fader 
{
	public override bool start { get; set; }
	public override float fadeDamp { get; set; }
	public override bool isFadeIn { get; set; }
	public override float lastTime { get; set; }
	public override bool startedLoading { get; set; }

	public override void StartFading()
	{
		throw new System.NotImplementedException();
	}

	public override IEnumerator FadeIt()
	{
		throw new System.NotImplementedException();
	}
}

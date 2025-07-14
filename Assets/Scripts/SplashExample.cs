using UnityEngine;

public class SplashExample : SplashModelBase
{
	public UISprite Logo1;

	public UISprite Logo2;

	public float FadeTime = 2f;

	private void Start()
	{
		StartCoroutine(StartSplash(FadeTime, Logo1, Logo2));
	}

	protected internal override void OnSplashFinish()
	{
		UnityEngine.Debug.Log("Go to Menu");
	}
}

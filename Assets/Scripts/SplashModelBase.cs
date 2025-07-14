using System.Collections;
using UnityEngine;

public class SplashModelBase : MonoBehaviour
{
	protected internal void HideSprites(UISprite sprite1, UISprite sprite2)
	{
		if ((bool)sprite1)
		{
			sprite1.alpha = 0f;
		}
		if ((bool)sprite2)
		{
			sprite2.alpha = 0f;
		}
		else
		{
			UnityEngine.Debug.LogWarning("Some sprites missing!");
		}
	}

	protected internal IEnumerator FadeTo(UISprite sprite, float aValue, float aTime)
	{
		if ((bool)sprite)
		{
			Color color = sprite.color;
			float alpha = color.a;
			yield return new WaitForSeconds(2f);
			for (float f = 0f; f < 1f; f += Time.deltaTime / aTime)
			{
				Color newColor = sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(alpha, aValue, f));
				yield return null;
			}
		}
	}

	protected internal IEnumerator StartSplash(float fadeTime, UISprite logo1, UISprite logo2)
	{
		HideSprites(logo1, logo2);
		yield return StartCoroutine(FadeTo(logo1, 1f, fadeTime));
		yield return StartCoroutine(FadeTo(logo1, 0f, fadeTime));
		yield return StartCoroutine(FadeTo(logo2, 1f, fadeTime));
		yield return StartCoroutine(FadeTo(logo2, 0f, fadeTime));
		yield return StartCoroutine("OnSplashFinish");
	}

	protected internal virtual void OnSplashFinish()
	{
	}
}

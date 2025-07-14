using System.Collections;
using UnityEngine;

public class Shadow : MonoBehaviour
{
	public enum ShadowType
	{
		None,
		FixedSize,
		SmartSize
	}

	public float size;

	public Transform shadow;

	public ShadowType shadowType;

	public void Set(float size, ShadowType shadowType)
	{
		this.shadowType = shadowType;
		this.size = size;
	}

	private void OnEnable()
	{
		if (!ShadowPool.IsStarted)
		{
			StartCoroutine(checkStarted());
		}
		else if (size == 0f)
		{
			StartCoroutine(checkEnabled());
		}
		else
		{
			shadow = ShadowPool.instance.CastShadow(base.transform, size, shadowType);
		}
	}

	private IEnumerator checkEnabled()
	{
		while (size == 0f)
		{
			yield return new WaitForFixedUpdate();
		}
		shadow = ShadowPool.instance.CastShadow(base.transform, size, shadowType);
	}

	private IEnumerator checkStarted()
	{
		while (!ShadowPool.IsStarted)
		{
			yield return new WaitForFixedUpdate();
		}
		shadow = ShadowPool.instance.CastShadow(base.transform, size, shadowType);
	}
}

using System.Collections;
using UnityEngine;

public class FortuneHandTutorial : MonoBehaviour
{
	public Vector2 Start_pos;

	public Vector2 Mid_pos;

	public Vector2 End_pos;

	private IEnumerator animEnum;

	private void OnEnable()
	{
		animEnum = Move();
		StartCoroutine(animEnum);
	}

	private void OnDisable()
	{
		if (animEnum != null)
		{
			StopCoroutine(animEnum);
		}
		animEnum = null;
	}

	private IEnumerator Move()
	{
		float t = 0f;
		UISprite s = base.gameObject.GetComponent<UISprite>();
		float min = 0.2f;
		float max = 0.8f;
		while (true)
		{
			if (!(t < 1f))
			{
				s.alpha = 0f;
				while (t > 0f)
				{
					t -= Time.unscaledDeltaTime;
					yield return null;
				}
				continue;
			}
			base.transform.localPosition = Bezier2(Start_pos, Mid_pos, End_pos, t);
			if (t < min)
			{
				s.alpha = t * 5f;
			}
			if (t > max)
			{
				s.alpha = (1f - t) * 5f;
			}
			t += Time.unscaledDeltaTime / 2f;
			yield return null;
		}
	}

	private Vector2 Bezier2(Vector2 Start, Vector2 Control, Vector2 End, float t)
	{
		return (1f - t) * (1f - t) * Start + 2f * t * (1f - t) * Control + t * t * End;
	}

	private Vector3 Bezier2(Vector3 Start, Vector3 Control, Vector3 End, float t)
	{
		return (1f - t) * (1f - t) * Start + 2f * t * (1f - t) * Control + t * t * End;
	}

	private Vector2 Bezier3(Vector2 s, Vector2 st, Vector2 et, Vector2 e, float t)
	{
		return (((-s + 3f * (st - et) + e) * t + (3f * (s + et) - 6f * st)) * t + 3f * (st - s)) * t + s;
	}

	private Vector3 Bezier3(Vector3 s, Vector3 st, Vector3 et, Vector3 e, float t)
	{
		return (((-s + 3f * (st - et) + e) * t + (3f * (s + et) - 6f * st)) * t + 3f * (st - s)) * t + s;
	}
}

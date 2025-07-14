using System.Collections;
using UnityEngine;

public class MOVEDOWNUP : MonoBehaviour
{
	public float MaxPoY;

	public float zatrimka;

	public float speed = 1f;

	private float yTempMax;

	private float yTempMin;

	private Coroutine corut;

	private void OnEnable()
	{
		corut = StartCoroutine(move());
		Vector3 localPosition = base.transform.localPosition;
		yTempMax = localPosition.y;
		Vector3 localPosition2 = base.transform.localPosition;
		yTempMin = localPosition2.y - MaxPoY;
	}

	private IEnumerator move()
	{
		while (true)
		{
			Vector3 localPosition = base.transform.localPosition;
			if (localPosition.y < yTempMax)
			{
				base.transform.localPosition += Vector3.up * speed;
				yield return 0;
				continue;
			}
			while (true)
			{
				Vector3 localPosition2 = base.transform.localPosition;
				if (!(localPosition2.y > yTempMin))
				{
					break;
				}
				base.transform.localPosition += Vector3.down * speed;
				yield return 0;
			}
			yield return new WaitForSeconds(zatrimka);
		}
	}

	private void OnDisable()
	{
		Transform transform = base.transform;
		Vector3 localPosition = base.transform.localPosition;
		float x = localPosition.x;
		float y = yTempMax;
		Vector3 localPosition2 = base.transform.localPosition;
		transform.localPosition = new Vector3(x, y, localPosition2.z);
		if (corut != null)
		{
			StopCoroutine(corut);
		}
	}
}

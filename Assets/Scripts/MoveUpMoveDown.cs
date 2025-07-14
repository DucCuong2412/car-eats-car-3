using System.Collections;
using UnityEngine;

public class MoveUpMoveDown : MonoBehaviour
{
	public Vector3 Max;

	public Vector3 Min;

	public float zatrimka;

	public float speed = 1f;

	private void OnEnable()
	{
		StartCoroutine(move());
		base.transform.localPosition = Min;
	}

	private IEnumerator move()
	{
		while (true)
		{
			Vector3 localPosition = base.transform.localPosition;
			if (localPosition.y < Max.y)
			{
				if (Time.timeScale != 0f)
				{
					base.transform.localPosition += Vector3.up * speed;
				}
				yield return 0;
				continue;
			}
			while (true)
			{
				Vector3 localPosition2 = base.transform.localPosition;
				if (!(localPosition2.y > Min.y))
				{
					break;
				}
				if (Time.timeScale != 0f)
				{
					base.transform.localPosition += Vector3.down * speed;
				}
				yield return 0;
			}
			yield return new WaitForSeconds(zatrimka);
		}
	}
}

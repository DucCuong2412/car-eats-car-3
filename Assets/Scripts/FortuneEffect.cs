using System.Collections;
using UnityEngine;

public class FortuneEffect : MonoBehaviour
{
	public TweenRotation Effect;

	private bool isShow;

	public GameObject animMarker;

	public GameObject ButtonAnim;

	public GameObject ButtonNoAnim;

	private void OnEnable()
	{
		SetButton();
	}

	private void Update()
	{
		if (Progress.levels.tickets > 0)
		{
			animMarker.SetActive(value: true);
		}
		else
		{
			animMarker.SetActive(value: false);
		}
	}

	public void SetButton()
	{
		if (CheckTickets())
		{
			ButtonAnim.SetActive(value: true);
			ButtonNoAnim.SetActive(value: false);
			if (base.gameObject.activeSelf && base.gameObject.activeInHierarchy)
			{
				StartCoroutine(Anim());
			}
		}
		else
		{
			ButtonAnim.SetActive(value: false);
			ButtonNoAnim.SetActive(value: true);
		}
	}

	private bool CheckTickets()
	{
		if (Progress.levels.tickets > 0)
		{
			isShow = true;
		}
		else
		{
			isShow = false;
		}
		return isShow;
	}

	private IEnumerator Anim()
	{
		while (base.gameObject.activeSelf)
		{
			if ((bool)Effect)
			{
				if (isShow)
				{
					Effect.enabled = true;
					yield return Utilities.WaitForRealSeconds(0.6f);
					Effect.enabled = false;
					yield return Utilities.WaitForRealSeconds(1.5f);
				}
				else
				{
					Effect.enabled = false;
				}
			}
			yield return null;
		}
	}
}

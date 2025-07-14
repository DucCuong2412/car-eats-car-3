using System.Collections;
using UnityEngine;

public class ControllerMarkerEggs : MonoBehaviour
{
	public GameObject show;

	public Animator anim;

	private int is_ON = Animator.StringToHash("is_ON");

	private int is_EGGS = Animator.StringToHash("is_EGGS");

	private int is_EGGintroducted = Animator.StringToHash("is_EGGintroducted");

	public ScrollRectSnapLEXTry Scrolls;

	private void OnEnable()
	{
		if (!Progress.shop.showmarkerEggs && Progress.levels._packUnderground[1] != null && Progress.levels._packUnderground[1]._level[2].isOpen)
		{
			if (!Progress.shop.showmarkerEggs2)
			{
				StartCoroutine(WaitAndGo());
			}
			else if (Progress.shop.showmarkerEggs2)
			{
				StartCoroutine(WaitAndGo(shows: true));
			}
		}
	}

	private IEnumerator WaitAndGo(bool shows = false)
	{
		yield return new WaitForSeconds(2f);
		show.SetActive(value: true);
		anim.SetBool(is_ON, value: true);
		anim.SetBool(is_EGGS, value: true);
		anim.SetBool(is_EGGintroducted, shows);
		Progress.shop.showmarkerEggs2 = true;
	}

	public void Checks()
	{
		Progress.shop.showmarkerEggs = true;
		anim.SetBool(is_ON, value: false);
	}
}

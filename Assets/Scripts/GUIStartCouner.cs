using SmartLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIStartCouner : MonoBehaviour
{
	public Text Label;

	public Animation anim;

	private int currentlevel = -1;

	private static string str_timer_3_2_1 = "timer_3_2_1";

	private IEnumerator counting;

	public void StartCounting(int level, int from, Action callback)
	{
		if (counting != null)
		{
			StopCoroutine(counting);
		}
		counting = countDown(level, from, callback);
		StartCoroutine(counting);
	}

	private IEnumerator countDown(int level, int from, Action callback)
	{
		yield return new WaitForSeconds(0.25f);
		RaceLogic.instance.gui.interface_Controlls.topLeftBtn.SetActive(value: false);
		if (!Progress.shop.Tutorial)
		{
			if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
			{
				if (currentlevel != level)
				{
					Label.text = LanguageManager.Instance.GetTextValue("LEVEL *").Replace("*", level.ToString());
					anim.Play();
					yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
					currentlevel = level;
				}
			}
			else if (!Progress.shop.EsterLevelPlay)
			{
				if (Progress.shop.bossLevel)
				{
					if (currentlevel != level)
					{
						Label.text = LanguageManager.Instance.GetTextValue("BOSS LEVEL");
						anim.Play();
						yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
						currentlevel = level;
					}
				}
				else if (!Progress.shop.bossLevel && !Progress.shop.ArenaNew)
				{
					if (currentlevel != level)
					{
						Label.text = LanguageManager.Instance.GetTextValue("SPECIAL MISSION");
						anim.Play();
						yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
						currentlevel = level;
					}
				}
				else if (Progress.shop.ArenaNew && currentlevel != level)
				{
					Label.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", RaceLogic.instance.pack.ToString());
					anim.Play();
					yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
					currentlevel = level;
				}
			}
			else if (currentlevel != level)
			{
				Label.text = LanguageManager.Instance.GetTextValue("EASTER EGG HUNT").Replace("*", string.Empty);
				anim.Play();
				yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
				currentlevel = level;
			}
		}
		else if (currentlevel != level)
		{
			Label.text = string.Empty;
			anim.Play();
			yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
			currentlevel = level;
		}
		for (int i = from; i >= 0; i--)
		{
			Label.text = ((i <= 0) ? LanguageManager.Instance.GetTextValue("1-2-3-GO!") : i.ToString());
			anim.Play();
			yield return new WaitForSeconds(anim[str_timer_3_2_1].length + 0.1f);
		}
		callback();
		anim.Stop();
		Label.transform.localScale = Vector2.zero;
		counting = null;
		RaceLogic.instance.gui.interface_Controlls.topLeftBtn.SetActive(value: true);
	}
}

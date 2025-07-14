using ArabicSupport;
using SmartLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForPersian : MonoBehaviour
{
	public bool need = true;

	private Text texts;

	private string code;

	private TextAnchor temp;

	private bool rev;

	private IEnumerator Start()
	{
		LanguageManager instance = LanguageManager.Instance;
		instance.OnChangeLanguage = (ChangeLanguageEventHandler)Delegate.Combine(instance.OnChangeLanguage, new ChangeLanguageEventHandler(UpdateText));
		texts = GetComponent<Text>();
		temp = texts.alignment;
		yield return 0;
		UpdateText(LanguageManager.Instance);
	}

	private void UpdateText(LanguageManager thisLanguageManager)
	{
		code = LocalizationManager.instance.currentlyLanguageCode;
		if (need)
		{
			if (code.Contains("fa-IR"))
			{
				texts.alignment = TextAnchor.MiddleRight;
				if (!rev)
				{
					rev = !rev;
					texts.text = Reverse(texts.text);
				}
			}
			else
			{
				if (rev)
				{
					rev = !rev;
					texts.text = Reverse(texts.text);
				}
				texts.alignment = temp;
			}
		}
		else if (code.Contains("fa-IR"))
		{
			if (!rev)
			{
				rev = !rev;
				texts.text = Reverse(texts.text);
			}
		}
		else if (rev)
		{
			rev = !rev;
			texts.text = Reverse(texts.text);
		}
	}

	public static string Reverse(string s)
	{
		return ArabicFixer.Fix(s);
	}
}

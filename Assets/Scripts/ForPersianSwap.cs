using SmartLocalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForPersianSwap : MonoBehaviour
{
	public Text txt1;

	public Text txt2;

	private void Start()
	{
		StartCoroutine(Check());
	}

	private IEnumerator Check()
	{
		yield return new WaitForSeconds(0.1f);
		string code = LocalizationManager.instance.currentlyLanguageCode;
		if (code.Contains("fa-IR"))
		{
			string text = txt1.text;
			string text2 = txt2.text;
			txt2.text = text;
			txt1.text = text2;
		}
	}
}

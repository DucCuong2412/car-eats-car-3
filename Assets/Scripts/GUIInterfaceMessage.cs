using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIInterfaceMessage : MonoBehaviour
{
	public enum Words
	{
		yourock,
		yumyum,
		combo,
		wow,
		tasty,
		copter,
		airstrike,
		police,
		bombers,
		morepower,
		lesspower,
		bosskiller
	}

	public GUIMessagesWords tr;

	public Text label;

	public TweenScale tween;

	public void AnimateWithText(Words text)
	{
		string text2 = string.Empty;
		switch (text)
		{
		case Words.combo:
			text2 = tr.GetTextValue(tr.combo);
			break;
		case Words.yourock:
			text2 = tr.GetTextValue(tr.yourock);
			break;
		case Words.tasty:
			text2 = tr.GetTextValue(tr.tasty);
			break;
		case Words.copter:
			text2 = tr.GetTextValue(tr.copter);
			break;
		case Words.airstrike:
			text2 = tr.GetTextValue(tr.airstrike);
			break;
		case Words.police:
			text2 = tr.GetTextValue(tr.police);
			break;
		case Words.yumyum:
			text2 = tr.GetTextValue(tr.yumyum);
			break;
		case Words.wow:
			text2 = tr.GetTextValue(tr.wow);
			break;
		case Words.bombers:
			text2 = tr.GetTextValue(tr.bombers);
			break;
		case Words.morepower:
			text2 = tr.GetTextValue(tr.morepower);
			break;
		case Words.lesspower:
			text2 = tr.GetTextValue(tr.lesspower);
			break;
		case Words.bosskiller:
			text2 = tr.GetTextValue(tr.bosskiller);
			break;
		}
		AnimateWithText(text2);
	}

	public void OnEnable()
	{
	}

	public void AnimateWithText(string text)
	{
		label.text = text;
		tween.ResetToBeginning();
		tween.PlayForward();
	}

	private IEnumerator test()
	{
		label.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1.5f);
		label.gameObject.SetActive(value: false);
	}
}

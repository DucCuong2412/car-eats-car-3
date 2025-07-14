using SmartLocalization;
using UnityEngine;

public class GUIBoostersWords : MonoBehaviour
{
	public string free;

	public string norubies;

	public string active;

	public string GetTextValue(string key)
	{
		if (key != string.Empty)
		{
			return LanguageManager.Instance.GetTextValue(key);
		}
		return "No Rubies";
	}
}

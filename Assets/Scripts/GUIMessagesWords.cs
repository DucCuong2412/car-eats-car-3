using SmartLocalization;
using UnityEngine;

public class GUIMessagesWords : MonoBehaviour
{
	public string yourock;

	public string yumyum;

	public string combo;

	public string wow;

	public string tasty;

	public string copter;

	public string airstrike;

	public string police;

	public string bombers;

	public string morepower;

	public string lesspower;

	public string bosskiller;

	public string GetTextValue(string key)
	{
		return LanguageManager.Instance.GetTextValue(key);
	}
}

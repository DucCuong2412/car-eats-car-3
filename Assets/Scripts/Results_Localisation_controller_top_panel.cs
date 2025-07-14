using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class Results_Localisation_controller_top_panel : MonoBehaviour
{
	public Text level;

	public Text number;

	private void Start()
	{
		if (!Progress.shop.EsterLevelPlay)
		{
			if (Progress.shop.endlessLevel && !Progress.shop.bossLevel)
			{
				level.text = LanguageManager.Instance.GetTextValue("SPECIAL MISSION");
				number.text = string.Empty;
			}
			else if (Progress.shop.bossLevel)
			{
				level.text = LanguageManager.Instance.GetTextValue("BOSS LEVEL");
				number.text = string.Empty;
			}
			else if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel && !Progress.shop.EsterLevelPlay)
			{
				level.text = LanguageManager.Instance.GetTextValue("LEVEL *").Replace("*", string.Empty);
			}
		}
		else if (Progress.shop.EsterLevelPlay)
		{
			level.text = LanguageManager.Instance.GetTextValue("EASTER EGG HUNT");
			number.text = string.Empty;
		}
	}
}

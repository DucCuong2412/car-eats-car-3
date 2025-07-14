using System.Collections.Generic;
using UnityEngine;

namespace SmartLocalization
{
	public class LocalizationManager : MonoBehaviour
	{
		private LanguageManager languageManager;

		private static LocalizationManager _instance;

		private string keyForSaving = "currentlyLanguageCode";

		public static LocalizationManager instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject gameObject = new GameObject("_localizationManager");
					_instance = gameObject.AddComponent<LocalizationManager>();
					_instance.languageManager = LanguageManager.Instance;
					_instance.languageManager.ChangeLanguage(_instance.LoadLanguageCode());
				}
				return _instance;
			}
		}

		public Dictionary<string, string> currentlyLanguageDatabase => LanguageManager.Instance.RawTextDatabase;

		public string currentlyLanguageCode => languageManager.CurrentlyLoadedCulture.languageCode;

		public void SaveNextAvailableLanguage(bool apply = false)
		{
			List<SmartCultureInfo> supportedLanguages = languageManager.GetSupportedLanguages();
			string languageCode = supportedLanguages[0].languageCode;
			for (int i = 0; i < supportedLanguages.Count; i++)
			{
				if (supportedLanguages[i].languageCode == currentlyLanguageCode && i < supportedLanguages.Count - 1)
				{
					languageCode = supportedLanguages[++i].languageCode;
					break;
				}
			}
			SaveLanguageCode(languageCode);
			if (apply)
			{
				languageManager.ChangeLanguage(languageCode);
			}
		}

		public void SaveNextAvailableLanguage(string language)
		{
			List<SmartCultureInfo> supportedLanguages = languageManager.GetSupportedLanguages();
			for (int i = 0; i < supportedLanguages.Count; i++)
			{
				if (supportedLanguages[i].languageCode == language)
				{
					SaveLanguageCode(supportedLanguages[i].languageCode);
					languageManager.ChangeLanguage(supportedLanguages[i].languageCode);
					UnityEngine.Debug.Log("Language selected: " + supportedLanguages[i].languageCode);
					return;
				}
			}
			UnityEngine.Debug.LogError("Can't found language:" + language);
		}

		public void SetDefaultLanguage()
		{
			languageManager.ChangeLanguage(LoadLanguageCode());
		}

		private string LoadLanguageCode()
		{
			string text = "en-US";
			string result = text;
			if (PlayerPrefs.HasKey(keyForSaving))
			{
				return PlayerPrefs.GetString(keyForSaving);
			}
			SmartCultureInfo supportedSystemLanguage = languageManager.GetSupportedSystemLanguage();
			if (supportedSystemLanguage != null)
			{
				return supportedSystemLanguage.languageCode;
			}
			string systemLanguageEnglishName = LanguageManager.Instance.GetSystemLanguageEnglishName();
			foreach (SmartCultureInfo supportedLanguage in LanguageManager.Instance.GetSupportedLanguages())
			{
				if (supportedLanguage.englishName.Contains(systemLanguageEnglishName))
				{
					result = supportedLanguage.languageCode;
				}
			}
			return result;
		}

		private void SaveLanguageCode(string languageCode)
		{
			PlayerPrefs.SetString(keyForSaving, languageCode);
		}
	}
}

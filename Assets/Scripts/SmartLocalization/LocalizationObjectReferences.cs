using System.Collections.Generic;
using UnityEngine;

namespace SmartLocalization
{
	public class LocalizationObjectReferences : MonoBehaviour
	{
		public List<LocalizationPair> Pairs = new List<LocalizationPair>();

		private string UnsupportedKeys;

		private int nullTextCount;

		public void RefreshLanguageLables()
		{
			SetLanguageToLabels(LocalizationManager.instance.currentlyLanguageDatabase);
		}

		private void Start()
		{
			SetLanguageToLabels(LocalizationManager.instance.currentlyLanguageDatabase);
		}

		private void SetLanguageToLabels(Dictionary<string, string> languageDatabase)
		{
			foreach (LocalizationPair pair in Pairs)
			{
				LocalizationPair current = pair;
				if (current.UITextReference != null)
				{
					string value = string.Empty;
					if (languageDatabase.TryGetValue(current.StringKey, out value))
					{
						current.UITextReference.text = value;
					}
					else
					{
						UnsupportedKeys = UnsupportedKeys + current.StringKey + ", ";
					}
				}
				else
				{
					nullTextCount++;
				}
			}
			if (!string.IsNullOrEmpty(UnsupportedKeys))
			{
				UnsupportedKeys = UnsupportedKeys.Remove(UnsupportedKeys.LastIndexOf(','));
			}
			if (nullTextCount > 0)
			{
				string text = $"Localization object has {nullTextCount} null references text. Please check it";
			}
			UnsupportedKeys = null;
			nullTextCount = 0;
		}
	}
}

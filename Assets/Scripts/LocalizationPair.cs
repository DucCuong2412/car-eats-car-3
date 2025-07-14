using System;
using UnityEngine.UI;

namespace SmartLocalization
{
	[Serializable]
	public struct LocalizationPair
	{
		public string StringKey;

		public Text UITextReference;
	}
}

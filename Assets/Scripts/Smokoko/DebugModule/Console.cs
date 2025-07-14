using UnityEngine;
using UnityEngine.UI;

namespace Smokoko.DebugModule
{
	public class Console : MonoBehaviour
	{
		public Text text;

		private int _fixedSizeX;

		private int fixedSizeX
		{
			get
			{
				if (_fixedSizeX == 0)
				{
					_fixedSizeX = Screen.width;
				}
				return _fixedSizeX;
			}
		}

		private void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		private void OnTriggerEnter()
		{
		}

		public void Toggle()
		{
			base.gameObject.SetActive(!base.gameObject.activeSelf);
		}

		public void AddText(string addText)
		{
			text.text = "\n" + text.text + addText;
			UpdateSize();
		}

		public void ClearText()
		{
			text.text = string.Empty;
			UpdateSize();
		}

		private void UpdateSize()
		{
			int num = (int)text.preferredHeight;
			text.rectTransform.sizeDelta = new Vector2(fixedSizeX, num);
		}
	}
}

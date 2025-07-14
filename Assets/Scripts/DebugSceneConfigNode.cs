using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugSceneConfigNode : MonoBehaviour
{
	public Text nameText;

	public Text cacheDateText;

	public Button loadButton;

	public Text loadButtonText;

	private string _name;

	private string _cacheDate;

	private string _serverDate;

	private Action<string> load;

	public void Init(Action<string> onUpdate, string name, string cacheDate = "", string serverDate = "")
	{
		_name = name;
		_cacheDate = cacheDate;
		_serverDate = serverDate;
		nameText.text = _name;
		cacheDateText.text = _cacheDate;
		buttonEnable(_serverDate);
		load = onUpdate;
		loadButton.onClick.AddListener(onLoadPressed);
	}

	private void onLoadPressed()
	{
		if (load != null)
		{
			load(nameText.text);
			cacheDateText.text = _serverDate;
			buttonEnable(string.Empty);
		}
	}

	private void buttonEnable(string text)
	{
		bool flag = text != string.Empty;
		loadButton.enabled = flag;
		loadButton.image.color = ((!flag) ? Color.grey : Color.green);
		loadButtonText.text = text;
	}
}

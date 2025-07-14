using UnityEngine;
using UnityEngine.UI;

public class linkCec2 : MonoBehaviour
{
	public Button _btn_CEC2;

	private void OnEnable()
	{
		_btn_CEC2.onClick.AddListener(OpenStore);
	}

	private void OnDisable()
	{
		_btn_CEC2.onClick.RemoveAllListeners();
	}

	public void OpenStore()
	{
		string link_Android = LinkToCarEatsCar2.instance.link.Link_Android;
		Application.OpenURL(link_Android);
	}
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoVideoAvailable : MonoBehaviour
{
	public static bool IsActive;

	public Button buttonCloseNoVideo;

	public static void Show()
	{
		GameObject gameObject = GameObject.Find("noVideoBox");
		if (!gameObject)
		{
			SceneManager.LoadScene("noVideoBox", LoadSceneMode.Additive);
		}
		else
		{
			gameObject.SetActive(value: true);
		}
		IsActive = true;
	}

	public static void Hide()
	{
		GameObject gameObject = GameObject.Find("noVideoBox");
		if ((bool)gameObject)
		{
			gameObject.SetActive(value: false);
		}
		IsActive = false;
	}

	private void OnEnable()
	{
		IsActive = true;
		if (buttonCloseNoVideo == null)
		{
			buttonCloseNoVideo = GetComponentInChildren<Button>();
		}
		buttonCloseNoVideo.onClick.AddListener(ClosePanel);
	}

	private void OnQuit()
	{
		IsActive = false;
		ClosePanel();
	}

	private void OnDisable()
	{
		IsActive = false;
		buttonCloseNoVideo.onClick.RemoveAllListeners();
	}

	private void ClosePanel()
	{
		IsActive = false;
		base.gameObject.SetActive(value: false);
	}
}

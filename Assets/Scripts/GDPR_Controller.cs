using SA.Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GDPR_Controller : MonoBehaviour
{
	public GameObject _GdprWindow;

	public Button _yesGdprBtn;

	public Button _noGdprBtn;

	public Button _showGdprBtn;

	public Button _link;

	public string URL;

	private GameObject cahs;

	public Camera cam;

	public bool NeedCheckRegion;

	private object region;

	private void OnEnable()
	{
		if (_yesGdprBtn != null)
		{
			_yesGdprBtn.onClick.AddListener(YesGDPR);
		}
		if (_noGdprBtn != null)
		{
			_noGdprBtn.onClick.AddListener(NoGDPR);
		}
		if (_showGdprBtn != null)
		{
			_showGdprBtn.onClick.AddListener(ShowWindow);
		}
		if (_link != null)
		{
			_link.onClick.AddListener(Link);
		}
		Scene activeScene = SceneManager.GetActiveScene();
		if (NeedCheckRegion && activeScene.name == "PreSplash")
		{
			StartCoroutine(checkRegion());
		}
		else
		{
			if (_GdprWindow != null)
			{
				_GdprWindow.SetActive(value: true);
			}
			if (cam != null)
			{
				cam.enabled = false;
			}
		}
		if (_showGdprBtn != null)
		{
			_showGdprBtn.gameObject.SetActive(Progress.settings.showGDPRads);
		}
	}

	private void OnDisable()
	{
		if (_yesGdprBtn != null)
		{
			_yesGdprBtn.onClick.RemoveAllListeners();
		}
		if (_noGdprBtn != null)
		{
			_noGdprBtn.onClick.RemoveAllListeners();
		}
		if (_showGdprBtn != null)
		{
			_showGdprBtn.onClick.RemoveAllListeners();
		}
	}

	private IEnumerator checkRegion()
	{
		WWW www = new WWW("http://ip-api.com/json");
		yield return www;
		if (www.error != null)
		{
			SceneManager.LoadScene("Splash");
			yield break;
		}
		Dictionary<string, object> dictionary = Json.Deserialize(www.text) as Dictionary<string, object>;
		dictionary.TryGetValue("countryCode", out region);
		UnityEngine.Debug.Log(region.ToString());
		if (region.ToString().Contains("BE") || region.ToString().Contains("EL") || region.ToString().Contains("LT") || region.ToString().Contains("PT") || region.ToString().Contains("BG") || region.ToString().Contains("ES") || region.ToString().Contains("LU") || region.ToString().Contains("RO") || region.ToString().Contains("CZ") || region.ToString().Contains("FR") || region.ToString().Contains("HU") || region.ToString().Contains("SI") || region.ToString().Contains("DK") || region.ToString().Contains("HR") || region.ToString().Contains("NO") || region.ToString().Contains("MT") || region.ToString().Contains("SK") || region.ToString().Contains("DE") || region.ToString().Contains("IT") || region.ToString().Contains("NL") || region.ToString().Contains("FI") || region.ToString().Contains("EE") || region.ToString().Contains("CY") || region.ToString().Contains("AT") || region.ToString().Contains("SE") || region.ToString().Contains("IE") || region.ToString().Contains("LV") || region.ToString().Contains("PL") || region.ToString().Contains("CH") || region.ToString().Contains("LI") || region.ToString().Contains("IS") || region.ToString().Contains("GB"))
		{
			if (!Progress.settings.showGDPRads)
			{
				_GdprWindow.SetActive(value: true);
			}
			else
			{
				SceneManager.LoadScene("Splash");
			}
		}
		else
		{
			_GdprWindow.SetActive(value: false);
			SceneManager.LoadScene("Splash");
		}
	}

	private void Link()
	{
		Application.OpenURL(URL);
	}

	private void ShowWindow()
	{
		if (cahs != null)
		{
			cahs.SetActive(value: true);
		}
		else
		{
			SceneManager.LoadScene("PreSplash", LoadSceneMode.Additive);
		}
	}

	private void YesGDPR()
	{
		_GdprWindow.SetActive(value: false);
		Progress.settings.GDPRads = "1";
		UnityEngine.Debug.Log("posle Progress.game.GDPRads ====> " + Progress.settings.GDPRads);
		AdColonyWrapper.instance.ConfigureAds();
		MetaData metaData = new MetaData("gdpr");
		metaData.Set("consent", "true");
		Advertisement.SetMetaData(metaData);
		if (!Progress.settings.showGDPRads)
		{
			Progress.settings.showGDPRads = true;
			if (SceneManager.GetActiveScene().name == "PreSplash")
			{
				SceneManager.LoadScene("Splash");
			}
		}
		else
		{
			cahs = _GdprWindow;
			_GdprWindow.SetActive(value: false);
		}
		cam.enabled = false;
	}

	private void NoGDPR()
	{
		_GdprWindow.SetActive(value: false);
		Progress.settings.GDPRads = "0";
		UnityEngine.Debug.Log("posle Progress.game.GDPRads ====> " + Progress.settings.GDPRads);
		AdColonyWrapper.instance.ConfigureAds();
		MetaData metaData = new MetaData("gdpr");
		metaData.Set("consent", "false");
		Advertisement.SetMetaData(metaData);
		if (!Progress.settings.showGDPRads)
		{
			Progress.settings.showGDPRads = true;
			if (SceneManager.GetActiveScene().name == "PreSplash")
			{
				SceneManager.LoadScene("Splash");
			}
		}
		else
		{
			cahs = _GdprWindow;
			_GdprWindow.SetActive(value: false);
		}
		cam.enabled = false;
	}
}

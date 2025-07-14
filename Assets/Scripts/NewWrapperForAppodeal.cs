using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewWrapperForAppodeal : MonoBehaviour
{
	private GameObject loader;

	private static NewWrapperForAppodeal _instance;

	private Action OnNoVideoAction = delegate
	{
	};

	private Action<bool> OnVideoAction = delegate
	{
	};

	private Action NoSucssesVideoAction = delegate
	{
	};

	public static NewWrapperForAppodeal instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("NewWrapperForAppodeal");
				_instance = gameObject.AddComponent<NewWrapperForAppodeal>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return _instance;
		}
	}

	public void onRewardedVideoLoaded(bool precache)
	{
		AdvertWrapper.instance.HideLoader();
	}

	public void onRewardedVideoFailedToLoad()
	{
		OnNoVideoAction();
		AdvertWrapper.instance.HideLoader();
		HideLoader();
	}

	public void onRewardedVideoShown()
	{
		AdvertWrapper.instance.HideLoader();
		HideLoader();
	}

	public void onRewardedVideoClosed(bool finished)
	{
		AdvertWrapper.instance.HideLoader();
		OnVideoAction(finished);
		HideLoader();
	}

	public void onRewardedVideoFinished(double amount, string name)
	{
		AdvertWrapper.instance.HideLoader();
		HideLoader();
	}

	private IEnumerator checkInternetConnection(Action<bool> action)
	{
		yield return 0;
		loader = GameObject.Find("LoaderCanvas");
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null)
		{
			action(obj: false);
		}
		else
		{
			action(obj: true);
		}
	}

	public void ShowVideo(Action<bool> onVideoSucess = null, Action onNoVideo = null, Action onNoSucssesVideo = null)
	{
		SceneManager.LoadScene("Loader", LoadSceneMode.Additive);
		OnNoVideoAction = onNoVideo;
		OnVideoAction = onVideoSucess;
		NoSucssesVideoAction = onNoSucssesVideo;
		AdvertWrapper.instance.ShowSpilVideo(showNoVideo: true, string.Empty, onVideoSucess, onNoVideo);
	}

	public void Video()
	{
		StartCoroutine(checkInternetConnection(delegate(bool isConnected)
		{
			if (isConnected)
			{
				StartCoroutine(WaitForAdvertResponse());
			}
			else
			{
				OnNoVideoAction();
				HideLoader();
			}
		}));
	}

	private void HideLoader()
	{
		UnityEngine.Debug.Log("Hide Loader>>  appodel 1");
		StartCoroutine(HL());
	}

	private IEnumerator HL()
	{
		UnityEngine.Debug.Log("Hide Loader>>  appodel 2");
		if (loader != null)
		{
			loader.SetActive(value: false);
		}
		else
		{
			loader = GameObject.Find("LoaderCanvas");
			if (loader != null)
			{
				loader.SetActive(value: false);
			}
		}
		if (loader != null)
		{
			while (loader.activeSelf)
			{
				yield return 0;
				loader.SetActive(value: false);
			}
		}
	}

	private IEnumerator WaitForAdvertResponse()
	{
		float time = 0f;
		while (time < 10f)
		{
			time += Time.unscaledDeltaTime;
			yield return 0;
		}
		HideLoader();
		OnNoVideoAction();
		Time.timeScale = 1f;
	}
}

using System;
using System.Collections;
using UnityEngine;

public class AdvertWrapper : MonoBehaviour
{
	private static bool CanPlayVideo;

	private static bool CanShowMoreApps;

	private static AdvertWrapper _instance;

	private Action pOnNoVideoAction;

	private GameObject loader;

	public Coroutine waitResponseCoroutine;

	public static bool AdvertRevive;

	private Coroutine corut;

	public bool cheack;

	public static AdvertWrapper instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("AdvertWrapper");
				_instance = gameObject.AddComponent<AdvertWrapper>();
				_instance.Init();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return _instance;
		}
	}

	public void Init()
	{
	}

	public void ShowInterstitial(bool show)
	{
		int num = Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack);
		if (num < 3)
		{
			return;
		}
		if (Progress.shop.BuyForRealMoney)
		{
			UnityEngine.Debug.Log(" Byla pocupka za real dengi");
		}
		else if (show)
		{
			bool flag = false;
			flag = UnityADSManager.ShowAd(delegate
			{
			});
			UnityEngine.Debug.Log("Unity Interstitial show");
			if (!flag)
			{
				UnityEngine.Debug.Log("AdColony Interstitial show");
				AdColonyWrapper.instance.PlayAdInter(delegate
				{
				});
			}
		}
	}

	public void ShowBaner()
	{
		int num = Utilities.LevelNumberGlobal(Progress.levels.Max_Active_Level, Progress.levels.Max_Active_Pack);
		if (num >= 3 && Progress.shop.BuyForRealMoney)
		{
			UnityEngine.Debug.Log(" Byla pocupka za real dengi");
		}
	}

	public void HideLoader()
	{
		UnityEngine.Debug.Log("Hide Loader");
		if (loader != null)
		{
			loader.SetActive(value: false);
			UnityEngine.Object.Destroy(loader);
			return;
		}
		loader = GameObject.Find("LoaderCanvas");
		if (loader != null)
		{
			loader.SetActive(value: false);
		}
		UnityEngine.Object.Destroy(loader);
	}

	public IEnumerator HIDESLOADERS()
	{
		float time = 0f;
		while (time < 0.5f)
		{
			time += Time.unscaledDeltaTime;
			yield return 0;
			UnityEngine.Debug.Log("FUCKING HIDE LOADER!!!!!!!");
			HideLoader();
		}
	}

	public void ShowSpilVideo(bool showNoVideo, string location = "Default", Action<bool> onVideoSucess = null, Action onNoVideo = null)
	{
		pOnNoVideoAction = onNoVideo;
		Action<bool> actionFinish = delegate(bool b)
		{
			HideLoader();
			if (onVideoSucess != null)
			{
				onVideoSucess(b);
			}
		};
		Action call = delegate
		{
			if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
			{
				UnityADSManager.ShowAd(actionFinish, video: true);
			}
			else
			{
				bool flag = false;
				flag = UnityADSManager.ShowAd(actionFinish, video: true);
				UnityEngine.Debug.Log("Unity ADS Show? ==>" + flag);
				if (!flag)
				{
					UnityEngine.Debug.Log("AdColony video start >>>>");
					StopCoroutine(corut);
					AdColonyWrapper.instance.callBack = actionFinish;
					AdColonyWrapper.instance.PlayAdVideo(delegate
					{
						HideLoader();
						if (pOnNoVideoAction != null)
						{
							pOnNoVideoAction();
						}
					});
				}
			}
		};
		corut = StartCoroutine(StartLoader(call, 10f));
	}

	private IEnumerator StartLoader(Action call, float timeout = -1f)
	{
		if (loader == null)
		{
			yield return 0;
			loader = GameObject.Find("LoaderCanvas");
		}
		else
		{
			loader.SetActive(value: true);
		}
		yield return null;
		call();
		if (!(timeout > 0f))
		{
			yield break;
		}
		while (timeout > 0f)
		{
			if (!loader.activeSelf)
			{
				yield break;
			}
			if (NoVideoAvailable.IsActive)
			{
				HideLoader();
				yield break;
			}
			timeout -= Time.unscaledDeltaTime;
			yield return null;
		}
		HideLoader();
		if (!cheack)
		{
			NoVideoAvailable.Show();
			if (pOnNoVideoAction != null)
			{
				pOnNoVideoAction();
			}
		}
		cheack = false;
	}

	private void SpilVideoIsNotAvailable(Action onNoVideo = null)
	{
		Time.timeScale = 1f;
		onNoVideo?.Invoke();
	}
}

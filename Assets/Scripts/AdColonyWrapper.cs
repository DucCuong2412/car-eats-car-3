using AdColony;
using System;
using UnityEngine;

public class AdColonyWrapper : MonoBehaviour
{
	private string zoneIDVideo = "vz5bfcbda16e61448488";

	private string zoneIDInter = "vzca835306d8f84d8cb0";

	private string appID = "app860f0f02a8974a8b9d";

	private static AdColonyWrapper _instance;

	private InterstitialAd _adVideo;

	private InterstitialAd _adInter;

	public Action<bool> callBack;

	public static AdColonyWrapper instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("AdColonyWrapper");
				_instance = gameObject.AddComponent<AdColonyWrapper>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return _instance;
		}
	}

	public void ConfigureAds()
	{
		AppOptions appOptions = new AppOptions();
		appOptions.UserId = "Smokoko";
		appOptions.AdOrientation = AdOrientationType.AdColonyOrientationAll;
		appOptions.GdprRequired = true;
		appOptions.GdprConsentString = Progress.settings.GDPRads;
		string[] array = new string[2]
		{
			zoneIDVideo,
			zoneIDInter
		};
		Ads.Configure(appID, appOptions, zoneIDVideo, zoneIDInter);
		Register();
		RegisterForAdsCallbacks();
		RequestAdVideo();
		RequestAdInter();
		CallBack();
	}

	public void Register()
	{
		Ads.OnRequestInterstitial += delegate(InterstitialAd ad)
		{
			UnityEngine.Debug.Log("OnRequestInterstitial  RequestInterstitialAd + " + ad.ZoneId);
			if (ad.ZoneId.Contains(zoneIDVideo))
			{
				UnityEngine.Debug.Log("OnRequestInterstitial  _adVideo + " + ad.ZoneId);
				_adVideo = ad;
			}
			else if (ad.ZoneId.Contains(zoneIDInter))
			{
				UnityEngine.Debug.Log("OnRequestInterstitial  _adInter + " + ad.ZoneId);
				_adInter = ad;
			}
		};
	}

	public void RegisterForAdsCallbacks()
	{
		Ads.OnExpiring += delegate(InterstitialAd ad)
		{
			UnityEngine.Debug.Log("OnExpiring  RequestInterstitialAd + " + ad.ZoneId);
			Ads.RequestInterstitialAd(ad.ZoneId, null);
		};
		Ads.OnClosed += delegate(InterstitialAd ad)
		{
			UnityEngine.Debug.Log("OnClose  RequestInterstitialAd + " + ad.ZoneId);
			if (ad.ZoneId.Contains(zoneIDVideo))
			{
				_adVideo = null;
			}
			else if (ad.ZoneId.Contains(zoneIDInter))
			{
				_adInter = null;
			}
			Register();
			Ads.RequestInterstitialAd(ad.ZoneId, null);
		};
	}

	public void CallBack()
	{
		Ads.OnRewardGranted += delegate(string zoneId, bool success, string name, int amount)
		{
			callBack(success);
		};
	}

	public void RequestAdVideo()
	{
		UnityEngine.Debug.Log("RequestInterstitialAd  zoneIDCandy");
		Ads.RequestInterstitialAd(zoneIDVideo, null);
	}

	public void RequestAdInter()
	{
		UnityEngine.Debug.Log("RequestInterstitialAd  zoneIDArmor");
		Ads.RequestInterstitialAd(zoneIDInter, null);
	}

	public void PlayAdVideo(Action Novideo)
	{
		UnityEngine.Debug.Log("play video");
		if (_adVideo != null)
		{
			UnityEngine.Debug.Log("_adVideo != null");
			Ads.ShowAd(_adVideo);
			return;
		}
		UnityEngine.Debug.Log("_adVideo == null");
		Register();
		Ads.RequestInterstitialAd(zoneIDVideo, null);
		Novideo();
	}

	public void PlayAdInter(Action Novideo)
	{
		UnityEngine.Debug.Log("play video");
		if (_adInter != null)
		{
			UnityEngine.Debug.Log("_adVideo != null");
			Ads.ShowAd(_adInter);
			return;
		}
		UnityEngine.Debug.Log("_adVideo == null");
		Register();
		Ads.RequestInterstitialAd(zoneIDInter, null);
		Novideo();
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class InAppManager : MonoBehaviour
{
	public class InAppPrice
	{
		public string purchase;

		public string price;

		public InAppPrice(string _purchase, string _price)
		{
			purchase = _purchase;
			price = _price;
		}

		public InAppPrice()
		{
		}
	}

	private static InAppManager _instance;

	public bool isInitializing;

	public bool isInitialized;

	public static InAppManager instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_InAppManager");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				_instance = gameObject.AddComponent<InAppManager>();
			}
			return _instance;
		}
	}

	public virtual string getOfflinePrice(InAppSettings.Purchases purch)
	{
		return GetDefaultPrice(purch);
	}

	public void InitPurchases()
	{
		isInitializing = true;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			InAppWrapperiOS.instance.init(GetSKU(), OnStoreIsLoaded);
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			InAppWrapperAndroid.instance.init(GetSKU(), OnStoreIsLoaded);
		}
		else
		{
			isInitializing = false;
		}
	}

	public void Purchase(InAppSettings.Purchases purchase, Action callback)
	{
		CheckToInit();
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			InAppWrapperiOS.instance.buyItem(GetSKU(purchase), callback);
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			InAppWrapperAndroid.instance.purchase(GetSKU(purchase), callback);
		}
	}

	public string GetPrice(InAppSettings.Purchases purchase)
	{
		string text = string.Empty;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			text = InAppWrapperiOS.instance.getLocPrice(GetSKU(purchase));
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			text = InAppWrapperAndroid.instance.getPrice(GetSKU(purchase));
		}
		if (text != string.Empty)
		{
			PlayerPrefs.SetString(GetSKU(purchase), text);
		}
		return (!(text != string.Empty)) ? getOfflinePrice(purchase) : text;
	}

	public void RestorePurchases(Action<List<string>> callbackPurchasedIDs = null)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			InAppWrapperiOS.instance.restorePurchases(callbackPurchasedIDs);
		}
		if (Application.platform == RuntimePlatform.Android)
		{
			InAppWrapperAndroid.instance.restorePurchases(callbackPurchasedIDs);
		}
	}

	private string GetSKU(InAppSettings.Purchases purch)
	{
		return InAppSettings.instance.GetSKU(purch);
	}

	private List<string> GetSKU()
	{
		return InAppSettings.instance.GetAllSKU();
	}

	private string GetDefaultPrice(InAppSettings.Purchases purch)
	{
		return InAppSettings.instance.GetDefaultPrice(purch);
	}

	private void CheckToInit()
	{
		if (!isInitializing && !isInitialized)
		{
			isInitializing = true;
			InitPurchases();
		}
	}

	private void SaveOfflinePrices()
	{
	}

	private void OnStoreIsLoaded()
	{
		SaveOfflinePrices();
		isInitializing = false;
		isInitialized = true;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppWrapperAndroid
{
	private const string BUYKEY = "BuyInAppNotConsume";

	private static InAppWrapperAndroid _instance;

	public static InAppWrapperAndroid instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new InAppWrapperAndroid();
			}
			return _instance;
		}
	}

	public bool isInited => false;

	public void init(List<string> _purchasesList, Action callback, Action<string[]> onRetrieve = null)
	{
	}

	public void consume(string SKU = null)
	{
	}

	public void purchase(string SKU, Action _callback = null)
	{
		InAppManager.instance.StartCoroutine(Purchasing(SKU, _callback));
	}

	private IEnumerator Purchasing(string SKU, Action _callback = null)
	{
		UnityEngine.Debug.Log("Consuming");
		consume();
		yield return null;
	}

	public string getPrice(string sku)
	{
		return string.Empty;
	}

	public void restorePurchases(Action<List<string>> callbackPurchasedIDs = null)
	{
	}
}

using System;
using System.Collections.Generic;

public class InAppWrapperiOS
{
	private static InAppWrapperiOS _instance;

	public static InAppWrapperiOS instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new InAppWrapperiOS();
			}
			return _instance;
		}
	}

	public void init(List<string> _purchasesList, Action callback)
	{
	}

	public void buyItem(string productId, Action callback = null)
	{
	}

	public string getLocPrice(string id)
	{
		return string.Empty;
	}

	public void restorePurchases(Action<List<string>> callbackPurchasedIDs = null)
	{
	}
}

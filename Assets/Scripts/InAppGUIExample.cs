using UnityEngine;

public class InAppGUIExample : MonoBehaviour
{
	private void OnGUI()
	{
		string text = InAppManagerExample.instance.isInitialized ? "initialized" : ((!InAppManagerExample.instance.isInitializing) ? "null" : "initializing");
		GUI.Label(new Rect(10f, 10f, 100f, 50f), text);
		if (!InAppManagerExample.instance.isInitialized)
		{
			if (!InAppManagerExample.instance.isInitializing && GUI.Button(new Rect(150f, 10f, 100f, 50f), "Init"))
			{
				InAppManagerExample.instance.InitPurchases();
			}
			return;
		}
		Rect position = new Rect(10f, 100f, 120f, 60f);
		if (GUI.Button(position, InAppIDsExample.Purchases.Moneytier1.ToString()))
		{
			InAppManagerExample.instance.Purchase(InAppIDsExample.Purchases.Moneytier1, Product1Bought);
		}
		GUI.Label(new Rect(position.x + 150f, position.y + 25f, position.width, position.height), InAppManagerExample.instance.GetPrice(InAppIDsExample.Purchases.Moneytier1));
		position.y += 75f;
		if (GUI.Button(position, InAppIDsExample.Purchases.Moneytier2.ToString()))
		{
			InAppManagerExample.instance.Purchase(InAppIDsExample.Purchases.Moneytier2, Product2Bought);
		}
		GUI.Label(new Rect(position.x + 150f, position.y + 25f, position.width, position.height), InAppManagerExample.instance.GetPrice(InAppIDsExample.Purchases.Moneytier2));
		position.y += 75f;
		if (GUI.Button(position, InAppIDsExample.Purchases.Moneytier3.ToString()))
		{
			InAppManagerExample.instance.Purchase(InAppIDsExample.Purchases.Moneytier3, Product3Bought);
		}
		GUI.Label(new Rect(position.x + 150f, position.y + 25f, position.width, position.height), InAppManagerExample.instance.GetPrice(InAppIDsExample.Purchases.Moneytier3));
		position.y += 75f;
		if (GUI.Button(position, InAppIDsExample.Purchases.OpenAllLevels.ToString()))
		{
			InAppManagerExample.instance.Purchase(InAppIDsExample.Purchases.OpenAllLevels, Product4Bought);
		}
		GUI.Label(new Rect(position.x + 150f, position.y + 25f, position.width, position.height), InAppManagerExample.instance.GetPrice(InAppIDsExample.Purchases.OpenAllLevels));
	}

	private void Product1Bought()
	{
		UnityEngine.Debug.Log("Product1Bought");
	}

	private void Product2Bought()
	{
		UnityEngine.Debug.Log("Product2Bought");
	}

	private void Product3Bought()
	{
		UnityEngine.Debug.Log("Product3Bought");
	}

	private void Product4Bought()
	{
		UnityEngine.Debug.Log("Product4Bought");
	}
}

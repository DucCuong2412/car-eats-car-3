using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_shop : MonoBehaviour
{
	private static GUI_shop _instance;

	public ShopModel Model;

	private bool isInited;

	private bool initing;

	private PremiumShopNew _shopWindowModel;

	public static GUI_shop instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("GUI_shop").AddComponent<GUI_shop>();
			}
			return _instance;
		}
	}

	private IEnumerator InitShop(Action callback = null)
	{
		OnGarageLoaded i = null;
		initing = true;
		if (Model == null)
		{
			if (UnityEngine.Object.FindObjectOfType<ShopModel>() == null)
			{
				UnityEngine.Object.Instantiate(Resources.Load("GarageLoad"));
				UnityEngine.Object.Instantiate(Resources.Load("Garage"));
				while (i == null)
				{
					i = UnityEngine.Object.FindObjectOfType<OnGarageLoaded>();
					yield return Utilities.WaitForRealSeconds(1f);
				}
				while (Model == null)
				{
					Model = UnityEngine.Object.FindObjectOfType<ShopModel>();
					yield return Utilities.WaitForRealSeconds(1f);
				}
			}
			else
			{
				Model = UnityEngine.Object.FindObjectOfType<ShopModel>();
			}
		}
		if (!isInited)
		{
			Model.InitShop(Progress.shop, Progress.levels.active_pack, callback);
			isInited = true;
		}
		yield return null;
		Game.PopInnerState("shop_load", executeClose: false);
		ShowShop(callback);
		if (i != null)
		{
			i.EndLoad();
		}
		initing = false;
	}

	private IEnumerator InitShopPremium(Action callback = null)
	{
		OnGarageLoaded i = null;
		initing = true;
		if (Model == null)
		{
			if (UnityEngine.Object.FindObjectOfType<ShopModel>() == null)
			{
				UnityEngine.Object.Instantiate(Resources.Load("GarageLoad"));
				UnityEngine.Object.Instantiate(Resources.Load("Garage"));
				while (i == null)
				{
					i = UnityEngine.Object.FindObjectOfType<OnGarageLoaded>();
					yield return Utilities.WaitForRealSeconds(1f);
				}
				while (Model == null)
				{
					Model = UnityEngine.Object.FindObjectOfType<ShopModel>();
					yield return Utilities.WaitForRealSeconds(1f);
				}
			}
			else
			{
				Model = UnityEngine.Object.FindObjectOfType<ShopModel>();
			}
		}
		if (!isInited)
		{
			Model.InitShop(Progress.shop, Progress.levels.active_pack, callback);
			isInited = true;
		}
		yield return null;
		Game.PopInnerState("shop_load", executeClose: false);
		Model.ShowShopPremium();
		if (i != null)
		{
			i.EndLoad();
		}
		initing = false;
	}

	public void ShowShop(Action callback = null)
	{
	}

	public void ShowPremiumShop(Action callback = null)
	{
		if (Game.currentState != Game.gameState.Shop)
		{
			Game.OnStateChange(Game.gameState.Shop);
		}
		if (Model == null)
		{
			if (!initing)
			{
				Game.PushInnerState("shop_load", delegate
				{
				});
				StartCoroutine(InitShopPremium(callback));
			}
		}
		else
		{
			Model.ShowShopPremium();
		}
	}

	public void HideShop()
	{
		Model.CloseShop(delegate
		{
		});
		Game.OnStateChange(Game.gameState.PrevState);
	}

	public void ShopBuyCanvas(bool isCoins = false)
	{
		ShowBuyCanvasWindow(isCoins);
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	public void ShowBuyCanvasWindow(bool isCoins = false, Action closeShop = null)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins, closeShop));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false, Action CloseShop = null)
	{
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		if (!isCoins)
		{
			ShowGetCoinsAndFuel();
		}
		else
		{
			ShowGetCoinsAndFuel(isCoinsPanel: true);
		}
	}

	private void ShowGetCoinsAndFuel(bool isCoinsPanel = false)
	{
		_shopWindowModel.gameObject.SetActive(value: true);
		if (!isCoinsPanel)
		{
			_shopWindowModel.openFuel();
		}
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		if (!enable)
		{
			GameObject gameObject = GameObject.Find("ShopCanvasWindows(Clone)");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			_shopWindowModel = null;
		}
	}
}

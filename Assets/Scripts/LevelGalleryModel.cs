using Smokoko.DebugModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGalleryModel : MonoBehaviour
{
	private const int PACK_COUNT = 4;

	private int previousActivePack;

	private LevelGalleryView _view;

	public LevelPackCheck[] levelPackCheck = new LevelPackCheck[5];

	public int activePack;

	public int activeLevel;

	private bool pTeleporting;

	private PremiumShopNew _shopWindowModel;

	public LevelGalleryView View
	{
		get
		{
			if (_view == null)
			{
				_view = (LevelGalleryView)UnityEngine.Object.FindObjectOfType(typeof(LevelGalleryView));
			}
			return _view;
		}
	}

	public bool isTeleporting => pTeleporting;

	private void Awake()
	{
		Game.OnStateChange(Game.gameState.Levels);
		Progress.levels.Pack(1).isOpen = (Progress.levels.Pack(1).Level(1).isOpen = true);
	}

	private void Start()
	{
		View.InitPacks(4);
		activePack = (previousActivePack = Progress.levels.active_pack);
		activeLevel = Progress.levels.active_level;
		OpenLevels();
		View.FocusedOnPack(activePack);
		if (!TutorialLevels.needTutorial)
		{
			View.TeleportCarFromTo(0, 0, activePack, activeLevel, delegate
			{
				levelPackCheck[activePack].IconCheck(activeLevel).StartRotation();
			});
		}
	}

	private void OpenLevels()
	{
		for (byte b = 1; b <= 4; b = (byte)(b + 1))
		{
			for (byte b2 = 1; b2 <= 9; b2 = (byte)(b2 + 1))
			{
				bool isOpen = Progress.levels.Pack(b).Level(b2).isOpen;
				bool lgChainDropped = Progress.levels.Pack(b).Level(b2).lgChainDropped;
				Progress.levels.Pack(b).Level(b2).lgChainDropped = isOpen;
				int ticketsFound = 0;
				if (Progress.levels.Pack(b).Level(b2).tickets.Length > 0)
				{
					ticketsFound = Progress.levels.Pack(b).Level(b2).tickets.Split('|').Length;
				}
				View.SetLevel(b, b2, isOpen, !lgChainDropped, ticketsFound);
			}
		}
		Progress.Save(Progress.SaveType.Levels);
	}

	public void OpenLocationForRealMoney()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy " + activePack.ToString() + " location", new ButtonInfo("Buy", OpenLocation));
			return;
		}
		switch (activePack)
		{
		case 1:
			InAppManager.instance.Purchase(InAppSettings.Purchases.UnlockWorld1, OpenLocation);
			break;
		case 2:
			InAppManager.instance.Purchase(InAppSettings.Purchases.UnlockWorld2, OpenLocation);
			break;
		case 3:
			InAppManager.instance.Purchase(InAppSettings.Purchases.UnlockWorld3, OpenLocation);
			break;
		}
	}

	public void OpenLevelForRealMoney()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy next level", new ButtonInfo("Buy", OpenNext));
		}
		else
		{
			InAppManager.instance.Purchase(InAppSettings.Purchases.UnlockNext, OpenNext);
		}
	}

	private void OpenLocation()
	{
		Progress.levels.Pack(activePack).isOpen = true;
		for (int i = 1; i <= 9; i++)
		{
			if (!Progress.levels.Pack(activePack).Level(i).isOpen)
			{
				Progress.levels.Pack(activePack).Level(i).isOpen = true;
				Progress.levels.Pack(activePack).Level(i).lgChainDropped = true;
				int ticketsFound = 0;
				if (Progress.levels.Pack(activePack).Level(i).tickets.Length > 0)
				{
					ticketsFound = Progress.levels.Pack(activePack).Level(i).tickets.Split('|').Length;
				}
				View.SetLevel(activePack, i, isOpened: true, animate: true, ticketsFound);
			}
		}
		Progress.Save(Progress.SaveType.Levels);
		View.HideBuyAllLevelsPanel();
		Audio.PlayAsync("shop_purchase");
		switch (activePack)
		{
		case 1:
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world1", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		case 2:
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world2", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		case 3:
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world3", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		}
		Progress.review.atLeastOnePurchase = true;
	}

	private void OpenNext()
	{
		int num = GetLastAvailableLevelInPack(activePack) + 1;
		if (Progress.levels.Pack(activePack).Level(1).isOpen)
		{
			Progress.levels.Pack(activePack).Level(num).isOpen = true;
			Progress.levels.Pack(activePack).Level(num).lgChainDropped = true;
			int ticketsFound = 0;
			if (Progress.levels.Pack(activePack).Level(num).tickets.Length > 0)
			{
				ticketsFound = Progress.levels.Pack(activePack).Level(num).tickets.Split('|').Length;
			}
			View.SetLevel(activePack, num, isOpened: true, animate: true, ticketsFound);
		}
		else
		{
			Progress.levels.Pack(activePack).Level(1).isOpen = true;
			Progress.levels.Pack(activePack).Level(1).lgChainDropped = true;
			int ticketsFound2 = 0;
			if (Progress.levels.Pack(activePack).Level(1).tickets.Length > 0)
			{
				ticketsFound2 = Progress.levels.Pack(activePack).Level(1).tickets.Split('|').Length;
			}
			View.SetLevel(activePack, 1, isOpened: true, animate: true, ticketsFound2);
		}
		Progress.Save(Progress.SaveType.Levels);
		View.HideBuyAllLevelsPanel();
		Audio.PlayAsync("shop_purchase");
		Progress.review.atLeastOnePurchase = true;
	}

	private int GetLastAvailableLevelInPack(int pack)
	{
		for (byte b = 1; b <= 9; b = (byte)(b + 1))
		{
			if (!Progress.levels.Pack(pack).Level(b).isOpen)
			{
				return b - 1;
			}
		}
		return 9;
	}

	private int GetLastAvailablePack()
	{
		for (byte b = 2; b <= 4; b = (byte)(b + 1))
		{
			if (!Progress.levels.Pack(b).isOpen)
			{
				return b - 1;
			}
		}
		return 4;
	}

	public void ToLeft()
	{
		if (!isTeleporting)
		{
			activePack--;
			View.FocusedOnPack(activePack, 50f);
		}
	}

	public void ToRight()
	{
		if (!isTeleporting)
		{
			activePack++;
			View.FocusedOnPack(activePack, 50f);
		}
	}

	public void BuyOpenAllLevels()
	{
		UnityEngine.Debug.Log("BuyOpenAllLevels");
	}

	public void ShowMoreGames()
	{
	}

	public void OnShopBtnClick()
	{
		GUI_shop.instance.ShowShop();
		StartCoroutine(checkChangedCar());
	}

	private IEnumerator checkChangedCar()
	{
		while (Game.currentState == Game.gameState.Shop)
		{
			yield return null;
		}
		View.carAlfa.gameObject.SetActive(Progress.shop.activeCar != 1);
		View.tankAlfa.gameObject.SetActive(Progress.shop.activeCar == 1);
	}

	public void OnBackButtonClick()
	{
		Progress.Save(Progress.SaveType.Levels);
		Game.LoadLevel("Menu");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			activePack = 1;
			View.FocusedOnPack(1);
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			activePack = 2;
			View.FocusedOnPack(2);
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			activePack = 3;
			View.FocusedOnPack(3);
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			activePack = 4;
			View.FocusedOnPack(4);
		}
	}

	public void ShowBuyLevelsPanel()
	{
		if (!pTeleporting)
		{
			View.SetBuyLevelsPanel((!Progress.levels.Pack(activePack).Level(1).isOpen) ? "open first level" : "open next level");
		}
	}

	public void TeleportCar(int toPack, int toLevel, Action callback = null)
	{
		if (!pTeleporting)
		{
			if (previousActivePack == toPack && activeLevel == toLevel)
			{
				callback?.Invoke();
				return;
			}
			levelPackCheck[previousActivePack].IconCheck(activeLevel).StopRotation();
			pTeleporting = true;
			View.TeleportCarFromTo(previousActivePack, activeLevel, toPack, toLevel, delegate
			{
				pTeleporting = false;
				levelPackCheck[toPack].IconCheck(toLevel).StartRotation();
			}, callback);
			activePack = (previousActivePack = toPack);
			activeLevel = toLevel;
		}
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	public void ShowBuyCanvasWindow(bool isCoins = false)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false)
	{
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		ShowGetCoinsAndFuel();
	}

	public void ShowGetCoinsAndFuel()
	{
		_shopWindowModel.gameObject.SetActive(value: true);
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		View.SetTransperent(enable);
		if (!enable)
		{
			GameObject gameObject = GameObject.Find("ShopCanvasWindows");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			_shopWindowModel = null;
		}
	}
}

using Smokoko.DebugModule;
using System;
using UnityEngine;

public class ShopWindowModel : IShopWindowController
{
	private Progress.Shop _gameProgress;

	private Progress.GameEnergy _gameEnergy;

	private Action<Progress.Shop> _gameCallback;

	private Action<Progress.GameEnergy> _energyCallback;

	private Action _closeCallback;

	public Action _pcloseCallback;

	private IShopWindowView view;

	private float dt = -1f;

	public ShopWindowModel(IShopWindowView view)
	{
		this.view = view;
		view.Init(this);
	}

	public void OnCoinsButtonClick()
	{
		ShowCoins();
	}

	public void OnFuelButtonClick()
	{
		ShowFuel();
	}

	public void OnCloseButtonClick()
	{
		HideMainPanel();
		if (_gameCallback != null)
		{
			_gameCallback(_gameProgress);
		}
		if (_closeCallback != null)
		{
			_closeCallback();
		}
		if (_pcloseCallback != null)
		{
			_pcloseCallback();
		}
	}

	public void OnCloseVideoWindowButtonClick()
	{
		Game.PopInnerState("no_video", executeClose: false);
		view.SetVideoWindow(turn: false);
	}

	public void OnBuyCoinsClick(string index)
	{
		int current = int.Parse(index);
		switch (current)
		{
		case 1:
			ShowVideo();
			break;
		case 2:
		case 3:
		case 4:
		case 5:
			if (DebugFacade.isDebug)
			{
				ModalWindow.instance.Show("Buy coins pack " + current.ToString(), new ButtonInfo("Buy", delegate
				{
					OnMoneytierBought(current);
				}));
			}
			else
			{
				BuyCoinsForRealMoney(current);
			}
			break;
		}
	}

	public void OnBuyFuelClick(string index)
	{
		int num = int.Parse(index) - 1;
		switch (num)
		{
		case 4:
			BuyFuelByRealMoney();
			break;
		case 1:
			Progress.shop.timerForFuel = DateTime.Now.ToString();
			GameEnergyLogic.AddFuel(GameEnergyLogic.Values[1]);
			AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "free fuel");
			break;
		default:
			if (AddFuelForCoins(num))
			{
				Audio.PlayAsync("fuel");
			}
			else
			{
				ShowCoins();
			}
			break;
		}
	}

	private void BuyFuelByRealMoney()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy unlimited fuel", new ButtonInfo("Buy", OnFuelBought));
		}
		else
		{
			InAppManager.instance.Purchase(InAppSettings.Purchases.UnlimitedFuel, OnFuelBought);
		}
	}

	private void OnFuelBought()
	{
		Audio.PlayAsync("fuel");
		_gameEnergy.isInfinite = true;
		Progress.gameEnergy = _gameEnergy;
		GameEnergyLogic.Reset();
		view.SetInfinityIcon(turn: true);
		if (_energyCallback != null)
		{
			_energyCallback(_gameEnergy);
		}
		Progress.review.atLeastOnePurchase = true;
	}

	public void ShowCoins()
	{
		view.ShowCoins();
	}

	public void ShowFuel()
	{
		view.ShowFuel(CheckFuel());
		view.SetFuelPricesAndValues();
	}

	public void ShowMainPanel(bool isCoinsPanelShow, Progress.Shop gameProgress, Progress.GameEnergy gameEnergy, Action<Progress.Shop> gameCallBack, Action<Progress.GameEnergy> energyCallBack, Action closeCallback)
	{
		_gameProgress = gameProgress;
		_gameEnergy = gameEnergy;
		_gameCallback = gameCallBack;
		_energyCallback = energyCallBack;
		_closeCallback = closeCallback;
		view.ShowMainPanel(CheckFuel(), _gameProgress.currency, isCoinsPanelShow);
		view.SetCurrencyText(_gameProgress.currency);
		if (_gameEnergy.isInfinite)
		{
			view.SetInfinityIcon(turn: true);
		}
		else
		{
			view.SetFuelText(_gameEnergy.energy);
		}
		Game.PushInnerState("shop", HideMainPanel);
	}

	public void HideMainPanel()
	{
		Game.PopInnerState("shop", executeClose: false);
		view.HideMainPanel();
	}

	private void BuyCoinsForRealMoney(int index)
	{
		switch (index)
		{
		case 2:
			InAppManager.instance.Purchase(InAppSettings.Purchases.Rubies1, delegate
			{
				OnMoneytierBought(2);
			});
			break;
		case 3:
			InAppManager.instance.Purchase(InAppSettings.Purchases.Rubies2, delegate
			{
				OnMoneytierBought(3);
			});
			break;
		case 4:
			InAppManager.instance.Purchase(InAppSettings.Purchases.Rubies3, delegate
			{
				OnMoneytierBought(4);
			});
			break;
		case 5:
			InAppManager.instance.Purchase(InAppSettings.Purchases.Rubies4, delegate
			{
				OnMoneytierBought(5);
			});
			break;
		}
	}

	private void OnMoneytierBought(int current)
	{
		Audio.PlayAsync("shop_purchase");
		switch (current)
		{
		case 2:
			_gameProgress.currency += PriceConfig.instance.currency.coinsPack2;
			break;
		case 3:
			_gameProgress.currency += PriceConfig.instance.currency.coinsPack3;
			break;
		case 4:
			_gameProgress.currency += PriceConfig.instance.currency.coinsPack4;
			break;
		case 5:
			_gameProgress.currency += PriceConfig.instance.currency.coinsPack5;
			break;
		}
		if (_gameCallback != null)
		{
			view.SetCurrencyText(_gameProgress.currency);
			_gameCallback(_gameProgress);
		}
		Progress.review.atLeastOnePurchase = true;
	}

	private bool CheckFuel()
	{
		return GameEnergyLogic.isEnergyFull;
	}

	private void ShowVideo()
	{
		_gameProgress.currency += PriceConfig.instance.currency.coinsPack1;
		Progress.shop.timerForRuby = DateTime.Now.ToString();
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "free coins");
	}

	private void VideoClosed(string location, string reason)
	{
	}

	private void VideoAdsIsWatched(int k)
	{
		Audio.ResumeBackgroundMusic();
		Audio.Resume();
		Audio.PlayAsync("shop_purchase");
		_gameProgress.currency += PriceConfig.instance.currency.coinsPack1;
		if (_gameCallback != null)
		{
			view.SetCurrencyText(_gameProgress.currency);
			_gameCallback(_gameProgress);
		}
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "free coins");
	}

	private bool AddFuelForCoins(int current)
	{
		if (_gameProgress.currency >= GameEnergyLogic.Prices[current])
		{
			UnityEngine.Debug.Log("Minus currency !!!!!");
			_gameProgress.currency -= GameEnergyLogic.Prices[current];
			GameEnergyLogic.AddFuel(GameEnergyLogic.Values[current]);
			if (_gameCallback != null)
			{
				view.SetCurrencyText(_gameProgress.currency);
				_gameCallback(_gameProgress);
			}
			if (_energyCallback != null)
			{
				view.SetFuelText(_gameEnergy.energy);
				_energyCallback(_gameEnergy);
			}
			return true;
		}
		return false;
	}
}

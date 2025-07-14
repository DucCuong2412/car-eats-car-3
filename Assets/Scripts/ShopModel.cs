using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{
	public class CarState
	{
		public class UpgradesLevels
		{
			public int activeLevel;

			public List<int> prices = new List<int>();
		}

		public class Gadgets
		{
			public int price;

			public bool[] bougth = new bool[5];
		}

		public class Premium
		{
			public bool[] bougth = new bool[6];

			public bool[] equipped = new bool[6];
		}

		public int _price;

		public bool bought;

		public UpgradesLevels[] upgrades = new UpgradesLevels[6];

		public Gadgets[] gadgets = new Gadgets[5];

		public Premium premium = new Premium();

		public UpgradesLevels getUpgrade(int i)
		{
			if (upgrades[i] == null)
			{
				upgrades[i] = new UpgradesLevels();
			}
			return upgrades[i];
		}

		public Gadgets getGadget(int i)
		{
			if (gadgets[i] == null)
			{
				gadgets[i] = new Gadgets();
			}
			return gadgets[i];
		}
	}

	private ShopView _shopView;

	private int _coins;

	private bool isUpg = true;

	public CarState[] _car = new CarState[3];

	private int _currentCar;

	public UpgControl[] upgValue = new UpgControl[6];

	private int _currentUpg;

	private int _currentGad;

	public bool IsCarSpawned;

	private Car2DController _car2d;

	private Car2DController _premiumCar;

	private Car2DController _harwester;

	public List<GameObject> cars = new List<GameObject>();

	public List<Car2DController> Car2D = new List<Car2DController>();

	public bool isShopOpen;

	private ShopWindowModel _shopWindowModel;

	private int _energyCounter;

	private Progress.Shop progressShop;

	public byte currentPack;

	public Action callback;

	private bool _isEnergyInfinite;

	private bool prInited;

	private bool Inited;

	public Action onHideCallback;

	public ShopView shopView
	{
		get
		{
			if (_shopView == null)
			{
				_shopView = (ShopView)UnityEngine.Object.FindObjectOfType(typeof(ShopView));
			}
			return _shopView;
		}
	}

	public int Coins
	{
		get
		{
			return _coins;
		}
		set
		{
			_coins = value;
		}
	}

	public CarState CurrentCarState
	{
		get
		{
			if (_currentCar == 2)
			{
				Car(2);
			}
			return _car[_currentCar];
		}
	}

	public int CurrentCar
	{
		get
		{
			return _currentCar;
		}
		set
		{
			_currentCar = value;
			OnCarChange();
		}
	}

	public void HideShop()
	{
		shopView.StopAllCuruts();
		GUI_shop.instance.HideShop();
	}

	public CarState Car(int i)
	{
		if (_car[i] == null)
		{
			_car[i] = new CarState();
		}
		return _car[i];
	}

	private IEnumerator InitShopCanvasWindows()
	{
		UnityEngine.Object.Instantiate(Resources.Load("ShopCanvasWindows"));
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<ShopWindowView>().gameObject;
		IShopWindowView view = (IShopWindowView)go.GetComponent(typeof(IShopWindowView));
		_shopWindowModel = new ShopWindowModel(view);
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
		SetShopCanvasTransparent(enable: true);
		ShowGetCoinsAndFuel();
	}

	private void ShowGetCoinsAndFuel()
	{
		UnityEngine.Debug.Log("Minus currency !!!!!");
		progressShop.currency = Coins;
		_shopWindowModel.ShowMainPanel(isCoinsPanelShow: true, progressShop, Progress.gameEnergy, delegate
		{
			Coins = progressShop.currency;
			_shopView.SetCoinsLabel(Coins);
		}, delegate
		{
			if (!Progress.gameEnergy.isInfinite)
			{
				_shopView.SetEnergyLabel(Progress.gameEnergy.energy);
				_isEnergyInfinite = Progress.gameEnergy.isInfinite;
			}
			else
			{
				_shopView.SetFuelInfinytyIcon(enable: true);
			}
		}, delegate
		{
			SetShopCanvasTransparent(enable: false);
		});
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		_shopView.SetCanvasTransparent(enable);
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

	private void Update()
	{
		UpdateEnergyLabel();
	}

	private void UpdateEnergyLabel()
	{
		if (shopView.shopContent.activeSelf && !_isEnergyInfinite)
		{
			_energyCounter = GameEnergyLogic.GetEnergy;
			_shopView.SetEnergyLabel(_energyCounter);
		}
	}

	public void InitShop(Progress.Shop pShop, byte curPack, Action callback)
	{
		progressShop = pShop;
		currentPack = curPack;
		this.callback = callback;
		shopView.shopContent.SetActive(value: true);
		AddUpgPrices();
		AddGadgetsPrices();
		for (int i = 1; i <= 3; i++)
		{
			cars.Add(null);
			Car2D.Add(null);
		}
		shopView.shopContent.SetActive(value: false);
	}

	public void ShowShop()
	{
		shopView.ShowShop();
		_coins = progressShop.currency;
		SetPanel(Coins);
		shopView.SetCoinsLabel(Coins);
		_isEnergyInfinite = Progress.gameEnergy.isInfinite;
		if (_isEnergyInfinite)
		{
			_shopView.SetFuelInfinytyIcon(enable: true);
		}
		shopView.SetPremiumButtonIcon(IsShowPremiumItemIcon());
	}

	public void ShowShopPremium()
	{
		shopView.ShowShopPremium();
		_coins = progressShop.currency;
		SetPanel(Coins);
		shopView.shopContent.SetActive(value: false);
		shopView.SetCoinsLabel(Coins);
		_isEnergyInfinite = Progress.gameEnergy.isInfinite;
		if (_isEnergyInfinite)
		{
			_shopView.SetFuelInfinytyIcon(enable: true);
		}
		shopView.SetPremiumButtonIcon(IsShowPremiumItemIcon());
		ShowPremiumPanel();
	}

	private bool IsShowPremiumItemIcon()
	{
		for (int i = 1; i < 4; i++)
		{
			if (!Car(0).premium.bougth[i] || !Car(1).bought)
			{
				return true;
			}
		}
		return false;
	}

	private void SetPanel(int rubins)
	{
		if (CurrentCar == 2)
		{
			for (int i = 0; i < 5; i++)
			{
				CurrentCarState.getGadget(i);
			}
			AddGadgetsPrices();
		}
		int num = 0;
		while (true)
		{
			if (num < CurrentCarState.gadgets.Length)
			{
				if (rubins >= CurrentCarState.gadgets[num].price && !CurrentCarState.gadgets[num].bougth[num])
				{
					break;
				}
				isUpg = true;
				shopView.ChangePanels();
				num++;
				continue;
			}
			return;
		}
		isUpg = false;
		shopView.ChangePanels(isUpg);
	}

	public void OnSwapBtnClick()
	{
		isUpg = !isUpg;
		shopView.ChangePanels(isUpg);
		if (!isUpg)
		{
			shopView.StartVideoButsCurut();
		}
		else
		{
			shopView.StopAllCuruts();
		}
	}

	public void CheckForSpawn(int target)
	{
		foreach (GameObject car in cars)
		{
			if (car != null)
			{
				car.SetActive(value: false);
			}
		}
		if (cars.Count <= target || cars[target] == null)
		{
			SpawnCar(target);
		}
		cars[target].SetActive(value: true);
		switch (target)
		{
		case 0:
			_car2d = cars[target].GetComponent<Car2DController>();
			InitCar2D();
			return;
		case 1:
			_premiumCar = cars[target].GetComponent<Car2DController>();
			if (!prInited)
			{
				_premiumCar.Init(progressShop.Cars[1]);
			}
			prInited = true;
			return;
		}
		_harwester = cars[target].GetComponent<Car2DController>();
		progressShop.Cars[2].id = 2;
		if (!prInited)
		{
			_harwester.Init(progressShop.Cars[2]);
		}
		prInited = true;
	}

	public void SpawnCar(int carNum)
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Car_Player_" + carNum));
		Transform carSpawnParentTransform = shopView.carSpawnParentTransform;
		Vector3 localScale = gameObject.transform.localScale;
		gameObject.transform.parent = carSpawnParentTransform;
		gameObject.transform.localPosition = new Vector2(0f, 0f);
		gameObject.transform.rotation = carSpawnParentTransform.rotation;
		gameObject.transform.localScale = localScale;
		gameObject.transform.position = carSpawnParentTransform.position;
		Car2D[carNum] = gameObject.GetComponent<Car2DController>();
		cars[carNum] = gameObject;
		foreach (Rigidbody2D item in Car2D[carNum].WheelsRigitbodies())
		{
			UnityEngine.Object.Destroy(item);
		}
		gameObject.SetActive(value: false);
	}

	private void InitCar2D()
	{
		if (!Inited)
		{
			_car2d.Init(progressShop.Cars[0]);
		}
		Inited = true;
	}

	public void AddUpgPrices()
	{
		ShopSettings.GetShopData();
		for (int i = 0; i < 6; i++)
		{
			Car(0).getUpgrade(0).prices.Add(ShopSettings.GetShopData().health[i]);
			Car(0).getUpgrade(1).prices.Add(ShopSettings.GetShopData().turbo[i]);
			Car(0).getUpgrade(2).prices.Add(ShopSettings.GetShopData().wheels[i]);
			Car(0).getUpgrade(3).prices.Add(ShopSettings.GetShopData().engine[i]);
			Car(0).getUpgrade(4).prices.Add(ShopSettings.GetShopData().weapons[i]);
		}
	}

	private void AddGadgetsPrices()
	{
		for (int i = 0; i < 5; i++)
		{
			CurrentCarState.getGadget(i).price = ShopSettings.GetShopData().gadgetsPrices[i];
		}
	}

	private void OnCarChange()
	{
		if (CurrentCarState != null && isUpg)
		{
			shopView.RefreshUpgradesProgress();
		}
	}

	public void OnUpgradeClick(GameObject go)
	{
		_currentUpg = int.Parse(go.name) - 1;
		if (Car(CurrentCar).upgrades[_currentUpg].activeLevel < 5)
		{
			UnityEngine.Debug.Log("_currentUpg " + _currentUpg + " | Car(CurrentCar).upgrades[_currentUpg].activeLevel " + Car(CurrentCar).upgrades[_currentUpg].activeLevel);
			int num = Car(0).upgrades[_currentUpg].prices[Car(0).upgrades[_currentUpg].activeLevel + 1];
			SetAnimationSkiner(_currentUpg, isShow: true);
			shopView.RefreshUpgradeImages(_currentUpg);
			Game.PushInnerState("shop_purchase", OnClosePanelBtnClick);
		}
	}

	public void OnGadgetClick(GameObject go)
	{
		_currentGad = int.Parse(go.name) - 1;
		SetAnimationSkiner(_currentGad, isShow: true);
		shopView.RefreshGadgetImages(_currentGad);
		Game.PushInnerState("shop_purchase", OnClosePanelBtnClick);
	}

	public void OnClosePanelBtnClick()
	{
		Game.PopInnerState("shop_purchase", executeClose: false);
		shopView.HideConfirmPanel();
		if (isUpg)
		{
			SetAnimationSkiner(_currentUpg);
		}
		else
		{
			SetAnimationSkiner(_currentGad);
		}
	}

	private void SetAnimationSkiner(int current, bool isShow = false)
	{
		if (isUpg)
		{
			switch (current)
			{
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			}
		}
		else if (isShow)
		{
			if (CurrentCar == 0)
			{
				_car2d.GadgetModule.ActivateGadget(current, isShow);
			}
			else if (CurrentCar == 1)
			{
				_premiumCar.GadgetModule.ActivateGadget(current, isShow);
			}
			else
			{
				_harwester.GadgetModule.ActivateGadget(current, isShow);
			}
			if (current == 4)
			{
				shopView.SetBombRecharger(show: true);
			}
		}
		else
		{
			if (CurrentCar == 0)
			{
				_car2d.GadgetModule.DisactivateGadget(current);
			}
			else if (CurrentCar == 1)
			{
				_premiumCar.GadgetModule.DisactivateGadget(current);
			}
			else
			{
				_harwester.GadgetModule.DisactivateGadget(current);
			}
			if (current == 4)
			{
				shopView.SetBombRecharger(show: false);
			}
		}
	}

	public void GudgetBuyed(int index)
	{
		Car(0).gadgets[_currentGad].bougth[_currentGad] = true;
		if (CurrentCar == 0)
		{
			_car2d.GadgetModule.ActivateGadget(Car(0).gadgets[_currentGad].bougth);
		}
		else if (CurrentCar == 0)
		{
			_premiumCar.GadgetModule.ActivateGadget(Car(0).gadgets[_currentGad].bougth);
		}
		else
		{
			_harwester.GadgetModule.ActivateGadget(Car(0).gadgets[_currentGad].bougth);
		}
		shopView.SetGadgetBought(_currentGad);
		shopView.HideConfirmPanel();
		Audio.PlayAsync("gui_shop_purchase_01_sn");
		Audio.PlayAsync("gui_shop_transformcar_01_sn");
	}

	public void Buy()
	{
		if (isUpg)
		{
			return;
		}
		if (Coins - Car(0).gadgets[_currentGad].price >= 0)
		{
			UnityEngine.Debug.Log("!!! Car(0).gadgets[_currentGad].bougth[_currentGad] = true;");
			shopView.HideConfirmPanel();
			string name = string.Empty;
			switch (_currentGad)
			{
			case 0:
				Progress.shop.Gadget1Time = DateTime.UtcNow;
				name = "gadjet0";
				break;
			case 1:
				Progress.shop.Gadget2Time = DateTime.UtcNow;
				name = "gadjet1";
				break;
			case 2:
				Progress.shop.Gadget3Time = DateTime.UtcNow;
				name = "gadjet2";
				break;
			case 3:
				Progress.shop.Gadget4Time = DateTime.UtcNow;
				name = "gadjet3";
				break;
			case 4:
				Progress.shop.Gadget5Time = DateTime.UtcNow;
				name = "gadjet4";
				break;
			}
			Progress.Notifications notifications = Progress.notifications;
			NotificationsWrapper.Clear(notifications.GetNotificationId(name));
			notifications.Remove(name);
			Audio.PlayAsync("gui_shop_purchase_01_sn");
			shopView.StartVideoButsCurut();
		}
		else
		{
			Audio.Play("gui_denied");
			StartCoroutine(LoadBuyCanvasWindow());
		}
	}

	public void GadgetTryBuy(int index)
	{
		if (Car(0).gadgets[index].bougth[index])
		{
			UnityEngine.Debug.Log("!!! LEX BUYED index " + index + " | CurrentCar " + CurrentCar);
			return;
		}
		string name = string.Empty;
		switch (index)
		{
		case 0:
			name = "gadjet0";
			break;
		case 1:
			name = "gadjet1";
			break;
		case 2:
			name = "gadjet2";
			break;
		case 3:
			name = "gadjet3";
			break;
		case 4:
			name = "gadjet4";
			break;
		}
		Progress.Notifications notifications = Progress.notifications;
		NotificationsWrapper.Clear(notifications.GetNotificationId(name));
		notifications.Remove(name);
		Car(0).gadgets[index].bougth[index] = true;
		if (CurrentCar == 0)
		{
			_car2d.GadgetModule.ActivateGadget(Car(0).gadgets[index].bougth);
		}
		else if (CurrentCar == 0)
		{
			_premiumCar.GadgetModule.ActivateGadget(Car(0).gadgets[index].bougth);
		}
		else
		{
			_harwester.GadgetModule.ActivateGadget(Car(0).gadgets[index].bougth);
		}
		shopView.SetGadgetBought(index);
		Audio.PlayAsync("gui_shop_transformcar_01_sn");
	}

	public void ShowPremiumPanel()
	{
		shopView.SetPremiumButtonIcon(isShowed: false);
		shopView.ShowPremiumPanel();
		RefreshPremiumPanel();
		shopView.StopAllCuruts();
	}

	public void RefreshPremiumPanel(int currentPremium = 0)
	{
		if (Car(0).premium.bougth[1] && Car(0).premium.bougth[2] && Car(0).premium.bougth[3] && Car(0).premium.bougth[0])
		{
			Car(0).premium.bougth[4] = true;
		}
		if (Car(0).premium.bougth[4])
		{
			bool flag = true;
			for (int i = 0; i < 4; i++)
			{
				if (!Car(0).premium.equipped[i])
				{
					flag = false;
				}
			}
			Car(0).premium.equipped[4] = flag;
		}
		shopView.SetPremiumButton(currentPremium, Car(0).premium.bougth[currentPremium], Car(0).premium.equipped[currentPremium]);
		if (currentPremium == 0)
		{
			RefreshPanzerButton();
		}
	}

	private void RefreshPanzerButton()
	{
		if (CurrentCar == 0 && Car(1).bought)
		{
			Car(0).premium.bougth[0] = true;
			Car(0).premium.equipped[0] = false;
			shopView.SetPremiumButton(0, bougth: true, isEquip: false);
		}
		if (CurrentCar == 0 && !Car(1).bought)
		{
			Car(0).premium.bougth[0] = false;
			Car(0).premium.equipped[0] = false;
			shopView.SetPremiumButton(0, bougth: false, isEquip: false);
		}
		if (CurrentCar == 1)
		{
			Car(0).premium.bougth[0] = true;
			Car(0).premium.equipped[0] = true;
			shopView.SetPremiumButton(0, bougth: true, isEquip: true);
		}
		if (CurrentCar == 2)
		{
			Car(0).premium.bougth[5] = true;
			Car(0).premium.equipped[5] = true;
			shopView.SetPremiumButton(5, bougth: true, isEquip: true);
			Car(0).premium.bougth[0] = false;
			Car(0).premium.equipped[0] = false;
			shopView.SetPremiumButton(0, bougth: false, isEquip: false);
		}
		else if (Car(0).premium.bougth[5])
		{
			Car(0).premium.bougth[5] = true;
			Car(0).premium.equipped[5] = false;
			shopView.SetPremiumButton(5, bougth: true, isEquip: false);
		}
	}

	public void BuyAllPremiumsForRealMoney()
	{
		if (Car(0).premium.bougth[4])
		{
			bool flag = !Car(0).premium.equipped[4];
			for (int i = 0; i < 5; i++)
			{
				Car(0).premium.equipped[i] = flag;
				shopView.SetPremiumButton(i, bougth: true, flag);
			}
			if (flag)
			{
				CurrentCar = 1;
			}
			else
			{
				CurrentCar = 0;
			}
		}
	}

	public void BuyPremiumItemForRealMoney(int _currentPremiumItem)
	{
		if (_currentPremiumItem == 4)
		{
			BuyAllPremiumsForRealMoney();
		}
		else if (!Car(0).premium.bougth[_currentPremiumItem])
		{
		}
	}

	private void BuyPremiumItem(int current)
	{
		if (!Car(0).premium.bougth[current])
		{
			Audio.PlayAsync("shop_purchase");
			Car(0).premium.bougth[current] = true;
			Car(0).premium.equipped[current] = true;
			SetPremiumItem(current, activate: true);
			shopView.SetPremiumButton(current, bougth: true, isEquip: true);
		}
		Progress.review.atLeastOnePurchase = true;
	}

	public void BuyAllPremiums()
	{
		for (int i = 0; i < 5; i++)
		{
			BuyPremiumItem(i);
		}
	}

	private void EquipUnequip(int current)
	{
		if (Car(0).premium.equipped[current])
		{
			Car(0).premium.equipped[current] = false;
			shopView.SetPremiumButton(current, bougth: true, isEquip: false);
			SetPremiumItem(current, activate: false);
		}
		else
		{
			Car(0).premium.equipped[current] = true;
			shopView.SetPremiumButton(current, bougth: true, isEquip: true);
			SetPremiumItem(current, activate: true);
		}
	}

	private void SetPremiumItem(int current, bool activate)
	{
		switch (current)
		{
		case 1:
			break;
		case 2:
			break;
		case 4:
			break;
		case 0:
			if (CurrentCar == 0 || CurrentCar == 2)
			{
				CurrentCar = 1;
				CurrentCarState.bought = true;
				Progress.shop.NowSelectCarNeedForMe = CurrentCar;
				CheckForSpawn(CurrentCar);
				for (int m = 2; m < 5; m++)
				{
					SetPremiumItem(m, Car(0).premium.equipped[m]);
				}
				for (int n = 0; n < CurrentCarState.gadgets.Length; n++)
				{
					if (Car(0).gadgets[n].bougth[n])
					{
						_premiumCar.GadgetModule.ActivateGadget(n, active: true);
					}
				}
				shopView.ChangeBombIcon(6);
			}
			else
			{
				if (CurrentCar != 1 && CurrentCar != 2)
				{
					break;
				}
				CurrentCar = 0;
				Progress.shop.NowSelectCarNeedForMe = CurrentCar;
				CheckForSpawn(CurrentCar);
				for (int num = 2; num < 5; num++)
				{
					SetPremiumItem(num, Car(0).premium.equipped[num]);
				}
				for (int num2 = 0; num2 < CurrentCarState.gadgets.Length; num2++)
				{
					if (Car(0).gadgets[num2].bougth[num2])
					{
						_car2d.GadgetModule.ActivateGadget(num2, active: true);
					}
				}
				shopView.ChangeBombIcon(Car(0).upgrades[4].activeLevel + 1);
			}
			break;
		case 3:
			if (activate)
			{
				if (CurrentCar == 0)
				{
					_car2d.ShowTurel();
				}
				else if (CurrentCar == 1)
				{
					_premiumCar.ShowTurel();
				}
				else
				{
					_harwester.ShowTurel();
				}
			}
			else if (CurrentCar == 0)
			{
				_car2d.HideTurel();
			}
			else if (CurrentCar == 1)
			{
				_premiumCar.HideTurel();
			}
			else
			{
				_harwester.HideTurel();
			}
			break;
		case 5:
			if (CurrentCar == 0 || CurrentCar == 1)
			{
				Progress.shop.carForGarage = CurrentCar;
				CurrentCar = 2;
				Progress.shop.NowSelectCarNeedForMe = CurrentCar;
				CurrentCarState.bought = true;
				CheckForSpawn(CurrentCar);
				for (int i = 2; i < 5; i++)
				{
					bool flag = Car(0).premium.equipped[i];
					SetPremiumItem(i, !flag);
					SetPremiumItem(i, flag);
				}
				for (int j = 0; j < 5; j++)
				{
					if (Car(0).gadgets[j].bougth[j])
					{
						_car2d.GadgetModule.ActivateGadget(j, active: false);
						_car2d.GadgetModule.ActivateGadget(j, active: true);
					}
				}
				shopView.ChangeBombIcon(6);
			}
			else
			{
				if (CurrentCar != 2)
				{
					break;
				}
				Progress.shop.carForGarage = 0;
				CurrentCar = Progress.shop.carForGarage;
				Progress.shop.NowSelectCarNeedForMe = CurrentCar;
				CheckForSpawn(CurrentCar);
				for (int k = 2; k < 5; k++)
				{
					bool flag2 = Car(0).premium.equipped[k];
					SetPremiumItem(k, !flag2);
					SetPremiumItem(k, flag2);
				}
				for (int l = 0; l < CurrentCarState.gadgets.Length; l++)
				{
					if (Car(0).gadgets[l].bougth[l])
					{
						_car2d.GadgetModule.ActivateGadget(l, active: false);
						_car2d.GadgetModule.ActivateGadget(l, active: true);
					}
				}
				shopView.ChangeBombIcon(Car(0).upgrades[4].activeLevel + 1);
			}
			break;
		}
	}

	private bool EnoughMoney(int coins, int price)
	{
		if (coins - price >= 0)
		{
			return true;
		}
		return false;
	}

	private void SetCoins(int newVal)
	{
		Coins = newVal;
		shopView.SetCoinsLabel(Coins);
	}

	private void LoadProgress(Progress.Shop pShop)
	{
		Car(0).bought = true;
		CurrentCar = pShop.activeCar;
		UnityEngine.Debug.Log("!!! LEX  CurrentCar " + CurrentCar);
		Car(1).bought = pShop.Cars[1].bought;
		Car(2).bought = pShop.Cars[2].bought;
		_coins = pShop.currency;
		Car(0).getUpgrade(0).activeLevel = pShop.Cars[0].healthActLev;
		Car(0).getUpgrade(1).activeLevel = pShop.Cars[0].turboActLev;
		Car(0).getUpgrade(2).activeLevel = pShop.Cars[0].wheelsActLev;
		Car(0).getUpgrade(3).activeLevel = pShop.Cars[0].engineActLev;
		Car(0).getUpgrade(4).activeLevel = pShop.Cars[0].weaponActLev;
		Car(1).getUpgrade(0).activeLevel = (pShop.Cars[1].healthActLev = 5);
		Car(1).getUpgrade(1).activeLevel = (pShop.Cars[1].turboActLev = 5);
		Car(1).getUpgrade(2).activeLevel = (pShop.Cars[1].wheelsActLev = 5);
		Car(1).getUpgrade(3).activeLevel = (pShop.Cars[1].engineActLev = 5);
		Car(1).getUpgrade(4).activeLevel = (pShop.Cars[1].weaponActLev = 5);
		Car(2).getUpgrade(0).activeLevel = (pShop.Cars[1].healthActLev = 5);
		Car(2).getUpgrade(1).activeLevel = (pShop.Cars[1].turboActLev = 5);
		Car(2).getUpgrade(2).activeLevel = (pShop.Cars[1].wheelsActLev = 5);
		Car(2).getUpgrade(3).activeLevel = (pShop.Cars[1].engineActLev = 5);
		Car(2).getUpgrade(4).activeLevel = (pShop.Cars[1].weaponActLev = 5);
		for (int i = 0; i < 5; i++)
		{
			Car(0).gadgets[i].bougth[i] = pShop.Cars[0].boughtGadgets[i];
			Car(1).getGadget(i).bougth[i] = pShop.Cars[0].boughtGadgets[i];
			Car(2).getGadget(i).bougth[i] = pShop.Cars[0].boughtGadgets[i];
		}
		for (int j = 0; j < 5; j++)
		{
			bool[] bougth = Car(1).premium.bougth;
			int num = j;
			bool flag;
			Car(0).premium.bougth[j] = (flag = (pShop.Cars[0].premiumBougth.Length >= j && pShop.Cars[0].premiumBougth[j]));
			bougth[num] = flag;
			bool[] equipped = Car(1).premium.equipped;
			int num2 = j;
			Car(0).premium.equipped[j] = (flag = (pShop.Cars[0].premiumEquipped.Length >= j && pShop.Cars[0].premiumEquipped[j]));
			equipped[num2] = flag;
			bool[] bougth2 = Car(2).premium.bougth;
			int num3 = j;
			Car(0).premium.bougth[j] = (flag = (pShop.Cars[0].premiumBougth.Length >= j && pShop.Cars[0].premiumBougth[j]));
			bougth2[num3] = flag;
			bool[] equipped2 = Car(2).premium.equipped;
			int num4 = j;
			Car(0).premium.equipped[j] = (flag = (pShop.Cars[0].premiumEquipped.Length >= j && pShop.Cars[0].premiumEquipped[j]));
			equipped2[num4] = flag;
		}
	}

	private void SaveProgress()
	{
		progressShop.Cars[0].healthActLev = Car(0).getUpgrade(0).activeLevel;
		progressShop.Cars[0].turboActLev = Car(0).getUpgrade(1).activeLevel;
		progressShop.Cars[0].wheelsActLev = Car(0).getUpgrade(2).activeLevel;
		progressShop.Cars[0].engineActLev = Car(0).getUpgrade(3).activeLevel;
		progressShop.Cars[0].weaponActLev = Car(0).getUpgrade(4).activeLevel;
		progressShop.Cars[1].healthActLev = (Car(1).getUpgrade(0).activeLevel = 5);
		progressShop.Cars[1].turboActLev = (Car(1).getUpgrade(1).activeLevel = 5);
		progressShop.Cars[1].wheelsActLev = (Car(1).getUpgrade(2).activeLevel = 5);
		progressShop.Cars[1].engineActLev = (Car(1).getUpgrade(3).activeLevel = 5);
		progressShop.Cars[1].weaponActLev = (Car(1).getUpgrade(4).activeLevel = 5);
		progressShop.Cars[2].healthActLev = (Car(1).getUpgrade(0).activeLevel = 5);
		progressShop.Cars[2].turboActLev = (Car(1).getUpgrade(1).activeLevel = 5);
		progressShop.Cars[2].wheelsActLev = (Car(1).getUpgrade(2).activeLevel = 5);
		progressShop.Cars[2].engineActLev = (Car(1).getUpgrade(3).activeLevel = 5);
		progressShop.Cars[2].weaponActLev = (Car(1).getUpgrade(4).activeLevel = 5);
		for (int i = 0; i < 5; i++)
		{
			progressShop.Cars[0].boughtGadgets[i] = Car(0).gadgets[i].bougth[i];
			progressShop.Cars[1].boughtGadgets[i] = Car(0).gadgets[i].bougth[i];
			progressShop.Cars[2].boughtGadgets[i] = Car(0).gadgets[i].bougth[i];
		}
		for (int j = 0; j < 5; j++)
		{
			bool[] premiumBougth = progressShop.Cars[1].premiumBougth;
			int num = j;
			bool flag;
			progressShop.Cars[0].premiumBougth[j] = (flag = Car(0).premium.bougth[j]);
			premiumBougth[num] = flag;
			bool[] premiumEquipped = progressShop.Cars[1].premiumEquipped;
			int num2 = j;
			progressShop.Cars[0].premiumEquipped[j] = (flag = Car(0).premium.equipped[j]);
			premiumEquipped[num2] = flag;
			bool[] premiumBougth2 = progressShop.Cars[2].premiumBougth;
			int num3 = j;
			progressShop.Cars[0].premiumBougth[j] = (flag = Car(0).premium.bougth[j]);
			premiumBougth2[num3] = flag;
			bool[] premiumEquipped2 = progressShop.Cars[2].premiumEquipped;
			int num4 = j;
			progressShop.Cars[0].premiumEquipped[j] = (flag = Car(0).premium.equipped[j]);
			premiumEquipped2[num4] = flag;
		}
		progressShop.activeCar = CurrentCar;
		progressShop.Cars[1].bought = Car(1).bought;
		progressShop.Cars[2].bought = Car(2).bought;
		UnityEngine.Debug.Log("Minus currency !!!!!");
		progressShop.currency = _coins;
		Progress.shop = progressShop;
		Progress.Save(Progress.SaveType.Shop);
	}

	public void AddCoinsCheat(int count)
	{
		Coins += count;
		shopView.SetCoinsLabel(Coins);
	}

	public void AddCoinsForReal(int count)
	{
		Coins += count;
		shopView.SetCoinsLabel(Coins);
	}

	public void CloseShop(Action onHideCallback)
	{
		this.onHideCallback = onHideCallback;
		_shopView.StopRotateCogWheels();
		_shopView.HideShop();
		SaveProgress();
		if (callback != null)
		{
			callback();
		}
	}

	public bool AddFuelForCoins(int num)
	{
		if (Coins >= GameEnergyLogic.Prices[num])
		{
			Coins -= GameEnergyLogic.Prices[num];
			shopView.SetCoinsLabel(Coins);
			GameEnergyLogic.AddFuel(GameEnergyLogic.Values[num]);
			return true;
		}
		return false;
	}
}

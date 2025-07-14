using UnityEngine;
using UnityEngine.UI;

public class ControllerBtnUpgrate : MonoBehaviour
{
	public Button _btnArmor;

	public Button _btnSpeed;

	public Button _btnDamage;

	public Button _btnTurbo;

	public ControllerGarage CG;

	public ShopStatsForGarage SSFG;

	public ControllerStars CS;

	private void OnEnable()
	{
		_btnArmor.onClick.AddListener(Armor);
		_btnSpeed.onClick.AddListener(Speed);
		_btnDamage.onClick.AddListener(Damage);
		_btnTurbo.onClick.AddListener(Turbo);
		if (Progress.shop.activeCar >= 0)
		{
			if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 3)
			{
				_btnArmor.gameObject.SetActive(value: false);
			}
			else
			{
				_btnArmor.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 3)
			{
				_btnSpeed.gameObject.SetActive(value: false);
			}
			else
			{
				_btnSpeed.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 3)
			{
				_btnDamage.gameObject.SetActive(value: false);
			}
			else
			{
				_btnDamage.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 3)
			{
				_btnTurbo.gameObject.SetActive(value: false);
			}
			else
			{
				_btnTurbo.gameObject.SetActive(value: true);
			}
		}
	}

	private void OnDisable()
	{
		_btnArmor.onClick.RemoveAllListeners();
		_btnSpeed.onClick.RemoveAllListeners();
		_btnDamage.onClick.RemoveAllListeners();
		_btnTurbo.onClick.RemoveAllListeners();
	}

	private void Update()
	{
		if (Progress.shop.activeCar >= 0)
		{
			if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 3)
			{
				_btnArmor.gameObject.SetActive(value: false);
			}
			else
			{
				_btnArmor.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 3)
			{
				_btnSpeed.gameObject.SetActive(value: false);
			}
			else
			{
				_btnSpeed.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 3)
			{
				_btnDamage.gameObject.SetActive(value: false);
			}
			else
			{
				_btnDamage.gameObject.SetActive(value: true);
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 3)
			{
				_btnTurbo.gameObject.SetActive(value: false);
			}
			else
			{
				_btnTurbo.gameObject.SetActive(value: true);
			}
		}
	}

	private void Armor()
	{
		Audio.Play("gui_button_02_sn");
		if (ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[Progress.shop.Cars[Progress.shop.activeCar].healthActLev].price <= Progress.shop.currency)
		{
			if (Progress.shop.activeCar == 0)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car1", "armor");
			}
			else if (Progress.shop.activeCar == 1)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car2", "armor");
			}
			else if (Progress.shop.activeCar == 2)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car3", "armor");
			}
			Audio.PlayAsync("grage_transform_upgrade");
			Audio.Play("gui_shop_purchase_01_sn");
			Progress.shop.currency -= ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[Progress.shop.Cars[Progress.shop.activeCar].healthActLev].price;
			CG.controllers_Cars[Progress.shop.activeCar].animHull.Change();
			if (CG.controllers_Cars[Progress.shop.activeCar].animHull2 != null)
			{
				CG.controllers_Cars[Progress.shop.activeCar].animHull2.Change();
			}
			SSFG.ARMOR();
			CS.ArmorPre();
			if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev < 3)
			{
				Progress.shop.Cars[Progress.shop.activeCar].healthActLev++;
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 3)
			{
				_btnArmor.gameObject.SetActive(value: false);
			}
			else
			{
				_btnArmor.gameObject.SetActive(value: true);
			}
			CG.SetCoinsLabel(Progress.shop.currency);
			CG.ArmorPrice();
		}
		else
		{
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
	}

	public void Speed()
	{
		Audio.Play("gui_button_02_sn");
		if (ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[Progress.shop.Cars[Progress.shop.activeCar].engineActLev].price <= Progress.shop.currency)
		{
			if (Progress.shop.activeCar == 0)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car1", "speed");
			}
			else if (Progress.shop.activeCar == 1)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car2", "speed");
			}
			else if (Progress.shop.activeCar == 2)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car3", "speed");
			}
			Audio.Play("gui_shop_purchase_01_sn");
			Audio.PlayAsync("grage_transform_upgrade");
			Progress.shop.currency -= ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[Progress.shop.Cars[Progress.shop.activeCar].engineActLev].price;
			CG.controllers_Cars[Progress.shop.activeCar].animHull.Change();
			if (CG.controllers_Cars[Progress.shop.activeCar].animHull2 != null)
			{
				CG.controllers_Cars[Progress.shop.activeCar].animHull2.Change();
			}
			CG.controllers_Cars[Progress.shop.activeCar].animEngine.Change();
			CG.controllers_Cars[Progress.shop.activeCar].animWheel.Change();
			SSFG.SPEED();
			CS.speedPre();
			if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev < 3)
			{
				Progress.shop.Cars[Progress.shop.activeCar].engineActLev++;
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 3)
			{
				_btnSpeed.gameObject.SetActive(value: false);
			}
			else
			{
				_btnSpeed.gameObject.SetActive(value: true);
			}
			CG.SetCoinsLabel(Progress.shop.currency);
			CG.SpeedPrice();
		}
		else
		{
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
	}

	private void Damage()
	{
		Audio.Play("gui_button_02_sn");
		if (ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[Progress.shop.Cars[Progress.shop.activeCar].weaponActLev].price <= Progress.shop.currency)
		{
			if (Progress.shop.activeCar == 0)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car1", "damage");
			}
			else if (Progress.shop.activeCar == 1)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car2", "damage");
			}
			else if (Progress.shop.activeCar == 2)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car3", "damage");
			}
			Audio.PlayAsync("grage_transform_upgrade");
			Audio.Play("gui_shop_purchase_01_sn");
			SSFG.WEAPON();
			CS.damagePre();
			Progress.shop.currency -= ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[Progress.shop.Cars[Progress.shop.activeCar].weaponActLev].price;
			CG.controllers_Cars[Progress.shop.activeCar].animWeapon.Change();
			if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev < 3)
			{
				Progress.shop.Cars[Progress.shop.activeCar].weaponActLev++;
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 3)
			{
				_btnDamage.gameObject.SetActive(value: false);
			}
			else
			{
				_btnDamage.gameObject.SetActive(value: true);
			}
			CG.SetCoinsLabel(Progress.shop.currency);
			CG.WeaponPrice();
		}
		else
		{
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
	}

	private void Turbo()
	{
		Audio.Play("gui_button_02_sn");
		if (ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[Progress.shop.Cars[Progress.shop.activeCar].turboActLev].price <= Progress.shop.currency)
		{
			if (Progress.shop.activeCar == 0)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car1", "turbo");
			}
			else if (Progress.shop.activeCar == 1)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car2", "turbo");
			}
			else if (Progress.shop.activeCar == 2)
			{
				AnalyticsManager.LogEvent(EventCategoty.upgrades, "car3", "turbo");
			}
			Audio.PlayAsync("grage_transform_upgrade");
			Audio.Play("gui_shop_purchase_01_sn");
			Progress.shop.currency -= ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[Progress.shop.Cars[Progress.shop.activeCar].turboActLev].price;
			CG.controllers_Cars[Progress.shop.activeCar].animTurbo.Change();
			SSFG.TURBO();
			CS.turboPre();
			if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev < 3)
			{
				Progress.shop.Cars[Progress.shop.activeCar].turboActLev++;
			}
			if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 3)
			{
				_btnTurbo.gameObject.SetActive(value: false);
			}
			else
			{
				_btnTurbo.gameObject.SetActive(value: true);
			}
			CG.TurboPrice();
			CG.SetCoinsLabel(Progress.shop.currency);
		}
		else
		{
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
	}
}

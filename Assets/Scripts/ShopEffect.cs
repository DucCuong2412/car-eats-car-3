using UnityEngine;

public class ShopEffect : MonoBehaviour
{
	public GameObject Effect;

	private void Update()
	{
		if (Progress.shop.activeCar >= 0)
		{
			SetButton();
		}
	}

	public void SetButton()
	{
		Effect.SetActive(CheckPrices());
	}

	private bool CheckPrices()
	{
		if (!Progress.shop.Car.bomb_0_bounght && ShopManagerPrice.instance.Price.Bomb[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.bomb_1_bounght && ShopManagerPrice.instance.Price.Bomb[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.bomb_2_bounght && ShopManagerPrice.instance.Price.Bomb[2].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.bomb_3_bounght && ShopManagerPrice.instance.Price.Bomb[3].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.bomb_4_bounght && ShopManagerPrice.instance.Price.Bomb[4].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.Gadget_Magnet_bounght && ShopManagerPrice.instance.Price.Gadget[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.Gadget_EMP_bounght && ShopManagerPrice.instance.Price.Gadget[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.Gadget_LEDOLUCH_bounght && ShopManagerPrice.instance.Price.Gadget[2].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.Gadget_MISSLLE_bounght && ShopManagerPrice.instance.Price.Gadget[3].price <= Progress.shop.currency)
		{
			return true;
		}
		if (!Progress.shop.Car.Gadget_RECHARGER_bounght && ShopManagerPrice.instance.Price.Gadget[4].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.healthActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.healthActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.healthActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[2].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.engineActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.engineActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.engineActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[2].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.turboActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.turboActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.turboActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[2].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.weaponActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[0].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.weaponActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[1].price <= Progress.shop.currency)
		{
			return true;
		}
		if (Progress.shop.Car.weaponActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[2].price <= Progress.shop.currency)
		{
			return true;
		}
		return false;
	}
}

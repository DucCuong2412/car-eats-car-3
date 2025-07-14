using UnityEngine;

public class MarkerGarage : MonoBehaviour
{
	public GameObject armor;

	public GameObject speed;

	public GameObject turbo;

	public GameObject damage;

	public GameObject gadgets;

	public GameObject bomb;

	[Header("BOMBS")]
	public GameObject bomb1;

	public GameObject bomb2;

	public GameObject bomb3;

	public GameObject bomb4;

	public GameObject bomb5;

	[Header("GADJETS")]
	public GameObject gadgets1;

	public GameObject gadgets2;

	public GameObject gadgets3;

	public GameObject gadgets4;

	public GameObject gadgets5;

	private void OnEnable()
	{
		armor.SetActive(value: false);
		speed.SetActive(value: false);
		turbo.SetActive(value: false);
		damage.SetActive(value: false);
		gadgets.SetActive(value: false);
		bomb.SetActive(value: false);
		bomb1.SetActive(value: false);
		bomb2.SetActive(value: false);
		bomb3.SetActive(value: false);
		bomb4.SetActive(value: false);
		bomb5.SetActive(value: false);
		gadgets1.SetActive(value: false);
		gadgets2.SetActive(value: false);
		gadgets3.SetActive(value: false);
		gadgets4.SetActive(value: false);
		gadgets5.SetActive(value: false);
		MarkerBomb();
		MarkerGadget();
		MarkerSpeed();
		MarkerTurbo();
		MarkerDamage();
		MarkerArmor();
		MarkerBombs();
		MarkerGadgets();
	}

	public void up()
	{
		armor.SetActive(value: false);
		speed.SetActive(value: false);
		turbo.SetActive(value: false);
		damage.SetActive(value: false);
		gadgets.SetActive(value: false);
		bomb.SetActive(value: false);
		bomb1.SetActive(value: false);
		bomb2.SetActive(value: false);
		bomb3.SetActive(value: false);
		bomb4.SetActive(value: false);
		bomb5.SetActive(value: false);
		gadgets1.SetActive(value: false);
		gadgets2.SetActive(value: false);
		gadgets3.SetActive(value: false);
		gadgets4.SetActive(value: false);
		gadgets5.SetActive(value: false);
		MarkerBomb();
		MarkerGadget();
		MarkerSpeed();
		MarkerTurbo();
		MarkerDamage();
		MarkerArmor();
		MarkerBombs();
		MarkerGadgets();
	}

	private void MarkerBombs()
	{
		if (!Progress.shop.Car.bomb_0_bounght && ShopManagerPrice.instance.Price.Bomb[0].price <= Progress.shop.currency)
		{
			bomb1.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_1_bounght && ShopManagerPrice.instance.Price.Bomb[1].price <= Progress.shop.currency)
		{
			bomb2.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_2_bounght && ShopManagerPrice.instance.Price.Bomb[2].price <= Progress.shop.currency)
		{
			bomb3.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_3_bounght && ShopManagerPrice.instance.Price.Bomb[3].price <= Progress.shop.currency)
		{
			bomb4.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_4_bounght && ShopManagerPrice.instance.Price.Bomb[4].price <= Progress.shop.currency)
		{
			bomb5.SetActive(value: true);
		}
	}

	private void MarkerBomb()
	{
		if (!Progress.shop.Car.bomb_0_bounght && ShopManagerPrice.instance.Price.Bomb[0].price <= Progress.shop.currency)
		{
			bomb.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_1_bounght && ShopManagerPrice.instance.Price.Bomb[1].price <= Progress.shop.currency)
		{
			bomb.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_2_bounght && ShopManagerPrice.instance.Price.Bomb[2].price <= Progress.shop.currency)
		{
			bomb.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_3_bounght && ShopManagerPrice.instance.Price.Bomb[3].price <= Progress.shop.currency)
		{
			bomb.SetActive(value: true);
		}
		if (!Progress.shop.Car.bomb_4_bounght && ShopManagerPrice.instance.Price.Bomb[4].price <= Progress.shop.currency)
		{
			bomb.SetActive(value: true);
		}
	}

	private void MarkerGadgets()
	{
		if (!Progress.shop.Car.Gadget_MISSLLE_bounght && ShopManagerPrice.instance.Price.Gadget[3].price <= Progress.shop.currency)
		{
			gadgets1.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_LEDOLUCH_bounght && ShopManagerPrice.instance.Price.Gadget[2].price <= Progress.shop.currency)
		{
			gadgets2.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_Magnet_bounght && ShopManagerPrice.instance.Price.Gadget[0].price <= Progress.shop.currency)
		{
			gadgets3.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_RECHARGER_bounght && ShopManagerPrice.instance.Price.Gadget[4].price <= Progress.shop.currency)
		{
			gadgets4.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_EMP_bounght && ShopManagerPrice.instance.Price.Gadget[1].price <= Progress.shop.currency)
		{
			gadgets5.SetActive(value: true);
		}
	}

	private void MarkerGadget()
	{
		if (!Progress.shop.Car.Gadget_MISSLLE_bounght && ShopManagerPrice.instance.Price.Gadget[3].price <= Progress.shop.currency)
		{
			gadgets.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_LEDOLUCH_bounght && ShopManagerPrice.instance.Price.Gadget[2].price <= Progress.shop.currency)
		{
			gadgets.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_Magnet_bounght && ShopManagerPrice.instance.Price.Gadget[0].price <= Progress.shop.currency)
		{
			gadgets.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_RECHARGER_bounght && ShopManagerPrice.instance.Price.Gadget[4].price <= Progress.shop.currency)
		{
			gadgets.SetActive(value: true);
		}
		if (!Progress.shop.Car.Gadget_EMP_bounght && ShopManagerPrice.instance.Price.Gadget[1].price <= Progress.shop.currency)
		{
			gadgets.SetActive(value: true);
		}
	}

	private void MarkerArmor()
	{
		if (Progress.shop.Car.healthActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[0].price <= Progress.shop.currency)
		{
			armor.SetActive(value: true);
		}
		if (Progress.shop.Car.healthActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[1].price <= Progress.shop.currency)
		{
			armor.SetActive(value: true);
		}
		if (Progress.shop.Car.healthActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Armor[2].price <= Progress.shop.currency)
		{
			armor.SetActive(value: true);
		}
	}

	private void MarkerSpeed()
	{
		if (Progress.shop.Car.engineActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[0].price <= Progress.shop.currency)
		{
			speed.SetActive(value: true);
		}
		if (Progress.shop.Car.engineActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[1].price <= Progress.shop.currency)
		{
			speed.SetActive(value: true);
		}
		if (Progress.shop.Car.engineActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Speed[2].price <= Progress.shop.currency)
		{
			speed.SetActive(value: true);
		}
	}

	private void MarkerTurbo()
	{
		if (Progress.shop.Car.turboActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[0].price <= Progress.shop.currency)
		{
			turbo.SetActive(value: true);
		}
		if (Progress.shop.Car.turboActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[1].price <= Progress.shop.currency)
		{
			turbo.SetActive(value: true);
		}
		if (Progress.shop.Car.turboActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Turbo[2].price <= Progress.shop.currency)
		{
			turbo.SetActive(value: true);
		}
	}

	private void MarkerDamage()
	{
		if (Progress.shop.Car.weaponActLev == 0 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[0].price <= Progress.shop.currency)
		{
			damage.SetActive(value: true);
		}
		if (Progress.shop.Car.weaponActLev == 1 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[1].price <= Progress.shop.currency)
		{
			damage.SetActive(value: true);
		}
		if (Progress.shop.Car.weaponActLev == 2 && ShopManagerPrice.instance.Price.Car[Progress.shop.activeCar].Weapon[2].price <= Progress.shop.currency)
		{
			damage.SetActive(value: true);
		}
	}
}

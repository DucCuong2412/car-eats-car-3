using UnityEngine;
using UnityEngine.UI;

public class ShopStatsForGarage : MonoBehaviour
{
	public Text ArmorStock;

	public Text SpeedStock;

	public Text WeaponStock;

	public Text TurboStock;

	public Text ArmorStockU;

	public Text SpeedStockU;

	public Text WeaponStockU;

	public Text TurboStockU;

	public Text ArmorStockNew;

	public Text SpeedStockNew;

	public Text WeaponStockNew;

	public Text TurboStockNew;

	[Header("UPGRADE")]
	public Text Armor;

	public Text Speed;

	public Text Weapon;

	public Text Turbo;

	private void OnEnable()
	{
	}

	private void Update()
	{
		if (Progress.shop.activeCar >= 0)
		{
			ArmorStock.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.ArmorStats.ToString();
			SpeedStock.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.SpeedStats.ToString();
			WeaponStock.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.WeaponStats.ToString();
			TurboStock.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.TurboStats.ToString();
			ArmorStockU.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.ArmorStats.ToString();
			SpeedStockU.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.SpeedStats.ToString();
			WeaponStockU.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.WeaponStats.ToString();
			TurboStockU.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.TurboStats.ToString();
			ArmorStockNew.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.ArmorStats.ToString();
			SpeedStockNew.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.SpeedStats.ToString();
			WeaponStockNew.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.WeaponStats.ToString();
			TurboStockNew.text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Stock.TurboStats.ToString();
			Armor.text = Progress.shop.Car.ARMOR_STATS.ToString();
			Speed.text = Progress.shop.Car.SPEED_STATS.ToString();
			Weapon.text = Progress.shop.Car.WEAPON_STATS.ToString();
			Turbo.text = Progress.shop.Car.TURBO_STATS.ToString();
		}
	}

	public void ARMOR()
	{
		Progress.shop.Car.ARMOR_STATS = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Armor[Progress.shop.Car.healthActLev].price;
	}

	public void TURBO()
	{
		Progress.shop.Car.TURBO_STATS = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Turbo[Progress.shop.Car.turboActLev].price;
	}

	public void SPEED()
	{
		Progress.shop.Car.SPEED_STATS = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Speed[Progress.shop.Car.engineActLev].price;
	}

	public void WEAPON()
	{
		Progress.shop.Car.WEAPON_STATS = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Weapon[Progress.shop.Car.weaponActLev].price;
	}
}

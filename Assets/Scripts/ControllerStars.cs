using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerStars : MonoBehaviour
{
	[Header("Armor")]
	public List<GameObject> startSmallArmor = new List<GameObject>();

	public List<GameObject> startBigArmor = new List<GameObject>();

	public List<GameObject> startBigArmorAlpha = new List<GameObject>();

	public List<Text> startTextArmorAlpha = new List<Text>();

	[Header("Speed (Engine + wheels)")]
	public List<GameObject> startSmallSpeed = new List<GameObject>();

	public List<GameObject> startBigSpeed = new List<GameObject>();

	public List<GameObject> startBigSpeedAlpha = new List<GameObject>();

	public List<Text> startTextSpeedAlpha = new List<Text>();

	[Header("Damage (zybu)")]
	public List<GameObject> startSmallDamage = new List<GameObject>();

	public List<GameObject> startBigDamage = new List<GameObject>();

	public List<GameObject> startBigDamageAlpha = new List<GameObject>();

	public List<Text> startTextDamageAlpha = new List<Text>();

	[Header("Turbo")]
	public List<GameObject> startSmallTurbo = new List<GameObject>();

	public List<GameObject> startBigTurbo = new List<GameObject>();

	public List<GameObject> startBigTurboAlpha = new List<GameObject>();

	public List<Text> startTextTurboAlpha = new List<Text>();

	private int i;

	private void OnEnable()
	{
		i = 0;
	}

	private void Update()
	{
		if (i > 30)
		{
			up();
		}
		else
		{
			i++;
		}
	}

	private void up()
	{
		if (Progress.shop.activeCar >= 0)
		{
			Armor();
			speed();
			Turbo();
			Damage();
			Stats();
		}
	}

	private void Stats()
	{
		for (int i = 0; i < startTextArmorAlpha.Count; i++)
		{
			startTextArmorAlpha[i].text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Armor[i].price.ToString();
		}
		for (int j = 0; j < startTextSpeedAlpha.Count; j++)
		{
			startTextSpeedAlpha[j].text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Speed[j].price.ToString();
		}
		for (int k = 0; k < startTextDamageAlpha.Count; k++)
		{
			startTextDamageAlpha[k].text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Weapon[k].price.ToString();
		}
		for (int l = 0; l < startTextTurboAlpha.Count; l++)
		{
			startTextTurboAlpha[l].text = ShopManagerStats.instance.Price.Car[Progress.shop.activeCar].Turbo[l].price.ToString();
		}
	}

	private void Armor()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 0)
		{
			foreach (GameObject item in startBigArmor)
			{
				item.SetActive(value: false);
			}
			foreach (GameObject item2 in startSmallArmor)
			{
				item2.SetActive(value: false);
			}
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 1)
		{
			startBigArmor[0].SetActive(value: true);
			startBigArmor[1].SetActive(value: false);
			startBigArmor[2].SetActive(value: false);
			startSmallArmor[0].SetActive(value: true);
			startSmallArmor[1].SetActive(value: false);
			startSmallArmor[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 2)
		{
			startBigArmor[0].SetActive(value: true);
			startBigArmor[1].SetActive(value: true);
			startBigArmor[2].SetActive(value: false);
			startSmallArmor[0].SetActive(value: true);
			startSmallArmor[1].SetActive(value: true);
			startSmallArmor[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 3)
		{
			startBigArmor[0].SetActive(value: true);
			startBigArmor[1].SetActive(value: true);
			startBigArmor[2].SetActive(value: true);
			startSmallArmor[0].SetActive(value: true);
			startSmallArmor[1].SetActive(value: true);
			startSmallArmor[2].SetActive(value: true);
		}
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4)
		{
			startSmallArmor[0].SetActive(value: true);
			startSmallArmor[1].SetActive(value: true);
			startSmallArmor[2].SetActive(value: true);
		}
	}

	public void ArmorPre()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 0)
		{
			startBigArmorAlpha[0].SetActive(value: true);
			startBigArmorAlpha[1].SetActive(value: false);
			startBigArmorAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 1)
		{
			startBigArmorAlpha[0].SetActive(value: false);
			startBigArmorAlpha[1].SetActive(value: true);
			startBigArmorAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 2)
		{
			startBigArmorAlpha[0].SetActive(value: false);
			startBigArmorAlpha[1].SetActive(value: false);
			startBigArmorAlpha[2].SetActive(value: true);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].healthActLev == 3)
		{
			foreach (GameObject item in startBigArmorAlpha)
			{
				item.SetActive(value: false);
			}
		}
	}

	private void speed()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 0)
		{
			foreach (GameObject item in startBigSpeed)
			{
				item.SetActive(value: false);
			}
			foreach (GameObject item2 in startSmallSpeed)
			{
				item2.SetActive(value: false);
			}
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 1)
		{
			startBigSpeed[0].SetActive(value: true);
			startBigSpeed[1].SetActive(value: false);
			startBigSpeed[2].SetActive(value: false);
			startSmallSpeed[0].SetActive(value: true);
			startSmallSpeed[1].SetActive(value: false);
			startSmallSpeed[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 2)
		{
			startBigSpeed[0].SetActive(value: true);
			startBigSpeed[1].SetActive(value: true);
			startBigSpeed[2].SetActive(value: false);
			startSmallSpeed[0].SetActive(value: true);
			startSmallSpeed[1].SetActive(value: true);
			startSmallSpeed[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 3)
		{
			startBigSpeed[0].SetActive(value: true);
			startBigSpeed[1].SetActive(value: true);
			startBigSpeed[2].SetActive(value: true);
			startSmallSpeed[0].SetActive(value: true);
			startSmallSpeed[1].SetActive(value: true);
			startSmallSpeed[2].SetActive(value: true);
		}
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4)
		{
			startSmallSpeed[0].SetActive(value: true);
			startSmallSpeed[1].SetActive(value: true);
			startSmallSpeed[2].SetActive(value: true);
		}
	}

	public void speedPre()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 0)
		{
			startBigSpeedAlpha[0].SetActive(value: true);
			startBigSpeedAlpha[1].SetActive(value: false);
			startBigSpeedAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 1)
		{
			startBigSpeedAlpha[0].SetActive(value: false);
			startBigSpeedAlpha[1].SetActive(value: true);
			startBigSpeedAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 2)
		{
			startBigSpeedAlpha[0].SetActive(value: false);
			startBigSpeedAlpha[1].SetActive(value: false);
			startBigSpeedAlpha[2].SetActive(value: true);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].engineActLev == 3)
		{
			foreach (GameObject item in startBigSpeedAlpha)
			{
				item.SetActive(value: false);
			}
		}
	}

	private void Damage()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 0)
		{
			foreach (GameObject item in startBigDamage)
			{
				item.SetActive(value: false);
			}
			foreach (GameObject item2 in startSmallDamage)
			{
				item2.SetActive(value: false);
			}
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 1)
		{
			startBigDamage[0].SetActive(value: true);
			startBigDamage[1].SetActive(value: false);
			startBigDamage[2].SetActive(value: false);
			startSmallDamage[0].SetActive(value: true);
			startSmallDamage[1].SetActive(value: false);
			startSmallDamage[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 2)
		{
			startBigDamage[0].SetActive(value: true);
			startBigDamage[1].SetActive(value: true);
			startBigDamage[2].SetActive(value: false);
			startSmallDamage[0].SetActive(value: true);
			startSmallDamage[1].SetActive(value: true);
			startSmallDamage[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 3)
		{
			startBigDamage[0].SetActive(value: true);
			startBigDamage[1].SetActive(value: true);
			startBigDamage[2].SetActive(value: true);
			startSmallDamage[0].SetActive(value: true);
			startSmallDamage[1].SetActive(value: true);
			startSmallDamage[2].SetActive(value: true);
		}
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4)
		{
			startSmallDamage[0].SetActive(value: true);
			startSmallDamage[1].SetActive(value: true);
			startSmallDamage[2].SetActive(value: true);
		}
	}

	public void damagePre()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 0)
		{
			startBigDamageAlpha[0].SetActive(value: true);
			startBigDamageAlpha[1].SetActive(value: false);
			startBigDamageAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 1)
		{
			startBigDamageAlpha[0].SetActive(value: false);
			startBigDamageAlpha[1].SetActive(value: true);
			startBigDamageAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 2)
		{
			startBigDamageAlpha[0].SetActive(value: false);
			startBigDamageAlpha[1].SetActive(value: false);
			startBigDamageAlpha[2].SetActive(value: true);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].weaponActLev == 3)
		{
			foreach (GameObject item in startBigDamageAlpha)
			{
				item.SetActive(value: false);
			}
		}
	}

	private void Turbo()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 0)
		{
			foreach (GameObject item in startBigTurbo)
			{
				item.SetActive(value: false);
			}
			foreach (GameObject item2 in startSmallTurbo)
			{
				item2.SetActive(value: false);
			}
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 1)
		{
			startBigTurbo[0].SetActive(value: true);
			startBigTurbo[1].SetActive(value: false);
			startBigTurbo[2].SetActive(value: false);
			startSmallTurbo[0].SetActive(value: true);
			startSmallTurbo[1].SetActive(value: false);
			startSmallTurbo[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 2)
		{
			startBigTurbo[0].SetActive(value: true);
			startBigTurbo[1].SetActive(value: true);
			startBigTurbo[2].SetActive(value: false);
			startSmallTurbo[0].SetActive(value: true);
			startSmallTurbo[1].SetActive(value: true);
			startSmallTurbo[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 3)
		{
			startBigTurbo[0].SetActive(value: true);
			startBigTurbo[1].SetActive(value: true);
			startBigTurbo[2].SetActive(value: true);
			startSmallTurbo[0].SetActive(value: true);
			startSmallTurbo[1].SetActive(value: true);
			startSmallTurbo[2].SetActive(value: true);
		}
		if (Progress.shop.activeCar == 3 || Progress.shop.activeCar == 4)
		{
			startSmallTurbo[0].SetActive(value: true);
			startSmallTurbo[1].SetActive(value: true);
			startSmallTurbo[2].SetActive(value: true);
		}
	}

	public void turboPre()
	{
		if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 0)
		{
			startBigTurboAlpha[0].SetActive(value: true);
			startBigTurboAlpha[1].SetActive(value: false);
			startBigTurboAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 1)
		{
			startBigTurboAlpha[0].SetActive(value: false);
			startBigTurboAlpha[1].SetActive(value: true);
			startBigTurboAlpha[2].SetActive(value: false);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 2)
		{
			startBigTurboAlpha[0].SetActive(value: false);
			startBigTurboAlpha[1].SetActive(value: false);
			startBigTurboAlpha[2].SetActive(value: true);
		}
		else if (Progress.shop.Cars[Progress.shop.activeCar].turboActLev == 3)
		{
			foreach (GameObject item in startBigTurboAlpha)
			{
				item.SetActive(value: false);
			}
		}
	}
}

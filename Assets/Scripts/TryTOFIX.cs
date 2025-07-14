using UnityEngine;

public class TryTOFIX : MonoBehaviour
{
	public GameObject armor;

	public GameObject speed;

	public GameObject turbo;

	public GameObject weapon;

	private void Update()
	{
		if (Progress.shop.activeCar < 3)
		{
			if (Progress.shop.Car.bought)
			{
				armor.SetActive(value: true);
				speed.SetActive(value: true);
				turbo.SetActive(value: true);
				weapon.SetActive(value: true);
			}
			else
			{
				armor.SetActive(value: false);
				speed.SetActive(value: false);
				turbo.SetActive(value: false);
				weapon.SetActive(value: false);
			}
		}
		else
		{
			armor.SetActive(value: true);
			speed.SetActive(value: true);
			turbo.SetActive(value: true);
			weapon.SetActive(value: true);
		}
	}
}

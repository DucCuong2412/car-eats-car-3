using UnityEngine;

public class Car2DWeapons1Controller_Animator : MonoBehaviour
{
	public Animator Weapon;

	private int _is_tier_0 = Animator.StringToHash("is_tier_0");

	private int _is_tier_1 = Animator.StringToHash("is_tier_1");

	private int _is_tier_2 = Animator.StringToHash("is_tier_2");

	private int _is_tier_3 = Animator.StringToHash("is_tier_3");

	public bool Garage;

	public int carnum;

	private void OnEnable()
	{
		if (Progress.shop.Car.weaponActLev == 0)
		{
			Weapon.SetBool(_is_tier_0, value: true);
			Weapon.SetBool(_is_tier_1, value: false);
			Weapon.SetBool(_is_tier_2, value: false);
			Weapon.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.weaponActLev == 1)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: true);
			Weapon.SetBool(_is_tier_2, value: false);
			Weapon.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.weaponActLev == 2)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: false);
			Weapon.SetBool(_is_tier_2, value: true);
			Weapon.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.weaponActLev == 3)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: false);
			Weapon.SetBool(_is_tier_2, value: false);
			Weapon.SetBool(_is_tier_3, value: true);
		}
	}

	private void Update()
	{
		if (Garage)
		{
			if (Progress.shop.Cars[carnum].weaponActLev == 0)
			{
				Weapon.SetBool(_is_tier_0, value: true);
				Weapon.SetBool(_is_tier_1, value: false);
				Weapon.SetBool(_is_tier_2, value: false);
				Weapon.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].weaponActLev == 1)
			{
				Weapon.SetBool(_is_tier_0, value: false);
				Weapon.SetBool(_is_tier_1, value: true);
				Weapon.SetBool(_is_tier_2, value: false);
				Weapon.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].weaponActLev == 2)
			{
				Weapon.SetBool(_is_tier_0, value: false);
				Weapon.SetBool(_is_tier_1, value: false);
				Weapon.SetBool(_is_tier_2, value: true);
				Weapon.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].weaponActLev == 3)
			{
				Weapon.SetBool(_is_tier_0, value: false);
				Weapon.SetBool(_is_tier_1, value: false);
				Weapon.SetBool(_is_tier_2, value: false);
				Weapon.SetBool(_is_tier_3, value: true);
			}
		}
	}

	public void Damage()
	{
		Weapon.SetBool("is_attack", value: true);
	}

	public void OffDamage()
	{
		Weapon.SetBool("is_attack", value: false);
	}

	public void Change()
	{
		if (Progress.shop.Car.weaponActLev == 0)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: true);
			Weapon.SetBool(_is_tier_2, value: false);
			Weapon.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.weaponActLev == 1)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: false);
			Weapon.SetBool(_is_tier_2, value: true);
			Weapon.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.weaponActLev == 2)
		{
			Weapon.SetBool(_is_tier_0, value: false);
			Weapon.SetBool(_is_tier_1, value: false);
			Weapon.SetBool(_is_tier_2, value: false);
			Weapon.SetBool(_is_tier_3, value: true);
		}
	}
}

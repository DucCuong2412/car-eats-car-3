using UnityEngine;

public class Car2DTurboController_Animator : MonoBehaviour
{
	private int _is_tier_0 = Animator.StringToHash("is_tier_0");

	private int _is_tier_1 = Animator.StringToHash("is_tier_1");

	private int _is_tier_2 = Animator.StringToHash("is_tier_2");

	private int _is_tier_3 = Animator.StringToHash("is_tier_3");

	public Animator Turbo;

	public bool Garage;

	public int carnum;

	private void OnEnable()
	{
		if (Progress.shop.Car.turboActLev == 0)
		{
			Turbo.SetBool(_is_tier_0, value: true);
			Turbo.SetBool(_is_tier_1, value: false);
			Turbo.SetBool(_is_tier_2, value: false);
			Turbo.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.turboActLev == 1)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: true);
			Turbo.SetBool(_is_tier_2, value: false);
			Turbo.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.turboActLev == 2)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: false);
			Turbo.SetBool(_is_tier_2, value: true);
			Turbo.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.turboActLev == 3)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: false);
			Turbo.SetBool(_is_tier_2, value: false);
			Turbo.SetBool(_is_tier_3, value: true);
		}
	}

	private void Update()
	{
		if (Garage)
		{
			if (Progress.shop.Cars[carnum].turboActLev == 0)
			{
				Turbo.SetBool(_is_tier_0, value: true);
				Turbo.SetBool(_is_tier_1, value: false);
				Turbo.SetBool(_is_tier_2, value: false);
				Turbo.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].turboActLev == 1)
			{
				Turbo.SetBool(_is_tier_0, value: false);
				Turbo.SetBool(_is_tier_1, value: true);
				Turbo.SetBool(_is_tier_2, value: false);
				Turbo.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].turboActLev == 2)
			{
				Turbo.SetBool(_is_tier_0, value: false);
				Turbo.SetBool(_is_tier_1, value: false);
				Turbo.SetBool(_is_tier_2, value: true);
				Turbo.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].turboActLev == 3)
			{
				Turbo.SetBool(_is_tier_0, value: false);
				Turbo.SetBool(_is_tier_1, value: false);
				Turbo.SetBool(_is_tier_2, value: false);
				Turbo.SetBool(_is_tier_3, value: true);
			}
		}
	}

	public void On()
	{
		Turbo.SetBool("turbo_on", value: true);
	}

	public void Off()
	{
		Turbo.SetBool("turbo_on", value: false);
	}

	public void Change()
	{
		if (Progress.shop.Car.turboActLev == 0)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: true);
			Turbo.SetBool(_is_tier_2, value: false);
			Turbo.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.turboActLev == 1)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: false);
			Turbo.SetBool(_is_tier_2, value: true);
			Turbo.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.turboActLev == 2)
		{
			Turbo.SetBool(_is_tier_0, value: false);
			Turbo.SetBool(_is_tier_1, value: false);
			Turbo.SetBool(_is_tier_2, value: false);
			Turbo.SetBool(_is_tier_3, value: true);
		}
	}
}

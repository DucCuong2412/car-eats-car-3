using System.Collections;
using UnityEngine;

public class Car2DWheelController_animator : MonoBehaviour
{
	public Animator Wheels;

	private int _is_tier_0 = Animator.StringToHash("is_tier_0");

	private int _is_tier_1 = Animator.StringToHash("is_tier_1");

	private int _is_tier_2 = Animator.StringToHash("is_tier_2");

	private int _is_tier_3 = Animator.StringToHash("is_tier_3");

	public bool Garage;

	public int carnum;

	private void OnEnable()
	{
		Wheels.enabled = true;
		if (Progress.shop.Car.engineActLev == 0)
		{
			Wheels.SetBool(_is_tier_0, value: true);
			Wheels.SetBool(_is_tier_1, value: false);
			Wheels.SetBool(_is_tier_2, value: false);
			Wheels.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 1)
		{
			Wheels.SetBool(_is_tier_0, value: false);
			Wheels.SetBool(_is_tier_1, value: true);
			Wheels.SetBool(_is_tier_2, value: false);
			Wheels.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 2)
		{
			Wheels.SetBool(_is_tier_0, value: false);
			Wheels.SetBool(_is_tier_1, value: false);
			Wheels.SetBool(_is_tier_2, value: true);
			Wheels.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 3)
		{
			Wheels.SetBool(_is_tier_0, value: false);
			Wheels.SetBool(_is_tier_1, value: false);
			Wheels.SetBool(_is_tier_2, value: false);
			Wheels.SetBool(_is_tier_3, value: true);
		}
		StartCoroutine(test());
	}

	private IEnumerator test()
	{
		yield return new WaitForSeconds(1f);
		if (!Garage)
		{
			Wheels.enabled = false;
		}
	}

	private void Update()
	{
		if (Garage)
		{
			if (Progress.shop.Cars[carnum].engineActLev == 0)
			{
				Wheels.SetBool(_is_tier_0, value: true);
				Wheels.SetBool(_is_tier_1, value: false);
				Wheels.SetBool(_is_tier_2, value: false);
				Wheels.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 1)
			{
				Wheels.SetBool(_is_tier_0, value: true);
				Wheels.SetBool(_is_tier_1, value: true);
				Wheels.SetBool(_is_tier_2, value: false);
				Wheels.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 2)
			{
				Wheels.SetBool(_is_tier_0, value: true);
				Wheels.SetBool(_is_tier_1, value: true);
				Wheels.SetBool(_is_tier_2, value: true);
				Wheels.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 3)
			{
				Wheels.SetBool(_is_tier_0, value: true);
				Wheels.SetBool(_is_tier_1, value: true);
				Wheels.SetBool(_is_tier_2, value: true);
				Wheels.SetBool(_is_tier_3, value: true);
			}
		}
	}

	public void Change()
	{
		if (Progress.shop.Car.engineActLev == 0)
		{
			Wheels.SetBool(_is_tier_0, value: true);
			Wheels.SetBool(_is_tier_1, value: true);
			Wheels.SetBool(_is_tier_2, value: false);
			Wheels.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 1)
		{
			Wheels.SetBool(_is_tier_0, value: true);
			Wheels.SetBool(_is_tier_1, value: true);
			Wheels.SetBool(_is_tier_2, value: true);
			Wheels.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 2)
		{
			Wheels.SetBool(_is_tier_0, value: true);
			Wheels.SetBool(_is_tier_1, value: true);
			Wheels.SetBool(_is_tier_2, value: true);
			Wheels.SetBool(_is_tier_3, value: true);
		}
	}
}

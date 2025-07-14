using UnityEngine;

public class Car2DEngineController_Animator : MonoBehaviour
{
	private int _is_tier_0 = Animator.StringToHash("is_tier_0");

	private int _is_tier_1 = Animator.StringToHash("is_tier_1");

	private int _is_tier_2 = Animator.StringToHash("is_tier_2");

	private int _is_tier_3 = Animator.StringToHash("is_tier_3");

	public Animator Engine;

	public bool Garage;

	public int carnum;

	private void OnEnable()
	{
		if (Progress.shop.Car.engineActLev == 0)
		{
			Engine.SetBool(_is_tier_0, value: true);
			Engine.SetBool(_is_tier_1, value: false);
			Engine.SetBool(_is_tier_2, value: false);
			Engine.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 1)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: true);
			Engine.SetBool(_is_tier_2, value: false);
			Engine.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 2)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: false);
			Engine.SetBool(_is_tier_2, value: true);
			Engine.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 3)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: false);
			Engine.SetBool(_is_tier_2, value: false);
			Engine.SetBool(_is_tier_3, value: true);
		}
	}

	private void Update()
	{
		if (Garage)
		{
			if (Progress.shop.Cars[carnum].engineActLev == 0)
			{
				Engine.SetBool(_is_tier_0, value: true);
				Engine.SetBool(_is_tier_1, value: false);
				Engine.SetBool(_is_tier_2, value: false);
				Engine.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 1)
			{
				Engine.SetBool(_is_tier_0, value: false);
				Engine.SetBool(_is_tier_1, value: true);
				Engine.SetBool(_is_tier_2, value: false);
				Engine.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 2)
			{
				Engine.SetBool(_is_tier_0, value: false);
				Engine.SetBool(_is_tier_1, value: false);
				Engine.SetBool(_is_tier_2, value: true);
				Engine.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].engineActLev == 3)
			{
				Engine.SetBool(_is_tier_0, value: false);
				Engine.SetBool(_is_tier_1, value: false);
				Engine.SetBool(_is_tier_2, value: false);
				Engine.SetBool(_is_tier_3, value: true);
			}
		}
	}

	public void Change()
	{
		if (Progress.shop.Car.engineActLev == 0)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: true);
			Engine.SetBool(_is_tier_2, value: false);
			Engine.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 1)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: false);
			Engine.SetBool(_is_tier_2, value: true);
			Engine.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.engineActLev == 2)
		{
			Engine.SetBool(_is_tier_0, value: false);
			Engine.SetBool(_is_tier_1, value: false);
			Engine.SetBool(_is_tier_2, value: false);
			Engine.SetBool(_is_tier_3, value: true);
		}
	}
}

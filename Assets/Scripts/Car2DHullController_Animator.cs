using UnityEngine;

public class Car2DHullController_Animator : MonoBehaviour
{
	public Animator HullAnimator;

	[Range(0f, 100f)]
	public float healt = 100f;

	private int _is_tier_0 = Animator.StringToHash("is_tier_0");

	private int _is_tier_1 = Animator.StringToHash("is_tier_1");

	private int _is_tier_2 = Animator.StringToHash("is_tier_2");

	private int _is_tier_3 = Animator.StringToHash("is_tier_3");

	public bool Garage;

	public int carnum;

	private void OnEnable()
	{
		if (Progress.shop.Car.healthActLev == 0)
		{
			HullAnimator.SetBool(_is_tier_0, value: true);
			HullAnimator.SetBool(_is_tier_1, value: false);
			HullAnimator.SetBool(_is_tier_2, value: false);
			HullAnimator.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.healthActLev == 1)
		{
			HullAnimator.SetBool(_is_tier_0, value: false);
			HullAnimator.SetBool(_is_tier_1, value: true);
			HullAnimator.SetBool(_is_tier_2, value: false);
			HullAnimator.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.healthActLev == 2)
		{
			HullAnimator.SetBool(_is_tier_0, value: false);
			HullAnimator.SetBool(_is_tier_1, value: false);
			HullAnimator.SetBool(_is_tier_2, value: true);
			HullAnimator.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.healthActLev == 3)
		{
			HullAnimator.SetBool(_is_tier_0, value: false);
			HullAnimator.SetBool(_is_tier_1, value: false);
			HullAnimator.SetBool(_is_tier_2, value: false);
			HullAnimator.SetBool(_is_tier_3, value: true);
		}
	}

	private void Update()
	{
		HullAnimator.SetFloat("hull_integrity", healt);
		if (Garage)
		{
			if (Progress.shop.Cars[carnum].healthActLev == 0)
			{
				HullAnimator.SetBool(_is_tier_0, value: true);
				HullAnimator.SetBool(_is_tier_1, value: false);
				HullAnimator.SetBool(_is_tier_2, value: false);
				HullAnimator.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].healthActLev == 1)
			{
				HullAnimator.SetBool(_is_tier_0, value: false);
				HullAnimator.SetBool(_is_tier_1, value: true);
				HullAnimator.SetBool(_is_tier_2, value: false);
				HullAnimator.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].healthActLev == 2)
			{
				HullAnimator.SetBool(_is_tier_0, value: false);
				HullAnimator.SetBool(_is_tier_1, value: false);
				HullAnimator.SetBool(_is_tier_2, value: true);
				HullAnimator.SetBool(_is_tier_3, value: false);
			}
			else if (Progress.shop.Cars[carnum].healthActLev == 3)
			{
				HullAnimator.SetBool(_is_tier_0, value: false);
				HullAnimator.SetBool(_is_tier_1, value: false);
				HullAnimator.SetBool(_is_tier_2, value: false);
				HullAnimator.SetBool(_is_tier_3, value: true);
			}
		}
	}

	public void Change()
	{
		if (Progress.shop.Car.healthActLev == 0)
		{
			HullAnimator.SetBool(_is_tier_0, value: true);
			HullAnimator.SetBool(_is_tier_1, value: true);
			HullAnimator.SetBool(_is_tier_2, value: false);
			HullAnimator.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.healthActLev == 1)
		{
			HullAnimator.SetBool(_is_tier_0, value: true);
			HullAnimator.SetBool(_is_tier_1, value: true);
			HullAnimator.SetBool(_is_tier_2, value: true);
			HullAnimator.SetBool(_is_tier_3, value: false);
		}
		else if (Progress.shop.Car.healthActLev == 2)
		{
			HullAnimator.SetBool(_is_tier_0, value: true);
			HullAnimator.SetBool(_is_tier_1, value: true);
			HullAnimator.SetBool(_is_tier_2, value: true);
			HullAnimator.SetBool(_is_tier_3, value: true);
		}
	}
}

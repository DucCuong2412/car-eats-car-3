using AnimationOrTween;
using UnityEngine;

public class CarShield : MonoBehaviour
{
	public float MaxValue = 150f;

	private float _value;

	private Animation _anim;

	private SpriteRenderer _sprite;

	private Car2DController _contrl;

	private float previosValue;

	public float previosBonusValue;

	private float dmg;

	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			_value = value;
			_Sprite.color = new Color(1f, 1f, 1f, Value / MaxValue + 0.3f);
			if (_value <= 0f)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	public Animation Anim
	{
		get
		{
			if (_anim == null)
			{
				_anim = base.gameObject.GetComponent<Animation>();
			}
			return _anim;
		}
	}

	private SpriteRenderer _Sprite
	{
		get
		{
			if (_sprite == null)
			{
				_sprite = base.gameObject.GetComponent<SpriteRenderer>();
			}
			return _sprite;
		}
	}

	private Car2DController Controller
	{
		get
		{
			if (_contrl == null)
			{
				_contrl = base.gameObject.GetComponentInParent<Car2DController>();
			}
			return _contrl;
		}
	}

	public void Init()
	{
		ActiveAnimation.Play(Anim, Direction.Forward);
		MaxValue = Controller.HealthModule._Barrel.MaxValue / 2f;
		Value = MaxValue;
		previosValue = Controller.HealthModule._Barrel.MaxValue;
		previosBonusValue = Controller.HealthModule.HealthBoost;
	}

	public void Disable()
	{
		if (base.gameObject.activeSelf)
		{
			Utilities.RunActor(Anim, isForward: false, delegate
			{
				base.gameObject.SetActive(value: false);
			});
		}
	}

	private void Update()
	{
		if (Controller.Enabled && base.gameObject.activeSelf)
		{
			dmg = 0f;
			if (previosValue > Controller.HealthModule._Barrel.Value)
			{
				dmg += previosValue - Controller.HealthModule._Barrel.Value;
				Controller.HealthModule._Barrel.Value += dmg;
				Value -= dmg;
			}
			if (previosBonusValue > Controller.HealthModule.HealthBoost)
			{
				dmg += previosBonusValue - Controller.HealthModule.HealthBoost;
				Controller.HealthModule.HealthBoost += dmg;
				Value -= dmg;
			}
			previosValue = Controller.HealthModule._Barrel.Value;
			previosBonusValue = Controller.HealthModule.HealthBoost;
		}
	}
}

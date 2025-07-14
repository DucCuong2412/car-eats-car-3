using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageCarList_Controller : MonoBehaviour
{
	[Header("Text Colors - RGB")]
	public Color ColorGreater;

	public Color ColorLess;

	public Color ColorEqual;

	public Color ColorUnequal;

	[Header("Global")]
	public Animator ShowHideAnim;

	public Button CloseBut;

	public Button ShowBut;

	[Header("Bot Panel")]
	public List<GarageCarList_CarBut> CarsButs;

	public GameObject CarCountersObj;

	public Text BuyedCarsText;

	public Text MaxCarsText;

	[Header("Left Panel")]
	public List<GameObject> LeftPanelIcos;

	public Text LeftPanel_carName;

	public GameObject LeftPanelStars;

	public Button LeftPanelGOTO_but;

	public Animator LeftPanelGOTO_anim;

	[Header("Right Panel")]
	public List<GameObject> RightPanelIcos;

	public Text RightPanel_carName;

	public GameObject RightPanelStars;

	public Button RightPanelGOTO_but;

	public Animator RightPanelGOTO_anim;

	[Header("Stats Panel")]
	public Text ArmorStatLeftText;

	public Text TurboStatLeftText;

	public Text SpeedStatLeftText;

	public Text DamageStatLeftText;

	public Text ArmorStatRightText;

	public Text TurboStatRightText;

	public Text SpeedStatRightText;

	public Text DamageStatRightText;

	public Button Fight_but;

	public Text LeftFightPercentText;

	public Text RightFightPercentText;

	public Animator StatArrowArmor;

	public Animator StatArrowTurbo;

	public Animator StatArrowSpeed;

	public Animator StatArrowDamage;

	public Animator StatArrowBeter;

	public Animator StatArrowOkLeft;

	public Animator StatArrowOkRight;

	private ScrollRectSnapLEXTry _scrollGarage;

	private Coroutine arrowsCorut;

	private Coroutine delayToFightCorut;

	private int _armorStatLeft;

	private int _turboStatLeft;

	private int _speedStatLeft;

	private int _damageStatLeft;

	private int _armorStatRight;

	private int _turboStatRight;

	private int _speedStatRight;

	private int _damageStatRight;

	private int _carIndexLeft = -1;

	private int _carIndexRight = -1;

	private int is_ON = Animator.StringToHash("is_ON");

	private int is_greater = Animator.StringToHash("is_greater");

	private int is_less = Animator.StringToHash("is_less");

	private int is_equal = Animator.StringToHash("is_equal");

	private const string ZERO_str = "0";

	private const string EMPTY_str = "";

	private bool _goTo;

	private void OnEnable()
	{
		CloseBut.onClick.AddListener(Hide);
		ShowBut.onClick.AddListener(Show);
		LeftPanelGOTO_but.onClick.AddListener(LeftGoTo_Car);
		RightPanelGOTO_but.onClick.AddListener(RightGoTo_Car);
		Fight_but.onClick.AddListener(Fight);
		LeftPanelStars.SetActive(value: false);
		RightPanelStars.SetActive(value: false);
		_scrollGarage = UnityEngine.Object.FindObjectOfType<ScrollRectSnapLEXTry>();
	}

	private void Show()
	{
		_goTo = false;
		ShowHideAnim.SetBool(is_ON, value: true);
		_carIndexLeft = -1;
		_carIndexRight = -1;
		SetInfoPanel(left: true, clear: true);
		SetInfoPanel(left: false, clear: true);
		Fight_but.gameObject.SetActive(value: false);
		StartCoroutine(DelayToStageCars());
		Audio.PlayAsync("gui_screen_on");
		Audio.PlayAsync("bullet_electrick");
	}

	private IEnumerator DelayToStageCars()
	{
		int count = CarsButs.Count;
		MaxCarsText.text = "/" + count.ToString();
		float t = 0.3f;
		while (t > 0f)
		{
			int buyedCar = 0;
			for (int i = 0; i < count; i++)
			{
				if (Progress.shop.Cars[i].equipped)
				{
					CarsButs[i].SetBuyed(buy: true);
					buyedCar++;
				}
				else
				{
					CarsButs[i].SetBuyed(buy: false);
				}
			}
			if (buyedCar != 0)
			{
				BuyedCarsText.text = buyedCar.ToString();
			}
			t -= Time.deltaTime;
			yield return null;
		}
		CarCountersObj.SetActive(value: false);
		yield return null;
		yield return null;
		CarCountersObj.SetActive(value: true);
	}

	public void PressCar(int index)
	{
		if (index == _carIndexLeft)
		{
			_carIndexLeft = -1;
			SetInfoPanel(left: true, clear: true);
		}
		else if (index == _carIndexRight)
		{
			_carIndexRight = -1;
			SetInfoPanel(left: false, clear: true);
		}
		else if (_carIndexLeft == -1)
		{
			_carIndexLeft = index;
			SetInfoPanel(left: true);
		}
		else
		{
			_carIndexRight = index;
			SetInfoPanel(left: false);
		}
	}

	private void SetInfoPanel(bool left, bool clear = false)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		num5 = CarsButs.Count;
		for (int i = 0; i < num5; i++)
		{
			if (_carIndexLeft - 1 == i || _carIndexRight - 1 == i)
			{
				CarsButs[i].SetActivate(active: true);
			}
			else
			{
				CarsButs[i].SetActivate(active: false);
			}
		}
		if (_carIndexLeft != -1 && _carIndexRight != -1)
		{
			Fight_but.gameObject.SetActive(value: true);
		}
		else
		{
			Fight_but.gameObject.SetActive(value: false);
		}
		LeftFightPercentText.text = string.Empty;
		RightFightPercentText.text = string.Empty;
		StatArrowOkLeft.SetBool(is_greater, value: false);
		StatArrowOkLeft.SetBool(is_less, value: false);
		StatArrowOkLeft.SetBool(is_equal, value: false);
		StatArrowOkRight.SetBool(is_greater, value: false);
		StatArrowOkRight.SetBool(is_less, value: false);
		StatArrowOkRight.SetBool(is_equal, value: false);
		if (clear)
		{
			if (left)
			{
				LeftPanel_carName.text = string.Empty;
				num5 = LeftPanelIcos.Count;
				for (int j = 0; j < num5; j++)
				{
					LeftPanelIcos[j].SetActive(value: false);
				}
				LeftPanel_carName.text = string.Empty;
				LeftPanelGOTO_anim.SetBool(is_ON, value: false);
				ArmorStatLeftText.text = string.Empty;
				TurboStatLeftText.text = string.Empty;
				SpeedStatLeftText.text = string.Empty;
				DamageStatLeftText.text = string.Empty;
			}
			else
			{
				RightPanel_carName.text = string.Empty;
				num5 = RightPanelIcos.Count;
				for (int k = 0; k < num5; k++)
				{
					RightPanelIcos[k].SetActive(value: false);
				}
				RightPanel_carName.text = string.Empty;
				RightPanelGOTO_anim.SetBool(is_ON, value: false);
				ArmorStatRightText.text = string.Empty;
				TurboStatRightText.text = string.Empty;
				SpeedStatRightText.text = string.Empty;
				DamageStatRightText.text = string.Empty;
			}
			if (_carIndexLeft == -1 && _carIndexRight != -1)
			{
				ArmorStatRightText.color = ColorUnequal;
				TurboStatRightText.color = ColorUnequal;
				SpeedStatRightText.color = ColorUnequal;
				DamageStatRightText.color = ColorUnequal;
			}
			else if (_carIndexRight == -1 && _carIndexLeft != -1)
			{
				ArmorStatLeftText.color = ColorUnequal;
				TurboStatLeftText.color = ColorUnequal;
				SpeedStatLeftText.color = ColorUnequal;
				DamageStatLeftText.color = ColorUnequal;
			}
		}
		else
		{
			if (left)
			{
				num5 = LeftPanelIcos.Count;
				for (int l = 0; l < num5; l++)
				{
					LeftPanelIcos[l].SetActive(value: false);
				}
				LeftPanelIcos[_carIndexLeft - 1].SetActive(value: true);
				LeftPanelGOTO_anim.SetBool(is_ON, value: true);
				Progress.Shop.CarInfo carInfo = Progress.shop.Cars[_carIndexLeft - 1];
				num = carInfo.healthActLev;
				num2 = carInfo.turboActLev;
				num3 = carInfo.engineActLev;
				num4 = carInfo.weaponActLev;
				ShopManagerStats.Cars cars = ShopManagerStats.instance.Price.Car[_carIndexLeft - 1];
				if (num == 0)
				{
					_armorStatLeft = cars.Stock.ArmorStats;
				}
				else
				{
					_armorStatLeft = cars.Armor[num - 1].price;
				}
				if (num2 == 0)
				{
					_turboStatLeft = cars.Stock.TurboStats;
				}
				else
				{
					_turboStatLeft = cars.Turbo[num2 - 1].price;
				}
				if (num3 == 0)
				{
					_speedStatLeft = cars.Stock.SpeedStats;
				}
				else
				{
					_speedStatLeft = cars.Speed[num3 - 1].price;
				}
				if (num4 == 0)
				{
					_damageStatLeft = cars.Stock.WeaponStats;
				}
				else
				{
					_damageStatLeft = cars.Weapon[num4 - 1].price;
				}
				ArmorStatLeftText.text = _armorStatLeft.ToString();
				TurboStatLeftText.text = _turboStatLeft.ToString();
				SpeedStatLeftText.text = _speedStatLeft.ToString();
				DamageStatLeftText.text = _damageStatLeft.ToString();
				LeftPanel_carName.text = GetCarName(_carIndexLeft);
			}
			else
			{
				num5 = RightPanelIcos.Count;
				for (int m = 0; m < num5; m++)
				{
					RightPanelIcos[m].SetActive(value: false);
				}
				RightPanelIcos[_carIndexRight - 1].SetActive(value: true);
				RightPanelGOTO_anim.SetBool(is_ON, value: true);
				Progress.Shop.CarInfo carInfo2 = Progress.shop.Cars[_carIndexRight - 1];
				num = carInfo2.healthActLev;
				num2 = carInfo2.turboActLev;
				num3 = carInfo2.engineActLev;
				num4 = carInfo2.weaponActLev;
				ShopManagerStats.Cars cars2 = ShopManagerStats.instance.Price.Car[_carIndexRight - 1];
				if (num == 0)
				{
					_armorStatRight = cars2.Stock.ArmorStats;
				}
				else
				{
					_armorStatRight = cars2.Armor[num - 1].price;
				}
				if (num2 == 0)
				{
					_turboStatRight = cars2.Stock.TurboStats;
				}
				else
				{
					_turboStatRight = cars2.Turbo[num2 - 1].price;
				}
				if (num3 == 0)
				{
					_speedStatRight = cars2.Stock.SpeedStats;
				}
				else
				{
					_speedStatRight = cars2.Speed[num3 - 1].price;
				}
				if (num4 == 0)
				{
					_damageStatRight = cars2.Stock.WeaponStats;
				}
				else
				{
					_damageStatRight = cars2.Weapon[num4 - 1].price;
				}
				ArmorStatRightText.text = _armorStatRight.ToString();
				TurboStatRightText.text = _turboStatRight.ToString();
				SpeedStatRightText.text = _speedStatRight.ToString();
				DamageStatRightText.text = _damageStatRight.ToString();
				RightPanel_carName.text = GetCarName(_carIndexRight);
			}
			if (_carIndexLeft == -1 && _carIndexRight != -1)
			{
				ArmorStatRightText.color = ColorUnequal;
				TurboStatRightText.color = ColorUnequal;
				SpeedStatRightText.color = ColorUnequal;
				DamageStatRightText.color = ColorUnequal;
			}
			else if (_carIndexRight == -1 && _carIndexLeft != -1)
			{
				ArmorStatLeftText.color = ColorUnequal;
				TurboStatLeftText.color = ColorUnequal;
				SpeedStatLeftText.color = ColorUnequal;
				DamageStatLeftText.color = ColorUnequal;
			}
			else
			{
				if (_armorStatLeft == _armorStatRight)
				{
					ArmorStatLeftText.color = ColorEqual;
					ArmorStatRightText.color = ColorEqual;
				}
				else if (_armorStatLeft > _armorStatRight)
				{
					ArmorStatLeftText.color = ColorGreater;
					ArmorStatRightText.color = ColorLess;
				}
				else
				{
					ArmorStatLeftText.color = ColorLess;
					ArmorStatRightText.color = ColorGreater;
				}
				if (_turboStatLeft == _turboStatRight)
				{
					TurboStatLeftText.color = ColorEqual;
					TurboStatRightText.color = ColorEqual;
				}
				else if (_turboStatLeft > _turboStatRight)
				{
					TurboStatLeftText.color = ColorGreater;
					TurboStatRightText.color = ColorLess;
				}
				else
				{
					TurboStatLeftText.color = ColorLess;
					TurboStatRightText.color = ColorGreater;
				}
				if (_speedStatLeft == _speedStatRight)
				{
					SpeedStatLeftText.color = ColorEqual;
					SpeedStatRightText.color = ColorEqual;
				}
				else if (_speedStatLeft > _speedStatRight)
				{
					SpeedStatLeftText.color = ColorGreater;
					SpeedStatRightText.color = ColorLess;
				}
				else
				{
					SpeedStatLeftText.color = ColorLess;
					SpeedStatRightText.color = ColorGreater;
				}
				if (_damageStatLeft == _damageStatRight)
				{
					DamageStatLeftText.color = ColorEqual;
					DamageStatRightText.color = ColorEqual;
				}
				else if (_damageStatLeft > _damageStatRight)
				{
					DamageStatLeftText.color = ColorGreater;
					DamageStatRightText.color = ColorLess;
				}
				else
				{
					DamageStatLeftText.color = ColorLess;
					DamageStatRightText.color = ColorGreater;
				}
			}
		}
		if (arrowsCorut != null)
		{
			StopCoroutine(arrowsCorut);
		}
		arrowsCorut = StartCoroutine(VisBeterArrows());
		if (delayToFightCorut != null)
		{
			StopCoroutine(delayToFightCorut);
		}
	}

	private string GetCarName(int carIndex)
	{
		string key = string.Empty;
		switch (carIndex)
		{
		case 1:
			key = "GATOR";
			break;
		case 2:
			key = "HARVESTER";
			break;
		case 3:
			key = "Archiver";
			break;
		case 4:
			key = "LOCOMACHINE";
			break;
		case 5:
			key = "BERSERKER";
			break;
		case 6:
			key = "Beetlee";
			break;
		case 7:
			key = "FRANCOPSTEIN";
			break;
		case 8:
			key = "CAROCOP";
			break;
		case 9:
			key = "RABBITSTER";
			break;
		case 10:
			key = "SCORPION";
			break;
		case 11:
			key = "COCKCHAFER";
			break;
		case 12:
			key = "TANKOMINATOR";
			break;
		case 13:
			key = "ALLIGATOR";
			break;
		case 14:
			key = "TURTLE";
			break;
		}
		return LanguageManager.Instance.GetTextValue(key);
	}

	private IEnumerator VisBeterArrows()
	{
		StatArrowArmor.SetBool(is_greater, value: false);
		StatArrowArmor.SetBool(is_less, value: false);
		StatArrowArmor.SetBool(is_equal, value: false);
		StatArrowTurbo.SetBool(is_greater, value: false);
		StatArrowTurbo.SetBool(is_less, value: false);
		StatArrowTurbo.SetBool(is_equal, value: false);
		StatArrowSpeed.SetBool(is_greater, value: false);
		StatArrowSpeed.SetBool(is_less, value: false);
		StatArrowSpeed.SetBool(is_equal, value: false);
		StatArrowDamage.SetBool(is_greater, value: false);
		StatArrowDamage.SetBool(is_less, value: false);
		StatArrowDamage.SetBool(is_equal, value: false);
		StatArrowBeter.SetBool(is_greater, value: false);
		StatArrowBeter.SetBool(is_less, value: false);
		StatArrowBeter.SetBool(is_equal, value: false);
		if (_carIndexLeft != -1 && _carIndexRight != -1)
		{
			if (_armorStatLeft == _armorStatRight)
			{
				StatArrowArmor.SetBool(is_equal, value: true);
			}
			else if (_armorStatLeft > _armorStatRight)
			{
				StatArrowArmor.SetBool(is_greater, value: true);
			}
			else
			{
				StatArrowArmor.SetBool(is_less, value: true);
			}
			yield return new WaitForSeconds(0.25f);
			if (_turboStatLeft == _turboStatRight)
			{
				StatArrowTurbo.SetBool(is_equal, value: true);
			}
			else if (_turboStatLeft > _turboStatRight)
			{
				StatArrowTurbo.SetBool(is_greater, value: true);
			}
			else
			{
				StatArrowTurbo.SetBool(is_less, value: true);
			}
			yield return new WaitForSeconds(0.25f);
			if (_speedStatLeft == _speedStatRight)
			{
				StatArrowSpeed.SetBool(is_equal, value: true);
			}
			else if (_speedStatLeft > _speedStatRight)
			{
				StatArrowSpeed.SetBool(is_greater, value: true);
			}
			else
			{
				StatArrowSpeed.SetBool(is_less, value: true);
			}
			yield return new WaitForSeconds(0.25f);
			if (_damageStatLeft == _damageStatRight)
			{
				StatArrowDamage.SetBool(is_equal, value: true);
			}
			else if (_damageStatLeft > _damageStatRight)
			{
				StatArrowDamage.SetBool(is_greater, value: true);
			}
			else
			{
				StatArrowDamage.SetBool(is_less, value: true);
			}
		}
		arrowsCorut = null;
	}

	private void Fight()
	{
		Audio.PlayAsync("fight");
		Fight_but.gameObject.SetActive(value: false);
		float num = (float)(_armorStatLeft + _armorStatRight) / 100f;
		float num2 = (float)_armorStatLeft / num;
		float num3 = (float)_armorStatRight / num;
		num = (float)(_turboStatLeft + _turboStatRight) / 100f;
		float num4 = (float)_turboStatLeft / num;
		float num5 = (float)_turboStatRight / num;
		num = (float)(_speedStatLeft + _speedStatRight) / 100f;
		float num6 = (float)_speedStatLeft / num;
		float num7 = (float)_speedStatRight / num;
		num = (float)(_damageStatLeft + _damageStatRight) / 100f;
		float num8 = (float)_damageStatLeft / num;
		float num9 = (float)_damageStatRight / num;
		float num10 = num2 + num4 + num6 + num8;
		float num11 = num3 + num5 + num7 + num9;
		num = (num10 + num11) / 100f;
		num10 /= num;
		num11 /= num;
		delayToFightCorut = StartCoroutine(DelayToFight(num10, num11));
	}

	private IEnumerator DelayToFight(float sumLeft, float sumRight)
	{
		StatArrowOkLeft.SetBool(is_greater, value: false);
		StatArrowOkLeft.SetBool(is_less, value: false);
		StatArrowOkLeft.SetBool(is_equal, value: false);
		StatArrowOkRight.SetBool(is_greater, value: false);
		StatArrowOkRight.SetBool(is_less, value: false);
		StatArrowOkRight.SetBool(is_equal, value: false);
		string PERC_txt = "%";
		Audio.PlayAsync("gui_button_02_sn", 1.2f, loop: true);
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime / 2f;
			LeftFightPercentText.text = Math.Round((double)sumLeft * (double)t, 1).ToString() + PERC_txt;
			RightFightPercentText.text = Math.Round((double)sumRight * (double)t, 1).ToString() + PERC_txt;
			yield return null;
		}
		LeftFightPercentText.text = Math.Round(sumLeft, 1).ToString() + PERC_txt;
		RightFightPercentText.text = Math.Round(sumRight, 1).ToString() + PERC_txt;
		Audio.Stop("gui_button_02_sn");
		yield return new WaitForSeconds(0.5f);
		Audio.PlayRandom("actor_roar_01_sn", "actor_roar_02_sn", "actor_roar_03_sn", "actor_roar_04_sn", "actor_roar_05_sn", "actor_roar_06_sn", "actor_roar_07_sn");
		if (sumLeft == sumRight)
		{
			StatArrowOkLeft.SetBool(is_equal, value: true);
			StatArrowOkRight.SetBool(is_equal, value: true);
		}
		else if (sumLeft > sumRight)
		{
			StatArrowOkLeft.SetBool(is_greater, value: true);
			StatArrowOkRight.SetBool(is_less, value: true);
		}
		else
		{
			StatArrowOkLeft.SetBool(is_less, value: true);
			StatArrowOkRight.SetBool(is_greater, value: true);
		}
		yield return new WaitForSeconds(0.5f);
		if (sumLeft == sumRight)
		{
			StatArrowBeter.SetBool(is_equal, value: true);
		}
		else if (sumLeft > sumRight)
		{
			StatArrowBeter.SetBool(is_greater, value: true);
		}
		else
		{
			StatArrowBeter.SetBool(is_less, value: true);
		}
	}

	private void LeftGoTo_Car()
	{
		_scrollGarage.SetgoToCardX(right: true, _carIndexLeft + 1);
		_goTo = true;
		Hide();
	}

	private void RightGoTo_Car()
	{
		_scrollGarage.SetgoToCardX(right: true, _carIndexRight + 1);
		_goTo = true;
		Hide();
	}

	private void Hide()
	{
		LeftPanelGOTO_anim.SetBool(is_ON, value: false);
		RightPanelGOTO_anim.SetBool(is_ON, value: false);
		StartCoroutine(DelayHide());
	}

	private IEnumerator DelayHide()
	{
		yield return new WaitForSeconds(0.3f);
		ShowHideAnim.SetBool(is_ON, value: false);
		if (_goTo)
		{
			yield return new WaitForSeconds(0.1f);
			_scrollGarage.CarList = true;
			_scrollGarage.EndDrag();
		}
	}

	private void OnDisable()
	{
		CloseBut.onClick.RemoveAllListeners();
		ShowBut.onClick.RemoveAllListeners();
		LeftPanelGOTO_but.onClick.RemoveAllListeners();
		RightPanelGOTO_but.onClick.RemoveAllListeners();
		Fight_but.onClick.RemoveAllListeners();
	}
}

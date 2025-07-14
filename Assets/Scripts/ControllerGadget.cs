using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGadget : MonoBehaviour
{
	[Header("Animators")]
	public Animator bomb1;

	public Animator bomb2;

	public Animator bomb3;

	public Animator bomb4;

	public Animator bomb5;

	public Animator btn;

	[Header("Buttons")]
	public Button Equip;

	public Button Buy;

	public Button _btn_bomb1;

	public Button _btn_bomb2;

	public Button _btn_bomb3;

	public Button _btn_bomb4;

	public Button _btn_bomb5;

	public ControllerGarage CG;

	public MarkerGarage MG;

	private int _isEquiped = Animator.StringToHash("isEquiped");

	private int _isBought = Animator.StringToHash("isBought");

	private int _isChoosen = Animator.StringToHash("isChoosen");

	private static string str_gui_button_02_sn = "gui_button_02_sn";

	private static string str_gui_shop_purchase_01_sn = "gui_shop_purchase_01_sn";

	private static string str_actor_gadget_shield_sn = "actor_gadget_shield_sn";

	public Text price;

	private int _missileLauncha_isOn = Animator.StringToHash("missileLauncha_isOn");

	private int _EMPemmiter_isOn = Animator.StringToHash("EMPemmiter_isOn");

	private int _magnet_isOn = Animator.StringToHash("magnet_isOn");

	private int _bombGenerator_isOn = Animator.StringToHash("bombGenerator_isOn");

	private int _freezeRay_isOn = Animator.StringToHash("freezeRay_isOn");

	private void OnEnable()
	{
		Buy.onClick.AddListener(buy);
		_btn_bomb1.onClick.AddListener(OnClicBtnBomb1);
		_btn_bomb2.onClick.AddListener(OnClicBtnBomb2);
		_btn_bomb3.onClick.AddListener(OnClicBtnBomb3);
		_btn_bomb4.onClick.AddListener(OnClicBtnBomb4);
		_btn_bomb5.onClick.AddListener(OnClicBtnBomb5);
		StartCoroutine(fuck2());
	}

	private IEnumerator fuck2()
	{
		while (!btn.isInitialized)
		{
			yield return 0;
		}
		btn.SetBool(_isEquiped, value: true);
		btn.SetBool(_isBought, value: true);
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
		yield return 0;
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_MISSLLE_bounght)
		{
			bomb1.SetBool(_isBought, value: true);
			bomb1.SetBool(_isEquiped, value: true);
			while (!btn.isInitialized)
			{
				yield return 0;
			}
			btn.SetBool(_isEquiped, value: true);
			btn.SetBool(_isBought, value: true);
		}
		else
		{
			bomb1.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_LEDOLUCH_bounght)
		{
			bomb2.SetBool(_isBought, value: true);
			bomb2.SetBool(_isEquiped, value: true);
			while (!btn.isInitialized)
			{
				yield return 0;
			}
			btn.SetBool(_isEquiped, value: true);
			btn.SetBool(_isBought, value: true);
		}
		else
		{
			bomb2.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_Magnet_bounght)
		{
			bomb3.SetBool(_isBought, value: true);
			bomb3.SetBool(_isEquiped, value: true);
			while (!btn.isInitialized)
			{
				yield return 0;
			}
			btn.SetBool(_isEquiped, value: true);
			btn.SetBool(_isBought, value: true);
		}
		else
		{
			bomb3.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_RECHARGER_bounght)
		{
			bomb4.SetBool(_isBought, value: true);
			bomb4.SetBool(_isEquiped, value: true);
			while (!btn.isInitialized)
			{
				yield return 0;
			}
			btn.SetBool(_isEquiped, value: true);
			btn.SetBool(_isBought, value: true);
		}
		else
		{
			bomb4.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_EMP_bounght)
		{
			bomb5.SetBool(_isBought, value: true);
			bomb5.SetBool(_isEquiped, value: true);
			while (!btn.isInitialized)
			{
				yield return 0;
			}
			btn.SetBool(_isEquiped, value: true);
			btn.SetBool(_isBought, value: true);
		}
		else
		{
			bomb5.SetBool(_isBought, value: false);
		}
		yield return new WaitForSeconds(0.1f);
		if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_MISSLLE_bounght)
		{
			if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_LEDOLUCH_bounght)
			{
				if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_Magnet_bounght)
				{
					if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_RECHARGER_bounght)
					{
						if (Progress.shop.Cars[Progress.shop.activeCar].Gadget_EMP_bounght)
						{
							OnClicBtnBomb5();
						}
						else
						{
							OnClicBtnBomb4();
						}
					}
					else
					{
						OnClicBtnBomb3();
					}
				}
				else
				{
					OnClicBtnBomb2();
				}
			}
			else
			{
				OnClicBtnBomb1();
			}
		}
		else
		{
			OnClicBtnBomb1();
		}
	}

	private IEnumerator fuck()
	{
		yield return new WaitForSeconds(0.42f);
	}

	private void OnDisable()
	{
		Equip.onClick.RemoveAllListeners();
		Buy.onClick.RemoveAllListeners();
		_btn_bomb2.onClick.RemoveAllListeners();
		_btn_bomb3.onClick.RemoveAllListeners();
		_btn_bomb4.onClick.RemoveAllListeners();
		_btn_bomb5.onClick.RemoveAllListeners();
		_btn_bomb1.onClick.RemoveAllListeners();
	}

	private void buy()
	{
		Audio.Play(str_gui_button_02_sn);
		if (!bomb1.GetBool(_isBought) && bomb1.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Gadget[3].price <= Progress.shop.currency)
			{
				Audio.Play(str_gui_shop_purchase_01_sn);
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Gadget[3].price;
				Progress.Shop.CarInfo[] cars = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo in cars)
				{
					carInfo.Gadget_MISSLLE_bounght = true;
				}
				AnalyticsManager.LogEvent(EventCategoty.gadgets, "g1_rocket", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb1.SetBool(_isChoosen, value: true);
				bomb1.SetBool(_isBought, value: true);
				bomb1.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		}
		if (!bomb2.GetBool(_isBought) && bomb2.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Gadget[2].price <= Progress.shop.currency)
			{
				Audio.Play(str_gui_shop_purchase_01_sn);
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Gadget[2].price;
				Progress.Shop.CarInfo[] cars2 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo2 in cars2)
				{
					carInfo2.Gadget_LEDOLUCH_bounght = true;
				}
				AnalyticsManager.LogEvent(EventCategoty.gadgets, "g2_freez", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: true);
				bomb2.SetBool(_isBought, value: true);
				bomb2.SetBool(_isEquiped, value: true);
				bomb1.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		}
		if (!bomb3.GetBool(_isBought) && bomb3.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Gadget[0].price <= Progress.shop.currency)
			{
				Audio.Play(str_gui_shop_purchase_01_sn);
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Gadget[0].price;
				Progress.Shop.CarInfo[] cars3 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo3 in cars3)
				{
					carInfo3.Gadget_Magnet_bounght = true;
				}
				AnalyticsManager.LogEvent(EventCategoty.gadgets, "g3_magnet", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb3.SetBool(_isChoosen, value: true);
				bomb3.SetBool(_isBought, value: true);
				bomb3.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		}
		if (!bomb4.GetBool(_isBought) && bomb4.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Gadget[4].price <= Progress.shop.currency)
			{
				Audio.Play(str_gui_shop_purchase_01_sn);
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Gadget[4].price;
				Progress.Shop.CarInfo[] cars4 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo4 in cars4)
				{
					carInfo4.Gadget_RECHARGER_bounght = true;
				}
				AnalyticsManager.LogEvent(EventCategoty.gadgets, "g4_replicator", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb4.SetBool(_isChoosen, value: true);
				bomb4.SetBool(_isBought, value: true);
				bomb4.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		}
		if (!bomb5.GetBool(_isBought) && bomb5.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Gadget[1].price <= Progress.shop.currency)
			{
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Gadget[1].price;
				Audio.Play(str_gui_shop_purchase_01_sn);
				Progress.Shop.CarInfo[] cars5 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo5 in cars5)
				{
					carInfo5.Gadget_EMP_bounght = true;
				}
				AnalyticsManager.LogEvent(EventCategoty.gadgets, "g5_emp", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb5.SetBool(_isChoosen, value: true);
				bomb5.SetBool(_isBought, value: true);
				bomb5.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
			}
			else
			{
				CG.ShowBuyCanvasWindow(isCoins: true);
			}
		}
		GameCenter.OnPurchaseGadgets();
	}

	public void GadgetsAnimatorsOff()
	{
		if (Progress.shop.activeCar - 1 >= 0)
		{
			if (CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Missle.GetBool(_missileLauncha_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Magnet.GetBool(_magnet_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar - 1].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
			}
		}
		if (Progress.shop.activeCar >= 0)
		{
			if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(_missileLauncha_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(_magnet_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
			}
		}
		if (Progress.shop.activeCar + 1 <= CG.controllers_Cars.Count - 1 && Progress.shop.activeCar > -2)
		{
			if (CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Missle.GetBool(_missileLauncha_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Magnet.GetBool(_magnet_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
			}
			if (CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
			{
				CG.controllers_Cars[Progress.shop.activeCar + 1].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
			}
		}
	}

	private void OnClicBtnBomb1()
	{
		MG.gadgets1.SetActive(value: false);
		price.text = ShopManagerPrice.instance.Price.Gadget[3].price.ToString();
		if (bomb1.GetBool(_isBought) && bomb1.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else if (bomb1.GetBool(_isBought) && !bomb1.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: false);
		}
		else if (!bomb1.GetBool(_isBought) && !bomb1.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: false);
			btn.SetBool(_isEquiped, value: false);
		}
		if (bomb1.GetBool(_isChoosen))
		{
			Audio.Play(str_gui_button_02_sn);
		}
		else
		{
			Audio.PlayAsync(str_actor_gadget_shield_sn);
		}
		bomb1.SetBool(_isChoosen, value: true);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
		CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: true);
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(_magnet_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
		}
	}

	private void OnClicBtnBomb2()
	{
		MG.gadgets2.SetActive(value: false);
		price.text = ShopManagerPrice.instance.Price.Gadget[2].price.ToString();
		if (bomb2.GetBool(_isBought) && bomb2.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else if (bomb2.GetBool(_isBought) && !bomb2.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: false);
		}
		else if (!bomb2.GetBool(_isBought) && !bomb2.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: false);
			btn.SetBool(_isEquiped, value: false);
		}
		if (bomb2.GetBool(_isChoosen))
		{
			Audio.Play(str_gui_button_02_sn);
		}
		else
		{
			Audio.PlayAsync(str_actor_gadget_shield_sn);
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: true);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
		CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: true);
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(_missileLauncha_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(_magnet_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
		}
	}

	private void OnClicBtnBomb3()
	{
		MG.gadgets3.SetActive(value: false);
		price.text = ShopManagerPrice.instance.Price.Gadget[0].price.ToString();
		if (bomb3.GetBool(_isBought) && bomb3.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else if (bomb3.GetBool(_isBought) && !bomb3.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: false);
		}
		else if (!bomb3.GetBool(_isBought) && !bomb3.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: false);
			btn.SetBool(_isEquiped, value: false);
		}
		if (bomb3.GetBool(_isChoosen))
		{
			Audio.Play(str_gui_button_02_sn);
		}
		else
		{
			Audio.PlayAsync(str_actor_gadget_shield_sn);
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: true);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
		CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: true);
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(_missileLauncha_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
		}
	}

	private void OnClicBtnBomb4()
	{
		MG.gadgets4.SetActive(value: false);
		price.text = ShopManagerPrice.instance.Price.Gadget[4].price.ToString();
		if (bomb4.GetBool(_isBought) && bomb4.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else if (bomb4.GetBool(_isBought) && !bomb4.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: false);
		}
		else if (!bomb4.GetBool(_isBought) && !bomb4.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: false);
			btn.SetBool(_isEquiped, value: false);
		}
		if (bomb4.GetBool(_isChoosen))
		{
			Audio.Play(str_gui_button_02_sn);
		}
		else
		{
			Audio.PlayAsync(str_actor_gadget_shield_sn);
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: true);
		bomb5.SetBool(_isChoosen, value: false);
		CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: true);
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.GetBool(_EMPemmiter_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(_magnet_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(_missileLauncha_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
		}
	}

	private void OnClicBtnBomb5()
	{
		MG.gadgets5.SetActive(value: false);
		price.text = ShopManagerPrice.instance.Price.Gadget[1].price.ToString();
		if (bomb5.GetBool(_isBought) && bomb5.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else if (bomb5.GetBool(_isBought) && !bomb5.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: true);
			btn.SetBool(_isEquiped, value: false);
		}
		else if (!bomb5.GetBool(_isBought) && !bomb5.GetBool(_isEquiped))
		{
			btn.SetBool(_isBought, value: false);
			btn.SetBool(_isEquiped, value: false);
		}
		if (bomb5.GetBool(_isChoosen))
		{
			Audio.Play(str_gui_button_02_sn);
		}
		else
		{
			Audio.PlayAsync(str_actor_gadget_shield_sn);
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: true);
		CG.controllers_Cars[Progress.shop.activeCar].animGadgets.EMP.SetBool(_EMPemmiter_isOn, value: true);
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.GetBool(_bombGenerator_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Rechrger.SetBool(_bombGenerator_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.GetBool(_missileLauncha_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Missle.SetBool(_missileLauncha_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.GetBool(_magnet_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.Magnet.SetBool(_magnet_isOn, value: false);
		}
		if (CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.GetBool(_freezeRay_isOn))
		{
			CG.controllers_Cars[Progress.shop.activeCar].animGadgets.LedoLuch.SetBool(_freezeRay_isOn, value: false);
		}
	}
}

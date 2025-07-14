using UnityEngine;
using UnityEngine.UI;

public class ControllerBomb : MonoBehaviour
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

	public MarkerGarage MG;

	private int _isBought = Animator.StringToHash("isBought");

	private int _isEquiped = Animator.StringToHash("isEquiped");

	private int _isChoosen = Animator.StringToHash("isChoosen");

	public ControllerGarage CG;

	public Text price;

	private void OnEnable()
	{
		btn.SetBool(_isBought, value: false);
		if (!Progress.shop.Cars[Progress.shop.activeCar].bomb_0_bounght && !Progress.shop.Cars[Progress.shop.activeCar].bomb_1_bounght && !Progress.shop.Cars[Progress.shop.activeCar].bomb_2_bounght && !Progress.shop.Cars[Progress.shop.activeCar].bomb_3_bounght && !Progress.shop.Cars[Progress.shop.activeCar].bomb_4_bounght)
		{
			OnClicBtnBomb1();
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bomb_0_bounght)
		{
			btn.SetBool(_isBought, value: true);
			bomb1.SetBool(_isBought, value: true);
		}
		else
		{
			bomb1.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bomb_1_bounght)
		{
			btn.SetBool(_isBought, value: true);
			bomb2.SetBool(_isBought, value: true);
		}
		else
		{
			bomb2.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bomb_2_bounght)
		{
			btn.SetBool(_isBought, value: true);
			bomb3.SetBool(_isBought, value: true);
		}
		else
		{
			bomb3.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bomb_3_bounght)
		{
			btn.SetBool(_isBought, value: true);
			bomb4.SetBool(_isBought, value: true);
		}
		else
		{
			bomb4.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bomb_4_bounght)
		{
			btn.SetBool(_isBought, value: true);
			bomb5.SetBool(_isBought, value: true);
		}
		else
		{
			bomb5.SetBool(_isBought, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bombActLev == 5)
		{
			bomb1.SetBool(_isEquiped, value: true);
			bomb1.SetBool(_isChoosen, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else
		{
			bomb1.SetBool(_isEquiped, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bombActLev == 1)
		{
			bomb2.SetBool(_isEquiped, value: true);
			bomb2.SetBool(_isChoosen, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else
		{
			bomb2.SetBool(_isEquiped, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bombActLev == 2)
		{
			bomb3.SetBool(_isEquiped, value: true);
			bomb3.SetBool(_isChoosen, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else
		{
			bomb3.SetBool(_isEquiped, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bombActLev == 3)
		{
			bomb4.SetBool(_isEquiped, value: true);
			bomb4.SetBool(_isChoosen, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else
		{
			bomb4.SetBool(_isEquiped, value: false);
		}
		if (Progress.shop.Cars[Progress.shop.activeCar].bombActLev == 4)
		{
			bomb5.SetBool(_isEquiped, value: true);
			bomb5.SetBool(_isChoosen, value: true);
			btn.SetBool(_isEquiped, value: true);
		}
		else
		{
			bomb5.SetBool(_isEquiped, value: false);
		}
		Equip.onClick.AddListener(equip);
		Buy.onClick.AddListener(buy);
		_btn_bomb1.onClick.AddListener(OnClicBtnBomb1);
		_btn_bomb2.onClick.AddListener(OnClicBtnBomb2);
		_btn_bomb3.onClick.AddListener(OnClicBtnBomb3);
		_btn_bomb4.onClick.AddListener(OnClicBtnBomb4);
		_btn_bomb5.onClick.AddListener(OnClicBtnBomb5);
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

	private void equip()
	{
		Audio.Play("gui_button_02_sn");
		if (bomb1.GetBool(_isBought) && !bomb1.GetBool(_isEquiped) && bomb1.GetBool(_isChoosen))
		{
			Progress.shop.Cars[Progress.shop.activeCar].bombActLev = 5;
			bomb1.SetBool(_isChoosen, value: true);
			bomb1.SetBool(_isBought, value: true);
			bomb1.SetBool(_isEquiped, value: true);
			bomb2.SetBool(_isEquiped, value: false);
			bomb3.SetBool(_isEquiped, value: false);
			bomb4.SetBool(_isEquiped, value: false);
			bomb5.SetBool(_isEquiped, value: false);
			bomb2.SetBool(_isChoosen, value: false);
			bomb3.SetBool(_isChoosen, value: false);
			bomb4.SetBool(_isChoosen, value: false);
			bomb5.SetBool(_isChoosen, value: false);
			btn.SetBool(_isEquiped, value: true);
		}
		if (bomb2.GetBool(_isBought) && !bomb2.GetBool(_isEquiped) && bomb2.GetBool(_isChoosen))
		{
			Progress.shop.Cars[Progress.shop.activeCar].bombActLev = 1;
			bomb2.SetBool(_isChoosen, value: true);
			bomb2.SetBool(_isBought, value: true);
			bomb2.SetBool(_isEquiped, value: true);
			bomb1.SetBool(_isEquiped, value: false);
			bomb3.SetBool(_isEquiped, value: false);
			bomb4.SetBool(_isEquiped, value: false);
			bomb5.SetBool(_isEquiped, value: false);
			bomb1.SetBool(_isChoosen, value: false);
			bomb3.SetBool(_isChoosen, value: false);
			bomb4.SetBool(_isChoosen, value: false);
			bomb5.SetBool(_isChoosen, value: false);
			btn.SetBool(_isEquiped, value: true);
		}
		if (bomb3.GetBool(_isBought) && !bomb3.GetBool(_isEquiped) && bomb3.GetBool(_isChoosen))
		{
			Progress.shop.Cars[Progress.shop.activeCar].bombActLev = 2;
			bomb3.SetBool(_isChoosen, value: true);
			bomb3.SetBool(_isBought, value: true);
			bomb3.SetBool(_isEquiped, value: true);
			bomb2.SetBool(_isEquiped, value: false);
			bomb1.SetBool(_isEquiped, value: false);
			bomb4.SetBool(_isEquiped, value: false);
			bomb5.SetBool(_isEquiped, value: false);
			bomb2.SetBool(_isChoosen, value: false);
			bomb1.SetBool(_isChoosen, value: false);
			bomb4.SetBool(_isChoosen, value: false);
			bomb5.SetBool(_isChoosen, value: false);
			btn.SetBool(_isEquiped, value: true);
		}
		if (bomb4.GetBool(_isBought) && !bomb4.GetBool(_isEquiped) && bomb4.GetBool(_isChoosen))
		{
			Progress.shop.Cars[Progress.shop.activeCar].bombActLev = 3;
			bomb4.SetBool(_isChoosen, value: true);
			bomb4.SetBool(_isBought, value: true);
			bomb4.SetBool(_isEquiped, value: true);
			bomb2.SetBool(_isEquiped, value: false);
			bomb3.SetBool(_isEquiped, value: false);
			bomb1.SetBool(_isEquiped, value: false);
			bomb5.SetBool(_isEquiped, value: false);
			bomb2.SetBool(_isChoosen, value: false);
			bomb3.SetBool(_isChoosen, value: false);
			bomb1.SetBool(_isChoosen, value: false);
			bomb5.SetBool(_isChoosen, value: false);
			btn.SetBool(_isEquiped, value: true);
		}
		if (bomb5.GetBool(_isBought) && !bomb5.GetBool(_isEquiped) && bomb5.GetBool(_isChoosen))
		{
			Progress.shop.Cars[Progress.shop.activeCar].bombActLev = 4;
			btn.SetBool(_isEquiped, value: true);
			bomb5.SetBool(_isChoosen, value: true);
			bomb5.SetBool(_isBought, value: true);
			bomb5.SetBool(_isEquiped, value: true);
			bomb2.SetBool(_isEquiped, value: false);
			bomb3.SetBool(_isEquiped, value: false);
			bomb4.SetBool(_isEquiped, value: false);
			bomb1.SetBool(_isEquiped, value: false);
			bomb2.SetBool(_isChoosen, value: false);
			bomb3.SetBool(_isChoosen, value: false);
			bomb4.SetBool(_isChoosen, value: false);
			bomb1.SetBool(_isChoosen, value: false);
		}
	}

	private void buy()
	{
		Audio.Play("gui_button_02_sn");
		if (!bomb1.GetBool(_isBought) && bomb1.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Bomb[0].price <= Progress.shop.currency)
			{
				Audio.Play("gui_shop_purchase_01_sn");
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Bomb[0].price;
				AnalyticsManager.LogEvent(EventCategoty.bombs, "b1_basic", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				Progress.Shop.CarInfo[] cars = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo in cars)
				{
					carInfo.bombActLev = 5;
					carInfo.bomb_0_bounght = true;
				}
				Progress.Save(Progress.SaveType.Shop);
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb1.SetBool(_isChoosen, value: true);
				bomb1.SetBool(_isBought, value: true);
				bomb1.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				bomb2.SetBool(_isEquiped, value: false);
				bomb3.SetBool(_isEquiped, value: false);
				bomb4.SetBool(_isEquiped, value: false);
				bomb5.SetBool(_isEquiped, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
				return;
			}
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
		if (!bomb2.GetBool(_isBought) && bomb2.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Bomb[1].price <= Progress.shop.currency)
			{
				Audio.Play("gui_shop_purchase_01_sn");
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Bomb[1].price;
				AnalyticsManager.LogEvent(EventCategoty.bombs, "b2_frag", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				Progress.Shop.CarInfo[] cars2 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo2 in cars2)
				{
					carInfo2.bombActLev = 1;
					carInfo2.bomb_1_bounght = true;
				}
				Progress.Save(Progress.SaveType.Shop);
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: true);
				bomb2.SetBool(_isBought, value: true);
				bomb2.SetBool(_isEquiped, value: true);
				bomb1.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isEquiped, value: false);
				bomb3.SetBool(_isEquiped, value: false);
				bomb4.SetBool(_isEquiped, value: false);
				bomb5.SetBool(_isEquiped, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
				return;
			}
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
		if (!bomb3.GetBool(_isBought) && bomb3.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Bomb[2].price <= Progress.shop.currency)
			{
				Audio.Play("gui_shop_purchase_01_sn");
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Bomb[2].price;
				AnalyticsManager.LogEvent(EventCategoty.bombs, "b3_flame", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				Progress.Shop.CarInfo[] cars3 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo3 in cars3)
				{
					carInfo3.bombActLev = 2;
					carInfo3.bomb_2_bounght = true;
				}
				Progress.Save(Progress.SaveType.Shop);
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb3.SetBool(_isChoosen, value: true);
				bomb3.SetBool(_isBought, value: true);
				bomb3.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				bomb2.SetBool(_isEquiped, value: false);
				bomb1.SetBool(_isEquiped, value: false);
				bomb4.SetBool(_isEquiped, value: false);
				bomb5.SetBool(_isEquiped, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
				return;
			}
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
		if (!bomb4.GetBool(_isBought) && bomb4.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Bomb[3].price <= Progress.shop.currency)
			{
				Audio.Play("gui_shop_purchase_01_sn");
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Bomb[3].price;
				AnalyticsManager.LogEvent(EventCategoty.bombs, "b4_cluster", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				Progress.Shop.CarInfo[] cars4 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo4 in cars4)
				{
					carInfo4.bombActLev = 3;
					carInfo4.bomb_3_bounght = true;
				}
				Progress.Save(Progress.SaveType.Shop);
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb4.SetBool(_isChoosen, value: true);
				bomb4.SetBool(_isBought, value: true);
				bomb4.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				bomb5.SetBool(_isChoosen, value: false);
				bomb2.SetBool(_isEquiped, value: false);
				bomb3.SetBool(_isEquiped, value: false);
				bomb1.SetBool(_isEquiped, value: false);
				bomb5.SetBool(_isEquiped, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
				return;
			}
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
		if (!bomb5.GetBool(_isBought) && bomb5.GetBool(_isChoosen))
		{
			if (ShopManagerPrice.instance.Price.Bomb[4].price <= Progress.shop.currency)
			{
				Audio.Play("gui_shop_purchase_01_sn");
				Progress.shop.currency -= ShopManagerPrice.instance.Price.Bomb[4].price;
				AnalyticsManager.LogEvent(EventCategoty.bombs, "b5_shrink", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
				Progress.Shop.CarInfo[] cars5 = Progress.shop.Cars;
				foreach (Progress.Shop.CarInfo carInfo5 in cars5)
				{
					carInfo5.bombActLev = 4;
					carInfo5.bomb_4_bounght = true;
				}
				Progress.Save(Progress.SaveType.Shop);
				btn.SetBool(_isBought, value: true);
				btn.SetBool(_isEquiped, value: true);
				bomb5.SetBool(_isChoosen, value: true);
				bomb5.SetBool(_isBought, value: true);
				bomb5.SetBool(_isEquiped, value: true);
				bomb2.SetBool(_isChoosen, value: false);
				bomb3.SetBool(_isChoosen, value: false);
				bomb4.SetBool(_isChoosen, value: false);
				bomb1.SetBool(_isChoosen, value: false);
				bomb2.SetBool(_isEquiped, value: false);
				bomb3.SetBool(_isEquiped, value: false);
				bomb4.SetBool(_isEquiped, value: false);
				bomb1.SetBool(_isEquiped, value: false);
				CG.SetCoinsLabel(Progress.shop.currency);
				return;
			}
			CG.ShowBuyCanvasWindow(isCoins: true);
		}
		GameCenter.OnPurchaseBombs();
	}

	private void OnClicBtnBomb1()
	{
		MG.bomb1.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
		price.text = ShopManagerPrice.instance.Price.Bomb[0].price.ToString();
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
		bomb1.SetBool(_isChoosen, value: true);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
	}

	private void OnClicBtnBomb2()
	{
		MG.bomb2.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
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
			price.text = ShopManagerPrice.instance.Price.Bomb[1].price.ToString();
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: true);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
	}

	private void OnClicBtnBomb3()
	{
		MG.bomb3.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
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
			price.text = ShopManagerPrice.instance.Price.Bomb[2].price.ToString();
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: true);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: false);
	}

	private void OnClicBtnBomb4()
	{
		MG.bomb4.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
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
			price.text = ShopManagerPrice.instance.Price.Bomb[3].price.ToString();
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: true);
		bomb5.SetBool(_isChoosen, value: false);
	}

	private void OnClicBtnBomb5()
	{
		MG.bomb5.SetActive(value: false);
		Audio.Play("gui_button_02_sn");
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
			price.text = ShopManagerPrice.instance.Price.Bomb[4].price.ToString();
		}
		bomb1.SetBool(_isChoosen, value: false);
		bomb2.SetBool(_isChoosen, value: false);
		bomb3.SetBool(_isChoosen, value: false);
		bomb4.SetBool(_isChoosen, value: false);
		bomb5.SetBool(_isChoosen, value: true);
	}
}

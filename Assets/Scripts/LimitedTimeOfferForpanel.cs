using CompleteProject;
using SmartLocalization;
using Smokoko.DebugModule;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LimitedTimeOfferForpanel : MonoBehaviour
{
	public Animator AnimShowHide;

	public Text timerInBody;

	public Text timerInBody2;

	public Text timerInBody3;

	public Button _btnBodyBuy;

	public Button _btnBodyBuyCars;

	public Button _btnBodyBuyCars2;

	public Button _btnBodyExit;

	public float TimerShow;

	public float TimerShow2;

	public float TimerShow3;

	public float TimeRefresh;

	public Text Price;

	public Text PriceCars;

	public Text PriceCars2;

	public GameObject offer1;

	public GameObject offer2;

	public GameObject offer3;

	private int isON = Animator.StringToHash("isON");

	private DateTime? TimerForSpecialOfferShow;

	private DateTime? TimerForSpecialOfferRefresh;

	private void OnEnable()
	{
		_btnBodyBuy.onClick.AddListener(ClicBuy);
		_btnBodyBuyCars.onClick.AddListener(ClicBuyCars);
		_btnBodyBuyCars2.onClick.AddListener(ClicBuyCars2);
		_btnBodyExit.onClick.AddListener(ClicExit);
		string text = Purchaser.m_StoreController.products.WithID(Purchaser.Megapack).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Megapack).metadata.localizedPrice.ToString();
		text = ((!string.IsNullOrEmpty(text)) ? text : "$9.99");
		Price.text = text;
		string text2 = Purchaser.m_StoreController.products.WithID(Purchaser.Carpark).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Carpark).metadata.localizedPrice.ToString();
		text2 = ((!string.IsNullOrEmpty(text2)) ? text2 : "$9.99");
		PriceCars.text = text2;
		string text3 = Purchaser.m_StoreController.products.WithID(Purchaser.Carpark2).metadata.isoCurrencyCode + Purchaser.m_StoreController.products.WithID(Purchaser.Carpark2).metadata.localizedPrice.ToString();
		text3 = ((!string.IsNullOrEmpty(text2)) ? text3 : "$9.99");
		PriceCars2.text = text3;
		AnimShowHide.SetBool(isON, value: true);
		if (Progress.shop.TimerForSpecialOfferShow == string.Empty)
		{
			Progress.shop.TimerForSpecialOfferShow = DateTime.MinValue.ToString();
			TimerForSpecialOfferShow = DateTime.MinValue;
		}
		if (!TimerForSpecialOfferShow.HasValue)
		{
			TimerForSpecialOfferShow = DateTime.Parse(Progress.shop.TimerForSpecialOfferShow);
		}
		TimeSpan timeSpan = DateTime.Now - TimerForSpecialOfferShow.Value;
		double num = (double)((TimerShow - 1f) * 60f * 60f) - timeSpan.TotalSeconds;
		double num2 = (double)((TimerShow2 - 1f) * 60f * 60f) - timeSpan.TotalSeconds;
		double num3 = (double)((TimerShow3 - 1f) * 60f * 60f) - timeSpan.TotalSeconds;
		Notifications("SO1", 5, (int)num);
		Notifications("SO2", 6, (int)num2);
		Notifications("SO3", 7, (int)num3);
	}

	private void OnDisable()
	{
		_btnBodyBuy.onClick.RemoveAllListeners();
		_btnBodyExit.onClick.RemoveAllListeners();
		_btnBodyBuyCars.onClick.RemoveAllListeners();
		_btnBodyBuyCars2.onClick.RemoveAllListeners();
	}

	private void ClicBuy()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Buy MEGA PACK!!!!! ", new ButtonInfo("Buy", delegate
			{
				Buy();
			}));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.Megapack, Buy);
		}
	}

	private void Buy()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "mega_pack", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Level.ToString());
		Progress.shop.BuyForRealMoney = true;
		Progress.shop.dronBeeBuy = true;
		Progress.shop.dronBeeActive = true;
		Progress.shop.Cars[3].equipped = true;
		Progress.shop.Cars[3].bought = true;
		Progress.gameEnergy.isInfinite = true;
		Progress.review.atLeastOnePurchase = true;
		Progress.shop.BuyLimittedOffer = true;
		ClicExit();
	}

	private void ClicBuyCars()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Buy CAR PACK!!!!! ", new ButtonInfo("Buy", delegate
			{
				BuyCars();
			}));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.Carpark, BuyCars);
		}
	}

	private void BuyCars()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "carpack", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Level.ToString());
		Progress.shop.Cars[3].equipped = true;
		Progress.shop.Cars[3].bought = true;
		Progress.shop.Cars[4].equipped = true;
		Progress.shop.Cars[4].bought = true;
		Progress.shop.Cars[5].equipped = true;
		Progress.shop.Cars[5].bought = true;
		Progress.shop.Cars[6].equipped = true;
		Progress.shop.Cars[6].bought = true;
		Progress.shop.Get1partForPoliceCar = true;
		Progress.shop.Get2partForPoliceCar = true;
		Progress.shop.Get3partForPoliceCar = true;
		Progress.shop.Get4partForPoliceCar = true;
		Progress.shop.CollKill1Car = 120;
		Progress.shop.CollKill2Car = 120;
		Progress.shop.CollKill3Car = 120;
		Progress.shop.CollKill4Car = 120;
		Progress.shop.BuyLimittedOffer = true;
		ClicExit();
	}

	private void ClicBuyCars2()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Buy CAR PACK!!!!! ", new ButtonInfo("Buy", delegate
			{
				BuyCars2();
			}));
		}
		else
		{
			Purchaser.BuyProductID(Purchaser.Carpark2, BuyCars2);
		}
	}

	private void BuyCars2()
	{
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "carpack_2", Progress.levels.Max_Active_Pack.ToString() + "_" + Progress.levels.Max_Active_Level.ToString());
		Progress.shop.BuyForRealMoney = true;
		Progress.shop.Cars[7].equipped = true;
		Progress.shop.Cars[7].bought = true;
		Progress.shop.Cars[8].equipped = true;
		Progress.shop.Cars[8].bought = true;
		Progress.shop.Cars[9].equipped = true;
		Progress.shop.Cars[9].bought = true;
		Progress.shop.Cars[10].equipped = true;
		Progress.shop.Cars[10].bought = true;
		Progress.shop.Get1partForPoliceCar2 = true;
		Progress.shop.Get2partForPoliceCar2 = true;
		Progress.shop.Get3partForPoliceCar2 = true;
		Progress.shop.Get4partForPoliceCar2 = true;
		Progress.shop.CollKill1Car2 = 1200;
		Progress.shop.CollKill2Car2 = 1200;
		Progress.shop.CollKill3Car2 = 1200;
		Progress.shop.CollKill4Car2 = 1200;
		Progress.shop.BuyLimittedOffer = true;
		ClicExit();
	}

	private void ClicExit()
	{
		Game.OnStateChange(Game.gameState.Levels);
		AnimShowHide.SetBool(isON, value: false);
		StartCoroutine(DelayToHide());
	}

	private IEnumerator DelayToHide()
	{
		yield return new WaitForSeconds(0.5f);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			ClicExit();
		}
		if (Progress.shop.Cars[7].equipped && Progress.shop.Cars[8].equipped && Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped)
		{
			offer3.SetActive(value: false);
		}
		if (Progress.shop.Cars[3].equipped && Progress.shop.Cars[4].equipped && Progress.shop.Cars[5].equipped && Progress.shop.Cars[6].equipped)
		{
			offer2.SetActive(value: false);
		}
		if (Progress.shop.dronBeeBuy && Progress.shop.Cars[3].equipped && Progress.gameEnergy.isInfinite)
		{
			offer1.SetActive(value: false);
		}
		Timer();
		if (Progress.shop.Cars[7].equipped && Progress.shop.Cars[8].equipped && Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped && Progress.shop.Cars[3].equipped && Progress.shop.Cars[4].equipped && Progress.shop.Cars[5].equipped && Progress.shop.Cars[6].equipped && Progress.shop.dronBeeBuy && Progress.gameEnergy.isInfinite)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void Timer()
	{
		if (Progress.shop.TimerForSpecialOfferShow == string.Empty)
		{
			Progress.shop.TimerForSpecialOfferShow = DateTime.MinValue.ToString();
			TimerForSpecialOfferShow = DateTime.MinValue;
		}
		if (!TimerForSpecialOfferShow.HasValue)
		{
			TimerForSpecialOfferShow = DateTime.Parse(Progress.shop.TimerForSpecialOfferShow);
		}
		TimeSpan timeSpan = DateTime.Now - TimerForSpecialOfferShow.Value;
		double num = (double)(TimerShow * 60f * 60f) - timeSpan.TotalSeconds;
		double num2 = (double)(TimerShow2 * 60f * 60f) - timeSpan.TotalSeconds;
		double num3 = (double)(TimerShow3 * 60f * 60f) - timeSpan.TotalSeconds;
		int num4 = (int)(num % 3600.0) / 60;
		int num5 = (int)(num % 60.0);
		int num6 = (int)(num / 60.0) / 60;
		string text = $"{num6:D2}:{num4:D2}:{num5:D2}";
		int num7 = (int)(num2 % 3600.0) / 60;
		int num8 = (int)(num2 % 60.0);
		int num9 = (int)(num2 / 60.0) / 60;
		string text2 = $"{num9:D2}:{num7:D2}:{num8:D2}";
		int num10 = (int)(num3 % 3600.0) / 60;
		int num11 = (int)(num3 % 60.0);
		int num12 = (int)(num3 / 60.0) / 60;
		string text3 = $"{num12:D2}:{num10:D2}:{num11:D2}";
		if (timeSpan.TotalSeconds < (double)(TimerShow * 60f * 60f))
		{
			Progress.shop.RefreshLimittedOffer = false;
			timerInBody.text = text;
		}
		else
		{
			timerInBody.text = text;
			Progress.shop.RefreshLimittedOffer = true;
			Progress.shop.TimerForSpecialOfferRefresh = DateTime.Now.ToString();
			TimerForSpecialOfferRefresh = DateTime.Now;
		}
		if (timeSpan.TotalSeconds < (double)(TimerShow2 * 60f * 60f))
		{
			Progress.shop.RefreshLimittedOffer = false;
			timerInBody2.text = text2;
		}
		else
		{
			timerInBody2.text = text2;
			offer2.SetActive(value: false);
		}
		if (timeSpan.TotalSeconds < (double)(TimerShow3 * 60f * 60f))
		{
			Progress.shop.RefreshLimittedOffer = false;
			timerInBody3.text = text3;
		}
		else
		{
			timerInBody3.text = text3;
			offer3.SetActive(value: false);
		}
	}

	private void Notifications(string nameStr, int ids, int Timer)
	{
		Progress.Notifications notifications = Progress.notifications;
		notifications.Remove(nameStr);
		int notificationId = Progress.notifications.GetNotificationId(nameStr);
		NotificationsWrapper.Clear(ids);
		notifications.Remove(ids);
		string textValue = LanguageManager.Instance.GetTextValue("Limited offer, left * min");
		textValue = textValue.Replace("*", "60");
		int id = NotificationsWrapper.ScheduleLocalNotification(ids, "Car Eats Car 3", textValue, Timer);
		notifications.Add(id, nameStr);
		Progress.notifications = notifications;
	}

	private void RefreshTimer()
	{
		if (Progress.shop.TimerForSpecialOfferRefresh == string.Empty)
		{
			Progress.shop.TimerForSpecialOfferRefresh = DateTime.MinValue.ToString();
			TimerForSpecialOfferRefresh = DateTime.MinValue;
		}
		if (!TimerForSpecialOfferRefresh.HasValue)
		{
			TimerForSpecialOfferRefresh = DateTime.Parse(Progress.shop.TimerForSpecialOfferRefresh);
		}
		if ((DateTime.Now - TimerForSpecialOfferRefresh.Value).TotalSeconds < (double)(TimeRefresh * 60f * 60f))
		{
			Progress.shop.RefreshLimittedOffer = true;
			return;
		}
		Progress.shop.RefreshLimittedOffer = false;
		Progress.shop.TimerForSpecialOfferShow = DateTime.Now.ToString();
		TimerForSpecialOfferShow = DateTime.Now;
	}
}

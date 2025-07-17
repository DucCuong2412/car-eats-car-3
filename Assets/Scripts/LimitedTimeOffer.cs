using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitedTimeOffer : MonoBehaviour
{
	//public Button _btnLimitted;

	public GameObject BodySpecial;

	//public Text timerInBtn;

	public float TimerShow;

	public float TimerShow2;

	public float TimerShow3;

	public float TimeRefresh;

	public List<GameObject> Cars = new List<GameObject>();

	private Game.gameState state;

	private DateTime? TimerForSpecialOfferShow;

	private DateTime? TimerForSpecialOfferRefresh;

	private void OnEnable()
	{
		state = Game.gameState.Levels;
		//_btnLimitted.onClick.AddListener(ClicShow);
		int index = UnityEngine.Random.Range(0, Cars.Count);
		int count = Cars.Count;
		for (int i = 0; i < count; i++)
		{
			//Cars[i].SetActive(value: false);
		}
		//Cars[index].SetActive(value: true);
		if ((Progress.levels.Max_Active_Pack > 1 && Progress.levels.Max_Active_Level > 6 && !Progress.shop.ShowLimittedOffer) || (Progress.levels.Max_Active_Pack == 1 && Progress.levels.Max_Active_Level > 6 && !Progress.shop.ShowLimittedOffer))
		{
			Progress.shop.TimerForSpecialOfferShow = DateTime.Now.ToString();
			Progress.shop.RefreshLimittedOffer = false;
			if (Progress.levels.InUndeground && !Progress.shop.ShowLimittedOffer)
			{
				Game.OnStateChange(Game.gameState.OpenWindow);
				Progress.shop.TimerForSpecialOfferShow = DateTime.Now.ToString();
				if (Progress.shop.Cars[7].equipped && Progress.shop.Cars[8].equipped && Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped && Progress.shop.Cars[3].equipped && Progress.shop.Cars[4].equipped && Progress.shop.Cars[5].equipped && Progress.shop.Cars[6].equipped && Progress.shop.dronBeeBuy && Progress.gameEnergy.isInfinite)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("special_offers", typeof(GameObject))) as GameObject;
					gameObject.transform.SetParent(BodySpecial.transform);
					gameObject.transform.localScale = Vector3.one;
					BodySpecial.SetActive(value: true);
				}
				Progress.shop.RefreshLimittedOffer = false;
				Progress.shop.ShowLimittedOffer = true;
			}
		}
		else if (Progress.levels.Max_Active_Pack == 1 && Progress.levels.Max_Active_Level == 5 && !Progress.shop.ShowLimittedOffer)
		{
			Game.OnStateChange(Game.gameState.OpenWindow);
			Progress.shop.TimerForSpecialOfferShow = DateTime.Now.ToString();
			if (Progress.shop.Cars[7].equipped && Progress.shop.Cars[8].equipped && Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped && Progress.shop.Cars[3].equipped && Progress.shop.Cars[4].equipped && Progress.shop.Cars[5].equipped && Progress.shop.Cars[6].equipped && Progress.shop.dronBeeBuy && Progress.gameEnergy.isInfinite)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("special_offers", typeof(GameObject))) as GameObject;
				gameObject2.transform.SetParent(BodySpecial.transform);
				gameObject2.transform.localScale = Vector3.one;
				BodySpecial.SetActive(value: true);
			}
			Progress.shop.RefreshLimittedOffer = false;
			Progress.shop.ShowLimittedOffer = true;
		}
	}

	private void OnDisable()
	{
		//_btnLimitted.onClick.RemoveAllListeners();
	}

	private void ClicShow()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("special_offers", typeof(GameObject))) as GameObject;
		gameObject.transform.SetParent(BodySpecial.transform);
		BodySpecial.gameObject.SetActive(value: true);
		gameObject.transform.localScale = Vector3.one;
	}

	private void Update()
	{
		if (Progress.shop.Cars[7].equipped && Progress.shop.Cars[8].equipped && Progress.shop.Cars[9].equipped && Progress.shop.Cars[10].equipped && Progress.shop.Cars[3].equipped && Progress.shop.Cars[4].equipped && Progress.shop.Cars[5].equipped && Progress.shop.Cars[6].equipped && Progress.shop.dronBeeBuy && Progress.shop.Cars[3].equipped && Progress.gameEnergy.isInfinite)
		{
			//_btnLimitted.gameObject.SetActive(value: false);
		}
		else if (Progress.shop.ShowLimittedOffer)
		{
			//if (_btnLimitted.gameObject == null) return;
			if (Progress.shop.RefreshLimittedOffer)
			{
				RefreshTimer();
			}
			else
			{
				Timer();
			}
		}
		else
		{
			//_btnLimitted.gameObject.SetActive(value: false);
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
		int num2 = (int)(num % 3600.0) / 60;
		int num3 = (int)(num % 60.0);
		int num4 = (int)(num / 60.0) / 60;
		string text = $"{num4:D2}:{num2:D2}:{num3:D2}";
		double num5 = (double)(TimerShow2 * 60f * 60f) - timeSpan.TotalSeconds;
		int num6 = (int)(num5 % 3600.0) / 60;
		int num7 = (int)(num5 % 60.0);
		int num8 = (int)(num5 / 60.0) / 60;
		string text2 = $"{num8:D2}:{num6:D2}:{num7:D2}";
		double num9 = (double)(TimerShow3 * 60f * 60f) - timeSpan.TotalSeconds;
		int num10 = (int)(num9 % 3600.0) / 60;
		int num11 = (int)(num9 % 60.0);
		int num12 = (int)(num9 / 60.0) / 60;
		string text3 = $"{num12:D2}:{num10:D2}:{num11:D2}";
		if (timeSpan.TotalSeconds < (double)(TimerShow * 60f * 60f))
		{
			Progress.shop.RefreshLimittedOffer = false;
			if (num9 > 0.0)
			{
				//timerInBtn.text = text3;
			}
			else if (num5 > 0.0)
			{
				//timerInBtn.text = text2;
			}
			else
			{
				//timerInBtn.text = text;
			}
		}
		else
		{
			Progress.shop.RefreshLimittedOffer = true;
			Progress.shop.TimerForSpecialOfferRefresh = DateTime.Now.ToString();
			TimerForSpecialOfferRefresh = DateTime.Now;
		}
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

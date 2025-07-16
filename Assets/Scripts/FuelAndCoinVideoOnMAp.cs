using System;
using UnityEngine;

public class FuelAndCoinVideoOnMAp : MonoBehaviour
{
	//public GameObject _btnFirstVideoFuel;

	public GameObject _btnFirstVideoCoin;

	public CounterController Fuel;

	public CounterController Coin;

	private bool videoForCoin;

	private bool videoForFuel;

	private DateTime? shopDateForFuel;

	private DateTime? shopDateForRuby;

	private void Update()
	{
		ruby();
		fuel();
		if (Progress.gameEnergy.energy >= PriceConfig.instance.energy.maxValue)
		{
			//_btnFirstVideoFuel.gameObject.SetActive(value: false);
		}
		else if (Progress.gameEnergy.isInfinite)
		{
			//_btnFirstVideoFuel.gameObject.SetActive(value: false);
		}
		else if (Progress.shop.CollReclamForFuel != 3)
		{
			//_btnFirstVideoFuel.gameObject.SetActive(value: true);
		}
		else
		{
			//_btnFirstVideoFuel.gameObject.SetActive(value: false);
		}
	}

	private void ruby()
	{
		if (!string.IsNullOrEmpty(Progress.shop.timerForRuby))
		{
			if (Progress.shop.CollReclamForRuby == 3)
			{
				if (Progress.shop.timerForRuby == string.Empty)
				{
					Progress.shop.timerForRuby = DateTime.MinValue.ToString();
					shopDateForRuby = DateTime.MinValue;
				}
				if (!shopDateForRuby.HasValue)
				{
					shopDateForRuby = DateTime.Parse(Progress.shop.timerForRuby);
				}
				if ((DateTime.Now - shopDateForRuby.Value).TotalSeconds > (double)PriceConfig.instance.currency.timeForRuby)
				{
					int num = Utilities.LevelNumberGlobal(Progress.levels.active_level, Progress.levels.active_pack);
					if (num > 3)
					{
						_btnFirstVideoCoin.gameObject.SetActive(value: true);
					}
					Progress.shop.CollReclamForRuby = 0;
				}
				else
				{
					_btnFirstVideoCoin.gameObject.SetActive(value: false);
				}
			}
			else
			{
				_btnFirstVideoCoin.gameObject.SetActive(value: true);
			}
		}
		else
		{
			Progress.shop.timerForRuby = DateTime.MinValue.ToString();
			shopDateForRuby = DateTime.MinValue;
		}
	}

	private void fuel()
	{
		if (!string.IsNullOrEmpty(Progress.shop.timerForFuel))
		{
			if (Progress.shop.CollReclamForFuel == 3)
			{
				if (Progress.shop.timerForFuel == string.Empty)
				{
					Progress.shop.timerForFuel = DateTime.MinValue.ToString();
					shopDateForFuel = DateTime.MinValue;
				}
				if (!shopDateForFuel.HasValue)
				{
					shopDateForFuel = DateTime.Parse(Progress.shop.timerForFuel);
				}
				if ((DateTime.Now - shopDateForFuel.Value).TotalSeconds > (double)PriceConfig.instance.currency.timeForFuel)
				{
					//_btnFirstVideoFuel.gameObject.SetActive(value: true);
					Progress.shop.CollReclamForFuel = 0;
				}
				else
				{
					//_btnFirstVideoFuel.gameObject.SetActive(value: false);
				}
			}
			else
			{
				//_btnFirstVideoFuel.gameObject.SetActive(value: true);
			}
		}
		else
		{
			Progress.shop.timerForFuel = DateTime.MinValue.ToString();
			shopDateForFuel = DateTime.MinValue;
		}
	}
}

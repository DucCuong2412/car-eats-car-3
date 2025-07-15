using SmartLocalization;
using System;
using System.Globalization;
using UnityEngine;

public class GameEnergyLogic : MonoBehaviour
{
	public class cBarrel
	{
		[HideInInspector]
		public Action<float> callback;

		private float minVolume;

		[SerializeField]
		public float maxVolume;

		[SerializeField]
		private float volume;

		[SerializeField]
		private float overVolume;

		public float Volume
		{
			get
			{
				return volume + overVolume;
			}
			set
			{
				volume = value;
				if (callback != null)
				{
					callback(volume / maxVolume);
				}
			}
		}

		public float Use(float value)
		{
			float num = volume + overVolume;
			float num2 = num - value;
			if (num2 > maxVolume)
			{
				overVolume = num2 - maxVolume;
			}
			else
			{
				overVolume = 0f;
			}
			Volume = Mathf.Clamp(num2, minVolume, maxVolume);
			return num - volume;
		}

		public void Restore(float value)
		{
			Use(0f - value);
		}
	}

	public class GameEnergy
	{
		public int maximum = 2000;

		public int eachStart = 5;

		public int restoreTime = 3;

		public int[] values = new int[5];

		public int[] prices = new int[5];
	}

	public static GameEnergyLogic _instance;

	public UILabel label;

	public static Action<int> OnFuelRestored = delegate
	{
	};

	private float restoreTime = 10f;

	private cBarrel barrel = new cBarrel();

	public GameEnergy energyConfig;

	private string dateFormat = "dd.MM HH:mm:ss";

	private string timerFormat = "mm:ss";

	private TimeSpan timeSpan;

	private string notificationName = "BarrelNotification";

	private DateTime p_lastDate = DateTime.MinValue;

	private DateTime fuelTimer;

	public static GameEnergyLogic instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_enegry");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				_instance = gameObject.AddComponent<GameEnergyLogic>();
			}
			return _instance;
		}
	}

	public string FuelTimer => fuelTimer.ToString(timerFormat);

	public static string NextFuelTimer
	{
		get
		{
			if (string.IsNullOrEmpty(instance.FuelTimer))
			{
				return string.Empty;
			}
			return instance.FuelTimer;
		}
	}

	public static bool isEnoughForRace
	{
		get
		{
			if (Progress.gameEnergy.isInfinite)
			{
				return true;
			}
			return instance.Energy >= instance.energyConfig.eachStart;
		}
	}

	public static bool isEnergyFull => instance.barrel.Volume >= instance.barrel.maxVolume;

	public static double GetTotalTime => instance.timeSpan.TotalSeconds;

	public static float GetRestoreTime => instance.restoreTime;

	public static int GetEnergy => instance.Energy;

	public static int[] Values => instance.energyConfig.values;

	public static int[] Prices => instance.energyConfig.prices;

	public int Energy
	{
		get
		{
			return (int)barrel.Volume;
		}
		set
		{
			barrel.Volume = value;
		}
	}

	private DateTime nextFullBarrel
	{
		get
		{
			double value = (double)((barrel.maxVolume - barrel.Volume) * restoreTime) - (DateTime.Now - lastAddedDate).TotalSeconds;
			return DateTime.Now.AddSeconds(value);
		}
	}

	private DateTime lastAddedDate
	{
		get
		{
			if (p_lastDate == DateTime.MinValue)
			{
				string text = Progress.gameEnergy.lastAddedDate;
				if (string.IsNullOrEmpty(text))
				{
					text = DateTime.Now.ToString(dateFormat);
					Energy = (int)barrel.maxVolume;
					Progress.GameEnergy gameEnergy = Progress.gameEnergy;
					gameEnergy.energy = Energy;
					Progress.gameEnergy = gameEnergy;
				}
				p_lastDate = DateTime.ParseExact(text, dateFormat, CultureInfo.InvariantCulture);
			}
			return p_lastDate;
		}
		set
		{
			Progress.GameEnergy gameEnergy = Progress.gameEnergy;
			gameEnergy.lastAddedDate = value.ToString(dateFormat);
			Progress.gameEnergy = gameEnergy;
			p_lastDate = value;
		}
	}

	public static bool Init()
	{
		return instance;
	}

	public static void Reset()
	{
		if (_instance != null && _instance.gameObject != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			_instance = null;
		}
		Init();
	}

	public static bool GetFuelForRace()
	{
		if (instance.barrel.Volume < (float)instance.energyConfig.eachStart)
		{
			return false;
		}
		if (instance.barrel.Volume >= instance.barrel.maxVolume)
		{
			instance.lastAddedDate = DateTime.Now;
		}
		if (!Progress.gameEnergy.isInfinite)
		{
			instance.barrel.Use(instance.energyConfig.eachStart);
		}
		Progress.GameEnergy gameEnergy = Progress.gameEnergy;
		gameEnergy.energy = instance.Energy;
		Progress.gameEnergy = gameEnergy;
		AddNotificationOfFullBarrel(changed: true);
		return true;
	}

	public static int AddFuel(int amount = 1)
	{
		instance.barrel.Restore(amount);
		Progress.GameEnergy gameEnergy = Progress.gameEnergy;
		gameEnergy.energy = instance.Energy;
		Progress.gameEnergy = gameEnergy;
		if (amount != 1)
		{
			AddNotificationOfFullBarrel(changed: true);
		}
		OnFuelRestored(gameEnergy.energy);
		return instance.Energy;
	}

	private void Awake()
	{
		energyConfig = new GameEnergy
		{
			eachStart = PriceConfig.instance.energy.eachStart,
			maximum = PriceConfig.instance.energy.maxValue,
			restoreTime = PriceConfig.instance.energy.restoreTime,
			prices = new int[4]
			{
				PriceConfig.instance.energy.fuelPack1Price,
				PriceConfig.instance.energy.fuelPack2Price,
				PriceConfig.instance.energy.fuelPack3Price,
				PriceConfig.instance.energy.fuelPack4Price
			},
			values = new int[4]
			{
				PriceConfig.instance.energy.fuelPack1,
				PriceConfig.instance.energy.fuelPack2,
				PriceConfig.instance.energy.fuelPack3,
				PriceConfig.instance.energy.fuelPack4
			}
		};
		if (Progress.gameEnergy.isInfinite)
		{
			barrel.maxVolume = energyConfig.maximum;
			if (Progress.gameEnergy.energy < energyConfig.maximum)
			{
				barrel.Volume = energyConfig.maximum;
			}
			else
			{
				barrel.Volume = Progress.gameEnergy.energy;
			}
			restoreTime = energyConfig.restoreTime * 60;
		}
		else
		{
			barrel.maxVolume = energyConfig.maximum;
			barrel.Volume = Progress.gameEnergy.energy;
			restoreTime = energyConfig.restoreTime * 60;
		}
		if (barrel.Volume >= barrel.maxVolume)
		{
			Progress.Notifications notifications = Progress.notifications;
			int notificationId = notifications.GetNotificationId(notificationName);
			NotificationsWrapper.Clear(notificationId);
			notifications.Remove(notificationId);
			Progress.notifications = notifications;
		}
	}

	private static void AddNotificationOfFullBarrel(bool changed = false)
	{
		if (changed)
		{
			instance.lastAddedDate = DateTime.Now.AddSeconds(0.0 - (DateTime.Now - instance.lastAddedDate).TotalSeconds);
		}
		Progress.Notifications notifications = Progress.notifications;
		NotificationsWrapper.Clear(1);
		notifications.Remove(1);
		TimeSpan timeSpan = instance.nextFullBarrel - DateTime.Now;
		if (!(timeSpan.TotalSeconds <= 0.0) && !(instance.barrel.Volume >= instance.barrel.maxVolume))
		{
			string textValue = LanguageManager.Instance.GetTextValue("Your gas has refilled in Car Eats Car. Let's go race!");
			int id = NotificationsWrapper.ScheduleLocalNotification(1, "Car Eats Car 3", textValue, (int)timeSpan.TotalSeconds);
			notifications.Add(id, instance.notificationName);
		}
		Progress.notifications = notifications;
	}

	private void Update()
	{
		if (barrel.Volume >= barrel.maxVolume)
		{
			return;
		}
		this.timeSpan = DateTime.Now - lastAddedDate;
		if (this.timeSpan.TotalSeconds - (double)restoreTime > 0.0)
		{
			int num = (int)(this.timeSpan.TotalSeconds / (double)restoreTime);
			if (barrel.Volume + (float)num > barrel.maxVolume)
			{
				num = (int)barrel.maxVolume - Energy;
			}
			AddFuel(num);
			lastAddedDate = DateTime.Now;
		}
		TimeSpan timeSpan = new TimeSpan((long)(int)((double)restoreTime - this.timeSpan.TotalSeconds) * 10000000L);
		fuelTimer = new DateTime((timeSpan.Ticks <= 0) ? 0 : timeSpan.Ticks);
	}
}

using Smokoko.Progress;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class Progress : ProgressBase<Progress>
{
	public enum SaveType
	{
		Shop,
		Levels,
		Settings,
		Review
	}

	[Serializable]
	public class Shop
	{
		public class PremiumContent
		{
			public PremiumContentItem panzer = new PremiumContentItem();

			public PremiumContentItem antigravs = new PremiumContentItem();

			public PremiumContentItem superturbo = new PremiumContentItem();

			public PremiumContentItem blaster = new PremiumContentItem();

			public PremiumContentItem tankominator = new PremiumContentItem();
		}

		public class PremiumContentItem
		{
			public bool isBought;

			public bool isEquipped;
		}

		[Serializable]
		public class CarInfo
		{
			public int id;

			public int SPEED_STATS;

			public int ARMOR_STATS;

			public int TURBO_STATS;

			public int WEAPON_STATS;

			public bool bought;

			public bool equipped;

			public int healthActLev;

			public int turboActLev;

			public int engineActLev;

			public int bombActLev;

			public int weaponActLev;

			public bool bomb_0_bounght;

			public bool bomb_1_bounght;

			public bool bomb_2_bounght;

			public bool bomb_3_bounght;

			public bool bomb_4_bounght;

			public bool Gadget_Magnet_bounght;

			public bool Gadget_EMP_bounght;

			public bool Gadget_MISSLLE_bounght;

			public bool Gadget_RECHARGER_bounght;

			public bool Gadget_LEDOLUCH_bounght;

			public bool[] boughtGadgets = new bool[5];

			public bool[] premiumBougth = new bool[5];

			public bool[] premiumEquipped = new bool[5];

			public int wheelsActLev;
		}

		public bool Undeground2;

		public bool showmarkerEggs;

		public bool showmarkerEggs2;

		public int PolispediaChusenCarFromGarage = -1;

		public int BuyPack = -1;

		public bool TestFor9;

		public bool Promo1Show = true;

		public bool Promo2Show = true;

		public int ValentineMaxDistance;

		public int ValentineDistance;

		public List<bool> Incubator_Eggs = new List<bool>();

		public bool showPP;

		public bool NeverShowPP;

		public DateTime EsterEndTime = new DateTime(2018, 4, 15);

		public int EsterEggsBalance;

		public int estetForFreeVideo;

		public DateTime EsterX2BuyTime = DateTime.MinValue;

		public bool EsterX2TimeActivate;

		public string EsterFreeBuyTime;

		public DateTime EsterLastPlayTime = DateTime.MinValue;

		public bool EsterShowIntroPlay = true;

		public bool EsterShowOnMap;

		public bool EsterLevelPlay;

		public bool EsterForMap;

		public DateTime Incubator_Time = DateTime.MinValue;

		public DateTime Incubator_LastPlay = DateTime.MinValue;

		public int Incubator_CurrentEggNum = -1;

		public int Incubator_EvoStage = -1;

		public int Incubator_EvoProgressStep = -1;

		public int Incubator_CountRuby1;

		public int Incubator_CountRuby2;

		public int Incubator_CountRuby3;

		public int Incubator_CountRuby4;

		public bool Incubator_Ruby1CanGateFree;

		public bool Incubator_Ruby2CanGateFree;

		public bool Incubator_Ruby3CanGateFree;

		public bool Incubator_Ruby4CanGateFree;

		public List<bool> Incubator_RubySetActive;

		public List<bool> Incubator_RubySetCompleat;

		public bool Get1partForPoliceCar;

		public bool Get2partForPoliceCar;

		public bool Get3partForPoliceCar;

		public bool Get4partForPoliceCar;

		public int CollKill1Car;

		public int CollKill2Car;

		public int CollKill3Car;

		public int CollKill4Car;

		public bool Get1partForPoliceCar2;

		public bool Get2partForPoliceCar2;

		public bool Get3partForPoliceCar2;

		public bool Get4partForPoliceCar2;

		public int CollKill1Car2;

		public int CollKill2Car2;

		public int CollKill3Car2;

		public int CollKill4Car2;

		public bool Get1partForPoliceCar3;

		public bool Get2partForPoliceCar3;

		public bool Get3partForPoliceCar3;

		public bool Get4partForPoliceCar3;

		public int CollKill1Car3;

		public int CollKill2Car3;

		public int CollKill3Car3;

		public int CollKill4Car3;

		public bool BlockSpavnRubi;

		public List<bool> MonstroLocks = new List<bool>();

		public List<bool> MonstroCanGetReward = new List<bool>();

		public bool Key1;

		public bool Key2;

		public bool Key3;

		public bool ArenaBrifOpen;

		public bool ArenaBrifOpenFromGarage;

		public int Arena1MaxDistance;

		public int Arena2MaxDistance;

		public int Arena3MaxDistance;

		public int Arena1Distance;

		public int Arena2Distance;

		public int Arena3Distance;

		public bool Arema1MapButOpenned;

		public bool Arema2MapButOpenned;

		public bool Arema3MapButOpenned;

		public float forArenaDamageEnemy;

		public List<bool> SpecialMissionsGated = new List<bool>();

		public List<bool> SpecialMissionsFirstOpen = new List<bool>();

		public DateTime SpecialMissionsLastPlay = DateTime.MinValue;

		public DateTime SpecialMissionsOpenTime = DateTime.MinValue;

		public int ActivCellNum = -1;

		public bool ShowRelamaWindow;

		public bool BuyForRealMoney;

		public bool TutorialsInCage;

		public int SpecialMissionsRewardCar;

		public bool TutorialNeed = true;

		public bool TutorialBadgeNeed;

		public bool Tutorial = true;

		public bool TutorialGarage = true;

		public bool TutorialFin = true;

		public bool StartComixShow;

		public bool BossDeath1Undeground;

		public bool BossDeath2Undeground;

		public bool BossDeath1;

		public bool BossDeath2;

		public bool BossDeath3;

		public bool BossDeath1Reward;

		public bool BossDeath2Reward;

		public bool BossDeath3Reward;

		public bool GotoGarageOutIncubator;

		public bool dronBeeActive;

		public bool dronBombsActive;

		public bool dronFireActive;

		public bool dronBeeBuy;

		public bool dronBombsBuy;

		public bool dronFireBuy;

		public bool dronFireEggBuy;

		public bool dronFireEvolFin;

		public List<bool> MapDecorsOpenned = new List<bool>();

		public bool isFirst = true;

		public bool driveOnTankominato = true;

		public bool NeedForDB = true;

		public bool NeedForDBExit = true;

		public bool NeedForFB = true;

		public bool endlessLevel;

		public bool ArenaNew;

		public bool ArenaNewChit;

		public bool LoadPolicePedia;

		public string TimerForSpecialOfferShow = string.Empty;

		public string TimerForSpecialOfferRefresh = string.Empty;

		public bool RefreshLimittedOffer = true;

		public bool ShowLimittedOffer;

		public bool BuyLimittedOffer;

		public string timerForRuby = string.Empty;

		public string timerForFuel = string.Empty;

		public int CollReclamForFuel;

		public int CollReclamForRuby;

		public string timerForFortune = "0";

		public bool forFB = true;

		public bool ForRateUs;

		public bool gameCenterCheckedCloudSave;

		public int currency;

		public int tickets;

		public int turboBoost;

		public int healthBost;

		public int restoreBoost;

		public int activeCar;

		public bool NeedForDBX2;

		public bool shopinlevel;

		public bool Monstroinlevel;

		public bool premiumShop;

		public bool premiumShopforFirst = true;

		public bool foundProgress = true;

		public int carForGarage;

		public int NowSelectCarNeedForMe;

		public bool needRateUs2 = true;

		public int carsForMeInGarage;

		public bool bossLevel;

		public bool bossBlockPlayerRide;

		public bool BuyedDrill;

		public bool TakeDrone;

		public bool fb_price_ned;

		public int DailyLastLocNotifID = -1;

		public DateTime Gadget1Time = DateTime.MinValue;

		public DateTime Gadget2Time = DateTime.MinValue;

		public DateTime Gadget3Time = DateTime.MinValue;

		public DateTime Gadget4Time = DateTime.MinValue;

		public DateTime Gadget5Time = DateTime.MinValue;

		public bool showOpenGateCar2;

		public bool showOpenGateCar3;

		public bool showOpenGateCar4;

		public bool showOpenGateCar5;

		public CarInfo[] Cars = new CarInfo[14]
		{
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo(),
			new CarInfo()
		};

		public CarInfo Car
		{
			get
			{
				if (activeCar < 0)
				{
					activeCar = 0;
				}
				return Cars[activeCar];
			}
		}

		public string GetTimeToBuy(int maxMins, int index)
		{
			DateTime d = DateTime.MinValue;
			switch (index)
			{
			case 0:
				d = Gadget1Time;
				break;
			case 1:
				d = Gadget2Time;
				break;
			case 2:
				d = Gadget3Time;
				break;
			case 3:
				d = Gadget4Time;
				break;
			case 4:
				d = Gadget5Time;
				break;
			}
			if ((DateTime.UtcNow - d).TotalMinutes < (double)maxMins)
			{
				DateTime dateTime = new DateTime((d.AddMinutes(maxMins) - DateTime.UtcNow).Ticks);
				return dateTime.ToString("mm:ss");
			}
			return "00:00";
		}

		public void UpdateProgress()
		{
			bool flag = false;
			CarInfo[] cars = Cars;
			foreach (CarInfo carInfo in cars)
			{
				if (carInfo.premiumBougth.Length == 4)
				{
					bool[] array = new bool[5];
					carInfo.premiumBougth.CopyTo(array, 0);
					array[array.Length - 1] = false;
					carInfo.premiumBougth = array;
					flag = true;
				}
				if (carInfo.premiumEquipped.Length == 4)
				{
					bool[] array2 = new bool[5];
					carInfo.premiumEquipped.CopyTo(array2, 0);
					array2[array2.Length - 1] = false;
					carInfo.premiumEquipped = array2;
					flag = true;
				}
			}
			if (flag)
			{
				ProgressBase<Progress>.Save();
			}
		}
	}

	[Serializable]
	public class PackInfo
	{
		[Serializable]
		public class LevelInfo
		{
			public bool isOpen;

			public bool rewarded;

			public bool lgChainDropped;

			public string tickets = string.Empty;

			public int ticket;

			public int oldticket;
		}

		public bool isOpen;

		public bool packAnimed;

		public LevelInfo[] _level = new LevelInfo[50];

		public LevelInfo Level()
		{
			return Level(levels.active_level);
		}

		public LevelInfo Level(int i)
		{
			if (_level[i] == null)
			{
				_level[i] = new LevelInfo();
			}
			return _level[i];
		}
	}

	[Serializable]
	public class Retention
	{
		public const string dateFormat = "yyyy.MM.dd";

		public string day = string.Empty;

		public bool isToday => getDate.Year == DateTime.Now.Year && getDate.Month == DateTime.Now.Month && getDate.Day == DateTime.Now.Day;

		private DateTime getDate => DateTime.ParseExact(day, "yyyy.MM.dd", CultureInfo.InvariantCulture);

		public void SetToday()
		{
			day = DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
		}
	}

	[Serializable]
	public class Fortunes
	{
		public float SumPercentRuby;

		public float SumPercentHP;

		public float SumPercentDamage;

		public float SumPercentTurbo;

		public int Dirka;

		public int SumBombStart;

		public bool GOGOGOGOGOGO;
	}

	[Serializable]
	public class Levels
	{
		public bool InUndeground;

		public bool InUndegroundComixShowed;

		public bool InUndegroundComixShowed2;

		public bool InUndegroundPreloader;

		public bool InUndegroundIn_OutPreloader;

		public List<ResultBoxesConfig.Level> ResultBoxRev_Undeground1;

		public List<bool> ResultBoxRev_Undeground1_Adds;

		public DateTime ResultBoxRev_Undeground1_LastGetTime = DateTime.MinValue;

		public int CounterForWinForX2;

		public Retention[] retention = new Retention[0];

		private const string dateFormat = "yyyy.MM.dd";

		public bool winArena1;

		public bool winArena2;

		public bool winArena3;

		public bool winArena4;

		public byte active_pack = 1;

		public byte active_level = 1;

		public int active_pack_last_openned = 1;

		public int active_level_last_openned = 1;

		public int active_pack_last_openned_under = 1;

		public int active_level_last_openned_under = 1;

		public int active_boss_pack_last_openned = -1;

		public int active_boss_pack_last_openned_undeground = -1;

		public byte Max_Active_Pack = 1;

		public byte Max_Active_Level = 1;

		public byte Max_Active_Pack_under = 1;

		public byte Max_Active_Level_under = 1;

		public bool Win_36_Lvl;

		public int dayEnded;

		public int DAILYBONUS;

		public DateTime TakeDate = DateTime.Now;

		public byte arena_pack = 1;

		public byte arena_level = 1;

		public int total_starts;

		public bool forChek = true;

		public int tickets;

		public float BestScoreArena1;

		public float BestScoreArena2;

		public float BestScoreArena3;

		public float BestScoreArena4;

		public bool ForTutorialFortune;

		public bool RateUsRemind = true;

		public bool RateUsOpenned1;

		public bool RateUsOpenned2;

		public bool RateUsOpenned3;

		public PackInfo[] _pack = new PackInfo[6];

		public PackInfo[] _packUnderground = new PackInfo[6];

		public void SendRetention()
		{
			Retention retention = Array.Find(this.retention, (Retention r) => r.isToday);
			if (retention == null)
			{
				retention = new Retention();
				retention.SetToday();
				Array.Resize(ref this.retention, this.retention.Length + 1);
				this.retention[this.retention.Length - 1] = retention;
				string day = this.retention[0].day;
				string s = DateTime.Now.ToString("yyyy.MM.dd");
				DateTime d = DateTime.ParseExact(day, "yyyy.MM.dd", CultureInfo.InvariantCulture);
				DateTime d2 = DateTime.ParseExact(s, "yyyy.MM.dd", CultureInfo.InvariantCulture);
				int dayNum = (int)(d2 - d).TotalDays + 1;
				AnalyticsManager.LogRetention(dayNum);
				AnalyticsManager.LogRollingRetention(this.retention.Length);
			}
		}

		public PackInfo Pack()
		{
			return Pack(active_pack);
		}

		public PackInfo Pack(int p, bool createUndeground = false)
		{
			if (p < 0)
			{
				return null;
			}
			if (InUndeground || createUndeground)
			{
				if (_packUnderground[p] == null)
				{
					_packUnderground[p] = new PackInfo();
				}
				return _packUnderground[p];
			}
			if (_pack[p] == null)
			{
				_pack[p] = new PackInfo();
			}
			return _pack[p];
		}
	}

	[Serializable]
	public class Review
	{
		public bool isWatchingFacebook;

		public bool isWatchingTwitter;

		public bool isShareReplay;

		public bool atLeastOnePurchase;
	}

	[Serializable]
	public class Settings
	{
		public bool isMusic = true;

		public bool isSound = true;

		public bool isLeftHanded;

		public bool isGhost = true;

		public bool x2damage;

		public bool ReduceDamage;

		public bool showComics = true;

		public bool Easy = true;

		public bool Medium;

		public bool Hard;

		public float timerForx2damage = 10f;

		public string FriendId = string.Empty;

		public bool LoginToGP;

		public bool showGDPRads;

		public string GDPRads = "1";
	}

	[Serializable]
	public class AchievementsProgress
	{
		public int destroyerValue;

		public int mechanicValue;

		public int angelValue;

		public int acrobatValue;

		public int tycoonValue;

		public int driverValue;
	}

	[Serializable]
	public class GameEnergy
	{
		public int energy;

		public string lastAddedDate = string.Empty;

		public bool isInfinite;
	}

	[Serializable]
	public class Notification
	{
		public string name = string.Empty;

		public int id;
	}

	[Serializable]
	public class Notifications
	{
		[NonSerialized]
		private List<Notification> list = new List<Notification>();

		public Notification[] notifications = new Notification[0];

		public void Remove(int _id)
		{
			list.Clear();
			list.AddRange(notifications);
			list.RemoveAll((Notification n) => n.id == _id);
			notifications = list.ToArray();
		}

		public void Remove(string _name)
		{
			list.Clear();
			list.AddRange(notifications);
			list.RemoveAll((Notification n) => n.name.Equals(_name));
			notifications = list.ToArray();
		}

		public int GetNotificationId(string _name)
		{
			list.Clear();
			list.AddRange(notifications);
			return list.Find((Notification notification) => notification.name.Equals(_name))?.id ?? (-1);
		}

		public void Add(int _id, string _name)
		{
			list.Clear();
			list.AddRange(notifications);
			Notification notification = new Notification();
			notification.id = _id;
			notification.name = _name;
			Notification item = notification;
			list.Add(item);
			notifications = list.ToArray();
		}
	}

	[Serializable]
	public class Achievements
	{
		public int obstaclesDestroyed;

		public int[] bossesDestroyed = new int[3];

		public int enemiesDestroyed;

		public int earnedRubies;

		public int usedBoosters;

		public int flipsDone;

		public int fuelSpend;

		public int levelStarts;

		public int rubiesHighscore;

		public int CivilHighscore;

		public int SaveFriends;

		private const string dateFormat = "yyyy.MM.dd";

		public string[] daysInGame = new string[0];

		public int getTotalDaysInGame
		{
			get
			{
				bool flag = false;
				string[] array = daysInGame;
				foreach (string text in array)
				{
					if (text.Equals(DateTime.Now.ToString("yyyy.MM.dd")))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					string[] array2 = new string[daysInGame.Length + 1];
					daysInGame.CopyTo(array2, 0);
					array2[array2.Length - 1] = DateTime.Now.ToString("yyyy.MM.dd");
					daysInGame = array2;
				}
				return daysInGame.Length;
			}
		}

		public int collectedBoxes
		{
			get
			{
				int num = 0;
				for (int i = 1; i <= 4; i++)
				{
					for (int j = 1; j <= 9; j++)
					{
						string tickets = levels.Pack(i).Level(j).tickets;
						int num2 = (tickets.Length > 0) ? tickets.Split('|').Length : 0;
						num += num2;
					}
				}
				return num;
			}
		}

		public int purchasedUpgrades
		{
			get
			{
				int num = 0;
				num += shop.Cars[0].engineActLev;
				num += shop.Cars[0].healthActLev;
				num += shop.Cars[0].turboActLev;
				num += shop.Cars[0].weaponActLev;
				num += shop.Cars[0].wheelsActLev;
				bool[] boughtGadgets = shop.Cars[0].boughtGadgets;
				foreach (bool flag in boughtGadgets)
				{
					num += (flag ? 1 : 0);
				}
				return num;
			}
		}
	}

	[SerializeField]
	private Fortunes p_fortunes = new Fortunes();

	[SerializeField]
	private Shop p_shop = new Shop();

	[SerializeField]
	private Levels p_levels = new Levels();

	[SerializeField]
	private PackInfo p_Info = new PackInfo();

	[SerializeField]
	private Review p_review = new Review();

	[SerializeField]
	private Settings p_settings = new Settings();

	[SerializeField]
	private GameEnergy p_energy = new GameEnergy();

	[SerializeField]
	private Notifications p_notifications = new Notifications();

	[SerializeField]
	private Notification p_notification = new Notification();

	[SerializeField]
	private Achievements p_achievements = new Achievements();

	[SerializeField]
	private AchievementsProgress p_achievementProgress = new AchievementsProgress();

	public static Shop shop
	{
		get
		{
			ProgressBase<Progress>.instance.p_shop.UpdateProgress();
			return ProgressBase<Progress>.instance.p_shop;
		}
		set
		{
			ProgressBase<Progress>.instance.p_shop = value;
			ProgressBase<Progress>.SaveField("p_shop");
		}
	}

	public static Fortunes fortune
	{
		get
		{
			return ProgressBase<Progress>.instance.p_fortunes;
		}
		set
		{
			ProgressBase<Progress>.instance.p_fortunes = value;
			ProgressBase<Progress>.SaveField("p_fortunes");
		}
	}

	public static Levels levels
	{
		get
		{
			return ProgressBase<Progress>.instance.p_levels;
		}
		set
		{
			ProgressBase<Progress>.instance.p_levels = value;
			ProgressBase<Progress>.SaveField("p_levels");
		}
	}

	public static PackInfo packInfo
	{
		get
		{
			return ProgressBase<Progress>.instance.p_Info;
		}
		set
		{
			ProgressBase<Progress>.instance.p_Info = value;
			ProgressBase<Progress>.SaveField("p_Info");
		}
	}

	public static Review review
	{
		get
		{
			return ProgressBase<Progress>.instance.p_review;
		}
		set
		{
			ProgressBase<Progress>.instance.p_review = value;
			ProgressBase<Progress>.SaveField("p_review");
		}
	}

	public static Settings settings
	{
		get
		{
			return ProgressBase<Progress>.instance.p_settings;
		}
		set
		{
			ProgressBase<Progress>.instance.p_settings = value;
			ProgressBase<Progress>.SaveField("p_settings");
		}
	}

	public static GameEnergy gameEnergy
	{
		get
		{
			return ProgressBase<Progress>.instance.p_energy;
		}
		set
		{
			ProgressBase<Progress>.instance.p_energy = value;
			ProgressBase<Progress>.SaveField("p_energy");
		}
	}

	public static Notifications notifications
	{
		get
		{
			return ProgressBase<Progress>.instance.p_notifications;
		}
		set
		{
			ProgressBase<Progress>.instance.p_notifications = value;
			ProgressBase<Progress>.SaveField("p_notifications");
		}
	}

	public static Notification notification
	{
		get
		{
			return ProgressBase<Progress>.instance.p_notification;
		}
		set
		{
			ProgressBase<Progress>.instance.p_notification = value;
			ProgressBase<Progress>.SaveField("p_notification");
		}
	}

	public static Achievements achievements => ProgressBase<Progress>.instance.p_achievements;

	public static AchievementsProgress achievementsProgress => ProgressBase<Progress>.instance.p_achievementProgress;

	public void ForSave(string loadedLevelNum, int loadedLevelCoins)
	{
		StartCoroutine(forProgress(loadedLevelNum, loadedLevelCoins));
	}

	public IEnumerator forProgress(string loadedLevelNum, int loadedLevelCoins)
	{
		while (shop.NeedForDBExit)
		{
			yield return 0;
		}
		CloudGameSave.ShowWithText(loadedLevelNum, loadedLevelCoins);
	}

	public static void OpenNextLevel()
	{
		byte active_pack = ProgressBase<Progress>.instance.p_levels.active_pack;
		byte active_level = ProgressBase<Progress>.instance.p_levels.active_level;
		if (levels.InUndeground && active_level == 12)
		{
			Save(SaveType.Levels);
			return;
		}
		if (active_level < 12)
		{
			ProgressBase<Progress>.instance.p_levels.Pack(active_pack).Level(active_level + 1).isOpen = true;
		}
		else if (active_pack < 3)
		{
			ProgressBase<Progress>.instance.p_levels.Pack(active_pack + 1).isOpen = true;
			ProgressBase<Progress>.instance.p_levels.Pack(active_pack + 1).Level(1).isOpen = true;
		}
		else
		{
			UnityEngine.Debug.LogWarning("The last level is won!!! ///Todo goto level gallery! Maybe...");
		}
		Save(SaveType.Levels);
	}

	public static void SetNextLevel()
	{
		byte active_pack = ProgressBase<Progress>.instance.p_levels.active_pack;
		byte active_level = ProgressBase<Progress>.instance.p_levels.active_level;
		if (levels.InUndeground)
		{
			if (active_level < 12)
			{
				if (!shop.endlessLevel)
				{
					ProgressBase<Progress>.instance.p_levels.active_level = (byte)(active_level + 1);
				}
				if (ProgressBase<Progress>.instance.p_levels.active_level > ProgressBase<Progress>.instance.p_levels.Max_Active_Level_under)
				{
					if (!shop.endlessLevel)
					{
						ProgressBase<Progress>.instance.p_levels.Max_Active_Level_under = (byte)(active_level + 1);
					}
					ProgressBase<Progress>.instance.p_levels.Max_Active_Pack_under = ProgressBase<Progress>.instance.p_levels.active_pack;
				}
			}
			int num = Utilities.LevelNumberGlobal(ProgressBase<Progress>.instance.p_levels.active_level, ProgressBase<Progress>.instance.p_levels.active_pack);
			int num2 = Utilities.LevelNumberGlobal(ProgressBase<Progress>.instance.p_levels.Max_Active_Level_under, ProgressBase<Progress>.instance.p_levels.Max_Active_Pack_under);
			if (num2 <= num)
			{
				ProgressBase<Progress>.instance.p_levels.Max_Active_Pack_under = ProgressBase<Progress>.instance.p_levels.active_pack;
				ProgressBase<Progress>.instance.p_levels.Max_Active_Level_under = ProgressBase<Progress>.instance.p_levels.active_level;
			}
		}
		else
		{
			if (active_level < 12)
			{
				if (!shop.endlessLevel)
				{
					ProgressBase<Progress>.instance.p_levels.active_level = (byte)(active_level + 1);
				}
				if (ProgressBase<Progress>.instance.p_levels.active_level > ProgressBase<Progress>.instance.p_levels.Max_Active_Level && !shop.endlessLevel && !shop.EsterLevelPlay && !shop.ArenaNew && !shop.bossLevel)
				{
					ProgressBase<Progress>.instance.p_levels.Max_Active_Level = (byte)(active_level + 1);
					ProgressBase<Progress>.instance.p_levels.Max_Active_Pack = ProgressBase<Progress>.instance.p_levels.active_pack;
				}
			}
			else if (active_pack < 3)
			{
				ProgressBase<Progress>.instance.p_levels.active_pack = (byte)(active_pack + 1);
				ProgressBase<Progress>.instance.p_levels.active_level = 1;
				if (ProgressBase<Progress>.instance.p_levels.active_pack > ProgressBase<Progress>.instance.p_levels.Max_Active_Pack)
				{
					ProgressBase<Progress>.instance.p_levels.Max_Active_Pack = (byte)(active_pack + 1);
					ProgressBase<Progress>.instance.p_levels.Max_Active_Level = 1;
				}
			}
			else
			{
				ProgressBase<Progress>.instance.p_levels.Win_36_Lvl = true;
				UnityEngine.Debug.LogWarning("!The last level is won!!! ///Todo goto level gallery! Maybe...");
			}
			if (!shop.endlessLevel && !shop.EsterLevelPlay && !shop.ArenaNew && !shop.bossLevel)
			{
				int num3 = Utilities.LevelNumberGlobal(ProgressBase<Progress>.instance.p_levels.active_level, ProgressBase<Progress>.instance.p_levels.active_pack);
				int num4 = Utilities.LevelNumberGlobal(ProgressBase<Progress>.instance.p_levels.Max_Active_Level, ProgressBase<Progress>.instance.p_levels.Max_Active_Pack);
				if (num4 <= num3)
				{
					ProgressBase<Progress>.instance.p_levels.Max_Active_Pack = ProgressBase<Progress>.instance.p_levels.active_pack;
					ProgressBase<Progress>.instance.p_levels.Max_Active_Level = ProgressBase<Progress>.instance.p_levels.active_level;
				}
			}
		}
		Save(SaveType.Levels);
	}

	public static void SetActiveLevel(byte level)
	{
		ProgressBase<Progress>.instance.p_levels.active_level = level;
		Save(SaveType.Levels);
	}

	public static void Save(SaveType type)
	{
	}

	public static void Reset()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		ProgressBase<Progress>.GetInstance().ClearAllFields();
	}
}

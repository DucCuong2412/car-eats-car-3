using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PostProcessLoader : MonoBehaviour
{
	public Camera cam;

	public SpriteRenderer splash1;

	public SpriteRenderer splash2;

	private static bool isGCConectionFirst = true;

	[CompilerGenerated]
	private static GameCenterWrapper.DelAvailableSaveFound _003C_003Ef__mg_0024cache0;

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			NotificationsWrapper.HideAllNotifications();
		}
	}

	private void Start()
	{
		if (!Progress.shop.showPP)
		{
			if (Progress.shop.Tutorial)
			{
				Progress.shop.showPP = false;
				Progress.shop.NeverShowPP = true;
			}
			else
			{
				Progress.shop.showPP = true;
			}
		}
		Progress.shop.EsterEndTime = new DateTime(2018, 5, 6);
		Time.timeScale = 1f;
		Screen.sleepTimeout = -1;
		AdColonyWrapper.instance.ConfigureAds();
		if (Progress.shop.Arena1MaxDistance >= DifficultyConfig.instance.MetrivForARENA1)
		{
			Progress.shop.Key1 = true;
		}
		if (Progress.shop.Arena2MaxDistance >= DifficultyConfig.instance.MetrivForARENA2)
		{
			Progress.shop.Key2 = true;
		}
		if (Progress.shop.Arena3MaxDistance >= DifficultyConfig.instance.MetrivForARENA3)
		{
			Progress.shop.Key3 = true;
		}
		Progress.shop.EsterLevelPlay = false;
		if (Progress.shop.Cars.Length < 5)
		{
			List<Progress.Shop.CarInfo> list = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item in cars)
			{
				list.Add(item);
			}
			Progress.Shop.CarInfo carInfo = new Progress.Shop.CarInfo();
			carInfo.bought = true;
			carInfo.weaponActLev = 3;
			carInfo.wheelsActLev = 3;
			carInfo.engineActLev = 3;
			carInfo.healthActLev = 3;
			carInfo.turboActLev = 3;
			carInfo.SPEED_STATS = 120;
			carInfo.ARMOR_STATS = 30;
			carInfo.WEAPON_STATS = 180;
			carInfo.TURBO_STATS = 300;
			list.Add(carInfo);
			Progress.shop.Cars = list.ToArray();
		}
		if (Progress.shop.Cars.Length < 6)
		{
			List<Progress.Shop.CarInfo> list2 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars2 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item2 in cars2)
			{
				list2.Add(item2);
			}
			Progress.Shop.CarInfo carInfo2 = new Progress.Shop.CarInfo();
			carInfo2.weaponActLev = 3;
			carInfo2.wheelsActLev = 3;
			carInfo2.engineActLev = 3;
			carInfo2.healthActLev = 3;
			carInfo2.turboActLev = 3;
			list2.Add(carInfo2);
			Progress.shop.Cars = list2.ToArray();
		}
		if (Progress.shop.Cars.Length < 7)
		{
			List<Progress.Shop.CarInfo> list3 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars3 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item3 in cars3)
			{
				list3.Add(item3);
			}
			Progress.Shop.CarInfo carInfo3 = new Progress.Shop.CarInfo();
			carInfo3.bought = true;
			carInfo3.weaponActLev = 0;
			carInfo3.wheelsActLev = 0;
			carInfo3.engineActLev = 0;
			carInfo3.healthActLev = 0;
			carInfo3.turboActLev = 0;
			list3.Add(carInfo3);
			Progress.shop.Cars = list3.ToArray();
		}
		if (Progress.shop.Cars.Length < 8)
		{
			List<Progress.Shop.CarInfo> list4 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars4 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item4 in cars4)
			{
				list4.Add(item4);
			}
			Progress.Shop.CarInfo carInfo4 = new Progress.Shop.CarInfo();
			carInfo4.bought = true;
			carInfo4.weaponActLev = 0;
			carInfo4.wheelsActLev = 0;
			carInfo4.engineActLev = 0;
			carInfo4.healthActLev = 0;
			carInfo4.turboActLev = 0;
			list4.Add(carInfo4);
			Progress.shop.Cars = list4.ToArray();
		}
		if (Progress.shop.Cars.Length < 9)
		{
			List<Progress.Shop.CarInfo> list5 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars5 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item5 in cars5)
			{
				list5.Add(item5);
			}
			Progress.Shop.CarInfo carInfo5 = new Progress.Shop.CarInfo();
			carInfo5.bought = true;
			carInfo5.weaponActLev = 0;
			carInfo5.wheelsActLev = 0;
			carInfo5.engineActLev = 0;
			carInfo5.healthActLev = 0;
			carInfo5.turboActLev = 0;
			list5.Add(carInfo5);
			Progress.shop.Cars = list5.ToArray();
		}
		if (Progress.shop.Cars.Length < 10)
		{
			List<Progress.Shop.CarInfo> list6 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars6 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item6 in cars6)
			{
				list6.Add(item6);
			}
			Progress.Shop.CarInfo carInfo6 = new Progress.Shop.CarInfo();
			carInfo6.bought = true;
			carInfo6.weaponActLev = 0;
			carInfo6.wheelsActLev = 0;
			carInfo6.engineActLev = 0;
			carInfo6.healthActLev = 0;
			carInfo6.turboActLev = 0;
			list6.Add(carInfo6);
			Progress.shop.Cars = list6.ToArray();
		}
		if (Progress.shop.Cars.Length < 11)
		{
			List<Progress.Shop.CarInfo> list7 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars7 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item7 in cars7)
			{
				list7.Add(item7);
			}
			Progress.Shop.CarInfo carInfo7 = new Progress.Shop.CarInfo();
			carInfo7.bought = true;
			carInfo7.weaponActLev = 0;
			carInfo7.wheelsActLev = 0;
			carInfo7.engineActLev = 0;
			carInfo7.healthActLev = 0;
			carInfo7.turboActLev = 0;
			list7.Add(carInfo7);
			Progress.shop.Cars = list7.ToArray();
		}
		if (Progress.shop.Cars.Length < 12)
		{
			List<Progress.Shop.CarInfo> list8 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars8 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item8 in cars8)
			{
				list8.Add(item8);
			}
			Progress.Shop.CarInfo carInfo8 = new Progress.Shop.CarInfo();
			carInfo8.bought = true;
			carInfo8.weaponActLev = 0;
			carInfo8.wheelsActLev = 0;
			carInfo8.engineActLev = 0;
			carInfo8.healthActLev = 0;
			carInfo8.turboActLev = 0;
			list8.Add(carInfo8);
			Progress.shop.Cars = list8.ToArray();
		}
		if (Progress.shop.Cars.Length < 13)
		{
			List<Progress.Shop.CarInfo> list9 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars9 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item9 in cars9)
			{
				list9.Add(item9);
			}
			Progress.Shop.CarInfo carInfo9 = new Progress.Shop.CarInfo();
			carInfo9.bought = true;
			carInfo9.weaponActLev = 0;
			carInfo9.wheelsActLev = 0;
			carInfo9.engineActLev = 0;
			carInfo9.healthActLev = 0;
			carInfo9.turboActLev = 0;
			list9.Add(carInfo9);
			Progress.shop.Cars = list9.ToArray();
		}
		if (Progress.shop.Cars.Length < 14)
		{
			List<Progress.Shop.CarInfo> list10 = new List<Progress.Shop.CarInfo>();
			Progress.Shop.CarInfo[] cars10 = Progress.shop.Cars;
			foreach (Progress.Shop.CarInfo item10 in cars10)
			{
				list10.Add(item10);
			}
			Progress.Shop.CarInfo carInfo10 = new Progress.Shop.CarInfo();
			carInfo10.bought = true;
			carInfo10.weaponActLev = 0;
			carInfo10.wheelsActLev = 0;
			carInfo10.engineActLev = 0;
			carInfo10.healthActLev = 0;
			carInfo10.turboActLev = 0;
			list10.Add(carInfo10);
			Progress.shop.Cars = list10.ToArray();
		}
		Progress.shop.Cars[3].weaponActLev = 3;
		Progress.shop.Cars[3].wheelsActLev = 3;
		Progress.shop.Cars[3].engineActLev = 3;
		Progress.shop.Cars[3].healthActLev = 3;
		Progress.shop.Cars[3].turboActLev = 3;
		Progress.shop.Cars[4].weaponActLev = 3;
		Progress.shop.Cars[4].wheelsActLev = 3;
		Progress.shop.Cars[4].engineActLev = 3;
		Progress.shop.Cars[4].healthActLev = 3;
		Progress.shop.Cars[4].turboActLev = 3;
		Progress.shop.Cars[5].weaponActLev = 3;
		Progress.shop.Cars[5].wheelsActLev = 3;
		Progress.shop.Cars[5].engineActLev = 3;
		Progress.shop.Cars[5].healthActLev = 3;
		Progress.shop.Cars[5].turboActLev = 3;
		Progress.shop.Cars[3].bought = true;
		Progress.shop.Cars[4].bought = true;
		Progress.shop.Cars[5].bought = true;
		Progress.shop.Cars[6].bought = true;
		Progress.shop.Cars[7].bought = true;
		Progress.shop.Cars[8].bought = true;
		Progress.shop.Cars[9].bought = true;
		Progress.shop.Cars[10].bought = true;
		Progress.shop.Cars[11].bought = true;
		Progress.shop.Cars[12].bought = true;
		Progress.shop.Cars[13].bought = true;
		if (Progress.shop.activeCar == 13)
		{
			if (Progress.shop.Cars[13].equipped)
			{
				Progress.shop.activeCar = 13;
			}
			else
			{
				Progress.shop.activeCar = 12;
			}
		}
		if (Progress.shop.activeCar == 12)
		{
			if (Progress.shop.Cars[12].equipped)
			{
				Progress.shop.activeCar = 12;
			}
			else
			{
				Progress.shop.activeCar = 11;
			}
		}
		if (Progress.shop.activeCar == 11)
		{
			if (Progress.shop.Cars[11].equipped)
			{
				Progress.shop.activeCar = 11;
			}
			else
			{
				Progress.shop.activeCar = 10;
			}
		}
		if (Progress.shop.activeCar == 10)
		{
			if (!Progress.shop.Cars[10].equipped)
			{
				if (!Progress.shop.Cars[9].equipped)
				{
					if (!Progress.shop.Cars[8].equipped)
					{
						if (!Progress.shop.Cars[7].equipped)
						{
							if (!Progress.shop.Cars[6].equipped)
							{
								if (!Progress.shop.Cars[5].equipped)
								{
									if (!Progress.shop.Cars[4].equipped)
									{
										if (!Progress.shop.Cars[3].equipped)
										{
											if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
											{
												if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
												{
													Progress.shop.activeCar = 0;
												}
												else
												{
													Progress.shop.activeCar = 1;
												}
											}
											else
											{
												Progress.shop.activeCar = 2;
											}
										}
										else
										{
											Progress.shop.activeCar = 3;
										}
									}
									else
									{
										Progress.shop.activeCar = 4;
									}
								}
								else
								{
									Progress.shop.activeCar = 5;
								}
							}
							else
							{
								Progress.shop.activeCar = 6;
							}
						}
						else
						{
							Progress.shop.activeCar = 7;
						}
					}
					else
					{
						Progress.shop.activeCar = 8;
					}
				}
				else
				{
					Progress.shop.activeCar = 9;
				}
			}
			else
			{
				Progress.shop.activeCar = 10;
			}
		}
		else if (Progress.shop.activeCar == 9)
		{
			if (!Progress.shop.Cars[9].equipped)
			{
				if (!Progress.shop.Cars[8].equipped)
				{
					if (!Progress.shop.Cars[7].equipped)
					{
						if (!Progress.shop.Cars[6].equipped)
						{
							if (!Progress.shop.Cars[5].equipped)
							{
								if (!Progress.shop.Cars[4].equipped)
								{
									if (!Progress.shop.Cars[3].equipped)
									{
										if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
										{
											if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
											{
												Progress.shop.activeCar = 0;
											}
											else
											{
												Progress.shop.activeCar = 1;
											}
										}
										else
										{
											Progress.shop.activeCar = 2;
										}
									}
									else
									{
										Progress.shop.activeCar = 3;
									}
								}
								else
								{
									Progress.shop.activeCar = 4;
								}
							}
							else
							{
								Progress.shop.activeCar = 5;
							}
						}
						else
						{
							Progress.shop.activeCar = 6;
						}
					}
					else
					{
						Progress.shop.activeCar = 7;
					}
				}
				else
				{
					Progress.shop.activeCar = 8;
				}
			}
			else
			{
				Progress.shop.activeCar = 9;
			}
		}
		else if (Progress.shop.activeCar == 8)
		{
			if (!Progress.shop.Cars[8].equipped)
			{
				if (!Progress.shop.Cars[7].equipped)
				{
					if (!Progress.shop.Cars[6].equipped)
					{
						if (!Progress.shop.Cars[5].equipped)
						{
							if (!Progress.shop.Cars[4].equipped)
							{
								if (!Progress.shop.Cars[3].equipped)
								{
									if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
									{
										if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
										{
											Progress.shop.activeCar = 0;
										}
										else
										{
											Progress.shop.activeCar = 1;
										}
									}
									else
									{
										Progress.shop.activeCar = 2;
									}
								}
								else
								{
									Progress.shop.activeCar = 3;
								}
							}
							else
							{
								Progress.shop.activeCar = 4;
							}
						}
						else
						{
							Progress.shop.activeCar = 5;
						}
					}
					else
					{
						Progress.shop.activeCar = 6;
					}
				}
				else
				{
					Progress.shop.activeCar = 7;
				}
			}
			else
			{
				Progress.shop.activeCar = 8;
			}
		}
		else if (Progress.shop.activeCar == 7)
		{
			if (!Progress.shop.Cars[7].equipped)
			{
				if (!Progress.shop.Cars[6].equipped)
				{
					if (!Progress.shop.Cars[5].equipped)
					{
						if (!Progress.shop.Cars[4].equipped)
						{
							if (!Progress.shop.Cars[3].equipped)
							{
								if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
								{
									if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
									{
										Progress.shop.activeCar = 0;
									}
									else
									{
										Progress.shop.activeCar = 1;
									}
								}
								else
								{
									Progress.shop.activeCar = 2;
								}
							}
							else
							{
								Progress.shop.activeCar = 3;
							}
						}
						else
						{
							Progress.shop.activeCar = 4;
						}
					}
					else
					{
						Progress.shop.activeCar = 5;
					}
				}
				else
				{
					Progress.shop.activeCar = 6;
				}
			}
			else
			{
				Progress.shop.activeCar = 7;
			}
		}
		else if (Progress.shop.activeCar == 6)
		{
			if (!Progress.shop.Cars[6].equipped)
			{
				if (!Progress.shop.Cars[5].equipped)
				{
					if (!Progress.shop.Cars[4].equipped)
					{
						if (!Progress.shop.Cars[3].equipped)
						{
							if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
							{
								if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
								{
									Progress.shop.activeCar = 0;
								}
								else
								{
									Progress.shop.activeCar = 1;
								}
							}
							else
							{
								Progress.shop.activeCar = 2;
							}
						}
						else
						{
							Progress.shop.activeCar = 3;
						}
					}
					else
					{
						Progress.shop.activeCar = 4;
					}
				}
				else
				{
					Progress.shop.activeCar = 5;
				}
			}
			else
			{
				Progress.shop.activeCar = 6;
			}
		}
		else if (Progress.shop.activeCar == 5)
		{
			if (!Progress.shop.Cars[5].equipped)
			{
				if (!Progress.shop.Cars[4].equipped)
				{
					if (!Progress.shop.Cars[3].equipped)
					{
						if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
						{
							if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
							{
								Progress.shop.activeCar = 0;
							}
							else
							{
								Progress.shop.activeCar = 1;
							}
						}
						else
						{
							Progress.shop.activeCar = 2;
						}
					}
					else
					{
						Progress.shop.activeCar = 3;
					}
				}
				else
				{
					Progress.shop.activeCar = 4;
				}
			}
			else
			{
				Progress.shop.activeCar = 5;
			}
		}
		else if (Progress.shop.activeCar == 4)
		{
			if (!Progress.shop.Cars[4].equipped)
			{
				if (!Progress.shop.Cars[3].equipped)
				{
					if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
					{
						if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
						{
							Progress.shop.activeCar = 0;
						}
						else
						{
							Progress.shop.activeCar = 1;
						}
					}
					else
					{
						Progress.shop.activeCar = 2;
					}
				}
				else
				{
					Progress.shop.activeCar = 3;
				}
			}
			else
			{
				Progress.shop.activeCar = 4;
			}
		}
		else if (Progress.shop.activeCar == 3)
		{
			if (!Progress.shop.Cars[3].equipped)
			{
				if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
				{
					if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
					{
						Progress.shop.activeCar = 0;
					}
					else
					{
						Progress.shop.activeCar = 1;
					}
				}
				else
				{
					Progress.shop.activeCar = 2;
				}
			}
			else
			{
				Progress.shop.activeCar = 3;
			}
		}
		else if (Progress.shop.activeCar == 2)
		{
			if (!Progress.shop.BossDeath2 && !Progress.shop.Cars[2].equipped)
			{
				if (!Progress.shop.BossDeath1 && !Progress.shop.Cars[1].equipped)
				{
					Progress.shop.activeCar = 0;
				}
				else
				{
					Progress.shop.activeCar = 1;
				}
			}
			else
			{
				Progress.shop.activeCar = 2;
			}
		}
		else if (Progress.shop.activeCar == 1)
		{
			if (Progress.shop.BossDeath1 && Progress.shop.Cars[1].equipped)
			{
				Progress.shop.activeCar = 1;
			}
			else
			{
				Progress.shop.activeCar = 0;
			}
		}
		for (int num5 = 0; num5 < Progress.shop.Cars.Length; num5++)
		{
			if (Progress.shop.Cars[num5].ARMOR_STATS <= ShopManagerStats.instance.Price.Car[num5].Stock.ArmorStats)
			{
				Progress.shop.Cars[num5].ARMOR_STATS = ShopManagerStats.instance.Price.Car[num5].Stock.ArmorStats;
			}
			if (Progress.shop.Cars[num5].SPEED_STATS <= ShopManagerStats.instance.Price.Car[num5].Stock.SpeedStats)
			{
				Progress.shop.Cars[num5].SPEED_STATS = ShopManagerStats.instance.Price.Car[num5].Stock.SpeedStats;
			}
			if (Progress.shop.Cars[num5].TURBO_STATS <= ShopManagerStats.instance.Price.Car[num5].Stock.TurboStats)
			{
				Progress.shop.Cars[num5].TURBO_STATS = ShopManagerStats.instance.Price.Car[num5].Stock.TurboStats;
			}
			if (Progress.shop.Cars[num5].WEAPON_STATS <= ShopManagerStats.instance.Price.Car[num5].Stock.WeaponStats)
			{
				Progress.shop.Cars[num5].WEAPON_STATS = ShopManagerStats.instance.Price.Car[num5].Stock.WeaponStats;
			}
		}
		if (Progress.shop.Cars.Length == 4)
		{
			Progress.shop.Cars[3].bought = true;
			Progress.shop.Cars[3].weaponActLev = 3;
			Progress.shop.Cars[3].wheelsActLev = 3;
			Progress.shop.Cars[3].engineActLev = 3;
			Progress.shop.Cars[3].healthActLev = 3;
			Progress.shop.Cars[3].turboActLev = 3;
		}
		StartCoroutine(WaitAnConnetToGamecenter());
		NotificationsWrapper.HideAllNotifications();
		double totalSeconds = (DateTime.Now - Progress.levels.TakeDate).TotalSeconds;
		float num6 = (long)totalSeconds;
		if (num6 >= 86400f)
		{
			if (num6 >= 172800f)
			{
				Progress.levels.dayEnded = 0;
				Progress.shop.NeedForDB = true;
			}
			else
			{
				Progress.shop.NeedForDB = true;
			}
		}
		int notificationId = Progress.notifications.GetNotificationId("BarrelNotificationPlusOneDay");
		NotificationsWrapper.Clear(notificationId);
		Progress.notifications.Remove(notificationId);
		Progress.shop.Cars[0].bought = true;
		Progress.shop.Cars[0].equipped = true;
		if (!Progress.review.atLeastOnePurchase)
		{
		}
		StartCoroutine(showSplash2());
	}

	private IEnumerator WaitAnConnetToGamecenter()
	{
		yield return null;
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		login();
		if (isGCConectionFirst && Game.GPWasConnected() != 0)
		{
			isGCConectionFirst = false;
			GameCenter.Init();
			Game.InitGameCenter();
			if (Progress.shop.foundProgress)
			{
				GameCenterWrapper.OnAvailableSaveFound = (GameCenterWrapper.DelAvailableSaveFound)Delegate.Combine(GameCenterWrapper.OnAvailableSaveFound, new GameCenterWrapper.DelAvailableSaveFound(Game.OnSavesLoaded));
			}
		}
	}

	public void login()
	{
		Social.localUser.Authenticate(delegate(bool success)
		{
			if (success)
			{
				UnityEngine.Debug.Log("Login to GP success  ==> " + success);
				GameCenterWrapper.isLoggedIn = true;
			}
			else
			{
				GameCenterWrapper.isLoggedIn = false;
				UnityEngine.Debug.Log("Login to GP success  ==> " + success);
			}
			Progress.settings.LoginToGP = success;
		});
	}

	private IEnumerator showSplash2()
	{
		HSBColor hsb = HSBColor.FromColor(cam.backgroundColor);
		while (hsb.b > 0f)
		{
			hsb.b -= 0.05f;
			cam.backgroundColor = hsb.ToColor();
			yield return new WaitForFixedUpdate();
		}
		float speed = 0.02f;
		while (true)
		{
			Color color = splash2.color;
			if (!(color.a < 1f))
			{
				break;
			}
			SpriteRenderer spriteRenderer = splash2;
			Color color2 = splash2.color;
			spriteRenderer.color = new Color(1f, 1f, 1f, color2.a + speed);
			yield return new WaitForFixedUpdate();
		}
		InitAll();
		yield return new WaitForSeconds(3f);
		while (true)
		{
			Color color3 = splash2.color;
			if (!(color3.a > 0f))
			{
				break;
			}
			SpriteRenderer spriteRenderer2 = splash2;
			Color color4 = splash2.color;
			spriteRenderer2.color = new Color(1f, 1f, 1f, color4.a - speed);
			yield return new WaitForFixedUpdate();
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Preloader");
	}

	private void InitAll()
	{
		AnalyticsManager.StartSession();
		InAppManager.instance.InitPurchases();
		GameEnergyLogic.Init();
	}
}

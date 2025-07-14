using Smokoko.DebugModule;
using Smokoko.Social;
using UnityEngine;

public class GameCheats : MonoBehaviour
{
	private ModalWindow _modal;

	private int cheatsWindowID = -1;

	private int cheatsInApWindowID = -1;

	public ModalWindow modal
	{
		get
		{
			if (_modal == null)
			{
				_modal = ModalWindow.instance;
			}
			return _modal;
		}
	}

	private void badge()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 12; j++)
			{
				if (Progress.levels._pack[i]._level[j] != null)
				{
					Progress.levels._pack[i]._level[j].oldticket = 3;
					Progress.levels._pack[i]._level[j].ticket = 3;
				}
			}
		}
	}

	public void Show()
	{
		if (modal.HasWindow(cheatsWindowID))
		{
			modal.Show(cheatsWindowID);
			return;
		}
		cheatsWindowID = modal.Show("Some game cheats", new ButtonInfo("Dron fire", delegate
		{
			Progress.shop.dronFireActive = !Progress.shop.dronFireActive;
			Progress.shop.dronFireBuy = !Progress.shop.dronFireBuy;
		}), new ButtonInfo("Tank", delegate
		{
			Progress.shop.activeCar = 11;
		}), new ButtonInfo("Kristal", delegate
		{
			Progress.shop.Incubator_CountRuby1 = 100;
			Progress.shop.Incubator_CountRuby2 = 100;
			Progress.shop.Incubator_CountRuby3 = 100;
			Progress.shop.Incubator_CountRuby4 = 100;
		}), new ButtonInfo("Check Devise ==> ", delegate
		{
			ISN_Logger.Log("SystemInfo.deviceModel   =>" + SystemInfo.deviceModel);
			ISN_Logger.Log("SystemInfo.deviceName   =>" + SystemInfo.deviceName);
			ISN_Logger.Log("SystemInfo.deviceType   =>" + SystemInfo.deviceType);
			ISN_Logger.Log("SystemInfo.deviceUniqueIdentifier   =>" + SystemInfo.deviceUniqueIdentifier);
		}), new ButtonInfo("CLEAR _ SPAWN NOTIF  ==> ", delegate
		{
			Progress.Notifications notifications2 = Progress.notifications;
			NotificationsWrapper.Clear(3);
			notifications2.Remove(3);
			int id2 = NotificationsWrapper.ScheduleLocalNotification(3, "Car Eats Car 3_3", "TEST", 10);
			notifications2.Add(id2, "TEST");
			Progress.notifications = notifications2;
		}), new ButtonInfo("SPAWN NOTIF  ==> ", delegate
		{
			Progress.Notifications notifications = Progress.notifications;
			int id = NotificationsWrapper.ScheduleLocalNotification(4, "Car Eats Car 3_4", "TEST", 10);
			notifications.Add(id, "TEST");
			Progress.notifications = notifications;
		}), new ButtonInfo("EGSS TEST  ==> ", delegate
		{
			Progress.shop.Cars[9].equipped = false;
			Progress.shop.Cars[10].equipped = false;
			Progress.shop.Incubator_Eggs[1] = true;
			Progress.shop.Incubator_Eggs[2] = true;
			Progress.shop.Incubator_CountRuby1 = 10;
			Progress.shop.Incubator_CountRuby2 = 10;
			Progress.shop.Incubator_CountRuby3 = 10;
			Progress.shop.Incubator_CountRuby4 = 10;
		}), new ButtonInfo("VIDEO", delegate
		{
			NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
			{
				if (sucess)
				{
					UnityEngine.Debug.Log("video");
				}
				else
				{
					UnityEngine.Debug.Log("brake video");
				}
			}, delegate
			{
				UnityEngine.Debug.Log("no video");
			}, delegate
			{
				UnityEngine.Debug.Log("brake video");
			});
		}), new ButtonInfo("VIDEO adcolony", delegate
		{
			AdColonyWrapper.instance.PlayAdVideo(delegate
			{
				UnityEngine.Debug.Log("no video");
			});
		}), new ButtonInfo("inter", delegate
		{
			AdvertWrapper.instance.ShowInterstitial(show: true);
		}), new ButtonInfo("inter adcolony", delegate
		{
			AdColonyWrapper.instance.PlayAdInter(delegate
			{
				UnityEngine.Debug.Log("no video");
			});
		}), new ButtonInfo("Add + 50badge", delegate
		{
			Progress.levels.Pack(1).Level(1).oldticket += 50;
			Progress.levels.Pack(1).Level(1).ticket += 50;
		}), new ButtonInfo("SHARE TO FB", delegate
		{
			FBLeaderboard.PostToFacebook("TEST POST TO FB");
		}), new ButtonInfo("BADGE FOR ALL LEVELS", delegate
		{
			badge();
		}), new ButtonInfo("kill 1 boss", delegate
		{
			Progress.shop.BossDeath1 = true;
		}), new ButtonInfo("kill 2 boss", delegate
		{
			Progress.shop.BossDeath2 = true;
		}), new ButtonInfo("ON/OFF loc 9", delegate
		{
			Progress.shop.TestFor9 = !Progress.shop.TestFor9;
		}));
		modal.SetDialogLinesCount(cheatsWindowID, 2);
	}

	private void ShowInApps()
	{
		if (modal.HasWindow(cheatsInApWindowID))
		{
			modal.Show(cheatsInApWindowID);
			return;
		}
		cheatsInApWindowID = modal.Show("In-app purchases");
		modal.SetDialogLinesCount(cheatsInApWindowID, 4);
	}
}

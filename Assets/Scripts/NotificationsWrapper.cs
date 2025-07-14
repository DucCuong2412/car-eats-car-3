using System;
using UnityEngine;

public class NotificationsWrapper
{
	public static void ClearAll()
	{
		LocalNotification.CancelAllNotification();
	}

	public static void Clear(int id)
	{
		LocalNotification.CancelNotification(id);
	}

	public static int ScheduleLocalNotification(int id, string title, string message, int time, bool sound = true, int badges = 0)
	{
		LocalNotification.SendNotification(id, TimeSpan.FromSeconds(time), title, message, new Color32(byte.MaxValue, 68, 68, byte.MaxValue), sound, true, false, string.Empty, null, "default");
		return id;
	}

	public static void HideAllNotifications()
	{
	}
}

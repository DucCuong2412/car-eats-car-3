using SA.Common.Models;
using SA.Common.Pattern;
using System;
using UnityEngine;

public class NotificationExample : BaseIOSFeaturePreview
{
	private int lastNotificationId;

	private void Awake()
	{
		ISN_LocalNotificationsController.OnLocalNotificationReceived += HandleOnLocalNotificationReceived;
		if (Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_LocalNotification launchNotification = Singleton<ISN_LocalNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Notification", "Messgae: " + launchNotification.Message + "\nNotification Data: " + launchNotification.Data);
		}
		if (Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification != null)
		{
			ISN_RemoteNotification launchNotification2 = Singleton<ISN_RemoteNotificationsController>.Instance.LaunchNotification;
			IOSMessage.Create("Launch Remote Notification", "Body: " + launchNotification2.Body);
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local and Push Notifications", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Request Permissions"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.RequestNotificationPermissions();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Print Notification Settings"))
		{
			CheckNotificationSettings();
		}
		StartY += YButtonStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule Notification Silent"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += OnNotificationScheduleResult;
			ISN_LocalNotification iSN_LocalNotification = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text No Sound", useSound: false);
			iSN_LocalNotification.SetData("some_test_data");
			iSN_LocalNotification.Schedule();
			lastNotificationId = iSN_LocalNotification.Id;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Schedule Notification"))
		{
			ISN_LocalNotificationsController.OnNotificationScheduleResult += OnNotificationScheduleResult;
			ISN_LocalNotification iSN_LocalNotification2 = new ISN_LocalNotification(DateTime.Now.AddSeconds(5.0), "Your Notification Text");
			iSN_LocalNotification2.SetData("some_test_data");
			iSN_LocalNotification2.SetSoundName("purchase_ok.wav");
			iSN_LocalNotification2.SetBadgesNumber(1);
			iSN_LocalNotification2.Schedule();
			lastNotificationId = iSN_LocalNotification2.Id;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cancel All Notifications"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelAllLocalNotifications();
			IOSNativeUtility.SetApplicationBagesNumber(0);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cansel Last Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.CancelLocalNotificationById(lastNotificationId);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local and Push Notifications", style);
		StartY += YLableStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reg Device For Push Notif. "))
		{
			Singleton<ISN_RemoteNotificationsController>.Instance.RegisterForRemoteNotifications(delegate(ISN_RemoteNotificationsRegistrationResult res)
			{
				UnityEngine.Debug.Log("ISN_RemoteNotificationsRegistrationResult: " + res.IsSucceeded);
				if (!res.IsSucceeded)
				{
					UnityEngine.Debug.Log(res.Error.Code + " / " + res.Error.Message);
				}
				else
				{
					UnityEngine.Debug.Log(res.Token.DeviceId);
				}
			});
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Game Kit Notification"))
		{
			Singleton<ISN_LocalNotificationsController>.Instance.ShowGmaeKitNotification("Title", "Message");
		}
	}

	public void CheckNotificationSettings()
	{
		int allowedNotificationsType = ISN_LocalNotificationsController.AllowedNotificationsType;
		UnityEngine.Debug.Log("AllowedNotificationsType: " + allowedNotificationsType);
		if ((allowedNotificationsType & 2) != 0)
		{
			UnityEngine.Debug.Log("Sound avaliable");
		}
		if ((allowedNotificationsType & 1) != 0)
		{
			UnityEngine.Debug.Log("Badge avaliable");
		}
		if ((allowedNotificationsType & 4) != 0)
		{
			UnityEngine.Debug.Log("Alert avaliable");
		}
	}

	private void HandleOnLocalNotificationReceived(ISN_LocalNotification notification)
	{
		IOSMessage.Create("Notification Received", "Messgae: " + notification.Message + "\nNotification Data: " + notification.Data);
	}

	private void OnNotificationScheduleResult(Result res)
	{
		ISN_LocalNotificationsController.OnNotificationScheduleResult -= OnNotificationScheduleResult;
		string empty = string.Empty;
		empty = ((!res.IsSucceeded) ? (empty + "Notification scheduling failed") : (empty + "Notification was successfully scheduled\n allowed notifications types: \n"));
		IOSMessage.Create("On Notification Schedule Result", empty);
	}
}

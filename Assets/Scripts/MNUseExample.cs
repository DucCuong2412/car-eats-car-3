using System;
using UnityEngine;

public class MNUseExample : MNFeaturePreview
{
	public string appleId = string.Empty;

	public string apdroidAppUrl = "market://details?id=com.google.earth";

	private void Awake()
	{
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Native Pop Ups", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Rate PopUp with events"))
		{
			MobileNativeRateUs mobileNativeRateUs = new MobileNativeRateUs("Like this game?", "Please rate to support future updates!");
			mobileNativeRateUs.SetAppleId(appleId);
			mobileNativeRateUs.SetAndroidAppUrl(apdroidAppUrl);
			MobileNativeRateUs mobileNativeRateUs2 = mobileNativeRateUs;
			mobileNativeRateUs2.OnComplete = (Action<MNDialogResult>)Delegate.Combine(mobileNativeRateUs2.OnComplete, new Action<MNDialogResult>(OnRatePopUpClose));
			mobileNativeRateUs.Start();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dialog PopUp"))
		{
			MobileNativeDialog mobileNativeDialog = new MobileNativeDialog("Dialog Titile", "Dialog message");
			MobileNativeDialog mobileNativeDialog2 = mobileNativeDialog;
			mobileNativeDialog2.OnComplete = (Action<MNDialogResult>)Delegate.Combine(mobileNativeDialog2.OnComplete, new Action<MNDialogResult>(OnDialogClose));
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Message PopUp"))
		{
			MobileNativeMessage mobileNativeMessage = new MobileNativeMessage("Message Titile", "Message message");
			MobileNativeMessage mobileNativeMessage2 = mobileNativeMessage;
			mobileNativeMessage2.OnComplete = (Action)Delegate.Combine(mobileNativeMessage2.OnComplete, new Action(OnMessageClose));
		}
		StartY += YButtonStep;
		StartX = XStartPos;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Prealoder"))
		{
			MNP.ShowPreloader("Title", "Message");
			Invoke("OnPreloaderTimeOut", 3f);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Hide Prealoder"))
		{
			MNP.HidePreloader();
		}
	}

	private void OnPreloaderTimeOut()
	{
		MNP.HidePreloader();
	}

	private void OnRatePopUpClose(MNDialogResult result)
	{
		switch (result)
		{
		case MNDialogResult.RATED:
			UnityEngine.Debug.Log("Rate Option pickied");
			break;
		case MNDialogResult.REMIND:
			UnityEngine.Debug.Log("Remind Option pickied");
			break;
		case MNDialogResult.DECLINED:
			UnityEngine.Debug.Log("Declined Option pickied");
			break;
		}
		new MobileNativeMessage("Result", result.ToString() + " button pressed");
	}

	private void OnDialogClose(MNDialogResult result)
	{
		switch (result)
		{
		case MNDialogResult.YES:
			UnityEngine.Debug.Log("Yes button pressed");
			break;
		case MNDialogResult.NO:
			UnityEngine.Debug.Log("No button pressed");
			break;
		}
		new MobileNativeMessage("Result", result.ToString() + " button pressed");
	}

	private void OnMessageClose()
	{
		new MobileNativeMessage("Result", "Message Closed");
	}
}

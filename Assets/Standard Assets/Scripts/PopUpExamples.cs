using SA.Common.Pattern;
using UnityEngine;

public class PopUpExamples : BaseIOSFeaturePreview
{
	private void Awake()
	{
		IOSNativePopUpManager.showMessage("Welcome", "Hey there, welcome to the Pop-ups testing scene!");
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Native Pop-ups", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Rate Pop-up with events"))
		{
			IOSRateUsPopUp iOSRateUsPopUp = IOSRateUsPopUp.Create("Like this game?", "Please rate to support future updates!");
			iOSRateUsPopUp.OnComplete += onRatePopUpClose;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dialog Pop-up"))
		{
			IOSDialog iOSDialog = IOSDialog.Create("Dialog Title", "Dialog message");
			iOSDialog.OnComplete += onDialogClose;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Message Pop-up"))
		{
			IOSMessage iOSMessage = IOSMessage.Create("Message Title", "Message body");
			iOSMessage.OnComplete += onMessageClose;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dismissed Pop-up"))
		{
			Invoke("dismissAlert", 2f);
			IOSMessage.Create("Hello", "I will die in 2 sec");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Open App Store"))
		{
			IOSNativeUtility.RedirectToAppStoreRatingPage();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Preloader "))
		{
			IOSNativeUtility.ShowPreloader();
			Invoke("HidePreloader", 3f);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Hide Preloader"))
		{
			HidePreloader();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Locale"))
		{
			IOSNativeUtility.OnLocaleLoaded += GetLocale;
			Singleton<IOSNativeUtility>.Instance.GetLocale();
		}
	}

	private void HidePreloader()
	{
		IOSNativeUtility.HidePreloader();
	}

	private void dismissAlert()
	{
		IOSNativePopUpManager.dismissCurrentAlert();
	}

	private void onRatePopUpClose(IOSDialogResult result)
	{
		switch (result)
		{
		case IOSDialogResult.RATED:
			ISN_Logger.Log("Rate button pressed");
			break;
		case IOSDialogResult.REMIND:
			ISN_Logger.Log("Remind button pressed");
			break;
		case IOSDialogResult.DECLINED:
			ISN_Logger.Log("Decline button pressed");
			break;
		}
		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	private void onDialogClose(IOSDialogResult result)
	{
		switch (result)
		{
		case IOSDialogResult.YES:
			ISN_Logger.Log("Yes button pressed");
			break;
		case IOSDialogResult.NO:
			ISN_Logger.Log("No button pressed");
			break;
		}
		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	private void onMessageClose()
	{
		ISN_Logger.Log("Message was just closed");
		IOSNativePopUpManager.showMessage("Result", "Message Closed");
	}

	private void GetLocale(ISN_Locale locale)
	{
		ISN_Logger.Log("GetLocale");
		ISN_Logger.Log(locale.DisplayCountry);
		IOSNativePopUpManager.showMessage("Locale Info:", "Country:" + locale.CountryCode + "/" + locale.DisplayCountry + "  :   Language:" + locale.LanguageCode + "/" + locale.DisplayLanguage);
		IOSNativeUtility.OnLocaleLoaded -= GetLocale;
	}
}

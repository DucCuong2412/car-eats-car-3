using UnityEngine;

public class SendMail : MonoBehaviour
{
	public static void EmailUs(string subject = "Car Eats Car 2/Suggestion", string body = "")
	{
		string feeedbackMail = RateUsLinks.Instance.feeedbackMail;
		subject = MyEscapeURL(subject);
		body = MyEscapeURL("Please Enter your message here\n\n\n\n________\n\nPlease Do Not Modify This\n\nModel: " + SystemInfo.deviceModel + "\n\nOS: " + SystemInfo.operatingSystem + "\n\n________");
		Application.OpenURL("mailto:" + feeedbackMail + "?subject=" + subject + "&body=" + body);
	}

	private static string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}
}

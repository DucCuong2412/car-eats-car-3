using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyView : MonoBehaviour
{
	public Animation anim;

	public int MoneyDay1 = 100;

	public int MoneyDay2 = 200;

	public int MoneyDay3 = 300;

	public int MoneyDay4 = 400;

	public int MoneyDay5;

	public int MoneyDay6 = 600;

	public Button CloseBut;

	public Button Day1ButOk;

	public GameObject Day1YesterdayOk;

	public GameObject Day1TodayBack;

	public GameObject Day1Normal;

	public Text Day1TextTodey;

	public Text Day1Text;

	public Button Day2ButOk;

	public GameObject Day2YesterdayOk;

	public GameObject Day2TodayBack;

	public GameObject Day2Normal;

	public Text Day2TextTodey;

	public Text Day2Text;

	public Button Day3ButOk;

	public GameObject Day3YesterdayOk;

	public GameObject Day3TodayBack;

	public GameObject Day3Normal;

	public Text Day3TextTodey;

	public Text Day3Text;

	public Button Day4ButOk;

	public GameObject Day4YesterdayOk;

	public GameObject Day4TodayBack;

	public GameObject Day4Normal;

	public Text Day4TextTodey;

	public Text Day4Text;

	public Button Day5ButOk;

	public GameObject Day5Panel;

	public GameObject Day5GoldPanel;

	public GameObject Day5TodayBack;

	public Button Day6ButOk;

	public GameObject Day6Panel;

	public GameObject Day6TodayBack;

	public Text Day6TextTodey;

	public Text Day6Text;

	public List<Text> textDay = new List<Text>();

	private int dayEnded;

	private int moneyNum;

	private string nameStr = "daily";

	private void Start()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		Audio.PlayAsync("gui_screen_on");
		ChengView();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			CloseFunc();
		}
	}

	private void ChengView()
	{
		for (int i = 0; i < textDay.Count; i++)
		{
			if (i == textDay.Count)
			{
				textDay[i].text = LanguageManager.Instance.GetTextValue("DAY *").Replace("*", i.ToString() + "+");
			}
			else
			{
				textDay[i].text = LanguageManager.Instance.GetTextValue("DAY *").Replace("*", (i + 1).ToString());
			}
		}
		CloseBut.onClick.AddListener(CloseFunc);
		Day1Normal.SetActive(value: true);
		Day1YesterdayOk.SetActive(value: false);
		Day1TodayBack.SetActive(value: false);
		Day2YesterdayOk.SetActive(value: false);
		Day2TodayBack.SetActive(value: false);
		Day2Normal.SetActive(value: true);
		Day3YesterdayOk.SetActive(value: false);
		Day3TodayBack.SetActive(value: false);
		Day3Normal.SetActive(value: true);
		Day4YesterdayOk.SetActive(value: false);
		Day4TodayBack.SetActive(value: false);
		Day4Normal.SetActive(value: true);
		Day5ButOk.gameObject.SetActive(value: false);
		Day5TodayBack.SetActive(value: false);
		Day5GoldPanel.SetActive(value: false);
		Day6ButOk.gameObject.SetActive(value: false);
		Day6Panel.SetActive(value: false);
		Day6TodayBack.SetActive(value: false);
		bool flag = false;
		double totalSeconds = (DateTime.Now - Progress.levels.TakeDate).TotalSeconds;
		float num = (long)totalSeconds;
		if (num >= 86400f)
		{
			if (num >= 172800f)
			{
				Progress.levels.dayEnded = 0;
				flag = true;
				Progress.shop.NeedForDB = true;
			}
			else
			{
				flag = true;
				Progress.shop.NeedForDB = true;
			}
		}
		dayEnded = Progress.levels.dayEnded;
		if (dayEnded == 0)
		{
			Progress.shop.NeedForDB = true;
			flag = true;
		}
		Day1TextTodey.text = MoneyDay1.ToString();
		Day2TextTodey.text = MoneyDay2.ToString();
		Day3TextTodey.text = MoneyDay3.ToString();
		Day4TextTodey.text = MoneyDay4.ToString();
		Day6TextTodey.text = MoneyDay6.ToString();
		Day1Text.text = MoneyDay1.ToString();
		Day2Text.text = MoneyDay2.ToString();
		Day3Text.text = MoneyDay3.ToString();
		Day4Text.text = MoneyDay4.ToString();
		Day6Text.text = MoneyDay6.ToString();
		switch (dayEnded)
		{
		case 0:
			if (flag)
			{
				Day1ButOk.transform.parent.gameObject.SetActive(value: true);
				moneyNum = MoneyDay1;
				Day1ButOk.onClick.AddListener(Take);
				Day1TodayBack.SetActive(value: true);
			}
			return;
		case 1:
			Day1YesterdayOk.SetActive(value: true);
			if (flag)
			{
				Day2TodayBack.SetActive(value: true);
				Day2ButOk.transform.parent.gameObject.SetActive(value: true);
				moneyNum = MoneyDay2;
				Day2ButOk.onClick.AddListener(Take);
			}
			return;
		case 2:
			Day1YesterdayOk.SetActive(value: true);
			Day2YesterdayOk.SetActive(value: true);
			if (flag)
			{
				Day3TodayBack.SetActive(value: true);
				Day3ButOk.transform.parent.gameObject.SetActive(value: true);
				moneyNum = MoneyDay3;
				Day3ButOk.onClick.AddListener(Take);
			}
			return;
		case 3:
			Day1YesterdayOk.SetActive(value: true);
			Day2YesterdayOk.SetActive(value: true);
			Day3YesterdayOk.SetActive(value: true);
			if (flag)
			{
				Day4TodayBack.SetActive(value: true);
				Day4ButOk.transform.parent.gameObject.SetActive(value: true);
				moneyNum = MoneyDay4;
				Day4ButOk.onClick.AddListener(Take);
			}
			return;
		case 4:
			Day1YesterdayOk.SetActive(value: true);
			Day2YesterdayOk.SetActive(value: true);
			Day3YesterdayOk.SetActive(value: true);
			Day4YesterdayOk.SetActive(value: true);
			if (flag)
			{
				Day5GoldPanel.SetActive(value: true);
				Day5TodayBack.SetActive(value: true);
				Day5ButOk.gameObject.SetActive(value: true);
				moneyNum = MoneyDay5;
				Day5ButOk.onClick.AddListener(Take);
			}
			return;
		}
		Day1YesterdayOk.SetActive(value: true);
		Day2YesterdayOk.SetActive(value: true);
		Day3YesterdayOk.SetActive(value: true);
		Day4YesterdayOk.SetActive(value: true);
		Day5Panel.SetActive(value: false);
		Day6Panel.SetActive(value: true);
		if (flag)
		{
			Day6TodayBack.SetActive(value: true);
			Day6ButOk.gameObject.SetActive(value: true);
			moneyNum = MoneyDay6;
			Day6ButOk.onClick.AddListener(Take);
		}
	}

	private void Take()
	{
		Audio.PlayAsync("boosters_purchase");
		Progress.shop.NeedForDB = false;
		Progress.shop.currency += moneyNum;
		Progress.levels.TakeDate = DateTime.Now;
		if (dayEnded == 4)
		{
			if (Progress.shop.BuyedDrill)
			{
				Progress.shop.currency += 1000;
			}
			else
			{
				Progress.shop.BuyedDrill = true;
			}
		}
		Progress.levels.dayEnded++;
		Progress.Notifications notifications = Progress.notifications;
		NotificationsWrapper.Clear(notifications.GetNotificationId(nameStr));
		notifications.Remove(nameStr);
		CloseFunc();
	}

	private void CloseFunc()
	{
		StartCoroutine(Close());
	}

	private IEnumerator Close()
	{
		anim.Play("dailyBonus_hide");
		while (anim.isPlaying)
		{
			yield return 0;
		}
		Game.OnStateChange(Game.gameState.Menu);
		base.gameObject.SetActive(value: false);
	}
}

using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusSetting : MonoBehaviour
{
	[Serializable]
	public class Days
	{
		public string DayNum;

		public GameObject _objText;

		public Text _NextTime;

		public Text _textDay;

		public List<GameObject> Icons = new List<GameObject>();

		public GameObject Drone;

		public GameObject GO;
	}

	public List<Days> list = new List<Days>();

	public Button _btnExit;

	public Button _btnTake;

	public Button _btnX2;

	public Animator Anim_Open_Close;

	public Animator AnimGlobal;

	public MoveOnPathScript MOPS;

	public GameObject DroneOn_off;

	public GameObject x2Anim;

	[HideInInspector]
	public int TempZmina;

	[Header("list do x2")]
	public List<GameObject> doX2 = new List<GameObject>();

	[Header("list posle x2")]
	public List<GameObject> posleX2 = new List<GameObject>();

	private Coroutine DronCorut;

	private int _isON = Animator.StringToHash("isON");

	private int _rewardIsTaken = Animator.StringToHash("rewardIsTaken");

	private int dayEnded;

	private string nameStr = "daily";

	private void OnEnable()
	{
		Progress.shop.NeedForDBExit = true;
		if (Progress.levels.dayEnded == 4)
		{
			_btnX2.gameObject.SetActive(value: false);
		}
		Audio.Play("gui_screen_on");
		list[1]._objText.SetActive(value: false);
		_btnTake.interactable = true;
		_btnExit.onClick.AddListener(Exit);
		_btnTake.onClick.AddListener(Take);
		_btnX2.onClick.AddListener(X2);
		Change();
		DronCorut = StartCoroutine(startDrone());
	}

	private IEnumerator checkInternetConnection(Action<bool> action)
	{
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null)
		{
			action(obj: false);
		}
		else
		{
			action(obj: true);
		}
	}

	private void X2()
	{
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				Sucses();
			}
			else
			{
				NoSucses();
			}
		}, delegate
		{
			NOvideo();
		}, delegate
		{
			NoSucses();
		});
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "x2_daily_bonus");
	}

	public void Sucses()
	{
		foreach (GameObject item in doX2)
		{
			item.SetActive(value: false);
		}
		foreach (GameObject item2 in posleX2)
		{
			item2.SetActive(value: true);
		}
		Progress.shop.NeedForDBX2 = true;
		x2Anim.SetActive(value: true);
		_btnX2.interactable = true;
		_btnX2.gameObject.SetActive(value: false);
	}

	public void NoSucses()
	{
		_btnX2.interactable = true;
	}

	public void NOvideo()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
		_btnX2.interactable = true;
	}

	private IEnumerator startDrone()
	{
		yield return 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Drone.activeSelf && i <= 4)
			{
				yield return new WaitForSeconds(1.5f);
				MOPS.transform.localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
				Audio.Play("drones", 1f, loop: true);
				DroneOn_off.SetActive(value: true);
				list[i].Drone.SetActive(value: false);
				TempZmina = i;
			}
		}
	}

	private void OnDisable()
	{
		Audio.Stop("drones");
		_btnExit.onClick.RemoveAllListeners();
		_btnTake.onClick.RemoveAllListeners();
	}

	private void Change()
	{
		foreach (Days item in list)
		{
			item.Drone.SetActive(value: false);
		}
		double totalSeconds = (DateTime.Now - Progress.levels.TakeDate).TotalSeconds;
		float num = (long)totalSeconds;
		if (num >= 86400f)
		{
			if (num >= 172800f)
			{
				Progress.levels.dayEnded = 0;
				Progress.shop.NeedForDB = true;
			}
			else
			{
				Progress.shop.NeedForDB = true;
			}
		}
		dayEnded = Progress.levels.dayEnded;
		if (dayEnded == 0)
		{
			Progress.shop.NeedForDB = true;
		}
		for (int i = 0; i < list.Count; i++)
		{
			list[i]._textDay.text = LanguageManager.Instance.GetTextValue("DAY *").Replace("*", (Progress.levels.dayEnded + 1 + i).ToString());
		}
		if (Progress.levels.dayEnded < ConfigForDB.instance.RevardOfFirstDay.Days.Count)
		{
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < list[j].Icons.Count; k++)
				{
					if (Progress.levels.dayEnded + j < ConfigForDB.instance.RevardOfFirstDay.Days.Count)
					{
						if (!Progress.shop.TakeDrone && ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded + j].Drone)
						{
							list[j].Icons[k].SetActive(value: false);
							list[j].Drone.SetActive(value: true);
						}
						else if (list[j].Icons[k].name == ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded + j].Icon.ToString())
						{
							list[j].Icons[k].SetActive(value: true);
						}
						else
						{
							list[j].Icons[k].SetActive(value: false);
						}
					}
					else if (list[j].Icons[k].name == ConfigForDB.instance.iconNextDay.ToString())
					{
						list[j].Icons[k].SetActive(value: true);
					}
					else
					{
						list[j].Icons[k].SetActive(value: false);
					}
				}
			}
		}
		else
		{
			foreach (Days item2 in list)
			{
				foreach (GameObject icon in item2.Icons)
				{
					if (icon.name == ConfigForDB.instance.iconNextDay.ToString())
					{
						icon.SetActive(value: true);
					}
					else
					{
						icon.SetActive(value: false);
					}
				}
			}
		}
		for (int l = 0; l < list.Count; l++)
		{
			if (list[l].Drone.activeSelf)
			{
				if (l == 0)
				{
					Transform transform = MOPS.PathToFollow.path_objs[0];
					Vector3 localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
					float x = localPosition.x - 136f;
					Vector3 localPosition2 = MOPS.PathToFollow.path_objs[0].localPosition;
					transform.localPosition = new Vector3(x, localPosition2.y);
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 1].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 2].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
				}
				if (l == 1)
				{
					Transform transform2 = MOPS.PathToFollow.path_objs[0];
					Vector3 localPosition3 = MOPS.PathToFollow.path_objs[0].localPosition;
					float x2 = localPosition3.x;
					Vector3 localPosition4 = MOPS.PathToFollow.path_objs[0].localPosition;
					transform2.localPosition = new Vector3(x2, localPosition4.y);
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 1].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 2].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
				}
				if (l >= 2 && l <= 4)
				{
					Transform transform3 = MOPS.PathToFollow.path_objs[0];
					Vector3 localPosition5 = MOPS.PathToFollow.path_objs[0].localPosition;
					float x3 = localPosition5.x + (float)(136 * (l - 1));
					Vector3 localPosition6 = MOPS.PathToFollow.path_objs[0].localPosition;
					transform3.localPosition = new Vector3(x3, localPosition6.y);
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 1].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
					MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 2].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
				}
			}
		}
	}

	private void Time()
	{
		double num = 86400.0 - (DateTime.Now - Progress.levels.TakeDate).TotalSeconds;
		int num2 = (int)(num / 60.0) / 60;
		int num3 = (int)(num % 3600.0) / 60;
		int num4 = (int)(num % 60.0);
		string text = string.Format("{0}:{1}:{2}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()), (num4 >= 10) ? num4.ToString() : ("0" + num4.ToString()));
		list[1]._NextTime.text = text;
	}

	private void Update()
	{
		Time();
	}

	private void Take()
	{
		list[1]._objText.SetActive(value: true);
		_btnTake.interactable = false;
		AnimGlobal.SetTrigger(_rewardIsTaken);
		_btnX2.gameObject.SetActive(value: false);
		Takes();
	}

	private void Takes()
	{
		Audio.PlayAsync("boosters_purchase");
		Progress.shop.NeedForDB = false;
		AnalyticsManager.LogEvent(EventCategoty.daily_bonus, "daily_bonus", "day " + Progress.levels.dayEnded.ToString());
		if (Progress.levels.dayEnded > ConfigForDB.instance.RevardOfFirstDay.Days.Count - 1)
		{
			if (!Progress.shop.NeedForDBX2)
			{
				Progress.shop.currency += ConfigForDB.instance.coinNextDay;
				GameEnergyLogic.AddFuel(ConfigForDB.instance.fuelNextDay);
			}
			else
			{
				Progress.shop.currency += ConfigForDB.instance.coinNextDay * 2;
				GameEnergyLogic.AddFuel(ConfigForDB.instance.fuelNextDay * 2);
			}
		}
		else
		{
			if (!Progress.shop.NeedForDBX2)
			{
				Progress.shop.currency += ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded].coin;
				GameEnergyLogic.AddFuel(ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded].fuel);
			}
			else
			{
				Progress.shop.currency += ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded].coin * 2;
				GameEnergyLogic.AddFuel(ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded].fuel * 2);
			}
			if (!Progress.shop.TakeDrone && ConfigForDB.instance.RevardOfFirstDay.Days[Progress.levels.dayEnded].Drone)
			{
				Progress.shop.TakeDrone = true;
				Progress.shop.dronBombsBuy = true;
				Progress.shop.dronBombsActive = true;
			}
		}
		Progress.levels.TakeDate = DateTime.Now;
		Progress.levels.dayEnded++;
		Progress.Notifications notifications = Progress.notifications;
		notifications.Remove(nameStr);
		int notificationId = Progress.notifications.GetNotificationId(nameStr);
		NotificationsWrapper.Clear(2);
		notifications.Remove(2);
		string textValue = LanguageManager.Instance.GetTextValue("Your daily bonus is ready. Get it!");
		int id = NotificationsWrapper.ScheduleLocalNotification(2, "Car Eats Car 3", textValue, 86400);
		notifications.Add(id, nameStr);
		Progress.notifications = notifications;
		Transform transform = MOPS.PathToFollow.path_objs[0];
		Vector3 localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
		float x = localPosition.x - 136f;
		Vector3 localPosition2 = MOPS.PathToFollow.path_objs[0].localPosition;
		transform.localPosition = new Vector3(x, localPosition2.y);
		MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 1].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
		MOPS.PathToFollow.path_objs[MOPS.PathToFollow.path_objs.Count - 2].localPosition = MOPS.PathToFollow.path_objs[0].localPosition;
		Progress.shop.NeedForDBX2 = false;
	}

	private void Exit()
	{
		StartCoroutine(close());
	}

	private IEnumerator close()
	{
		Progress.shop.NeedForDBExit = false;
		Anim_Open_Close.SetBool(_isON, value: false);
		Audio.Stop("drones");
		if (DronCorut != null)
		{
			StopCoroutine(DronCorut);
			DronCorut = null;
		}
		yield return new WaitForSeconds(1f);
		base.gameObject.SetActive(value: false);
	}
}

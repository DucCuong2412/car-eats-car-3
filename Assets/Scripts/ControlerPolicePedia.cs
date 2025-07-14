using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlerPolicePedia : MonoBehaviour
{
	[Header("preloader")]
	public PreloaderCanvas PC;

	[Header("animators Btn")]
	public Animator BtnAnim1;

	public Animator BtnAnim2;

	public Animator BtnAnim3;

	public Animator BtnAnim4;

	[Space(5f)]
	public Animator Btn2Anim1;

	public Animator Btn2Anim2;

	public Animator Btn2Anim3;

	public Animator Btn2Anim4;

	[Space(5f)]
	public Animator Btn3Anim1;

	public Animator Btn3Anim2;

	public Animator Btn3Anim3;

	public Animator Btn3Anim4;

	[Header("Text for btn")]
	public List<Text> TextDo1;

	public List<Text> TextPosle1;

	[Space(5f)]
	public List<Text> TextDo2;

	public List<Text> TextPosle2;

	[Space(5f)]
	public List<Text> TextDo3;

	public List<Text> TextPosle3;

	[Header("animators Cars")]
	public Animator PartCarAnim1;

	public Animator PartCarAnim2;

	public Animator PartCarAnim3;

	public Animator PartCarAnim4;

	[Space(5f)]
	public Animator PartCar2Anim1;

	public Animator PartCar2Anim2;

	public Animator PartCar2Anim3;

	public Animator PartCar2Anim4;

	[Space(5f)]
	public Animator PartCar3Anim1;

	public Animator PartCar3Anim2;

	public Animator PartCar3Anim3;

	public Animator PartCar3Anim4;

	[Header("buttons ")]
	public List<Button> _btn1;

	public List<Button> _btn2;

	public List<Button> _btn3;

	public ScrollForPolicopedia SFP;

	private int _isON = Animator.StringToHash("isON");

	private int _isPartTaken = Animator.StringToHash("isPartTaken");

	private int _isComplete = Animator.StringToHash("isComplete");

	[Header("Top Panel")]
	public CounterController coins;

	public CounterController fuel;

	public Button _Premiums;

	public Button _Exit;

	public GameObject Inf;

	[Header("Unlock car 1")]
	public GameObject Unlockparticl;

	public GameObject CarReal;

	public GameObject CarNotReal;

	[Header("Unlock car 2")]
	public GameObject Unlockparticl2;

	public GameObject Car2Real;

	public GameObject Car2NotReal;

	[Header("Unlock car 3")]
	public GameObject Unlockparticl3;

	public GameObject Car3Real;

	public GameObject Car3NotReal;

	private PremiumShopNew _shopWindowModel;

	private int ForCheak;

	private void OnEnable()
	{
		Chek();
		TextPosle1[0].text = ConfigForPolicePedia.instance.Car1.CollPart1.ToString();
		TextPosle1[1].text = ConfigForPolicePedia.instance.Car1.CollPart2.ToString();
		TextPosle1[2].text = ConfigForPolicePedia.instance.Car1.CollPart3.ToString();
		TextPosle1[3].text = ConfigForPolicePedia.instance.Car1.CollPart4.ToString();
		TextDo1[0].text = Progress.shop.CollKill1Car.ToString();
		TextDo1[1].text = Progress.shop.CollKill2Car.ToString();
		TextDo1[2].text = Progress.shop.CollKill3Car.ToString();
		TextDo1[3].text = Progress.shop.CollKill4Car.ToString();
		TextPosle2[0].text = ConfigForPolicePedia.instance.Car2.CollPart1.ToString();
		TextPosle2[1].text = ConfigForPolicePedia.instance.Car2.CollPart2.ToString();
		TextPosle2[2].text = ConfigForPolicePedia.instance.Car2.CollPart3.ToString();
		TextPosle2[3].text = ConfigForPolicePedia.instance.Car2.CollPart4.ToString();
		TextDo2[0].text = Progress.shop.CollKill1Car2.ToString();
		TextDo2[1].text = Progress.shop.CollKill2Car2.ToString();
		TextDo2[2].text = Progress.shop.CollKill3Car2.ToString();
		TextDo2[3].text = Progress.shop.CollKill4Car2.ToString();
		TextPosle3[0].text = ConfigForPolicePedia.instance.Car3.CollPart1.ToString();
		TextPosle3[1].text = ConfigForPolicePedia.instance.Car3.CollPart2.ToString();
		TextPosle3[2].text = ConfigForPolicePedia.instance.Car3.CollPart3.ToString();
		TextPosle3[3].text = ConfigForPolicePedia.instance.Car3.CollPart4.ToString();
		TextDo3[0].text = Progress.shop.CollKill1Car3.ToString();
		TextDo3[1].text = Progress.shop.CollKill2Car3.ToString();
		TextDo3[2].text = Progress.shop.CollKill3Car3.ToString();
		TextDo3[3].text = Progress.shop.CollKill4Car3.ToString();
		_btn1[0].onClick.AddListener(delegate
		{
			GetPart(1, 1);
		});
		_btn1[1].onClick.AddListener(delegate
		{
			GetPart(2, 1);
		});
		_btn1[2].onClick.AddListener(delegate
		{
			GetPart(3, 1);
		});
		_btn1[3].onClick.AddListener(delegate
		{
			GetPart(4, 1);
		});
		_btn2[0].onClick.AddListener(delegate
		{
			GetPart(1, 2);
		});
		_btn2[1].onClick.AddListener(delegate
		{
			GetPart(2, 2);
		});
		_btn2[2].onClick.AddListener(delegate
		{
			GetPart(3, 2);
		});
		_btn2[3].onClick.AddListener(delegate
		{
			GetPart(4, 2);
		});
		_btn3[0].onClick.AddListener(delegate
		{
			GetPart(1, 3);
		});
		_btn3[1].onClick.AddListener(delegate
		{
			GetPart(2, 3);
		});
		_btn3[2].onClick.AddListener(delegate
		{
			GetPart(3, 3);
		});
		_btn3[3].onClick.AddListener(delegate
		{
			GetPart(4, 3);
		});
		_Premiums.onClick.AddListener(PremiumClic);
		_Exit.onClick.AddListener(exitClic);
		StartCoroutine(hidePreloader());
	}

	private void OnDisable()
	{
		_btn1[0].onClick.RemoveAllListeners();
		_btn1[1].onClick.RemoveAllListeners();
		_btn1[2].onClick.RemoveAllListeners();
		_btn1[3].onClick.RemoveAllListeners();
		_btn2[0].onClick.RemoveAllListeners();
		_btn2[1].onClick.RemoveAllListeners();
		_btn2[2].onClick.RemoveAllListeners();
		_btn2[3].onClick.RemoveAllListeners();
		_btn3[0].onClick.RemoveAllListeners();
		_btn3[1].onClick.RemoveAllListeners();
		_btn3[2].onClick.RemoveAllListeners();
		_btn3[3].onClick.RemoveAllListeners();
		_Premiums.onClick.RemoveAllListeners();
		_Exit.onClick.RemoveAllListeners();
	}

	private IEnumerator hidePreloader()
	{
		yield return new WaitForSeconds(1.5f);
		PC.Hide();
	}

	private void PremiumClic()
	{
		Audio.Play("gui_button_02_sn");
		ShowBuyCanvasWindow(isCoins: true);
	}

	private void exitClic()
	{
		if (Progress.shop.activeCar == 11)
		{
			if (!Progress.shop.Cars[11].equipped)
			{
				Progress.shop.activeCar = 11;
			}
			else
			{
				Progress.shop.activeCar = 7;
			}
		}
		if (Progress.shop.activeCar == 7)
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
								if (Progress.shop.BossDeath2 && Progress.shop.Cars[2].equipped)
								{
									Progress.shop.activeCar = 2;
								}
								else if (Progress.shop.BossDeath1 && Progress.shop.Cars[1].equipped)
								{
									Progress.shop.activeCar = 1;
								}
								else
								{
									Progress.shop.activeCar = 0;
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
							if (Progress.shop.BossDeath2 && Progress.shop.Cars[2].equipped)
							{
								Progress.shop.activeCar = 2;
							}
							else if (Progress.shop.BossDeath1 && Progress.shop.Cars[1].equipped)
							{
								Progress.shop.activeCar = 1;
							}
							else
							{
								Progress.shop.activeCar = 0;
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
		Progress.shop.LoadPolicePedia = false;
		if (Progress.levels.InUndeground)
		{
			Game.LoadLevel("scene_underground_map_new");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}

	private void Chek()
	{
		if (Progress.shop.CollKill1Car >= ConfigForPolicePedia.instance.Car1.CollPart1 && Progress.shop.Get1partForPoliceCar)
		{
			BtnAnim1.SetBool(_isPartTaken, value: true);
			BtnAnim1.SetBool(_isComplete, value: true);
			PartCarAnim1.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill2Car >= ConfigForPolicePedia.instance.Car1.CollPart2 && Progress.shop.Get2partForPoliceCar)
		{
			BtnAnim2.SetBool(_isPartTaken, value: true);
			BtnAnim2.SetBool(_isComplete, value: true);
			PartCarAnim2.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill3Car >= ConfigForPolicePedia.instance.Car1.CollPart3 && Progress.shop.Get3partForPoliceCar)
		{
			BtnAnim3.SetBool(_isPartTaken, value: true);
			BtnAnim3.SetBool(_isComplete, value: true);
			PartCarAnim3.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill4Car >= ConfigForPolicePedia.instance.Car1.CollPart4 && Progress.shop.Get4partForPoliceCar)
		{
			BtnAnim4.SetBool(_isPartTaken, value: true);
			BtnAnim4.SetBool(_isComplete, value: true);
			PartCarAnim4.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill1Car >= ConfigForPolicePedia.instance.Car1.CollPart1 && !Progress.shop.Get1partForPoliceCar)
		{
			BtnAnim1.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill2Car >= ConfigForPolicePedia.instance.Car1.CollPart2 && !Progress.shop.Get2partForPoliceCar)
		{
			BtnAnim2.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill3Car >= ConfigForPolicePedia.instance.Car1.CollPart3 && !Progress.shop.Get3partForPoliceCar)
		{
			BtnAnim3.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill4Car >= ConfigForPolicePedia.instance.Car1.CollPart4 && !Progress.shop.Get4partForPoliceCar)
		{
			BtnAnim4.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
		{
			CarReal.SetActive(value: true);
			CarNotReal.SetActive(value: false);
		}
		if (Progress.shop.CollKill1Car2 >= ConfigForPolicePedia.instance.Car2.CollPart1 && Progress.shop.Get1partForPoliceCar2)
		{
			Btn2Anim1.SetBool(_isPartTaken, value: true);
			Btn2Anim1.SetBool(_isComplete, value: true);
			PartCar2Anim1.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill2Car2 >= ConfigForPolicePedia.instance.Car2.CollPart2 && Progress.shop.Get2partForPoliceCar2)
		{
			Btn2Anim2.SetBool(_isPartTaken, value: true);
			Btn2Anim2.SetBool(_isComplete, value: true);
			PartCar2Anim2.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill3Car2 >= ConfigForPolicePedia.instance.Car2.CollPart3 && Progress.shop.Get3partForPoliceCar2)
		{
			Btn2Anim3.SetBool(_isPartTaken, value: true);
			Btn2Anim3.SetBool(_isComplete, value: true);
			PartCar2Anim3.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill4Car2 >= ConfigForPolicePedia.instance.Car2.CollPart4 && Progress.shop.Get4partForPoliceCar2)
		{
			Btn2Anim4.SetBool(_isPartTaken, value: true);
			Btn2Anim4.SetBool(_isComplete, value: true);
			PartCar2Anim4.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill1Car2 >= ConfigForPolicePedia.instance.Car2.CollPart1 && !Progress.shop.Get1partForPoliceCar2)
		{
			Btn2Anim1.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill2Car2 >= ConfigForPolicePedia.instance.Car2.CollPart2 && !Progress.shop.Get2partForPoliceCar2)
		{
			Btn2Anim2.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill3Car2 >= ConfigForPolicePedia.instance.Car2.CollPart3 && !Progress.shop.Get3partForPoliceCar2)
		{
			Btn2Anim3.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill4Car2 >= ConfigForPolicePedia.instance.Car2.CollPart4 && !Progress.shop.Get4partForPoliceCar2)
		{
			Btn2Anim4.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
		{
			Car2Real.SetActive(value: true);
			Car2NotReal.SetActive(value: false);
		}
		if (Progress.shop.CollKill1Car3 >= ConfigForPolicePedia.instance.Car3.CollPart1 && Progress.shop.Get1partForPoliceCar3)
		{
			Btn3Anim1.SetBool(_isPartTaken, value: true);
			Btn3Anim1.SetBool(_isComplete, value: true);
			PartCar3Anim1.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill2Car3 >= ConfigForPolicePedia.instance.Car3.CollPart2 && Progress.shop.Get2partForPoliceCar3)
		{
			Btn3Anim2.SetBool(_isPartTaken, value: true);
			Btn3Anim2.SetBool(_isComplete, value: true);
			PartCar3Anim2.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill3Car3 >= ConfigForPolicePedia.instance.Car3.CollPart3 && Progress.shop.Get3partForPoliceCar3)
		{
			Btn3Anim3.SetBool(_isPartTaken, value: true);
			Btn3Anim3.SetBool(_isComplete, value: true);
			PartCar3Anim3.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill4Car3 >= ConfigForPolicePedia.instance.Car3.CollPart4 && Progress.shop.Get4partForPoliceCar3)
		{
			Btn3Anim4.SetBool(_isPartTaken, value: true);
			Btn3Anim4.SetBool(_isComplete, value: true);
			PartCar3Anim4.SetBool(_isPartTaken, value: true);
		}
		if (Progress.shop.CollKill1Car3 >= ConfigForPolicePedia.instance.Car3.CollPart1 && !Progress.shop.Get1partForPoliceCar3)
		{
			Btn3Anim1.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill2Car3 >= ConfigForPolicePedia.instance.Car3.CollPart2 && !Progress.shop.Get2partForPoliceCar3)
		{
			Btn3Anim2.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill3Car3 >= ConfigForPolicePedia.instance.Car3.CollPart3 && !Progress.shop.Get3partForPoliceCar3)
		{
			Btn3Anim3.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.CollKill4Car3 >= ConfigForPolicePedia.instance.Car3.CollPart4 && !Progress.shop.Get4partForPoliceCar3)
		{
			Btn3Anim4.SetBool(_isComplete, value: true);
		}
		if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
		{
			Car3Real.SetActive(value: true);
			Car3NotReal.SetActive(value: false);
		}
	}

	private IEnumerator spawnRealCar()
	{
		yield return new WaitForSeconds(1f);
		Audio.PlayAsync("policopedia2");
		Unlockparticl.SetActive(value: true);
		CarReal.SetActive(value: true);
		CarNotReal.SetActive(value: false);
		AnalyticsManager.LogEvent(EventCategoty.policopedia, "car_unlocked", "francopstein");
	}

	private IEnumerator spawnRealCar2()
	{
		yield return new WaitForSeconds(1f);
		Audio.PlayAsync("policopedia2");
		Unlockparticl2.SetActive(value: true);
		Car2Real.SetActive(value: true);
		Car2NotReal.SetActive(value: false);
		AnalyticsManager.LogEvent(EventCategoty.policopedia, "car_unlocked", "carocop");
	}

	private IEnumerator spawnRealCar3()
	{
		yield return new WaitForSeconds(1f);
		Audio.PlayAsync("policopedia2");
		Unlockparticl3.SetActive(value: true);
		Car3Real.SetActive(value: true);
		Car3NotReal.SetActive(value: false);
		AnalyticsManager.LogEvent(EventCategoty.policopedia, "car_unlocked", "tankominator");
	}

	private void GetPart(int part, int carNum)
	{
		Audio.PlayAsync("policopedia1");
		switch (carNum)
		{
		case 1:
			switch (part)
			{
			case 1:
				if (Progress.shop.CollKill1Car >= ConfigForPolicePedia.instance.Car1.CollPart1 && !Progress.shop.Get1partForPoliceCar)
				{
					BtnAnim1.SetBool(_isPartTaken, value: true);
					Progress.shop.Get1partForPoliceCar = true;
					PartCarAnim1.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
					{
						StartCoroutine(spawnRealCar());
					}
				}
				break;
			case 2:
				if (Progress.shop.CollKill2Car >= ConfigForPolicePedia.instance.Car1.CollPart2 && !Progress.shop.Get2partForPoliceCar)
				{
					BtnAnim2.SetBool(_isPartTaken, value: true);
					Progress.shop.Get2partForPoliceCar = true;
					PartCarAnim2.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
					{
						StartCoroutine(spawnRealCar());
					}
				}
				break;
			case 3:
				if (Progress.shop.CollKill3Car >= ConfigForPolicePedia.instance.Car1.CollPart3 && !Progress.shop.Get3partForPoliceCar)
				{
					BtnAnim3.SetBool(_isPartTaken, value: true);
					Progress.shop.Get3partForPoliceCar = true;
					PartCarAnim3.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
					{
						StartCoroutine(spawnRealCar());
					}
				}
				break;
			case 4:
				if (Progress.shop.CollKill4Car >= ConfigForPolicePedia.instance.Car1.CollPart4 && !Progress.shop.Get4partForPoliceCar)
				{
					BtnAnim4.SetBool(_isPartTaken, value: true);
					Progress.shop.Get4partForPoliceCar = true;
					PartCarAnim4.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar && Progress.shop.Get2partForPoliceCar && Progress.shop.Get3partForPoliceCar && Progress.shop.Get4partForPoliceCar)
					{
						StartCoroutine(spawnRealCar());
					}
				}
				break;
			}
			break;
		case 2:
			switch (part)
			{
			case 1:
				if (Progress.shop.CollKill1Car2 >= ConfigForPolicePedia.instance.Car2.CollPart1 && !Progress.shop.Get1partForPoliceCar2)
				{
					Btn2Anim1.SetBool(_isPartTaken, value: true);
					Progress.shop.Get1partForPoliceCar2 = true;
					PartCar2Anim1.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
					{
						StartCoroutine(spawnRealCar2());
					}
				}
				break;
			case 2:
				if (Progress.shop.CollKill2Car2 >= ConfigForPolicePedia.instance.Car2.CollPart2 && !Progress.shop.Get2partForPoliceCar2)
				{
					Btn2Anim2.SetBool(_isPartTaken, value: true);
					Progress.shop.Get2partForPoliceCar2 = true;
					PartCar2Anim2.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
					{
						StartCoroutine(spawnRealCar2());
					}
				}
				break;
			case 3:
				if (Progress.shop.CollKill3Car2 >= ConfigForPolicePedia.instance.Car2.CollPart3 && !Progress.shop.Get3partForPoliceCar2)
				{
					Btn2Anim3.SetBool(_isPartTaken, value: true);
					Progress.shop.Get3partForPoliceCar2 = true;
					PartCar2Anim3.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
					{
						StartCoroutine(spawnRealCar2());
					}
				}
				break;
			case 4:
				if (Progress.shop.CollKill4Car2 >= ConfigForPolicePedia.instance.Car2.CollPart4 && !Progress.shop.Get4partForPoliceCar2)
				{
					Btn2Anim4.SetBool(_isPartTaken, value: true);
					Progress.shop.Get4partForPoliceCar2 = true;
					PartCar2Anim4.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar2 && Progress.shop.Get2partForPoliceCar2 && Progress.shop.Get3partForPoliceCar2 && Progress.shop.Get4partForPoliceCar2)
					{
						StartCoroutine(spawnRealCar2());
					}
				}
				break;
			}
			break;
		case 3:
			switch (part)
			{
			case 1:
				if (Progress.shop.CollKill1Car3 >= ConfigForPolicePedia.instance.Car3.CollPart1 && !Progress.shop.Get1partForPoliceCar3)
				{
					Btn3Anim1.SetBool(_isPartTaken, value: true);
					Progress.shop.Get1partForPoliceCar3 = true;
					PartCar3Anim1.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
					{
						StartCoroutine(spawnRealCar3());
					}
				}
				break;
			case 2:
				if (Progress.shop.CollKill2Car3 >= ConfigForPolicePedia.instance.Car3.CollPart2 && !Progress.shop.Get2partForPoliceCar3)
				{
					Btn3Anim2.SetBool(_isPartTaken, value: true);
					Progress.shop.Get2partForPoliceCar3 = true;
					PartCar3Anim2.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
					{
						StartCoroutine(spawnRealCar3());
					}
				}
				break;
			case 3:
				if (Progress.shop.CollKill3Car3 >= ConfigForPolicePedia.instance.Car3.CollPart3 && !Progress.shop.Get3partForPoliceCar3)
				{
					Btn3Anim3.SetBool(_isPartTaken, value: true);
					Progress.shop.Get3partForPoliceCar3 = true;
					PartCar3Anim3.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
					{
						StartCoroutine(spawnRealCar3());
					}
				}
				break;
			case 4:
				if (Progress.shop.CollKill4Car3 >= ConfigForPolicePedia.instance.Car3.CollPart4 && !Progress.shop.Get4partForPoliceCar3)
				{
					Btn3Anim4.SetBool(_isPartTaken, value: true);
					Progress.shop.Get4partForPoliceCar3 = true;
					PartCar3Anim4.SetBool(_isPartTaken, value: true);
					if (Progress.shop.Get1partForPoliceCar3 && Progress.shop.Get2partForPoliceCar3 && Progress.shop.Get3partForPoliceCar3 && Progress.shop.Get4partForPoliceCar3)
					{
						StartCoroutine(spawnRealCar3());
					}
				}
				break;
			}
			break;
		}
	}

	private void Update()
	{
		if (ForCheak != SFP.GetMinButtonNum() + 1)
		{
			StartCoroutine(OffAllBtn(SFP.GetMinButtonNum() + 1));
		}
		ForCheak = SFP.GetMinButtonNum() + 1;
		Inf.SetActive(Progress.gameEnergy.isInfinite);
		fuel.gameObject.SetActive(!Progress.gameEnergy.isInfinite);
		coins.count = Progress.shop.currency.ToString();
		fuel.count = Progress.gameEnergy.energy.ToString();
	}

	private IEnumerator OffAllBtn(int car)
	{
		switch (car)
		{
		case 1:
			Btn2Anim1.SetBool(_isON, value: false);
			Btn3Anim1.SetBool(_isON, value: false);
			BtnAnim1.SetBool(_isON, value: true);
			yield return new WaitForSeconds(0.1f);
			BtnAnim2.SetBool(_isON, value: true);
			Btn2Anim2.SetBool(_isON, value: false);
			Btn3Anim2.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			BtnAnim3.SetBool(_isON, value: true);
			Btn2Anim3.SetBool(_isON, value: false);
			Btn3Anim3.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			BtnAnim4.SetBool(_isON, value: true);
			Btn2Anim4.SetBool(_isON, value: false);
			Btn3Anim4.SetBool(_isON, value: false);
			break;
		case 2:
			BtnAnim1.SetBool(_isON, value: false);
			Btn3Anim1.SetBool(_isON, value: false);
			Btn2Anim1.SetBool(_isON, value: true);
			yield return new WaitForSeconds(0.1f);
			Btn2Anim2.SetBool(_isON, value: true);
			Btn3Anim2.SetBool(_isON, value: false);
			BtnAnim2.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			BtnAnim3.SetBool(_isON, value: false);
			Btn3Anim3.SetBool(_isON, value: false);
			Btn2Anim3.SetBool(_isON, value: true);
			yield return new WaitForSeconds(0.1f);
			BtnAnim4.SetBool(_isON, value: false);
			Btn3Anim4.SetBool(_isON, value: false);
			Btn2Anim4.SetBool(_isON, value: true);
			break;
		case 3:
			BtnAnim1.SetBool(_isON, value: false);
			Btn2Anim1.SetBool(_isON, value: false);
			Btn3Anim1.SetBool(_isON, value: true);
			yield return new WaitForSeconds(0.1f);
			Btn2Anim2.SetBool(_isON, value: false);
			Btn3Anim2.SetBool(_isON, value: true);
			BtnAnim2.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			BtnAnim3.SetBool(_isON, value: false);
			Btn2Anim3.SetBool(_isON, value: false);
			Btn3Anim3.SetBool(_isON, value: true);
			yield return new WaitForSeconds(0.1f);
			BtnAnim3.SetBool(_isON, value: false);
			Btn2Anim4.SetBool(_isON, value: false);
			Btn3Anim4.SetBool(_isON, value: true);
			break;
		default:
			Btn2Anim1.SetBool(_isON, value: false);
			BtnAnim1.SetBool(_isON, value: false);
			Btn3Anim1.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			Btn2Anim2.SetBool(_isON, value: false);
			BtnAnim2.SetBool(_isON, value: false);
			Btn3Anim2.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			Btn2Anim3.SetBool(_isON, value: false);
			BtnAnim3.SetBool(_isON, value: false);
			Btn3Anim3.SetBool(_isON, value: false);
			yield return new WaitForSeconds(0.1f);
			Btn2Anim4.SetBool(_isON, value: false);
			BtnAnim4.SetBool(_isON, value: false);
			Btn3Anim4.SetBool(_isON, value: false);
			break;
		}
	}

	public void ShowBuyCanvasWindow(bool isCoins = false)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false)
	{
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		ShowGetCoinsAndFuel();
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	private void ShowGetCoinsAndFuel()
	{
		UnityEngine.Debug.Log("Minus currency !!!!!");
		_shopWindowModel.gameObject.SetActive(value: true);
	}
}

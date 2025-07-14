using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardAnimator : MonoBehaviour
{
	public List<GameObject> imgWheel8;

	public List<GameObject> imgWheel10;

	public List<GameObject> imgWheel12;

	public CounterController amount1;

	public CounterController amount2;

	public CounterController amount3;

	public GameObject percent1;

	public GameObject percent2;

	public GameObject percent3;

	public GameObject recalc1;

	public GameObject recalc2;

	public GameObject recalc3;

	public Animator animator_Res;

	public FortuneNEw FN;

	[Header("Animation and Animator")]
	public GameObject anim1;

	public GameObject anim2;

	public GameObject anim3;

	public GameObject animator1;

	public GameObject animator2;

	public GameObject animator3;

	private int _setUp_binary = Animator.StringToHash("setUp_binary");

	private int _isCombined = Animator.StringToHash("isCombined");

	public void Init(int count, SectorFortuneNew.SectorType win1, SectorFortuneNew.SectorType win2, SectorFortuneNew.SectorType win3, float amount1, CounterController text1, float amount2, CounterController text2, float amount3, CounterController text3, GameObject PercentGO1, GameObject PercentGO2, GameObject PercentGO3)
	{
		animator_Res.SetInteger(_setUp_binary, count);
		if (count > 10)
		{
			StartCoroutine(ForNullElements());
		}
		StartCoroutine(Recalculate(win1, win2, win3, amount1, text1, amount2, text2, amount3, text3, PercentGO1, PercentGO2, PercentGO3));
	}

	private IEnumerator ForNullElements()
	{
		yield return new WaitForSeconds(3.4f);
		Animator q = animator1.GetComponent<Animator>();
		Animator w = animator2.GetComponent<Animator>();
		Animator e = animator3.GetComponent<Animator>();
		q.SetBool(_isCombined, value: true);
		w.SetBool(_isCombined, value: true);
		e.SetBool(_isCombined, value: true);
	}

	public void ChangeImage(SectorFortuneNew.SectorType win, List<GameObject> img, float amount, CounterController text, GameObject PercentGO)
	{
		foreach (GameObject item in img)
		{
			if (item.name.Contains("armor") && win == SectorFortuneNew.SectorType.PersentHealth)
			{
				text.count = amount.ToString();
				item.SetActive(value: true);
			}
			else if (item.name.Contains("turbo") && win == SectorFortuneNew.SectorType.PersentTurbo)
			{
				text.count = amount.ToString();
				item.SetActive(value: true);
			}
			else if (item.name.Contains("damage") && win == SectorFortuneNew.SectorType.PersentDamage)
			{
				text.count = amount.ToString();
				item.SetActive(value: true);
			}
			else if (item.name.Contains("income") && win == SectorFortuneNew.SectorType.PersentRubins)
			{
				text.count = amount.ToString();
				item.SetActive(value: true);
			}
			else if (item.name.Contains("bomb") && win == SectorFortuneNew.SectorType.AddOneBomb)
			{
				text.count = amount.ToString();
				item.SetActive(value: true);
				PercentGO.SetActive(value: false);
			}
			else if (item.name.Contains("none") && win == SectorFortuneNew.SectorType.None)
			{
				text.gameObject.SetActive(value: false);
				item.SetActive(value: true);
			}
			else
			{
				item.SetActive(value: false);
			}
		}
	}

	public IEnumerator Recalculate(SectorFortuneNew.SectorType win1, SectorFortuneNew.SectorType win2, SectorFortuneNew.SectorType win3, float amount1, CounterController text1, float amount2, CounterController text2, float amount3, CounterController text3, GameObject PercentGO1, GameObject PercentGO2, GameObject PercentGO3)
	{
		int tempInt = 0;
		while (!recalc1.activeSelf && !recalc2.activeSelf && !recalc3.activeSelf)
		{
			tempInt++;
			yield return 0;
			if (tempInt > 300)
			{
				yield break;
			}
		}
		if (recalc1.activeSelf)
		{
			if (win3 == win1 && win2 == win1)
			{
				float temp9 = amount2 + amount3 + amount1;
				yield return new WaitForSeconds(1f);
				if (win2 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text3.count = temp9.ToString();
					text2.count = temp9.ToString();
					text1.count = temp9.ToString();
				}
				else
				{
					text3.count = temp9.ToString();
					text2.count = temp9.ToString();
					text1.count = temp9.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO2.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win2Amount = temp9;
				FN.win3Amount = temp9;
				FN.win1Amount = temp9;
			}
			else if (win1 == win2)
			{
				float temp8 = amount1 + amount2;
				yield return new WaitForSeconds(1f);
				if (win1 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text1.count = temp8.ToString();
					text2.count = temp8.ToString();
				}
				else
				{
					text1.count = temp8.ToString();
					text2.count = temp8.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO2.SetActive(value: false);
				}
				FN.win1Amount = temp8;
				FN.win2Amount = temp8;
			}
			else if (win1 == win3)
			{
				float temp7 = amount1 + amount3;
				yield return new WaitForSeconds(1f);
				if (win1 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text1.count = temp7.ToString();
					text3.count = temp7.ToString();
				}
				else
				{
					text1.count = temp7.ToString();
					text3.count = temp7.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win1Amount = temp7;
				FN.win3Amount = temp7;
			}
		}
		else if (recalc2.activeSelf)
		{
			if (win3 == win2 && win2 == win1)
			{
				float temp6 = amount2 + amount3 + amount1;
				yield return new WaitForSeconds(1f);
				if (win2 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text3.count = temp6.ToString();
					text2.count = temp6.ToString();
					text1.count = temp6.ToString();
				}
				else
				{
					text3.count = temp6.ToString();
					text2.count = temp6.ToString();
					text1.count = temp6.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO2.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win2Amount = temp6;
				FN.win3Amount = temp6;
				FN.win1Amount = temp6;
			}
			else if (win1 == win2)
			{
				float temp5 = amount1 + amount2;
				yield return new WaitForSeconds(1f);
				if (win2 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text1.count = temp5.ToString();
					text2.count = temp5.ToString();
				}
				else
				{
					text1.count = temp5.ToString();
					text2.count = temp5.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO2.SetActive(value: false);
				}
				FN.win1Amount = temp5;
				FN.win2Amount = temp5;
			}
			else if (win2 == win3)
			{
				float temp4 = amount2 + amount3;
				yield return new WaitForSeconds(1f);
				if (win2 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text3.count = temp4.ToString();
					text2.count = temp4.ToString();
				}
				else
				{
					text3.count = temp4.ToString();
					text2.count = temp4.ToString();
					PercentGO2.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win2Amount = temp4;
				FN.win3Amount = temp4;
			}
		}
		else
		{
			if (!recalc3.activeSelf)
			{
				yield break;
			}
			if (win3 == win2 && win3 == win1)
			{
				float temp3 = amount2 + amount3 + amount1;
				yield return new WaitForSeconds(1f);
				if (win3 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text3.count = temp3.ToString();
					text2.count = temp3.ToString();
					text1.count = temp3.ToString();
				}
				else
				{
					text3.count = temp3.ToString();
					text2.count = temp3.ToString();
					text1.count = temp3.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO2.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win2Amount = temp3;
				FN.win3Amount = temp3;
				FN.win1Amount = temp3;
			}
			else if (win3 == win2)
			{
				float temp2 = amount3 + amount2;
				yield return new WaitForSeconds(1f);
				if (win3 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text3.count = temp2.ToString();
					text2.count = temp2.ToString();
				}
				else
				{
					text3.count = temp2.ToString();
					text2.count = temp2.ToString();
					PercentGO2.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win2Amount = temp2;
				FN.win3Amount = temp2;
			}
			else if (win1 == win3)
			{
				float temp = amount1 + amount3;
				yield return new WaitForSeconds(1f);
				if (win3 != SectorFortuneNew.SectorType.AddOneBomb)
				{
					text1.count = temp.ToString();
					text3.count = temp.ToString();
				}
				else
				{
					text1.count = temp.ToString();
					text3.count = temp.ToString();
					PercentGO1.SetActive(value: false);
					PercentGO3.SetActive(value: false);
				}
				FN.win1Amount = temp;
				FN.win3Amount = temp;
			}
		}
	}
}

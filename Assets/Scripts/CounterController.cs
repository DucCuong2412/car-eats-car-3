using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour
{
	[Serializable]
	public class lists
	{
		public GameObject _obj;

		public Animator _change;

		public Animator _new;

		public Animator _old;
	}

	public bool for_map;

	public GameObject tochka;

	public bool Test;

	public string count;

	private string Do = string.Empty;

	private string Posle = string.Empty;

	public List<lists> _objAndAnimatorDo;

	public List<lists> _objAndAnimatorPosle;

	public bool StopCheng;

	public int CountAfterDot = -1;

	private int hash_isChanged = Animator.StringToHash("isChanged");

	private static string str_digit = "digit";

	private static string str_empty = string.Empty;

	private static char str_DOT = '.';

	private void Update()
	{
		if (StopCheng)
		{
			return;
		}
		rozbivka();
		int length = Do.Length;
		int num = _objAndAnimatorDo.Count;
		for (int i = 0; i < length; i++)
		{
			if (num > i)
			{
				_objAndAnimatorDo[i]._obj.SetActive(value: true);
			}
		}
		for (int j = length; j < num; j++)
		{
			_objAndAnimatorDo[j]._obj.SetActive(value: false);
		}
		if (tochka != null)
		{
			if (Posle.Length < 1)
			{
				tochka.SetActive(value: false);
			}
			else
			{
				tochka.SetActive(value: true);
			}
		}
		int num2 = _objAndAnimatorPosle.Count;
		int length2 = Posle.Length;
		if (num2 > 0)
		{
			for (int k = 0; k < length2; k++)
			{
				if (num2 > k)
				{
					_objAndAnimatorPosle[k]._obj.SetActive(value: true);
				}
			}
			for (int l = length2; l < num2; l++)
			{
				_objAndAnimatorPosle[l]._obj.SetActive(value: false);
			}
		}
		forUp();
		if (Test)
		{
			test();
		}
	}

	private int Change(int i, string coun)
	{
		if (i < coun.Length)
		{
			char c = coun[i];
			return (int)char.GetNumericValue(c);
		}
		return 0;
	}

	private void forUp()
	{
		StartCoroutine(up());
	}

	private void OnEnable()
	{
		if (!for_map)
		{
			return;
		}
		int length = Do.Length;
		for (int i = 0; i < length; i++)
		{
			lists lists = _objAndAnimatorDo[i];
			int num = Change(i, Do);
			if (lists._new.GetInteger(str_digit) != num)
			{
				lists._new.SetInteger(str_digit, num);
				lists._old.SetInteger(str_digit, num);
			}
		}
	}

	private IEnumerator up()
	{
		int count5 = Do.Length;
		for (int j = 0; j < count5; j++)
		{
			int count2 = _objAndAnimatorDo.Count;
			if (count2 <= j)
			{
				continue;
			}
			lists item = _objAndAnimatorDo[j];
			int change = Change(j, Do);
			if (item._new.GetInteger(str_digit) != change)
			{
				item._new.SetInteger(str_digit, change);
				if (!for_map)
				{
					item._change.SetTrigger(hash_isChanged);
					yield return new WaitForSeconds(0.05f);
				}
				item._old.SetInteger(str_digit, change);
			}
		}
		count5 = Posle.Length;
		for (int i = 0; i < count5; i++)
		{
			int count3 = _objAndAnimatorPosle.Count;
			if (count3 <= i)
			{
				continue;
			}
			lists item2 = _objAndAnimatorPosle[i];
			int change2 = Change(i, Posle);
			if (item2._new.GetInteger(str_digit) != change2)
			{
				item2._new.SetInteger(str_digit, change2);
				if (!for_map)
				{
					item2._change.SetTrigger(hash_isChanged);
					yield return new WaitForSeconds(0.05f);
				}
				item2._old.SetInteger(str_digit, change2);
			}
		}
	}

	private void test()
	{
		StartCoroutine(Tests());
	}

	private IEnumerator Tests()
	{
		float i = 0f;
		while (true)
		{
			i += Time.deltaTime;
			count = i.ToString();
			yield return null;
		}
	}

	private void rozbivka()
	{
		int num = count.IndexOf(str_DOT);
		if (num > -1)
		{
			Do = count.Substring(0, num + 1);
			Posle = count.Substring(num + 1);
		}
		else
		{
			Do = count;
			Posle = str_empty;
		}
	}
}

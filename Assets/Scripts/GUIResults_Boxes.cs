using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIResults_Boxes : MonoBehaviour
{
	public GameObject Container;

	public Animator GlobAnimator;

	public Animator ChooseTextAnim;

	public List<GUIResults_Boxes_Box> Boxes;

	public bool BoxesAnimHiden;

	private int is_ON = Animator.StringToHash("is_ON");

	private int is_mix = Animator.StringToHash("is_mix");

	private int itm_count = Animator.StringToHash("itm_count");

	private int _boxesNum;

	private List<ResultBoxesConfig.Revard> _revards = new List<ResultBoxesConfig.Revard>();

	private Coroutine ShowCorut;

	private bool _boxPressed;

	private int _boxPressedNum = -1;

	private bool _chusenBox;

	private void Start()
	{
		if (Progress.levels.ResultBoxRev_Undeground1 == null || Progress.levels.ResultBoxRev_Undeground1.Count == 0)
		{
			Progress.levels.ResultBoxRev_Undeground1 = ResultBoxesConfig.instance.LevelsBox;
		}
		int num = ResultBoxesConfig.instance.LevelsBox.Count - 13;
		if (Progress.levels.ResultBoxRev_Undeground1.Count == 13)
		{
			for (int i = 0; i < num; i++)
			{
				Progress.levels.ResultBoxRev_Undeground1.Add(ResultBoxesConfig.instance.LevelsBox[i + 13]);
			}
		}
	}

	public void Show()
	{
		_chusenBox = false;
		_boxPressed = false;
		BoxesAnimHiden = false;
		GlobAnimator.SetBool(is_ON, value: false);
		GlobAnimator.SetBool(is_mix, value: false);
		GlobAnimator.SetInteger(itm_count, 0);
		if (ShowCorut != null)
		{
			StopCoroutine(ShowCorut);
			ShowCorut = null;
		}
		int count = Boxes.Count;
		for (int i = 0; i < count; i++)
		{
			Boxes[i].RefreshFunc();
		}
		if (Progress.shop.bossLevel)
		{
			if (Progress.levels.active_pack == 1)
			{
				_revards = Progress.levels.ResultBoxRev_Undeground1[12].Revards;
			}
			else if (Progress.levels.active_pack == 2)
			{
				_revards = Progress.levels.ResultBoxRev_Undeground1[25].Revards;
			}
		}
		else if (Progress.levels.active_pack == 1)
		{
			_revards = Progress.levels.ResultBoxRev_Undeground1[Progress.levels.active_level_last_openned_under - 1].Revards;
		}
		else if (Progress.levels.active_pack == 2)
		{
			_revards = Progress.levels.ResultBoxRev_Undeground1[13 + Progress.levels.active_level_last_openned_under - 1].Revards;
		}
		_boxesNum = _revards.Count;
		count = Boxes.Count;
		for (int j = 0; j < count; j++)
		{
			Boxes[j].BoxNum = j;
		}
		count = _revards.Count;
		if (count <= 0)
		{
			BoxesAnimHiden = true;
			return;
		}
		BoxesAnimHiden = false;
		for (int k = 0; k < count; k++)
		{
			Boxes[k].SetContent(_revards[k]);
		}
		ShowCorut = StartCoroutine(ShowScenary());
	}

	private IEnumerator ShowScenary()
	{
		_boxPressed = false;
		GlobAnimator.SetBool(is_ON, value: true);
		GlobAnimator.SetInteger(itm_count, _boxesNum);
		float t11 = 2f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		if (_revards.Count == 1)
		{
			PressBox(0, _revards[0]);
			t11 = 5f;
			while (t11 > 0f)
			{
				t11 -= Time.deltaTime;
				yield return null;
			}
			GlobAnimator.SetBool(is_ON, value: false);
			t11 = 1f;
			while (t11 > 0f)
			{
				t11 -= Time.deltaTime;
				yield return null;
			}
			BoxesAnimHiden = true;
			ShowCorut = null;
			yield break;
		}
		for (int t8 = 0; t8 < _boxesNum; t8++)
		{
			Boxes[t8].SetHiglighted();
			t11 = 0.5f;
			while (t11 > 0f)
			{
				t11 -= Time.deltaTime;
				yield return null;
			}
		}
		t11 = 4f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		GlobAnimator.SetBool(is_mix, value: true);
		t11 = 0.2f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		GlobAnimator.SetBool(is_mix, value: false);
		int count4 = _revards.Count;
		for (int i = 0; i < count4; i++)
		{
			ResultBoxesConfig.Revard value = _revards[i];
			int index = UnityEngine.Random.Range(0, count4);
			_revards[i] = _revards[index];
			_revards[index] = value;
		}
		count4 = _revards.Count;
		for (int j = 0; j < count4; j++)
		{
			Boxes[j].SetContent(_revards[j]);
		}
		t11 = 3f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		ChooseTextAnim.SetBool(is_ON, value: true);
		count4 = Boxes.Count;
		for (int k = 0; k < count4; k++)
		{
			Boxes[k].SetReady();
		}
		while (!_boxPressed)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		t11 = 4f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		count4 = Boxes.Count;
		for (int l = 0; l < count4; l++)
		{
			if (_boxPressedNum != l)
			{
				Boxes[l].SetEnd();
			}
		}
		t11 = 3f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		GlobAnimator.SetBool(is_ON, value: false);
		t11 = 1f;
		while (t11 > 0f)
		{
			t11 -= Time.deltaTime;
			yield return null;
		}
		BoxesAnimHiden = true;
		ShowCorut = null;
	}

	public void PressBox(int num, ResultBoxesConfig.Revard revard)
	{
		if (_chusenBox)
		{
			return;
		}
		_chusenBox = true;
		Audio.PlayAsync("actor_gadget_shield_sn");
		ChooseTextAnim.SetBool(is_ON, value: false);
		_boxPressed = true;
		_boxPressedNum = num;
		Boxes[num].SetChoosen();
		int count = Boxes.Count;
		for (int i = 0; i < count; i++)
		{
			Boxes[i].SetRemindOff();
		}
		int index = 0;
		if (Progress.shop.bossLevel)
		{
			if (Progress.levels.active_pack == 1)
			{
				index = 12;
			}
			else if (Progress.levels.active_pack == 2)
			{
				index = 25;
			}
		}
		else if (Progress.levels.active_pack == 1)
		{
			index = Progress.levels.active_level_last_openned_under - 1;
		}
		else if (Progress.levels.active_pack == 2)
		{
			index = 13 + Progress.levels.active_level_last_openned_under - 1;
		}
		switch (revard)
		{
		case ResultBoxesConfig.Revard.Yellow:
			Progress.shop.Incubator_CountRuby3++;
			break;
		case ResultBoxesConfig.Revard.Green:
			Progress.shop.Incubator_CountRuby4++;
			break;
		case ResultBoxesConfig.Revard.Blue:
			Progress.shop.Incubator_CountRuby2++;
			break;
		case ResultBoxesConfig.Revard.White:
			Progress.shop.Incubator_CountRuby1++;
			break;
		case ResultBoxesConfig.Revard.Egg_1:
			Progress.shop.Incubator_Eggs[2] = true;
			break;
		case ResultBoxesConfig.Revard.Egg_2:
			Progress.shop.Incubator_Eggs[1] = true;
			break;
		case ResultBoxesConfig.Revard.Egg_5:
			Progress.shop.Incubator_Eggs[4] = true;
			break;
		case ResultBoxesConfig.Revard.Egg_6:
			Progress.shop.Incubator_Eggs[5] = true;
			break;
		}
		count = Progress.levels.ResultBoxRev_Undeground1[index].Revards.Count;
		for (int j = 0; j < count; j++)
		{
			if (Progress.levels.ResultBoxRev_Undeground1[index].Revards[j] == revard)
			{
				Progress.levels.ResultBoxRev_Undeground1[index].Revards.RemoveAt(j);
				break;
			}
		}
		if (Progress.levels.ResultBoxRev_Undeground1_Adds != null && Progress.levels.ResultBoxRev_Undeground1_Adds.Count != 0 && Progress.levels.ResultBoxRev_Undeground1_Adds[index])
		{
			Progress.levels.ResultBoxRev_Undeground1_Adds[index] = false;
			Progress.levels.ResultBoxRev_Undeground1[index].Revards.Clear();
			Progress.levels.ResultBoxRev_Undeground1_LastGetTime = DateTime.UtcNow;
		}
	}

	private void OnDisable()
	{
		if (ShowCorut != null)
		{
			StopCoroutine(ShowCorut);
			ShowCorut = null;
		}
	}
}

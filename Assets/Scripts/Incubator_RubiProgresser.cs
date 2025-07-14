using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incubator_RubiProgresser : MonoBehaviour
{
	public Animator StateAnimator;

	public Animator GemAnimator;

	public List<GameObject> WhiteStones;

	public List<GameObject> BlueStones;

	public List<GameObject> YellowStones;

	public List<GameObject> GreenStones;

	private int isUnlocked = Animator.StringToHash("isUnlocked");

	private int isActive = Animator.StringToHash("isActive");

	private int isComplete = Animator.StringToHash("isComplete");

	private int isArmor = Animator.StringToHash("isArmor");

	private int isTurbo = Animator.StringToHash("isTurbo");

	private int isWeapon = Animator.StringToHash("isWeapon");

	private int isEngine = Animator.StringToHash("isEngine");

	private int _rybyNum = 1;

	public void SetRuby(int num)
	{
		OffAll();
		_rybyNum = num;
		int num2 = 0;
		switch (num)
		{
		case 1:
			num2 = WhiteStones.Count;
			for (int l = 0; l < num2; l++)
			{
				WhiteStones[l].SetActive(value: true);
			}
			break;
		case 2:
			num2 = BlueStones.Count;
			for (int j = 0; j < num2; j++)
			{
				BlueStones[j].SetActive(value: true);
			}
			break;
		case 3:
			num2 = YellowStones.Count;
			for (int k = 0; k < num2; k++)
			{
				YellowStones[k].SetActive(value: true);
			}
			break;
		case 4:
			num2 = GreenStones.Count;
			for (int i = 0; i < num2; i++)
			{
				GreenStones[i].SetActive(value: true);
			}
			break;
		}
		tryCheng();
	}

	private void OffAll()
	{
		int count = WhiteStones.Count;
		for (int i = 0; i < count; i++)
		{
			WhiteStones[i].SetActive(value: false);
		}
		count = BlueStones.Count;
		for (int j = 0; j < count; j++)
		{
			BlueStones[j].SetActive(value: false);
		}
		count = YellowStones.Count;
		for (int k = 0; k < count; k++)
		{
			YellowStones[k].SetActive(value: false);
		}
		count = GreenStones.Count;
		for (int l = 0; l < count; l++)
		{
			GreenStones[l].SetActive(value: false);
		}
	}

	public void UnlockAnim()
	{
		StateAnimator.SetBool(isUnlocked, value: true);
		tryCheng();
	}

	public void ActiveAnim()
	{
		StateAnimator.SetBool(isActive, value: true);
		StartCoroutine(delay());
	}

	public void CompleteAnim()
	{
		StateAnimator.SetBool(isComplete, value: true);
		tryCheng();
	}

	private IEnumerator delay()
	{
		float t = 1f;
		while (t > 0f)
		{
			tryCheng();
			t -= Time.deltaTime;
			yield return null;
		}
	}

	private void tryCheng()
	{
		GemAnimator.SetBool(isArmor, value: false);
		GemAnimator.SetBool(isTurbo, value: false);
		GemAnimator.SetBool(isWeapon, value: false);
		GemAnimator.SetBool(isEngine, value: false);
		switch (_rybyNum)
		{
		case 1:
			GemAnimator.SetBool(isArmor, value: true);
			break;
		case 2:
			GemAnimator.SetBool(isTurbo, value: true);
			break;
		case 3:
			GemAnimator.SetBool(isWeapon, value: true);
			break;
		case 4:
			GemAnimator.SetBool(isEngine, value: true);
			break;
		}
	}
}

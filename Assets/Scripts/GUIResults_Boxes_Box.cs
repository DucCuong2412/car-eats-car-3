using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIResults_Boxes_Box : MonoBehaviour
{
	public Animator Anim;

	public List<GameObject> ContantList;

	public Button Btn;

	[HideInInspector]
	public int BoxNum;

	private ResultBoxesConfig.Revard _revard;

	private bool _remindet = true;

	private GUIResults_Boxes _controller;

	private int remind = Animator.StringToHash("remind");

	private int is_higlighted = Animator.StringToHash("is_higlighted");

	private int is_ready = Animator.StringToHash("is_ready");

	private int is_choosen = Animator.StringToHash("is_choosen");

	private int is_end = Animator.StringToHash("is_end");

	private void Start()
	{
		_controller = UnityEngine.Object.FindObjectOfType<GUIResults_Boxes>();
	}

	private void OnEnable()
	{
		RefreshFunc();
	}

	public void RefreshFunc()
	{
		Btn.onClick.RemoveAllListeners();
		Btn.onClick.AddListener(Press);
		_remindet = true;
		Anim.SetBool(is_higlighted, value: false);
		Anim.SetBool(is_ready, value: false);
		Anim.SetBool(is_choosen, value: false);
		Anim.SetBool(is_end, value: false);
	}

	public void SetContent(ResultBoxesConfig.Revard revard)
	{
		_revard = revard;
		int count = ContantList.Count;
		for (int i = 0; i < count; i++)
		{
			ContantList[i].SetActive(value: false);
		}
		ContantList[(int)(revard - 1)].SetActive(value: true);
	}

	private void Press()
	{
		_controller.PressBox(BoxNum, _revard);
	}

	public void SetRemindOff()
	{
		_remindet = false;
	}

	public void SetHiglighted()
	{
		if (base.gameObject.activeSelf)
		{
			Anim.SetBool(is_higlighted, value: true);
		}
	}

	public void SetReady()
	{
		if (base.gameObject.activeSelf)
		{
			Anim.SetBool(is_ready, value: true);
			StartCoroutine(DelayRemind());
		}
	}

	private IEnumerator DelayRemind()
	{
		int t = UnityEngine.Random.Range(50, 400);
		while (t > 0)
		{
			t--;
			yield return null;
		}
		if (_remindet)
		{
			Anim.SetTrigger(remind);
			StartCoroutine(DelayRemind());
		}
	}

	public void SetChoosen()
	{
		if (base.gameObject.activeSelf)
		{
			Anim.SetBool(is_choosen, value: true);
			_remindet = false;
		}
	}

	public void SetEnd()
	{
		if (base.gameObject.activeSelf)
		{
			Anim.SetBool(is_end, value: true);
		}
	}

	private void OnDisable()
	{
		Btn.onClick.RemoveAllListeners();
	}
}

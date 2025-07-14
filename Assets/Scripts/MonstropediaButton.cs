using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonstropediaButton : MonoBehaviour
{
	public Button But;

	public Animator Anim;

	public List<GameObject> Icos = new List<GameObject>();

	public ControllerMonstropedia controller;

	private int index;

	private bool lockBut = true;

	private void OnEnable()
	{
		But.onClick.AddListener(press);
	}

	private void OnDisable()
	{
		But.onClick.RemoveAllListeners();
	}

	private void press()
	{
		controller.PressBut(index);
	}

	public void SetIco(int num)
	{
		for (int i = 0; i < Icos.Count; i++)
		{
			Icos[i].SetActive(value: false);
		}
		Icos[num - 1].SetActive(value: true);
		index = num;
	}

	public void SetLock(bool _locked)
	{
		lockBut = _locked;
	}
}

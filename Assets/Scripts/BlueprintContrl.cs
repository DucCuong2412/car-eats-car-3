using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintContrl : MonoBehaviour
{
	public Animator Anim;

	private int _blueprint_isON = Animator.StringToHash("blueprint_isON");

	public Button _btnb1;

	public Button _btnb2;

	public Button _btnb3;

	public Button _btnb4;

	public Button _btnb5;

	public Button _btng1;

	public Button _btng2;

	public Button _btng3;

	public Button _btng4;

	public Button _btng5;

	public Button _btnExit;

	public List<GameObject> _go = new List<GameObject>();

	private void OnEnable()
	{
		_btnExit.onClick.AddListener(exit);
		_btnb1.onClick.AddListener(delegate
		{
			Clic(0);
		});
		_btnb2.onClick.AddListener(delegate
		{
			Clic(1);
		});
		_btnb3.onClick.AddListener(delegate
		{
			Clic(2);
		});
		_btnb4.onClick.AddListener(delegate
		{
			Clic(3);
		});
		_btnb5.onClick.AddListener(delegate
		{
			Clic(4);
		});
		_btng1.onClick.AddListener(delegate
		{
			Clic(6);
		});
		_btng2.onClick.AddListener(delegate
		{
			Clic(8);
		});
		_btng3.onClick.AddListener(delegate
		{
			Clic(7);
		});
		_btng4.onClick.AddListener(delegate
		{
			Clic(5);
		});
		_btng5.onClick.AddListener(delegate
		{
			Clic(9);
		});
	}

	private void OnDisable()
	{
		_btnExit.onClick.RemoveAllListeners();
		_btnb1.onClick.RemoveAllListeners();
		_btnb2.onClick.RemoveAllListeners();
		_btnb3.onClick.RemoveAllListeners();
		_btnb4.onClick.RemoveAllListeners();
		_btnb5.onClick.RemoveAllListeners();
		_btng1.onClick.RemoveAllListeners();
		_btng2.onClick.RemoveAllListeners();
		_btng3.onClick.RemoveAllListeners();
		_btng4.onClick.RemoveAllListeners();
		_btng5.onClick.RemoveAllListeners();
	}

	private void Clic(int i)
	{
		Anim.SetBool(_blueprint_isON, value: true);
		StartCoroutine(banner());
		for (int j = 0; j < _go.Count; j++)
		{
			if (j == i)
			{
				_go[j].SetActive(value: true);
			}
			else
			{
				_go[j].SetActive(value: false);
			}
		}
	}

	private IEnumerator banner()
	{
		yield return 0;
		AdvertWrapper.instance.ShowBaner();
	}

	private void exit()
	{
		StartCoroutine(ex());
	}

	private IEnumerator ex()
	{
		Anim.SetBool(_blueprint_isON, value: false);
		UnityEngine.Debug.Log("!!!!!!!!!!!!!!!!!!!!!!! hide banner");
		yield return 0;
	}
}

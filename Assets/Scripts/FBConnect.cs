using Smokoko.Social;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FBConnect : MonoBehaviour
{
	public Button _btnTake;

	public Button _btnLogin;

	public Button _btnClose;

	public Animator AnimTop;

	public Animator AnimGlobal;

	public Animator Anim_Open_Close;

	public FacebookFriendsLevelLogic FFLL;

	private int _connectionFailed = Animator.StringToHash("connectionFailed");

	private int _isConnected = Animator.StringToHash("isConnected");

	private int _close_isON = Animator.StringToHash("close_isON");

	private int _rewadTaken = Animator.StringToHash("rewadTaken");

	private int _isON = Animator.StringToHash("isON");

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
		}
	}

	private void OnEnable()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		Audio.Play("gui_screen_on");
		_btnLogin.onClick.AddListener(connect);
		_btnClose.onClick.AddListener(closes);
		_btnTake.onClick.AddListener(take);
	}

	private void OnDisable()
	{
		_btnLogin.onClick.RemoveAllListeners();
		_btnClose.onClick.RemoveAllListeners();
		_btnTake.onClick.RemoveAllListeners();
	}

	private void connect()
	{
		FBLeaderboard.LogIn();
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Combine(FBLeaderboard.OnUserLoggedIn, new Action<bool>(BoxOpen));
	}

	private void BoxOpen(bool a)
	{
		if (a)
		{
			GameCenter.OnConnectFB(1);
			AnimGlobal.SetBool(_connectionFailed, value: false);
			AnimGlobal.SetBool(_isConnected, value: true);
			if (Progress.shop.forFB)
			{
				AnimTop.SetBool(_close_isON, value: false);
			}
			else
			{
				AnimTop.SetBool(_close_isON, value: true);
			}
		}
		else
		{
			AnimGlobal.SetBool(_connectionFailed, value: true);
		}
	}

	private void closes()
	{
		Audio.PlayAsync("gui_button_02_sn");
		Progress.shop.fb_price_ned = true;
		StartCoroutine(close());
	}

	private IEnumerator close()
	{
		Anim_Open_Close.SetBool(_isON, value: false);
		yield return new WaitForSeconds(1f);
		Progress.shop.NeedForFB = false;
		Game.OnStateChange(Game.gameState.Menu);
		base.gameObject.SetActive(value: false);
	}

	private void take()
	{
		BoxReward();
	}

	private void BoxReward()
	{
	}

	private IEnumerator for_close_after_take()
	{
		yield return new WaitForSeconds(1f);
		closes();
	}
}

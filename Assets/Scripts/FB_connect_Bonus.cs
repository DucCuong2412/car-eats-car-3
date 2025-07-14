using Smokoko.Social;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FB_connect_Bonus : MonoBehaviour
{
	public Button FB_connect;

	public Button Close;

	public Animation anim;

	public GameObject GO;

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			closes();
		}
	}

	private void OnEnable()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		Audio.Play("gui_screen_on");
		FB_connect.onClick.AddListener(connect);
		Close.onClick.AddListener(closes);
		anim.Play("rateUs_show");
	}

	private void OnDisable()
	{
		FB_connect.onClick.RemoveAllListeners();
		Close.onClick.RemoveAllListeners();
	}

	private void connect()
	{
		FBLeaderboard.LogIn();
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Combine(FBLeaderboard.OnUserLoggedIn, new Action<bool>(BoxOpen));
	}

	private void closes()
	{
		StartCoroutine(closCor());
		Progress.shop.fb_price_ned = true;
	}

	private void BoxOpen(bool a)
	{
		if (a && Progress.shop.forFB)
		{
			BoxReward();
		}
		FBLeaderboard.OnUserLoggedIn = (Action<bool>)Delegate.Remove(FBLeaderboard.OnUserLoggedIn, new Action<bool>(BoxOpen));
	}

	private void BoxReward()
	{
	}

	private IEnumerator closCor()
	{
		anim.Play("rateUs_hide");
		while (anim.isPlaying)
		{
			yield return 0;
		}
		Game.OnStateChange(Game.gameState.Menu);
		GO.SetActive(value: false);
		Progress.shop.NeedForFB = false;
	}
}

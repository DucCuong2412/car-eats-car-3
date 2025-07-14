using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUICarInfoFields : MonoBehaviour
{
	public Slider healthBar;

	public Slider turboBar;

	[Header("bonus animators")]
	public Animator bombAnimator;

	public CounterController bombText;

	public Animator HealthAnimator;

	public CounterController HealthText;

	public Animator TurboAnimator;

	public CounterController TurboText;

	public GameObject ValentineHeart;

	public List<Image> bombIcon = new List<Image>();

	public CounterController rocketsLabel;

	public CounterController rubiesLabel;

	public Animator bombsAnim;

	public Animator rubyIcon;

	public Animation healthIcon;

	public Animation turboIcon;

	private int hash_isPickedUp = Animator.StringToHash("isPickedUp");

	private bool forward = true;

	private bool Static = true;

	private float pause = 0.5f;

	private void OnEnable()
	{
		if (Progress.shop.EsterLevelPlay)
		{
			rubyIcon.gameObject.SetActive(value: false);
			ValentineHeart.SetActive(value: true);
		}
		else
		{
			rubyIcon.gameObject.SetActive(value: true);
			ValentineHeart.SetActive(value: false);
		}
	}

	public void SetBombIcon(int bombtype)
	{
		foreach (Image item in bombIcon)
		{
			item.gameObject.SetActive(value: false);
		}
		switch (bombtype)
		{
		case 5:
			bombIcon[1].gameObject.SetActive(value: true);
			break;
		case 0:
			bombIcon[0].gameObject.SetActive(value: true);
			break;
		default:
			bombIcon[bombtype + 1].gameObject.SetActive(value: true);
			break;
		}
	}

	public void SetRubins(int rubinsCount)
	{
		RefreshRubiesLabel(rubinsCount);
		if (rubinsCount != 0 && base.gameObject.activeSelf)
		{
			StartCoroutine(RubyIconAnim());
		}
	}

	public void ChangeHealthBar(float val)
	{
		healthBar.value = val * (1f + Progress.fortune.SumPercentHP / 100f);
	}

	public void ChangeTurboBar(float val)
	{
		turboBar.value = val * (1f + Progress.fortune.SumPercentTurbo / 100f);
	}

	public void ChangeRocketCount(float val)
	{
		rocketsLabel.count = val.ToString();
		if (bombsAnim != null)
		{
			animatorsCor(bombsAnim);
		}
	}

	public void ChangeHealth()
	{
		if (healthIcon != null)
		{
			healthIcon.Play();
		}
	}

	public void ChangeTurbo()
	{
		if (turboIcon != null)
		{
			turboIcon.Play();
		}
	}

	private IEnumerator animatorsCor(Animator anim)
	{
		while (!anim.isInitialized)
		{
			yield return 0;
		}
		anim.SetTrigger(hash_isPickedUp);
	}

	public void ChangeBoostHealthBar(float val)
	{
	}

	public void ChangeBoostTurboBar(float val)
	{
	}

	public void SetActiveRecharger(bool active, Action callback = null)
	{
	}

	public void CheckTickets(int num)
	{
		AddTicket();
	}

	public void AddTicket()
	{
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	private void RefreshRubiesLabel(int count)
	{
		rubiesLabel.count = count.ToString();
	}

	private IEnumerator RubyIconAnim()
	{
		if (rubyIcon != null)
		{
			rubyIcon.SetTrigger(hash_isPickedUp);
		}
		yield return Utilities.WaitForRealSeconds(0.5f);
	}

	public void Update()
	{
		if (pause > 0f)
		{
			pause -= Time.deltaTime;
		}
	}
}

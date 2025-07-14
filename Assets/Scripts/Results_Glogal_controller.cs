using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Results_Glogal_controller : MonoBehaviour
{
	public GUIResults_Boxes BoxesController;

	public Animator PromoAnimator;

	public Results_new_controller R_N_C;

	public Results_lost_new_controller R_L_N_C;

	public Text numberlvl;

	public GameObject res;

	public Animation animfuel;

	public Text AnimfuelText;

	public GameObject winBody;

	public GameObject loseBody;

	public GameObject win_BG;

	public GameObject lose_BG;

	private int is_on = Animator.StringToHash("is_on");

	private Coroutine _corut;

	public void OnEnable()
	{
		win_BG.SetActive(value: false);
		winBody.SetActive(value: false);
		loseBody.SetActive(value: false);
		lose_BG.SetActive(value: false);
		res.SetActive(value: true);
		if (Progress.shop.EsterLevelPlay)
		{
			numberlvl.text = string.Empty;
		}
		else if (Progress.shop.endlessLevel)
		{
			numberlvl.text = string.Empty;
		}
		else if (Progress.shop.bossLevel)
		{
			numberlvl.text = string.Empty;
		}
		else if (Progress.shop.ArenaNew)
		{
			numberlvl.text = string.Empty;
		}
		else if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			numberlvl.text = Utilities.LevelNumberGlobal(RaceLogic.instance.level, RaceLogic.instance.pack).ToString();
		}
	}

	public void BoxesShow()
	{
		BoxesController.Show();
	}

	public void PressPromo1()
	{
		AnalyticsManager.LogEvent(EventCategoty.crosspromo, "result", "creatscar1");
		Application.OpenURL("market://details?id=com.SpilGames.CarEatsCar");
	}

	public void PressPromo2()
	{
		AnalyticsManager.LogEvent(EventCategoty.crosspromo, "result", "creatscar2");
		Application.OpenURL("market://details?id=com.spilgames.CarEatsCar2");
	}

	public void StartWindow()
	{
		_corut = StartCoroutine(DelayToPromo());
	}

	private IEnumerator DelayToPromo()
	{
		float t = 3f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		PromoAnimator.SetBool(is_on, value: true);
		_corut = null;
	}

	private void OnDisable()
	{
		if (_corut != null)
		{
			StopCoroutine(_corut);
			_corut = null;
		}
	}
}

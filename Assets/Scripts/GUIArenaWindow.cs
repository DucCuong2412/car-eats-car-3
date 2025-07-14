using AnimationOrTween;
using SmartLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIArenaWindow : MonoBehaviour
{
	public Slider BestScoreSlider;

	public Slider Score;

	public Text BestScore;

	public GameObject TakeReward;

	public GameObject NoTakeReward;

	public GameObject arenaReward;

	public Text Left;

	public Text Right;

	public Button Take;

	public Text dist;

	public Text best;

	public Animation anim;

	public List<GameObject> fill = new List<GameObject>();

	public List<GameObject> BP = new List<GameObject>();

	public Text Header;

	public Text textWinBlueprint;

	public Text textzirocka;

	public Text textArena;

	private void OnEnable()
	{
		if (Progress.levels.active_pack == 1)
		{
			BP[0].SetActive(value: true);
		}
		else if (Progress.levels.active_pack == 2)
		{
			BP[1].SetActive(value: true);
		}
		else if (Progress.levels.active_pack == 3)
		{
			BP[2].SetActive(value: true);
		}
		else if (Progress.levels.active_pack == 4)
		{
			BP[3].SetActive(value: true);
		}
		Take.onClick.AddListener(OnClicTake);
		if (Progress.levels.active_pack == 1 && Progress.levels.winArena1)
		{
			TakeReward.gameObject.SetActive(value: true);
			NoTakeReward.gameObject.SetActive(value: false);
		}
		else if (Progress.levels.active_pack == 2 && Progress.levels.winArena2)
		{
			TakeReward.gameObject.SetActive(value: true);
			NoTakeReward.gameObject.SetActive(value: false);
		}
		else if (Progress.levels.active_pack == 3 && Progress.levels.winArena3)
		{
			TakeReward.gameObject.SetActive(value: true);
			NoTakeReward.gameObject.SetActive(value: false);
		}
		else if (Progress.levels.active_pack == 4 && Progress.levels.winArena4)
		{
			TakeReward.gameObject.SetActive(value: true);
			NoTakeReward.gameObject.SetActive(value: false);
		}
		else
		{
			TakeReward.gameObject.SetActive(value: false);
			NoTakeReward.gameObject.SetActive(value: true);
		}
		Score.value = 0f;
		progresBar();
	}

	private void OnDisable()
	{
		Take.onClick.RemoveAllListeners();
	}

	private void OnClicTake()
	{
		if (Progress.shop.endlessLevel && Progress.levels.active_pack == 1)
		{
			Text bestScore = BestScore;
			Vector3 localPosition = RaceLogic.instance.car.transform.localPosition;
			bestScore.text = localPosition.x.ToString("0") + "M.";
			Vector3 localPosition2 = RaceLogic.instance.car.transform.localPosition;
			if (localPosition2.x > (float)EndlessLevelInfo.instance.Arena1)
			{
				Progress.levels.winArena1 = true;
			}
		}
		else if (Progress.shop.endlessLevel && Progress.levels.active_pack == 2)
		{
			Vector3 localPosition3 = RaceLogic.instance.car.transform.localPosition;
			if (localPosition3.x > (float)EndlessLevelInfo.instance.Arena2)
			{
				Progress.levels.winArena2 = true;
			}
		}
		else if (Progress.shop.endlessLevel && Progress.levels.active_pack == 3)
		{
			Vector3 localPosition4 = RaceLogic.instance.car.transform.localPosition;
			if (localPosition4.x > (float)EndlessLevelInfo.instance.Arena3)
			{
				Progress.levels.winArena3 = true;
			}
		}
		arenaReward.SetActive(value: false);
	}

	private void Update()
	{
		Header.text = Header.text.Replace("*", Progress.levels.active_pack.ToString());
		if (Progress.shop.endlessLevel && Progress.levels.active_pack == 1)
		{
			BestScoreSlider.value = Progress.levels.BestScoreArena1;
			BestScore.text = Progress.levels.BestScoreArena1.ToString("0") + "M.";
			best.text = Progress.levels.BestScoreArena1.ToString("0") + "M.";
		}
		else if (Progress.shop.endlessLevel && Progress.levels.active_pack == 2)
		{
			BestScoreSlider.value = Progress.levels.BestScoreArena2;
			BestScore.text = Progress.levels.BestScoreArena2.ToString("0") + "M.";
			best.text = Progress.levels.BestScoreArena2.ToString("0") + "M.";
		}
		else if (Progress.shop.endlessLevel && Progress.levels.active_pack == 3)
		{
			BestScoreSlider.value = Progress.levels.BestScoreArena3;
			BestScore.text = Progress.levels.BestScoreArena3.ToString("0") + "M.";
			best.text = Progress.levels.BestScoreArena3.ToString("0") + "M.";
		}
		else if (Progress.shop.endlessLevel && Progress.levels.active_pack == 4)
		{
			BestScoreSlider.value = Progress.levels.BestScoreArena4;
			BestScore.text = Progress.levels.BestScoreArena4.ToString("0") + "M.";
			best.text = Progress.levels.BestScoreArena4.ToString("0") + "M.";
		}
	}

	private void progresBar()
	{
		StartCoroutine(chek());
	}

	private IEnumerator CorutineForprogresBar()
	{
		while (true)
		{
			float value = Score.value;
			Vector3 localPosition = RaceLogic.instance.car.transform.localPosition;
			if (!(value < localPosition.x))
			{
				break;
			}
			Text text = dist;
			Vector3 localPosition2 = RaceLogic.instance.car.transform.localPosition;
			text.text = localPosition2.x.ToString("0") + "M.";
			if (Score.value != 3000f)
			{
				Score.value += 15f;
				Left.text = Score.value.ToString("0") + "M.";
				Right.text = Score.value.ToString("0") + "M.";
				yield return 0;
				if (Score.value > 200f)
				{
					Left.gameObject.SetActive(value: true);
					Right.gameObject.SetActive(value: false);
				}
				else
				{
					Left.gameObject.SetActive(value: false);
					Right.gameObject.SetActive(value: true);
				}
				continue;
			}
			yield break;
		}
		Slider score = Score;
		Vector3 localPosition3 = RaceLogic.instance.car.transform.localPosition;
		score.value = localPosition3.x;
	}

	private IEnumerator chek()
	{
		yield return StartCoroutine(CorutineForprogresBar());
		if (!(Score.value >= 3000f))
		{
			yield break;
		}
		foreach (GameObject item in fill)
		{
			item.SetActive(value: false);
		}
		if (Progress.levels.active_pack == 1 && !Progress.levels.winArena1)
		{
			arenaReward.SetActive(value: true);
			textWinBlueprint.text = LanguageManager.Instance.GetTextValue("CONGRATULATIONS! YOU'VE OPENED ANTIGRAVS");
			textzirocka.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.active_pack.ToString());
			textArena.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			Audio.Play("blueprint");
			fill[0].SetActive(value: true);
			ActiveAnimation.Play(anim, Direction.Forward);
		}
		else if (Progress.levels.active_pack == 2 && !Progress.levels.winArena2)
		{
			arenaReward.SetActive(value: true);
			textWinBlueprint.text = LanguageManager.Instance.GetTextValue("CONGRATULATIONS! YOU'VE OPENED MEGA TURBO");
			textzirocka.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.active_pack.ToString());
			textArena.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			Audio.Play("blueprint");
			fill[1].SetActive(value: true);
			ActiveAnimation.Play(anim, Direction.Forward);
		}
		else if (Progress.levels.active_pack == 3 && !Progress.levels.winArena3)
		{
			arenaReward.SetActive(value: true);
			textWinBlueprint.text = LanguageManager.Instance.GetTextValue("CONGRATULATIONS! YOU'VE OPENED SUPER GUN");
			textzirocka.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.active_pack.ToString());
			textArena.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			Audio.Play("blueprint");
			fill[2].SetActive(value: true);
			ActiveAnimation.Play(anim, Direction.Forward);
		}
		else if (Progress.levels.active_pack == 4 && !Progress.levels.winArena4)
		{
			arenaReward.SetActive(value: true);
			if (Progress.levels.winArena1 && Progress.levels.winArena2 && Progress.levels.winArena3)
			{
				textWinBlueprint.text = LanguageManager.Instance.GetTextValue("CONGRATULATIONS! YOU WON TANKOMINATOR");
				textzirocka.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.active_pack.ToString());
				textArena.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			}
			else
			{
				textWinBlueprint.text = LanguageManager.Instance.GetTextValue("CONGRATULATIONS! YOU'VE OPENED TANK");
				textzirocka.text = LanguageManager.Instance.GetTextValue("PART */4").Replace("*", Progress.levels.active_pack.ToString());
				textArena.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Progress.levels.active_pack.ToString());
			}
			Audio.Play("blueprint");
			fill[3].SetActive(value: true);
			ActiveAnimation.Play(anim, Direction.Forward);
		}
	}
}

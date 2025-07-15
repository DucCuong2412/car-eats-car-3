using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPositionBar : MonoBehaviour
{
	[Serializable]
	private class EnemyCar
	{
		public Transform Transf;

		public GameObject Marker;
	}

	public Slider progressSlider;

	public Image background;

	public GameObject Thumb;

	private float _startPos;

	private float _endPos;

	private List<Transform> aiList = new List<Transform>();

	private Transform _car;

	private List<ThumbsPool.ThumbObject> thumbsList2 = new List<ThumbsPool.ThumbObject>();

	public List<GameObject> thumbsList = new List<GameObject>();

	public List<Animator> thumbsListAnims;

	[Header("Arena")]
	public GameObject ArenaObj;

	public GameObject NormalSlideObj;

	public GameObject DistObj;

	public GameObject KeyObj;

	public Slider BestSlider;

	public Slider CurrentSlider;

	public Text MaxToWinText;

	public Text CurrentText;

	public CounterController CurrentDist;

	public CounterController BestDist;

	private List<EnemyCar> enemies = new List<EnemyCar>();

	private int maxDist = -1;

	private int is_underground = Animator.StringToHash("is_underground");

	private List<Animator> thumbsListAnimator = new List<Animator>();

	[HideInInspector]
	public float Distance;

	private float Koeff;

	public Animator KEys;

	private void OnEnable()
	{
		if (thumbsListAnimator.Count == 0)
		{
			thumbsListAnimator.Add(Thumb.GetComponent<Animator>());
			foreach (GameObject thumbs in thumbsList)
			{
				Animator componentInChildren = thumbs.GetComponentInChildren<Animator>();
				thumbsListAnimator.Add(componentInChildren);
			}
			if (Progress.shop.endlessLevel || Progress.shop.bossLevel || Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
			{
				foreach (Animator item in thumbsListAnimator)
				{
					item.enabled = false;
				}
			}
		}
		CurrentText.text = "0";
		ArenaObj.SetActive(value: false);
		NormalSlideObj.SetActive(value: false);
		if (Progress.shop.endlessLevel || Progress.shop.bossLevel)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.SetActive(value: true);
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			Thumb.SetActive(value: false);
			maxDist = EndlessLevelInfo.instance.GetArenaDist(Progress.levels.active_pack_last_openned);
			ArenaObj.SetActive(value: true);
			DistObj.SetActive(value: false);
			KeyObj.SetActive(value: false);
			int active_pack = Progress.levels.active_pack;
			switch (active_pack)
			{
			case 1:
				maxDist = DifficultyConfig.instance.MetrivForARENA1;
				break;
			case 2:
				maxDist = DifficultyConfig.instance.MetrivForARENA2;
				break;
			case 3:
				maxDist = DifficultyConfig.instance.MetrivForARENA3;
				break;
			}
			if (Progress.shop.EsterLevelPlay)
			{
				DistObj.SetActive(value: true);
				CurrentDist.count = "0";
				BestDist.count = Progress.shop.ValentineMaxDistance.ToString();
			}
			else if ((active_pack == 1 && !Progress.shop.Key1) || (active_pack == 2 && !Progress.shop.Key2) || (active_pack == 3 && !Progress.shop.Key3))
			{
				KeyObj.SetActive(value: true);
				BestSlider.gameObject.SetActive(value: true);
				BestSlider.maxValue = maxDist;
				switch (active_pack)
				{
				case 1:
					BestSlider.value = Progress.shop.Arena1MaxDistance;
					break;
				case 2:
					BestSlider.value = Progress.shop.Arena2MaxDistance;
					break;
				case 3:
					BestSlider.value = Progress.shop.Arena3MaxDistance;
					break;
				}
				if (BestSlider.value == 0f)
				{
					BestSlider.gameObject.SetActive(value: false);
				}
				CurrentSlider.maxValue = maxDist;
				MaxToWinText.text = maxDist.ToString() + " M.";
			}
			else
			{
				DistObj.SetActive(value: true);
				CurrentDist.count = "0";
				switch (active_pack)
				{
				case 1:
					BestDist.count = Progress.shop.Arena1MaxDistance.ToString();
					break;
				case 2:
					BestDist.count = Progress.shop.Arena2MaxDistance.ToString();
					break;
				case 3:
					BestDist.count = Progress.shop.Arena3MaxDistance.ToString();
					break;
				}
			}
		}
		else
		{
			NormalSlideObj.SetActive(value: true);
		}
	}

	private void Update()
	{
		if ((Progress.shop.EsterLevelPlay || Progress.shop.ArenaNew) && _car != null)
		{
			if (_car != null)
			{
				Vector3 localPosition = _car.localPosition;
				Distance = localPosition.x - _startPos;
			}
			if (Distance < 3000f)
			{
				Koeff = 2f;
			}
			else if (Distance > 3000f && Distance < 10000f)
			{
				Koeff = 1f;
			}
			else
			{
				Koeff = 0.5f;
			}
			Progress.shop.forArenaDamageEnemy = Distance / 1000f / Koeff;
			if (CurrentSlider != null && KeyObj.activeSelf)
			{
				Vector3 localPosition2 = _car.localPosition;
				if (localPosition2.x - _startPos > (float)maxDist)
				{
					CurrentSlider.value = maxDist;
				}
				else
				{
					Slider currentSlider = CurrentSlider;
					Vector3 localPosition3 = _car.localPosition;
					currentSlider.value = localPosition3.x - _startPos;
				}
				Text currentText = CurrentText;
				Vector3 localPosition4 = _car.localPosition;
				currentText.text = ((int)(localPosition4.x - _startPos)).ToString();
			}
			else if (DistObj.activeSelf)
			{
				CounterController currentDist = CurrentDist;
				Vector3 localPosition5 = _car.localPosition;
				currentDist.count = ((int)(localPosition5.x - _startPos)).ToString();
			}
			if (Progress.shop.EsterLevelPlay)
			{
				Progress.Shop shop = Progress.shop;
				Vector3 localPosition6 = _car.localPosition;
				shop.ValentineDistance = (int)(localPosition6.x - _startPos);
			}
			else if (Progress.levels.active_pack == 1)
			{
				if ((float)Progress.shop.Arena1Distance >= CurrentSlider.maxValue)
				{
					KEys.SetBool("isON", value: true);
				}
				Progress.Shop shop2 = Progress.shop;
				Vector3 localPosition7 = _car.localPosition;
				shop2.Arena1Distance = (int)(localPosition7.x - _startPos);
			}
			else if (Progress.levels.active_pack == 2)
			{
				if ((float)Progress.shop.Arena2Distance >= CurrentSlider.maxValue)
				{
					KEys.SetBool("isON", value: true);
				}
				Progress.Shop shop3 = Progress.shop;
				Vector3 localPosition8 = _car.localPosition;
				shop3.Arena2Distance = (int)(localPosition8.x - _startPos);
			}
			else if (Progress.levels.active_pack == 3)
			{
				if ((float)Progress.shop.Arena3Distance >= CurrentSlider.maxValue)
				{
					KEys.SetBool("isON", value: true);
				}
				Progress.Shop shop4 = Progress.shop;
				Vector3 localPosition9 = _car.localPosition;
				shop4.Arena3Distance = (int)(localPosition9.x - _startPos);
			}
		}
		else if (_car != null && progressSlider != null)
		{
			if (!Thumb.gameObject.activeSelf)
			{
				Thumb.gameObject.SetActive(value: true);
			}
			Thumb.transform.localPosition = new Vector3(RawVal(_car) * background.sprite.rect.width, 0f);
			Slider slider = progressSlider;
			Vector3 localPosition10 = Thumb.transform.localPosition;
			slider.value = localPosition10.x / background.sprite.rect.width;
			Thumb.transform.localPosition = new Vector3(RawVal(_car) * background.sprite.rect.width * 1.17f - 130f, 0f);
			for (int i = 0; i < enemies.Count; i++)
			{
				if (!enemies[i].Transf.gameObject.activeSelf)
				{
					enemies[i].Marker.SetActive(value: false);
					enemies[i].Marker = null;
					enemies.RemoveAt(i);
					i = -1;
				}
				else
				{
					enemies[i].Marker.transform.localPosition = new Vector3(RawVal(enemies[i].Transf) * background.sprite.rect.width * 1.17f - 130f, 0f);
				}
			}
		}
		else
		{
			Thumb.gameObject.SetActive(value: false);
		}
	}

	private void InitProgressBar(Transform car, float start, float finish)
	{
		_startPos = start;
		_endPos = finish;
		_car = car;
	}

	private IEnumerator changeProgressBar(Transform _carr, float _start, float _finish)
	{
		_car = null;
		yield return Utilities.WaitForRealSeconds(0.5f);
		InitProgressBar(_carr, _start, _finish);
	}

	private float RawVal(Transform tr)
	{
		Vector3 position = tr.position;
		return (position.x - _startPos) / (_endPos - _startPos);
	}

	public void InitAIs(Car2DAIController ai)
	{
		if (ai == null || Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			return;
		}
		EnemyCar enemyCar = new EnemyCar();
		enemyCar.Transf = ai.transform;
		for (int i = 0; i < thumbsList.Count; i++)
		{
			if (!thumbsList[i].activeSelf)
			{
				thumbsList[i].SetActive(value: true);
				if (Progress.levels.InUndeground)
				{
					thumbsListAnims[i].SetBool(is_underground, value: true);
				}
				else
				{
					thumbsListAnims[i].SetBool(is_underground, value: false);
				}
				enemyCar.Marker = thumbsList[i];
				break;
			}
		}
		if (enemyCar.Marker == null)
		{
			UnityEngine.Debug.Log("!!!!! LEX NOT enaff MAP markers!!!!!");
		}
		enemies.Add(enemyCar);
	}

	public void InitAIs(Transform trns)
	{
		if (trns == null || Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			return;
		}
		EnemyCar enemyCar = new EnemyCar();
		enemyCar.Transf = trns;
		for (int i = 0; i < thumbsList.Count; i++)
		{
			if (!thumbsList[i].activeSelf)
			{
				thumbsList[i].SetActive(value: true);
				if (Progress.levels.InUndeground)
				{
					thumbsListAnims[i].SetBool(is_underground, value: true);
				}
				else
				{
					thumbsListAnims[i].SetBool(is_underground, value: false);
				}
				enemyCar.Marker = thumbsList[i];
				break;
			}
		}
		if (enemyCar.Marker == null)
		{
			UnityEngine.Debug.Log("!!!!! LEX NOT enaff MAP markers!!!!!");
		}
		enemies.Add(enemyCar);
	}

	public void SetProgressBar(Transform car, float start, float finish)
	{
		if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			if (!Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
			{
				progressSlider.gameObject.SetActive(value: true);
			}
			StartCoroutine(changeProgressBar(car, start, finish));
		}
	}

	public void HideProgressBar()
	{
		progressSlider.gameObject.SetActive(value: false);
	}
}

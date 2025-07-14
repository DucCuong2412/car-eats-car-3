using System.Collections;
using UnityEngine;

public class TutorialRace : MonoBehaviour
{
	private enum tType
	{
		tilt,
		turbo,
		fire
	}

	private static TutorialRace instance;

	private Car2DController car;

	private TutorialGUI guiTutorial;

	private bool isShowedTurbo;

	private bool isShowedBomb;

	private bool isShowedTilt;

	private bool isPressedTurbo;

	private bool isPressedBomb;

	private bool isPressedTilt;

	private Coroutine animateCoroutine;

	public static TutorialRace CreateTutorial(Car2DController car, TutorialGUI guiTutorial)
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
		if (Progress.levels.active_pack == 1 && Progress.levels.active_level == 1 && PlayerPrefs.GetInt("Tutorial_race", 0) == 0)
		{
			GameObject gameObject = new GameObject("_tutorial");
			instance = gameObject.AddComponent<TutorialRace>();
			instance.Init(car, guiTutorial);
			return instance;
		}
		return null;
	}

	public static void ReInit()
	{
		if (!(instance == null))
		{
			instance.car.OnTurboChanged -= instance.car_OnTurboChanged;
			instance.car.OnWeaponChanged -= instance.car_OnWeaponChanged;
			if (Progress.levels.active_pack == 1 && Progress.levels.active_level == 1 && PlayerPrefs.GetInt("Tutorial_race", 0) == 0)
			{
				instance.StartCoroutine(instance.Reiniting());
			}
		}
	}

	private IEnumerator Reiniting()
	{
		yield return null;
		Init(car, guiTutorial);
	}

	private void Init(Car2DController car, TutorialGUI guiTutorial)
	{
		this.car = car;
		this.guiTutorial = guiTutorial;
		car.TurboModule.Increase(-100f);
		car.BombModule._Barrel.Increase(car.BombModule._Barrel.Value);
		car.OnTurboChanged += car_OnTurboChanged;
		car.OnWeaponChanged += car_OnWeaponChanged;
	}

	private void OnDisable()
	{
		if (car != null)
		{
			car.OnTurboChanged -= car_OnTurboChanged;
			car.OnWeaponChanged -= car_OnWeaponChanged;
		}
		StopAllCoroutines();
		instance = null;
	}

	private void car_OnTurboChanged(float value)
	{
		if (!isShowedTurbo && value > 0f)
		{
			isShowedTurbo = true;
			car.TurboModule.Increase(0.5f);
			StartCoroutine(WaitForShowTutorial(tType.turbo));
			car.OnTurboChanged -= car_OnTurboChanged;
		}
	}

	private void car_OnWeaponChanged(float value)
	{
		if (!isShowedBomb && value > 0f)
		{
			isShowedBomb = true;
			StartCoroutine(WaitForShowTutorial(tType.fire));
			car.OnWeaponChanged -= car_OnWeaponChanged;
		}
	}

	private IEnumerator WaitForShowTutorial(tType t)
	{
		while (Game.currentState == Game.gameState.Tutorial)
		{
			yield return null;
		}
		ShowTutorial(t, show: true);
	}

	public void OnRotationPress()
	{
		if (isShowedTilt && !isPressedTilt)
		{
			ShowTutorial(tType.tilt, show: false);
			isPressedTilt = true;
		}
	}

	public void OnTurboPress()
	{
		if (isShowedTurbo && !isPressedTurbo)
		{
			ShowTutorial(tType.turbo, show: false);
			isPressedTurbo = true;
		}
	}

	public void OnBombPress()
	{
		if (isShowedBomb && !isPressedBomb)
		{
			ShowTutorial(tType.fire, show: false);
			isPressedBomb = true;
		}
	}

	private void ShowTutorial(tType t, bool show)
	{
		guiTutorial.tint.SetActive(show);
		Time.timeScale = ((!show) ? 1f : 0f);
		if (show)
		{
			Game.OnStateChange(Game.gameState.Tutorial);
		}
		else
		{
			Game.OnStateChange(Game.gameState.Race);
		}
		switch (t)
		{
		case tType.tilt:
			guiTutorial.labelTilt.gameObject.SetActive(show);
			if (show)
			{
				animateCoroutine = StartCoroutine(AnimateButtons(guiTutorial.tiltButton1, guiTutorial.tiltButton2));
			}
			else if (animateCoroutine != null)
			{
				StopCoroutine(animateCoroutine);
				animateCoroutine = null;
				ResetScaleButtons();
				SetWidgetsOrder(guiTutorial.tiltButton1, -20);
				SetWidgetsOrder(guiTutorial.tiltButton2, -20);
			}
			break;
		case tType.turbo:
			guiTutorial.labelNitro.gameObject.SetActive(show);
			if (show)
			{
				animateCoroutine = StartCoroutine(AnimateButtons(guiTutorial.nitroButton));
			}
			else if (animateCoroutine != null)
			{
				StopCoroutine(animateCoroutine);
				animateCoroutine = null;
				ResetScaleButtons();
				SetWidgetsOrder(guiTutorial.nitroButton, -20);
			}
			break;
		case tType.fire:
			guiTutorial.labelBomb.gameObject.SetActive(show);
			if (show)
			{
				animateCoroutine = StartCoroutine(AnimateButtons(guiTutorial.bombButton));
			}
			else if (animateCoroutine != null)
			{
				StopCoroutine(animateCoroutine);
				animateCoroutine = null;
				ResetScaleButtons();
				PlayerPrefs.SetInt("Tutorial_race", 1);
				SetWidgetsOrder(guiTutorial.bombButton, -20);
			}
			break;
		}
	}

	private void SetWidgetsOrder(GameObject go, int addOrder)
	{
		UIWidget[] componentsInChildren = go.GetComponentsInChildren<UIWidget>();
		UIWidget[] array = componentsInChildren;
		foreach (UIWidget uIWidget in array)
		{
			uIWidget.depth += addOrder;
		}
	}

	private void Update()
	{
		if (!isPressedTilt && !isShowedTilt)
		{
			Vector3 position = car.transform.position;
			if (position.x >= 130f)
			{
				isShowedTilt = true;
				ShowTutorial(tType.tilt, show: true);
			}
		}
	}

	private IEnumerator AnimateButtons(params GameObject[] objects)
	{
		float scale = 1f;
		Vector3 localScale = objects[0].transform.localScale;
		float minScale = localScale.x;
		float maxScale = minScale + 0.1f;
		bool up = true;
		int countScaled = 0;
		yield return null;
		SetColliders(enabled: false);
		foreach (GameObject go in objects)
		{
			SetWidgetsOrder(go, 20);
		}
		while (true)
		{
			if ((!(scale < maxScale) || !up) && (!(scale > minScale) || up))
			{
				countScaled++;
				up = !up;
				if (countScaled == 4)
				{
					SetColliders(enabled: true);
				}
				continue;
			}
			yield return null;
			scale += 0.005f * (float)(up ? 1 : (-1));
			foreach (GameObject gameObject in objects)
			{
				gameObject.transform.localScale = Vector3.one * scale;
			}
		}
	}

	private void SetColliders(bool enabled)
	{
		guiTutorial.tiltButton1.GetComponent<BoxCollider2D>().enabled = enabled;
		guiTutorial.tiltButton2.GetComponent<BoxCollider2D>().enabled = enabled;
		guiTutorial.nitroButton.GetComponent<BoxCollider2D>().enabled = enabled;
		guiTutorial.bombButton.GetComponent<BoxCollider2D>().enabled = enabled;
	}

	private void ResetScaleButtons()
	{
		guiTutorial.tiltButton1.transform.localScale = Vector3.one;
		guiTutorial.tiltButton2.transform.localScale = Vector3.one;
		guiTutorial.nitroButton.transform.localScale = Vector3.one;
		guiTutorial.bombButton.transform.localScale = Vector3.one;
	}
}

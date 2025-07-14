using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScript : MonoBehaviour
{
	private RaceManager race;

	private Car2DController car;

	private GuiContainer gui;

	private CarFollow follow;

	private IEnumerator Start()
	{
		yield return 0;
		yield return 0;
		yield return 0;
		Screen.sleepTimeout = -1;
		Game.OnStateChange(Game.gameState.Preloader);
		bool test = false;
		PreloaderCanvas preloader = null;
		if (!test)
		{
			preloader = GameObject.Find("Preloader").GetComponent<PreloaderCanvas>();
		}
		yield return 0;
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "5 %";
		yield return 0;
		yield return 0;
		yield return 0;
		int pack = Progress.levels.active_pack;
		int level = Progress.levels.active_level;
		if (test)
		{
			int.TryParse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Split('_')[0], out pack);
			int.TryParse(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Split('_')[1], out level);
		}
		int carid = Progress.shop.activeCar;
		Transform start = null;
		Transform finish = null;
		Transform car = null;
		Action<Transform, Transform> getStartFinish = delegate(Transform _start, Transform _finish)
		{
			start = _start;
			finish = _finish;
		};
		Action<Transform> getCar = delegate(Transform carTr)
		{
			car = carTr;
		};
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "10 %";
		yield return 0;
		SceneManager.LoadScene("results_new", LoadSceneMode.Additive);
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "30 %";
		yield return 0;
		GameObject levelBuilder = new GameObject("_levelBuilder");
		LevelBuilder builder = levelBuilder.AddComponent<LevelBuilder>();
		if (test)
		{
			builder.buildTestRace(pack, level, getStartFinish);
		}
		else
		{
			builder.StartBuilding(pack, level, getStartFinish);
		}
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "60 %";
		yield return 0;
		yield return 0;
		Pool.Init();
		ShadowPool.Init();
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "95 %";
		yield return 0;
		SpawnCar(carid, getCar, start.position);
		race = RaceManager.instance;
		race.Init(car.transform, start, finish);
		gui = UnityEngine.Object.FindObjectOfType<GuiContainer>();
		SpawnParallax(pack);
		AttachFollower(car);
		RaceLogic.instance.Init(race, this.car, gui, follow, level, pack);
		yield return 0;
		yield return StartCoroutine(Pool.instance.EnableAllForFrame());
		yield return 0;
		Game.OnStateChange(Game.gameState.PreRace);
		float tt = 1.5f;
		while (tt > 0f)
		{
			tt -= Time.deltaTime;
			yield return null;
		}
		yield return 0;
		yield return 0;
		preloader.Zvantaj.text = "100 %";
		yield return 0;
		yield return 0;
		yield return 0;
		yield return 0;
		yield return 0;
		if (preloader != null)
		{
			preloader.Hide();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void CullAllAnimationsAndAnimators()
	{
		List<Animation> list = new List<Animation>();
		List<Animator> list2 = new List<Animator>();
		GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			list.AddRange(rootGameObjects[i].GetComponentsInChildren<Animation>(includeInactive: true));
			list2.AddRange(rootGameObjects[i].GetComponentsInChildren<Animator>(includeInactive: true));
		}
		for (int j = 0; j < list.Count; j++)
		{
			list[j].cullingType = AnimationCullingType.BasedOnRenderers;
		}
		for (int k = 0; k < list2.Count; k++)
		{
			if (!list2[k].gameObject.transform.parent.gameObject.name.Contains("turbo"))
			{
				list2[k].cullingMode = AnimatorCullingMode.CullUpdateTransforms;
			}
		}
	}

	private void SpawnCar(int id, Action<Transform> callback, Vector3 position)
	{
		GameObject original = null;
		if (Progress.shop.activeCar == 0)
		{
			original = (Resources.Load("PC_cars_new") as GameObject);
		}
		else if (Progress.shop.activeCar == 1)
		{
			original = (Resources.Load("PC_car_2") as GameObject);
		}
		else if (Progress.shop.activeCar == 2)
		{
			original = (Resources.Load("PC_car_3") as GameObject);
		}
		else if (Progress.shop.activeCar == 3)
		{
			original = (Resources.Load("PC_car_4") as GameObject);
		}
		else if (Progress.shop.activeCar == 4)
		{
			original = (Resources.Load("PC_car_5") as GameObject);
		}
		else if (Progress.shop.activeCar == 5)
		{
			original = (Resources.Load("PC_car_07") as GameObject);
		}
		else if (Progress.shop.activeCar == 6)
		{
			original = (Resources.Load("PC_car_police1") as GameObject);
		}
		else if (Progress.shop.activeCar == 7)
		{
			original = (Resources.Load("PC_car_police2") as GameObject);
		}
		else if (Progress.shop.activeCar == 8)
		{
			original = (Resources.Load("PC_car_08") as GameObject);
		}
		else if (Progress.shop.activeCar == 9)
		{
			original = (Resources.Load("PC_car_underground_02") as GameObject);
		}
		else if (Progress.shop.activeCar == 10)
		{
			original = (Resources.Load("PC_car_underground_01") as GameObject);
		}
		else if (Progress.shop.activeCar == 11)
		{
			original = (Resources.Load("PC_car_police3") as GameObject);
		}
		else if (Progress.shop.activeCar == 12)
		{
			original = (Resources.Load("PC_car_underground_05") as GameObject);
		}
		else if (Progress.shop.activeCar == 13)
		{
			original = (Resources.Load("PC_car_underground_06") as GameObject);
		}
		if (Progress.shop.activeCar == 13)
		{
			position += Vector3.up * 5f;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(original, position, Quaternion.identity);
		car = gameObject.GetComponent<Car2DController>();
		car.gameObject.SetActive(value: false);
		callback(gameObject.transform);
		Shadow shadow = gameObject.AddComponent<Shadow>();
		shadow.Set(1.1f, Shadow.ShadowType.FixedSize);
		shadow.Set(1.1f, Shadow.ShadowType.FixedSize);
	}

	private IEnumerator SpawnGUI(Transform car)
	{
		SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
		yield return null;
		gui = UnityEngine.Object.FindObjectOfType<GuiContainer>();
	}

	private void SpawnParallax(int pack)
	{
		ParallaxSystem.Create(Camera.main.transform, pack);
	}

	private void AttachFollower(Transform car)
	{
		CarFollow carFollow = null;
		carFollow = (follow = ((!(Camera.main.gameObject.GetComponent<CarFollow>() != null)) ? Camera.main.gameObject.AddComponent<CarFollow>() : Camera.main.gameObject.GetComponent<CarFollow>()));
	}
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceScriptForTutorials : MonoBehaviour
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
		int pack = 1;
		int level = 0;
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
		Game.OnStateChange(Game.gameState.PreRace);
		float tt = 1.5f;
		while (tt > 0f)
		{
			tt -= Time.deltaTime;
			yield return null;
		}
		yield return 0;
		yield return 0;
		AnalyticsManager.LogEvent(EventCategoty.result_tutorial, "result_tutorial", "start");
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
		Progress.shop.currency += 250;
		UnityEngine.Object.Destroy(base.gameObject);
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
		GameObject gameObject = UnityEngine.Object.Instantiate(original, position, Quaternion.identity);
		car = gameObject.GetComponent<Car2DController>();
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

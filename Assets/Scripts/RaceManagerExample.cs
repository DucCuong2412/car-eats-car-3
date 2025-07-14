using UnityEngine;

public class RaceManagerExample : RaceManagerBase
{
	public GameObject obj_Start;

	public GameObject obj_Finish;

	public GameObject obj_Car;

	public GameObject[] obj_enemies;

	private void Start()
	{
		Transform[] array = new Transform[obj_enemies.Length];
		for (int i = 0; i < obj_enemies.Length; i++)
		{
			array[i] = obj_enemies[i].transform;
		}
		Init(obj_Car.transform, obj_Start.transform, obj_Finish.transform, array);
	}

	private void OnGUI()
	{
		if (!base.isStarted && GUI.Button(new Rect(10f, 10f, 100f, 75f), "Start Race"))
		{
			StartRace();
		}
		int num = 0;
		int num2;
		for (num2 = (int)base.timeElapsed; num2 >= 60; num2 -= 60)
		{
			num++;
		}
		string str = (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString());
		GUI.Label(new Rect(10f, 10f, 100f, 50f), num.ToString() + ":" + str);
		if (GUI.Button(new Rect(350f, 10f, 100f, 75f), "Restart"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		GUI.Label(new Rect(150f, 10f, 150f, 75f), "position \t\t " + base.position + "\nreal cars \t\t " + (base.carTransforms.Count - 1) + "\nstart cars \t\t " + base.totalCars);
	}

	public override void StartRace()
	{
		base.StartRace();
	}

	public override void Update()
	{
		base.Update();
		if (!base.isStarted)
		{
			return;
		}
		for (int i = 0; i < obj_enemies.Length; i++)
		{
			if (!(obj_enemies[i] == null))
			{
				Vector3 position = obj_enemies[i].transform.position;
				position.x += Random.Range(0.001f, 0.05f);
				obj_enemies[i].transform.position = position;
			}
		}
	}

	public override void OnPositionChanged()
	{
		base.OnPositionChanged();
		UnityEngine.Debug.Log(base.position + "/" + (base.carTransforms.Count - 1) + "/" + base.totalCars);
	}

	public override void OnFinish()
	{
		base.OnFinish();
		UnityEngine.Debug.Log("OnFinish");
	}

	public override void OnLose()
	{
		base.OnLose();
		UnityEngine.Debug.Log("OnLose");
	}
}

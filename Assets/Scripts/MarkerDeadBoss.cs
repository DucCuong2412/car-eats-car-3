using UnityEngine;
using UnityEngine.UI;

public class MarkerDeadBoss : MonoBehaviour
{
	public GameObject marker;

	public Text text;

	private float test;

	private void OnEnable()
	{
		test = 0f;
	}

	private void Update()
	{
		if (Progress.shop.bossLevel && RaceLogic.instance.BossDeath)
		{
			float num = test;
			Vector3 position = RaceLogic.instance.car.transform.position;
			if ((float)Mathf.Abs((int)(num - position.x)) > 25f)
			{
				marker.SetActive(value: true);
			}
			else
			{
				marker.SetActive(value: false);
			}
			TESTS();
		}
		else
		{
			marker.SetActive(value: false);
		}
		if (RaceLogic.instance.car != null)
		{
			Text obj = text;
			float num2 = test;
			Vector3 position2 = RaceLogic.instance.car.transform.position;
			obj.text = Mathf.Abs((int)(num2 - position2.x)).ToString();
		}
	}

	private void TESTS()
	{
		if (test != 0f)
		{
			return;
		}
		int num = 0;
		while (true)
		{
			if (num >= LevelBuilder.instance.constructionsActivator.TEstList.Count)
			{
				return;
			}
			if (LevelBuilder.instance.constructionsActivator.TEstList[num].node.name == "finish_jail")
			{
				float x = LevelBuilder.instance.constructionsActivator.TEstList[num].node.position.x;
				Vector3 position = RaceLogic.instance.car.transform.position;
				if (x - position.x > 25f)
				{
					break;
				}
			}
			num++;
		}
		test = LevelBuilder.instance.constructionsActivator.TEstList[num].node.position.x;
	}
}

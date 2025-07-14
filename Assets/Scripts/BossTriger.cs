using UnityEngine;

public class BossTriger : MonoBehaviour
{
	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private bool chek;

	private void OnEnable()
	{
		chek = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((!(other.tag == CarMain) && !(other.tag == CarMainChild)) || chek)
		{
			return;
		}
		chek = true;
		GameObject obj = null;
		if (Progress.levels.InUndeground)
		{
			if (Progress.shop.Undeground2)
			{
				Vector3 position = RaceLogic.instance.car.transform.position;
				float x = position.x - 30f;
				Vector3 position2 = RaceLogic.instance.car.transform.position;
				obj = Pool.GameOBJECT(Pool.Bonus.boss2Undeground, new Vector2(x, position2.y + 20f));
			}
			else
			{
				Vector3 position3 = RaceLogic.instance.car.transform.position;
				float x2 = position3.x - 30f;
				Vector3 position4 = RaceLogic.instance.car.transform.position;
				obj = Pool.GameOBJECT(Pool.Bonus.boss1Undeground, new Vector2(x2, position4.y + 20f));
			}
		}
		else if (Progress.levels.active_pack == 1)
		{
			Vector3 position5 = RaceLogic.instance.car.transform.position;
			float x3 = position5.x - 30f;
			Vector3 position6 = RaceLogic.instance.car.transform.position;
			obj = Pool.GameOBJECT(Pool.Bonus.boss1, new Vector2(x3, position6.y + 20f));
		}
		else if (Progress.levels.active_pack == 2)
		{
			Vector3 position7 = RaceLogic.instance.car.transform.position;
			float x4 = position7.x - 30f;
			Vector3 position8 = RaceLogic.instance.car.transform.position;
			obj = Pool.GameOBJECT(Pool.Bonus.boss2, new Vector2(x4, position8.y + 20f));
		}
		else if (Progress.levels.active_pack == 3)
		{
			Vector3 position9 = RaceLogic.instance.car.transform.position;
			float x5 = position9.x - 30f;
			Vector3 position10 = RaceLogic.instance.car.transform.position;
			obj = Pool.GameOBJECT(Pool.Bonus.boss3, new Vector2(x5, position10.y + 20f));
		}
		Object.FindObjectOfType<AIActivator>().AddCarToList(obj);
		base.gameObject.SetActive(value: false);
	}
}

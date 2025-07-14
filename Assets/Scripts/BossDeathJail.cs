using System.Collections;
using UnityEngine;

public class BossDeathJail : MonoBehaviour
{
	public Transform PointFin;

	public GameObject car2;

	public GameObject car3;

	public GameObject _container;

	private Vector3 startCarPoint;

	private bool inFin;

	private void OnEnable()
	{
		inFin = false;
		if (!RaceLogic.instance.BossDeath)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		_container.SetActive(value: true);
		if (Progress.levels.active_boss_pack_last_openned == 1)
		{
			car2.SetActive(value: true);
		}
		else if (Progress.levels.active_boss_pack_last_openned == 2)
		{
			car3.SetActive(value: true);
		}
		else if (Progress.levels.active_boss_pack_last_openned == 3)
		{
			car2.SetActive(value: false);
			car3.SetActive(value: false);
		}
	}

	private IEnumerator pressForMoveBackDelay()
	{
		startCarPoint = RaceLogic.instance.car.gameObject.transform.position;
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			RaceLogic.instance.car.gameObject.transform.eulerAngles = Vector3.zero;
			RaceLogic.instance.car.gameObject.transform.position = Vector3.Lerp(startCarPoint, PointFin.position, 1f - t * t);
			yield return null;
		}
		StartCoroutine(timeToFin());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!inFin && (other.tag == "CarMain" || other.tag == "CarMainChild"))
		{
			inFin = true;
			StartCoroutine(pressForMoveBackDelay());
		}
		if (other.tag == "CarEnemy")
		{
			Car2DAIController componentInParent = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DAIController>();
			if (componentInParent != null)
			{
				componentInParent.Death(withReward: false);
			}
			Car2DControlerForBombCar componentInParent2 = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DControlerForBombCar>();
			if (componentInParent2 != null)
			{
				componentInParent2.Death(reward: false);
			}
		}
	}

	private IEnumerator timeToFin()
	{
		float t = 1f;
		while (t > 0f)
		{
			RaceLogic.instance.car.gameObject.transform.eulerAngles = Vector3.zero;
			RaceLogic.instance.car.gameObject.transform.position = PointFin.position;
			t -= Time.deltaTime;
			yield return null;
		}
		RaceLogic.instance.OnBossFinish();
		while (true)
		{
			RaceLogic.instance.car.gameObject.transform.eulerAngles = Vector3.zero;
			RaceLogic.instance.car.gameObject.transform.position = PointFin.position;
			yield return 0;
		}
	}
}

using UnityEngine;

public class CopterEnemyContrller : MonoBehaviour
{
	public bool PoliceCoter = true;

	public bool PoliceCoterBomb;

	public float speed = 4f;

	private float counter = 35f;

	private Vector3 pointMove = default(Vector3);

	public Vector2 OffsetOfCar = default(Vector2);

	public Vector2 OffsetCarDrop = default(Vector2);

	private float counterRound;

	public float speedRound = 1.5f;

	public float widthRound = 0.5f;

	public float heightRound = 2f;

	[Header("Bomber")]
	public int BombNums = 5;

	public float TimeBetwenBomb = 0.5f;

	public GameObject dropbomb;

	private int tBombNums;

	private float tIter;

	private Vector2 tOffset = Vector2.zero;

	private bool dropedCar;

	private GameObject carForDrop;

	private Car2DControlerForBombCar carForDropContr;

	private Car2DConstructor carForDropConstr;

	private Car2DSuspensionModuleBase carForDropSusp;

	private float counterBomb;

	private bool goRight = true;

	private void OnEnable()
	{
		if (!RaceLogic.instance.AllInitedForPool || RaceLogic.instance == null || RaceLogic.instance.car == null)
		{
			return;
		}
		goRight = true;
		counterBomb = 0f;
		tIter = 0f;
		tOffset = OffsetOfCar;
		Audio.PlayAsync("helicopter", 1f, loop: true);
		if (PoliceCoter)
		{
			carForDrop = Pool.GameOBJECT(Pool.Bonus.policecar, base.transform.position);
			carForDropContr = carForDrop.GetComponent<Car2DControlerForBombCar>();
			if (carForDropContr != null)
			{
				for (int i = 0; i < carForDropContr.WheelsRigitbodies.Count; i++)
				{
					carForDropContr.WheelsRigitbodies[i].isKinematic = true;
				}
				carForDropContr.enabled = false;
			}
			carForDropConstr = carForDrop.GetComponent<Car2DConstructor>();
			if (carForDropConstr != null)
			{
				carForDropConstr.enabled = false;
			}
			carForDropSusp = carForDrop.GetComponent<Car2DSuspensionModuleBase>();
			if (carForDropSusp != null)
			{
				carForDropSusp.RotationAcceleration = 0f;
			}
			carForDrop.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		}
		else
		{
			if (!PoliceCoter && !PoliceCoterBomb)
			{
				counter = 40f;
			}
			tBombNums = BombNums;
		}
	}

	private void Update()
	{
		if (PoliceCoter)
		{
			if (counter > 0f - tOffset.x)
			{
				counter -= Time.deltaTime * speed;
			}
			else
			{
				tIter += Time.deltaTime;
				if (tIter >= 1f)
				{
					if (!dropedCar)
					{
						dropedCar = true;
						if (carForDropContr != null)
						{
							carForDropContr.enabled = true;
							for (int i = 0; i < carForDropContr.WheelsRigitbodies.Count; i++)
							{
								carForDropContr.WheelsRigitbodies[i].isKinematic = false;
							}
						}
						if (carForDropConstr != null)
						{
							carForDropConstr.enabled = true;
						}
						if (carForDropSusp != null)
						{
							carForDropSusp.RotationAcceleration = 250f;
						}
					}
					tOffset = new Vector2(tOffset.x, tOffset.y + Time.deltaTime * 7f);
				}
				else if (tIter >= 4f)
				{
					tIter += 1f;
					Transform transform = base.transform;
					Vector3 position = RaceLogic.instance.car.transform.position;
					float x = position.x - 100f;
					Vector3 position2 = base.transform.position;
					float y = position2.y;
					Vector3 position3 = base.transform.position;
					transform.position = new Vector3(x, y, position3.z);
				}
			}
			if (tIter < 4f)
			{
				Vector3 position4 = RaceLogic.instance.car.transform.position;
				float x2 = position4.x - counter;
				Vector3 position5 = RaceLogic.instance.car.transform.position;
				float y2 = position5.y + tOffset.y;
				Vector3 position6 = base.transform.position;
				pointMove = new Vector3(x2, y2, position6.z);
				counterRound += Time.deltaTime * speedRound;
				Transform transform2 = base.transform;
				float x3 = pointMove.x + Mathf.Cos(counterRound) * widthRound;
				float y3 = pointMove.y + Mathf.Sin(counterRound) * heightRound;
				Vector3 position7 = base.transform.position;
				transform2.position = new Vector3(x3, y3, position7.z);
				if (!dropedCar)
				{
					Transform transform3 = carForDrop.transform;
					Vector3 position8 = base.transform.position;
					float x4 = position8.x + OffsetCarDrop.x;
					Vector3 position9 = base.transform.position;
					float y4 = position9.y + OffsetCarDrop.y;
					Vector3 position10 = base.transform.position;
					transform3.position = new Vector3(x4, y4, position10.z);
					carForDrop.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				}
			}
		}
		else if (!PoliceCoterBomb)
		{
			if (counter > tOffset.x)
			{
				counter -= Time.deltaTime * speed;
			}
			else if (tBombNums > 0)
			{
				counterBomb += Time.deltaTime;
				if (counterBomb >= TimeBetwenBomb)
				{
					tBombNums--;
					counterBomb = 0f;
					Pool.GetCopterBomb(dropbomb.transform.position);
				}
			}
			else
			{
				tOffset = new Vector2(tOffset.x, tOffset.y + Time.deltaTime * 5f);
				tIter += Time.deltaTime;
				if (tIter >= 4f)
				{
					counter = -100f;
				}
			}
			Vector3 position11 = RaceLogic.instance.car.transform.position;
			float x5 = position11.x + counter;
			Vector3 position12 = RaceLogic.instance.car.transform.position;
			float y5 = position12.y + tOffset.y;
			Vector3 position13 = base.transform.position;
			pointMove = new Vector3(x5, y5, position13.z);
			counterRound += Time.deltaTime * speedRound;
			Transform transform4 = base.transform;
			float x6 = pointMove.x + Mathf.Cos(counterRound) * widthRound;
			float y6 = pointMove.y + Mathf.Sin(counterRound) * heightRound;
			Vector3 position14 = base.transform.position;
			transform4.position = new Vector3(x6, y6, position14.z);
		}
		else
		{
			if (!PoliceCoterBomb)
			{
				return;
			}
			if (goRight)
			{
				counter -= Time.deltaTime * speed;
				if (counter < 0f - tOffset.x)
				{
					counter = 0f - tOffset.x;
					goRight = false;
				}
			}
			else
			{
				counter += Time.deltaTime * speed;
				if (counter > tOffset.x)
				{
					counter = tOffset.x;
					goRight = true;
				}
			}
			if (tBombNums > 0)
			{
				counterBomb += Time.deltaTime;
				if (counterBomb >= TimeBetwenBomb)
				{
					tBombNums--;
					counterBomb = 0f;
					GameObject gameObject = Pool.GameOBJECT(Pool.Bombs.bomblip, dropbomb.transform.position);
					gameObject.transform.position = dropbomb.transform.position;
					gameObject.SetActive(value: true);
				}
			}
			else
			{
				counterBomb += Time.deltaTime;
				tOffset = new Vector2(tOffset.x, tOffset.y + Time.deltaTime * 5f);
				if (counterBomb >= 3f)
				{
					base.gameObject.SetActive(value: false);
				}
			}
			Vector3 position15 = RaceLogic.instance.car.transform.position;
			float x7 = position15.x - counter;
			Vector3 position16 = RaceLogic.instance.car.transform.position;
			float y7 = position16.y + tOffset.y;
			Vector3 position17 = base.transform.position;
			pointMove = new Vector3(x7, y7, position17.z);
			counterRound += Time.deltaTime * speedRound;
			Transform transform5 = base.transform;
			float x8 = pointMove.x + Mathf.Cos(counterRound) * widthRound;
			float y8 = pointMove.y + Mathf.Sin(counterRound) * heightRound;
			Vector3 position18 = base.transform.position;
			transform5.position = new Vector3(x8, y8, position18.z);
		}
	}

	private void OnDisable()
	{
		if ((bool)RaceLogic.instance && (bool)RaceLogic.instance.car)
		{
			Audio.Stop("helicopter");
		}
	}
}

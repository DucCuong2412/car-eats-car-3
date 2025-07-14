using System.Collections.Generic;
using UnityEngine;

public class Car2DConstructor : MonoBehaviour
{
	public bool isAhead = true;

	public bool IsCivic;

	public bool isConvoi;

	public bool InitAiOnStart;

	private bool Inited;

	[Range(1f, 32f)]
	public int carType = 2;

	[Range(0f, 2f)]
	public int guns;

	[Range(0f, 5f)]
	public int rams;

	public int location = 1;

	[Header("Scraps")]
	public int colScraps = 1;

	public string scrap1;

	public string scrap2;

	public string scrap3;

	public string scrap4;

	public string scrap5;

	public Vector3 _RGB;

	[Header("Scraps pru ykyse")]
	public string scrap1y;

	public string scrap2y;

	public string scrap3y;

	public string scrap4y;

	public string scrap5y;

	public int CollRubyForYkys;

	public int CollRubyForVzruv;

	private GameObject[] bodiesForLocation = new GameObject[33];

	public List<GameObject> RamsForLocForward = new List<GameObject>();

	public List<GameObject> RamsForLocBackward = new List<GameObject>();

	public List<GameObject> GunForLocForward = new List<GameObject>();

	public List<GameObject> GunForLocBackward = new List<GameObject>();

	private List<GameObject>[] wheelsForLocations = new List<GameObject>[20];

	private List<Vector3>[] wheelsPosForLocations = new List<Vector3>[20];

	private int numCarForLoc;

	private static string strCopter = "Copter";

	private GameObject weaponyGun;

	private GameObject mas_gun;

	private static string StrEnemy_for_me = "Enemy_for_me";

	private static string Strbomb_car = "bomb_car";

	private GameObject weaponKonentorsGun;

	private GameObject gun;

	private GameObject weapony;

	private GameObject mas_ram;

	private bool findRam = true;

	private GameObject weaponKonentors;

	private GameObject RAM;

	private int CArTypeforME;

	public void Turn(bool forward)
	{
		if (forward != isAhead)
		{
			isAhead = forward;
			if (carType > 4)
			{
				IsCivic = true;
			}
			if (location == 1 && numCarForLoc != 3)
			{
				numCarForLoc = 3;
			}
			else if (location == 2 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			else if (location == 3 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			SwapBody();
			if (!base.name.Contains(strCopter) && !IsCivic)
			{
				setRam(rams);
				setGun(guns);
			}
		}
	}

	public void SwapBody()
	{
		if (base.name.Contains(strCopter))
		{
			base.transform.localScale = new Vector3(isAhead ? 1 : (-1), 1f, 1f);
		}
		else if (isAhead)
		{
			if (IsCivic)
			{
				if (Progress.shop.EsterLevelPlay)
				{
					numCarForLoc = 3;
				}
				FindBodies();
				bodiesForLocation[carType].SetActive(value: true);
				if (isConvoi)
				{
					bodiesForLocation[carType + 4].SetActive(value: false);
				}
				else
				{
					bodiesForLocation[carType + numCarForLoc].SetActive(value: false);
				}
				CArTypeforME = carType;
			}
			else
			{
				bodiesForLocation[carType].SetActive(value: true);
				bodiesForLocation[carType + 4].SetActive(value: false);
				CArTypeforME = carType;
			}
		}
		else if (IsCivic)
		{
			if (Progress.shop.EsterLevelPlay)
			{
				numCarForLoc = 3;
			}
			FindBodies();
			CArTypeforME = carType + numCarForLoc;
			bodiesForLocation[carType + numCarForLoc].SetActive(value: true);
			bodiesForLocation[carType].SetActive(value: false);
		}
		else
		{
			CArTypeforME = carType + 4;
			bodiesForLocation[carType + 4].SetActive(value: true);
			bodiesForLocation[carType].SetActive(value: false);
		}
	}

	public void SetCar(int car)
	{
		if (carType > 4)
		{
			IsCivic = true;
		}
		if (location == 1 && numCarForLoc != 3)
		{
			numCarForLoc = 3;
		}
		else if (location == 2 && numCarForLoc != 4)
		{
			numCarForLoc = 4;
		}
		else if (location == 3 && numCarForLoc != 4)
		{
			numCarForLoc = 4;
		}
		carType = car;
		FindCar();
		SetBody(car);
		SetWheel(car);
	}

	public void finalSetRam(int ram)
	{
		if (!IsCivic && !base.name.Contains(strCopter))
		{
			find_ram();
			setRam(ram);
		}
	}

	public void finalSetgun(int gun)
	{
		if (!IsCivic && !base.name.Contains(strCopter))
		{
			find_gun();
			setGun(gun);
		}
	}

	public void SetWheel(int car)
	{
		int num = 4;
		if (IsCivic)
		{
			if (location == 1 && numCarForLoc != 3)
			{
				numCarForLoc = 3;
			}
			else if (location == 2 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			else if (location == 3 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			num = numCarForLoc;
		}
		for (int i = 1; i <= num; i++)
		{
			if (wheelsForLocations[i] != null)
			{
				for (int j = 0; j < wheelsForLocations[i].Count; j++)
				{
					wheelsForLocations[i][j].GetComponent<Rigidbody2D>().isKinematic = true;
					wheelsForLocations[i][j].SetActive(value: false);
				}
			}
		}
		if (wheelsForLocations[car] != null)
		{
			for (int k = 0; k < wheelsForLocations[car].Count; k++)
			{
				wheelsForLocations[car][k].SetActive(value: true);
				wheelsForLocations[car][k].GetComponent<Rigidbody2D>().isKinematic = false;
				Transform transform = wheelsForLocations[car][k].transform;
				float num2 = isAhead ? 1 : (-1);
				Vector3 vector = wheelsPosForLocations[car][k];
				float num3 = (num2 == vector.z) ? 1 : (-1);
				Vector3 vector2 = wheelsPosForLocations[car][k];
				float x = num3 * vector2.x;
				Vector3 vector3 = wheelsPosForLocations[car][k];
				transform.localPosition = new Vector2(x, vector3.y);
			}
		}
	}

	public void SetBody(int car)
	{
		GameObject[] array = bodiesForLocation;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
		if (bodiesForLocation[car] != null)
		{
			bodiesForLocation[car].SetActive(value: true);
			SwapBody();
		}
	}

	private void FindCar()
	{
		if (carType > 4)
		{
			IsCivic = true;
		}
		if (IsCivic)
		{
			if (location == 1 && numCarForLoc != 3)
			{
				numCarForLoc = 3;
			}
			else if (location == 2 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			else if (location == 3 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
		}
		FindWheels();
		FindBodies();
		Inited = true;
	}

	private void FindWheels()
	{
		List<Transform> list = new List<Transform>();
		Rigidbody2D[] componentsInChildren = base.gameObject.GetComponentsInChildren<Rigidbody2D>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.GetComponent<CircleCollider2D>() != null)
			{
				list.Add(componentsInChildren[i].transform);
			}
		}
		int num = 4;
		if (IsCivic)
		{
			if (location == 1 && numCarForLoc != 3)
			{
				numCarForLoc = 3;
			}
			else if (location == 2 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			else if (location == 3 && numCarForLoc != 4)
			{
				numCarForLoc = 4;
			}
			num = numCarForLoc;
		}
		for (int j = 1; j <= num; j++)
		{
			foreach (Transform item in list)
			{
				if (j < 10)
				{
					if (item.name.Contains(string.Format("{0}", "0" + j)))
					{
						if (wheelsForLocations[j] == null)
						{
							wheelsForLocations[j] = new List<GameObject>();
						}
						wheelsForLocations[j].Add(item.gameObject);
						if (wheelsPosForLocations[j] == null)
						{
							wheelsPosForLocations[j] = new List<Vector3>();
						}
						List<Vector3> obj = wheelsPosForLocations[j];
						Vector3 localPosition = wheelsForLocations[j][wheelsForLocations[j].Count - 1].transform.localPosition;
						float x = localPosition.x;
						Vector3 localPosition2 = wheelsForLocations[j][wheelsForLocations[j].Count - 1].transform.localPosition;
						obj.Add(new Vector3(x, localPosition2.y, isAhead ? 1 : (-1)));
					}
				}
				else if (item.name.Contains($"{j}"))
				{
					if (wheelsForLocations[j] == null)
					{
						wheelsForLocations[j] = new List<GameObject>();
					}
					wheelsForLocations[j].Add(item.gameObject);
					if (wheelsPosForLocations[j] == null)
					{
						wheelsPosForLocations[j] = new List<Vector3>();
					}
					List<Vector3> obj2 = wheelsPosForLocations[j];
					Vector3 localPosition3 = wheelsForLocations[j][wheelsForLocations[j].Count - 1].transform.localPosition;
					float x2 = localPosition3.x;
					Vector3 localPosition4 = wheelsForLocations[j][wheelsForLocations[j].Count - 1].transform.localPosition;
					obj2.Add(new Vector3(x2, localPosition4.y, isAhead ? 1 : (-1)));
				}
			}
		}
	}

	private void FindBodies()
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < base.transform.childCount; i++)
		{
			list.Add(base.transform.GetChild(i));
			for (int j = 0; j < base.transform.GetChild(i).childCount; j++)
			{
				list.Add(base.transform.GetChild(i).GetChild(j));
			}
		}
		if (IsCivic)
		{
			for (int k = 1; k <= numCarForLoc * 2; k++)
			{
				foreach (Transform item in list)
				{
					if (k < 10)
					{
						if (item.name.Contains(string.Format("{0}", "0" + k)))
						{
							bodiesForLocation[k] = item.gameObject;
						}
					}
					else if (item.name.Contains($"{k}"))
					{
						bodiesForLocation[k] = item.gameObject;
					}
				}
			}
			return;
		}
		for (int l = 1; l <= 8; l++)
		{
			foreach (Transform item2 in list)
			{
				if (item2.name.Contains($"{l}"))
				{
					bodiesForLocation[l] = item2.gameObject;
				}
			}
			if (bodiesForLocation[l] == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = $"{l}";
				bodiesForLocation[l] = gameObject;
			}
		}
	}

	public void find_gun()
	{
		if (base.transform.name.Contains(StrEnemy_for_me) || base.transform.name.Contains("Enemy_easter") || base.transform.name.Contains("enemy_underground_01") || base.transform.name.Contains("enemy_underground_02") || base.transform.parent.name.Contains(Strbomb_car) || base.transform.parent.name.Contains("Police_car") || base.transform.parent.name.Contains("Undergound_car_boss") || base.transform.name.Contains("Undergound2_car_boss"))
		{
			return;
		}
		Transform[] componentsInChildren = bodiesForLocation[carType].transform.parent.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == "weaponry")
			{
				weaponyGun = transform.gameObject;
			}
		}
		Transform[] componentsInChildren2 = weaponyGun.GetComponentsInChildren<Transform>();
		Transform[] array2 = componentsInChildren2;
		foreach (Transform transform2 in array2)
		{
			if (transform2.name == "guns")
			{
				mas_gun = transform2.gameObject;
			}
		}
		Transform[] componentsInChildren3 = mas_gun.GetComponentsInChildren<Transform>();
		Transform[] array3 = componentsInChildren3;
		foreach (Transform transform3 in array3)
		{
			if (transform3.name.Contains("_f"))
			{
				GunForLocForward.Add(transform3.gameObject);
			}
			else if (transform3.name.Contains("_b"))
			{
				GunForLocBackward.Add(transform3.gameObject);
			}
		}
		GunForLocForward.Add(null);
		GunForLocBackward.Add(null);
	}

	public void setGun(int numbergun)
	{
		if (base.transform.name.Contains("bomb_car") || base.transform.name.Contains("Enemy_easter") || base.transform.name.Contains("Police_car") || base.transform.name.Contains("Enemy_for_me") || base.transform.name.Contains("small") || base.transform.name.Contains("enemy_underground_01") || base.transform.name.Contains("enemy_underground_02") || base.transform.name.Contains("Undergound_car_boss") || base.transform.name.Contains("Undergound2_car_boss"))
		{
			return;
		}
		foreach (GameObject item in GunForLocBackward)
		{
			if (item != null)
			{
				item.SetActive(value: false);
			}
		}
		foreach (GameObject item2 in GunForLocForward)
		{
			if (item2 != null)
			{
				item2.SetActive(value: false);
			}
		}
		Transform[] componentsInChildren = bodiesForLocation[CArTypeforME].GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == "weapon_connectors")
			{
				weaponKonentorsGun = transform.gameObject;
			}
		}
		Transform[] componentsInChildren2 = weaponKonentorsGun.GetComponentsInChildren<Transform>();
		Transform[] array2 = componentsInChildren2;
		foreach (Transform transform2 in array2)
		{
			if (transform2.name == "guns")
			{
				gun = transform2.gameObject;
			}
		}
		Transform[] componentsInChildren3 = gun.GetComponentsInChildren<Transform>();
		Transform[] array3 = componentsInChildren3;
		foreach (Transform transform3 in array3)
		{
			if (GunForLocBackward[numbergun] != null && transform3.name == GunForLocBackward[numbergun].name)
			{
				Vector3 position = transform3.transform.InverseTransformPoint(transform3.transform.position);
				GunForLocBackward[numbergun].transform.SetParent(transform3.transform);
				GunForLocBackward[numbergun].SetActive(value: true);
				GunForLocBackward[numbergun].transform.position = position;
				GunForLocBackward[numbergun].transform.localPosition = Vector3.zero;
			}
		}
		Transform[] array4 = componentsInChildren3;
		foreach (Transform transform4 in array4)
		{
			if (GunForLocForward[numbergun] != null && transform4.name == GunForLocForward[numbergun].name)
			{
				Vector3 position2 = transform4.transform.InverseTransformPoint(transform4.transform.position);
				GunForLocForward[numbergun].transform.SetParent(transform4.transform);
				GunForLocForward[numbergun].SetActive(value: true);
				GunForLocForward[numbergun].transform.position = position2;
				GunForLocForward[numbergun].transform.localPosition = Vector3.zero;
			}
		}
	}

	public void find_ram()
	{
		if (base.transform.name.Contains("Enemy_for_me") || base.transform.name.Contains("Enemy_easter") || base.transform.name.Contains("enemy_underground_01") || base.transform.name.Contains("enemy_underground_02") || base.transform.name.Contains("Undergound_car_boss") || base.transform.name.Contains("Undergound2_car_boss") || base.transform.parent.name.Contains("bomb_car") || base.transform.parent.name.Contains("Police_car"))
		{
			return;
		}
		Transform[] componentsInChildren = bodiesForLocation[carType].transform.parent.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == "weaponry")
			{
				weapony = transform.gameObject;
			}
		}
		Transform[] componentsInChildren2 = weapony.GetComponentsInChildren<Transform>();
		Transform[] array2 = componentsInChildren2;
		foreach (Transform transform2 in array2)
		{
			if (transform2.name == "rams")
			{
				mas_ram = transform2.gameObject;
			}
		}
		Transform[] componentsInChildren3 = mas_ram.GetComponentsInChildren<Transform>();
		Transform[] array3 = componentsInChildren3;
		foreach (Transform transform3 in array3)
		{
			if (transform3.name.Contains("_f"))
			{
				RamsForLocForward.Add(transform3.gameObject);
			}
			else if (transform3.name.Contains("_b"))
			{
				RamsForLocBackward.Add(transform3.gameObject);
			}
		}
		RamsForLocForward.Add(null);
		RamsForLocBackward.Add(null);
		findRam = false;
	}

	public void setRam(int numberRam)
	{
		if (base.transform.name.Contains("bomb_car") || base.transform.name.Contains("Police_car") || base.transform.name.Contains("Enemy_for_me") || base.transform.name.Contains("Enemy_easter") || base.transform.name.Contains("enemy_underground_01") || base.transform.name.Contains("enemy_underground_02") || base.transform.name.Contains("Undergound_car_boss") || base.transform.name.Contains("Undergound2_car_boss"))
		{
			return;
		}
		foreach (GameObject item in RamsForLocBackward)
		{
			if (item != null)
			{
				item.SetActive(value: false);
			}
		}
		foreach (GameObject item2 in RamsForLocForward)
		{
			if (item2 != null)
			{
				item2.SetActive(value: false);
			}
		}
		Transform[] componentsInChildren = bodiesForLocation[CArTypeforME].GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == "weapon_connectors")
			{
				weaponKonentors = transform.gameObject;
			}
		}
		if (!weaponKonentors)
		{
			return;
		}
		Transform[] componentsInChildren2 = weaponKonentors.GetComponentsInChildren<Transform>();
		Transform[] array2 = componentsInChildren2;
		foreach (Transform transform2 in array2)
		{
			if (transform2.name == "rams")
			{
				RAM = transform2.gameObject;
			}
		}
		Transform[] componentsInChildren3 = RAM.GetComponentsInChildren<Transform>();
		Transform[] array3 = componentsInChildren3;
		foreach (Transform transform3 in array3)
		{
			if (RamsForLocBackward[numberRam] != null && transform3.name == RamsForLocBackward[numberRam].name)
			{
				RamsForLocBackward[numberRam].transform.position = transform3.transform.position;
				RamsForLocBackward[numberRam].SetActive(value: true);
			}
		}
		Transform[] array4 = componentsInChildren3;
		foreach (Transform transform4 in array4)
		{
			if (RamsForLocForward[numberRam] != null && transform4.name == RamsForLocForward[numberRam].name)
			{
				RamsForLocForward[numberRam].transform.position = transform4.transform.position;
				RamsForLocForward[numberRam].SetActive(value: true);
			}
		}
	}

	public void Update()
	{
	}
}

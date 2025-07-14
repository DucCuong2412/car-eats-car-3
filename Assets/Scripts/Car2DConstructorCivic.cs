using System.Collections.Generic;
using UnityEngine;

public class Car2DConstructorCivic : MonoBehaviour
{
	public bool isAhead = true;

	public bool IsCivic;

	public bool isConvoi;

	public bool InitAiOnStart;

	private bool Inited;

	[Range(1f, 32f)]
	public int carType = 1;

	public int location = 1;

	[Header("Scraps")]
	public int colScraps = 1;

	public string scrap1;

	public string scrap2;

	public string scrap3;

	public string scrap4;

	public string scrap5;

	public string scrap1y;

	public string scrap2y;

	public string scrap3y;

	public string scrap4y;

	public string scrap5y;

	[HideInInspector]
	public Vector3 _RGB;

	public int CollRubyForYkys;

	public int CollRubyForVzruv;

	private int numCarForLoc;

	private GameObject[] bodiesForLocation = new GameObject[33];

	private List<GameObject>[] wheelsForLocations = new List<GameObject>[20];

	private List<Vector3>[] wheelsPosForLocations = new List<Vector3>[20];

	private static string strCopter = "Copter";

	public void Turn(bool forward)
	{
		if (forward != isAhead)
		{
			isAhead = forward;
			SwapBody();
		}
	}

	public void SwapWheels()
	{
	}

	public void SwapBody()
	{
		if (base.name.Contains(strCopter))
		{
			base.transform.localScale = new Vector3(isAhead ? 1 : (-1), 1f, 1f);
		}
		else if (isAhead)
		{
			bodiesForLocation[carType].SetActive(value: true);
			bodiesForLocation[carType + numCarForLoc].SetActive(value: false);
		}
		else
		{
			bodiesForLocation[carType + numCarForLoc].SetActive(value: true);
			bodiesForLocation[carType].SetActive(value: false);
		}
	}

	public void SetCar(int car)
	{
		carType = car;
		FindCar();
		SetBody(car);
		SetWheel(car);
	}

	public void SetWheel(int car)
	{
		for (int i = 1; i <= numCarForLoc; i++)
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
		if (!Inited)
		{
			FindWheels();
			FindBodies();
			Inited = true;
		}
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
		for (int j = 1; j <= numCarForLoc; j++)
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
			if (bodiesForLocation[k] == null)
			{
				GameObject gameObject = new GameObject();
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.name = $"{k}";
				bodiesForLocation[k] = gameObject;
			}
		}
	}

	public void Update()
	{
	}
}

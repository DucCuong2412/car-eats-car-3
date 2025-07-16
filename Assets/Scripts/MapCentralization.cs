using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCentralization : MonoBehaviour
{
	[Serializable]
	public class loc
	{
		public float namelvl;

		public Vector2 pos;
	}

	public bool Underground;

	//public Animator CrossPromo1;

	//public Animator CrossPromo2;

	public Camera Cameras;

	public ScrollRect SB;

	public List<loc> loc1 = new List<loc>();

	public List<loc> loc2 = new List<loc>();

	public List<loc> loc3 = new List<loc>();

	public Vector2 boos1;

	public Vector2 boos2;

	public Vector2 boos3;

	public List<loc> SpecialMission = new List<loc>();

	public List<CellContainer> CC = new List<CellContainer>();

	[Header("cootrrdinats")]
	public Vector2 coord;

	[Header("Missions Kompas")]
	public GameObject KompasObj;

	public GameObject KompasArrow;

	[Header("Promo Kompas")]
	public GameObject KompasPromo1Obj;

	public GameObject KompasPromo1Arrow;

	public GameObject KompasPromo2Obj;

	public GameObject KompasPromo2Arrow;

	[Header("Egg Kompas")]
	public GameObject KompasEggObj;

	public GameObject KompasEggArrow;

	private int is_active = Animator.StringToHash("is_active");

	private Vector2 vec = new Vector2(-300f, 0f);

	private bool _visEggArrow;

	private static float _borderTop = 600f;

	private static float _borderRight = 300f;

	private float _borderTopPerc = _borderTop / 100f;

	private float _borderRightPerc = _borderRight / 100f;

	private float percNum = 2.22222233f;

	private float minusAng = -1f;

	private float t1 = -1f;

	private Vector3 vec3Pos = Vector3.zero;

	private float ang = -1f;

	private bool _animOpened1;

	private bool _animOpened2;

	private Vector2 _posPromo1 = new Vector2(2500f, -450f);

	private Vector2 _posPromo2 = new Vector2(2550f, 400f);

	private Vector2 _posEgg = new Vector2(1200f, -100f);

	private void OnEnable()
	{
		StartCoroutine(onStart());
		_visEggArrow = false;
		if (Progress.levels.ResultBoxRev_Undeground1_Adds != null && Progress.levels.ResultBoxRev_Undeground1_Adds.Count > 0)
		{
			int count = Progress.levels.ResultBoxRev_Undeground1_Adds.Count;
			for (int i = 0; i < count; i++)
			{
				if (Progress.levels.ResultBoxRev_Undeground1_Adds[i])
				{
					_visEggArrow = true;
					break;
				}
			}
		}
		if ((bool)KompasEggObj)
		{
			if (_visEggArrow)
			{
				KompasEggObj.SetActive(value: true);
			}
			else
			{
				KompasEggObj.SetActive(value: false);
			}
		}
	}

	private IEnumerator onStart()
	{
		yield return 0;
		if (Progress.levels.InUndeground)
		{
			vec = new Vector2(0f, 0f);
		}
		else
		{
			vec = new Vector2(-300f, 0f);
		}
		int lvl = 1;
		foreach (CellContainer item in CC)
		{
			if (item.state == CellContainer.State.Active)
			{
				lvl = Utilities.LevelNumberGlobal(item.Level, item.Pack);
			}
		}
		for (int i = 0; i < loc1.Count; i++)
		{
			if (loc1[i].namelvl == (float)lvl)
			{
				if (lvl >= 0 && lvl <= 2)
				{
					SB.content.anchoredPosition = loc1[i].pos;
				}
				SB.content.anchoredPosition = loc1[i].pos + vec;
			}
		}
		for (int j = 0; j < loc2.Count; j++)
		{
			if (loc2[j].namelvl == (float)lvl)
			{
				SB.content.anchoredPosition = loc2[j].pos + vec;
			}
		}
		for (int k = 0; k < loc3.Count; k++)
		{
			if (loc3[k].namelvl == (float)lvl)
			{
				SB.content.anchoredPosition = loc3[k].pos + vec;
			}
		}
		if (Progress.shop.ActivCellNum == -1)
		{
			KompasObj.SetActive(value: false);
		}
		else
		{
			KompasObj.SetActive(value: true);
		}
	}

	public void PressPromo1()
	{
		AnalyticsManager.LogEvent(EventCategoty.crosspromo, "map", "creatscar1");
		Application.OpenURL("market://details?id=com.SpilGames.CarEatsCar");
	}

	public void PressPromo2()
	{
		AnalyticsManager.LogEvent(EventCategoty.crosspromo, "map", "creatscar2");
		Application.OpenURL("market://details?id=com.spilgames.CarEatsCar2");
	}

	private void Update()
	{
		if (Cameras != null)
		{
			if ((double)Cameras.aspect < 1.51)
			{
				if (Screen.width == 1024 && Screen.height == 768)
				{
					_borderRight = (float)Screen.width / 2f;
					_borderTop = (float)Screen.height / 2f;
				}
				else
				{
					_borderRight = (float)Screen.width / 1.5f;
					_borderTop = (float)Screen.height / 1.5f;
				}
			}
			else
			{
				_borderRight = Screen.width;
				_borderTop = Screen.height;
			}
		}
		coord = SB.content.anchoredPosition;
		if (!Underground && Progress.levels.Pack(1).Level(2).isOpen)
		{
			if (coord.x > 2100f)
			{
				if (coord.y < -100f && !_animOpened1)
				{
					//_animOpened1 = true;
					//CrossPromo1.SetBool(is_active, value: true);
					//Progress.shop.Promo1Show = false;
				}
				else if (coord.y >= -100f && _animOpened1)
				{
					//_animOpened1 = false;
					//CrossPromo1.SetBool(is_active, value: false);
				}
			}
			else if (_animOpened1)
			{
				//_animOpened1 = false;
				//CrossPromo1.SetBool(is_active, value: false);
			}
			if (coord.x > 2300f)
			{
				if (coord.y > 200f && !_animOpened2)
				{
					//_animOpened2 = true;
					//CrossPromo2.SetBool(is_active, value: true);
					//Progress.shop.Promo2Show = false;
				}
				else if (coord.y <= 200f && _animOpened2)
				{
					//_animOpened2 = false;
					//CrossPromo2.SetBool(is_active, value: false);
				}
			}
			else if (_animOpened2)
			{
				//	_animOpened2 = false;
				//	CrossPromo2.SetBool(is_active, value: false);
			}
			if (_visEggArrow)
			{
				if (Vector3.Distance(coord, _posEgg) < 700f)
				{
					KompasEggObj.SetActive(value: false);
				}
				else
				{
					KompasEggObj.SetActive(value: true);
					ang = getAngle(coord, _posEgg);
					KompasEggArrow.transform.eulerAngles = new Vector3(0f, 0f, ang);
					AngOpt();
					KompasEggObj.transform.localPosition = vec3Pos;
				}
			}
			if (Progress.shop.Promo1Show)
			{
				if (coord.x > 2100f && coord.y < -100f)
				{
					KompasPromo1Obj.SetActive(value: false);
				}
				else
				{
					KompasPromo1Obj.SetActive(value: true);
					ang = getAngle(coord, _posPromo1);
					KompasPromo1Arrow.transform.eulerAngles = new Vector3(0f, 0f, ang);
					AngOpt();
					KompasPromo1Obj.transform.localPosition = vec3Pos;
				}
			}
			else if (KompasPromo1Obj.activeSelf)
			{
				KompasPromo1Obj.SetActive(value: false);
			}
			if (Progress.shop.Promo2Show)
			{
				if (coord.x > 2300f && coord.y > 200f)
				{
					KompasPromo2Obj.SetActive(value: false);
				}
				else
				{
					KompasPromo2Obj.SetActive(value: true);
					ang = getAngle(coord, _posPromo2);
					KompasPromo2Arrow.transform.eulerAngles = new Vector3(0f, 0f, ang);
					AngOpt();
					KompasPromo2Obj.transform.localPosition = vec3Pos;
				}
			}
			else if (KompasPromo2Obj.activeSelf)
			{
				KompasPromo2Obj.SetActive(value: false);
			}
			if (Progress.shop.ActivCellNum != -1)
			{
				if (Vector3.Distance(coord, SpecialMission[Progress.shop.ActivCellNum].pos + vec) < 700f)
				{
					KompasObj.SetActive(value: false);
					return;
				}
				KompasObj.SetActive(value: true);
				ang = getAngle(coord, SpecialMission[Progress.shop.ActivCellNum].pos + vec);
				KompasArrow.transform.eulerAngles = new Vector3(0f, 0f, ang);
				AngOpt();
				KompasObj.transform.localPosition = vec3Pos;
			}
			else if (KompasObj.activeSelf)
			{
				KompasObj.SetActive(value: false);
			}
		}
		else
		{
			if (KompasObj != null && KompasObj.activeSelf)
			{
				KompasObj.SetActive(value: false);
			}
			if (KompasPromo1Obj != null && KompasPromo1Obj.activeSelf)
			{
				KompasPromo1Obj.SetActive(value: false);
			}
			if (KompasPromo2Obj != null && KompasPromo2Obj.activeSelf)
			{
				KompasPromo2Obj.SetActive(value: false);
			}
			if (KompasEggObj != null && KompasEggObj.activeSelf)
			{
				KompasEggObj.SetActive(value: false);
			}
		}
	}

	private void AngOpt()
	{
		if (ang > 45f && ang <= 135f)
		{
			if (ang > 45f && ang <= 90f)
			{
				minusAng = 90f - ang;
				t1 = minusAng * percNum * _borderTopPerc;
				vec3Pos = new Vector3(t1, _borderRight);
			}
			else
			{
				minusAng = 135f - ang;
				t1 = minusAng * percNum * _borderTopPerc;
				vec3Pos = new Vector3(0f - (_borderTop - t1), _borderRight);
			}
		}
		else if (ang > 135f || ang <= -135f)
		{
			if (ang > 135f && ang <= 180f)
			{
				minusAng = 180f - ang;
				t1 = minusAng * percNum * _borderRightPerc;
				vec3Pos = new Vector3(0f - _borderTop, t1);
			}
			else
			{
				minusAng = 180f + ang;
				t1 = minusAng * percNum * _borderRightPerc;
				vec3Pos = new Vector3(0f - _borderTop, 0f - t1);
			}
		}
		else if (ang > -135f && ang <= -45f)
		{
			if (ang <= -45f && ang > -90f)
			{
				minusAng = 90f + ang;
				t1 = minusAng * percNum * _borderTopPerc;
				vec3Pos = new Vector3(t1, 0f - _borderRight);
			}
			else
			{
				minusAng = 135f + ang;
				t1 = minusAng * percNum * _borderTopPerc;
				vec3Pos = new Vector3(0f - (_borderTop - t1), 0f - _borderRight);
			}
		}
		else if (ang > -45f && ang <= 45f)
		{
			if (ang <= 45f && ang > 0f)
			{
				t1 = ang * percNum * _borderRightPerc;
				vec3Pos = new Vector3(_borderTop, t1);
			}
			else
			{
				t1 = ang * percNum * _borderRightPerc;
				vec3Pos = new Vector3(_borderTop, t1);
			}
		}
	}

	private float getAngle(Vector2 startPos, Vector2 endPos)
	{
		return (0f - Vector2.Angle(Vector2.right, startPos - endPos)) * (float)((!(startPos.y > endPos.y)) ? 1 : (-1));
	}

	private float Angle(Vector2 a, Vector2 b)
	{
		Vector2 normalized = a.normalized;
		Vector2 normalized2 = b.normalized;
		float x = normalized.x * normalized2.x + normalized.y * normalized2.y;
		float y = normalized.y * normalized2.x - normalized.x * normalized2.y;
		return Mathf.Atan2(y, x) * 57.29578f;
	}
}

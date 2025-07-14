using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DAIController : MonoBehaviour
{
	public delegate void ValueChangedDelegate(float value);

	public delegate void DieDelegate();

	[Serializable]
	public class HubComplect
	{
		public List<Vector2> HUBs = new List<Vector2>();

		public int carType = -1;

		public bool isAhead;

		public bool isAheadDeath;
	}

	[Serializable]
	public class CIVIC
	{
		[Header("HEALTH")]
		public float BaseValue;

		[Header("TURBO")]
		public float Angle;

		public float MaxValue;

		public float Value;

		public float UsageValue;

		public float Power;

		public float RestoreTime;

		[Header("SUSPANSION")]
		public float BodyMass;

		public float WheelMass;

		public float SpringMass;

		public float Distance;

		public float Frequency;

		public float WheelsFriction;

		public float Damping;

		public float RotationAcceleration;

		[Header("SPEED")]
		public float MaxSpeed;

		public float Torque;

		[Header("REWARD")]
		public int Reward;
	}

	public class Hub
	{
		public SpringJoint2D Spring;

		public SliderJoint2D Slider;

		public HingeJoint2D Hinge;
	}

	[HideInInspector]
	private Car2DEngineModuleBase _enginemodule;

	[HideInInspector]
	private ZamorozkaScript _zs;

	[HideInInspector]
	private Car2DHealthModuleBase _healthmodule;

	[HideInInspector]
	private Car2DSuspensionModuleBase _suspensionmodule;

	[HideInInspector]
	private Car2DPowerModuleBase _turbomodule;

	public bool AnDeathConvoi;

	private GameObject body;

	private EatSensor eatSensor;

	[HideInInspector]
	public float EatDamage = 1f;

	private float Explosive;

	private float eat_delay = 0.5f;

	private float reward;

	[HideInInspector]
	public float InvisibleSpeed;

	private Transform _target;

	public List<GameObject> ConvoiOffObjs = new List<GameObject>();

	public List<GameObject> ConvoiCars = new List<GameObject>();

	public bool freeze;

	private static string strWheels = "Wheels";

	private List<Rigidbody2D> wheelsrigitbodyes;

	public tk2dSprite BodySprite;

	public int CollRubyForYkys;

	public int CollRubyForVzruv;

	public List<GameObject> BodySpritedamage = new List<GameObject>();

	private bool set100Perc;

	private bool set60Perc;

	private bool set30Perc;

	private static string strWheel = "Wheel";

	private static string strcarHP = "carHP";

	private Transform _healthLabel;

	private Transform _healthLabelPoolTrabsform;

	private static string str_helthBar_connector = "helthBar_connector";

	private Transform _healthLine;

	private Vector3 _yoffset = new Vector3(0f, 0f, 0f);

	private Car2DConstructor _const;

	private bool _enabled;

	private bool _iskinematic;

	private static string ustr_Civilians = "Civilians";

	public bool isConvoi;

	private List<HubComplect> tHUB_SJ = new List<HubComplect>();

	public Car2DCivilNEW CarTupe1b;

	public Car2DCivilNEW CarTupe1f;

	public Car2DCivilNEW CarTupe2b;

	public Car2DCivilNEW CarTupe2f;

	public Car2DCivilNEW CarTupe3b;

	public Car2DCivilNEW CarTupe3f;

	public Car2DCivilNEW CarTupe4b;

	public Car2DCivilNEW CarTupe4f;

	[HideInInspector]
	public bool BigToSmall;

	private bool ColorSeted;

	public int inCarType = -1;

	public int Colorindex = -1;

	private int _carMod = Animator.StringToHash("carMod");

	private int tHUBLastIndex = -1;

	public bool IsCivic = true;

	public CIVIC civic;

	private static string str_actor_roar_last_boss = "actor_roar_last_boss";

	private static string str_roar_0 = "actor_roar_0";

	private static string str_sn = "_sn";

	private int tNum;

	public bool NotCreateHP;

	private bool isAhead;

	private Hub _firstHub;

	private Hub _secondHub;

	private float returntime = 1f;

	private float timeleft;

	public tk2dSprite bodyspriteforw;

	public List<GameObject> BodySpritedamageforward = new List<GameObject>();

	private bool lastTorn;

	private static string strdmg_0 = "dmg_0";

	private static string strdmg_1 = "dmg_1";

	private static string strdmg_2 = "dmg_2";

	private float eatTime;

	private bool can_eat = true;

	private Rigidbody2D r;

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string actor_bite_01 = "actor_bite_01";

	[Header("Dont toch this")]
	public float Temp;

	[Header("Dont toch this 2")]
	public float TempToo;

	public Car2DEngineModuleBase EngineModule
	{
		get
		{
			if (_enginemodule == null)
			{
				_enginemodule = base.gameObject.AddComponent<Car2DEngineModuleBase>();
			}
			return _enginemodule;
		}
	}

	public ZamorozkaScript ZS
	{
		get
		{
			if (_zs == null)
			{
				_zs = base.gameObject.AddComponent<ZamorozkaScript>();
			}
			return _zs;
		}
	}

	public Car2DHealthModuleBase HealthModule
	{
		get
		{
			if (_healthmodule == null)
			{
				_healthmodule = base.gameObject.AddComponent<Car2DHealthModuleBase>();
			}
			return _healthmodule;
		}
	}

	public Car2DSuspensionModuleBase SuspensionModule
	{
		get
		{
			if (_suspensionmodule == null)
			{
				_suspensionmodule = base.gameObject.AddComponent<Car2DSuspensionModuleBase>();
			}
			return _suspensionmodule;
		}
	}

	public Car2DPowerModuleBase TurboModule
	{
		get
		{
			if (_turbomodule == null)
			{
				_turbomodule = base.gameObject.AddComponent<Car2DPowerModuleBase>();
			}
			return _turbomodule;
		}
	}

	public List<Rigidbody2D> WheelsRigitbodies
	{
		get
		{
			CircleCollider2D[] componentsInChildren = GetComponentsInChildren<CircleCollider2D>();
			List<Rigidbody2D> list = new List<Rigidbody2D>();
			Rigidbody2D rigidbody2D = null;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				rigidbody2D = componentsInChildren[i].GetComponent<Rigidbody2D>();
				if (!componentsInChildren[i].isTrigger && rigidbody2D != null && componentsInChildren[i].gameObject.activeSelf)
				{
					list.Add(rigidbody2D);
				}
				rigidbody2D = null;
				wheelsrigitbodyes = list;
			}
			return wheelsrigitbodyes;
		}
		set
		{
			wheelsrigitbodyes = value;
		}
	}

	private Vector3 yOffset
	{
		get
		{
			if (_yoffset.y == 0f)
			{
				Vector3 size = BodySprite.GetComponent<Renderer>().bounds.size;
				_yoffset = new Vector3(0f, size.y + 1f, 0f);
			}
			return _yoffset;
		}
		set
		{
			_yoffset = value;
		}
	}

	public Car2DConstructor constructor
	{
		get
		{
			if (_const == null)
			{
				_const = base.gameObject.GetComponent<Car2DConstructor>();
				if (_const == null)
				{
					_const = base.gameObject.AddComponent<Car2DConstructor>();
				}
				_const.location = Progress.levels.active_pack;
				_const.isConvoi = isConvoi;
				_const.IsCivic = IsCivic;
			}
			return _const;
		}
		set
		{
			_const = value;
		}
	}

	public bool Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			isKinematic = !value;
			Car2DSuspensionModuleBase suspensionModule = SuspensionModule;
			bool flag = _enabled = value;
			TurboModule.moduleEnabled = flag;
			flag = flag;
			HealthModule.moduleEnabled = flag;
			flag = flag;
			EngineModule.moduleEnabled = flag;
			suspensionModule.moduleEnabled = flag;
		}
	}

	public bool isKinematic
	{
		get
		{
			return _iskinematic;
		}
		set
		{
			_iskinematic = value;
			Rigidbody2D[] componentsInChildren = base.gameObject.GetComponentsInChildren<Rigidbody2D>();
			foreach (Rigidbody2D rigidbody2D in componentsInChildren)
			{
				rigidbody2D.isKinematic = _iskinematic;
			}
		}
	}

	private Hub firstHub
	{
		get
		{
			if (_firstHub == null)
			{
				Rigidbody2D rigidbody2D = wheelsrigitbodyes[0];
				for (int i = 0; i < WheelsRigitbodies.Count; i++)
				{
					if (isAhead)
					{
						Vector3 position = rigidbody2D.transform.position;
						float x = position.x;
						Vector3 position2 = WheelsRigitbodies[i].transform.position;
						if (x < position2.x)
						{
							goto IL_00a3;
						}
					}
					if (isAhead)
					{
						continue;
					}
					Vector3 position3 = rigidbody2D.transform.position;
					float x2 = position3.x;
					Vector3 position4 = WheelsRigitbodies[i].transform.position;
					if (!(x2 > position4.x))
					{
						continue;
					}
					goto IL_00a3;
					IL_00a3:
					rigidbody2D = WheelsRigitbodies[i];
				}
				Hub hub = new Hub();
				for (int j = 0; j < SuspensionModule.Hubs.Count; j++)
				{
					if (SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>().connectedBody == rigidbody2D)
					{
						hub.Hinge = SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>();
						hub.Spring = SuspensionModule.Hubs[j].GetComponent<SpringJoint2D>();
						hub.Slider = SuspensionModule.Hubs[j].GetComponent<SliderJoint2D>();
					}
				}
				_firstHub = hub;
			}
			return _firstHub;
		}
		set
		{
			_firstHub = value;
		}
	}

	private Hub secondHub
	{
		get
		{
			if (_secondHub == null)
			{
				Rigidbody2D rigidbody2D = wheelsrigitbodyes[0];
				for (int i = 0; i < WheelsRigitbodies.Count; i++)
				{
					if (isAhead)
					{
						Vector3 position = rigidbody2D.transform.position;
						float x = position.x;
						Vector3 position2 = WheelsRigitbodies[i].transform.position;
						if (x > position2.x)
						{
							goto IL_00a3;
						}
					}
					if (isAhead)
					{
						continue;
					}
					Vector3 position3 = rigidbody2D.transform.position;
					float x2 = position3.x;
					Vector3 position4 = WheelsRigitbodies[i].transform.position;
					if (!(x2 < position4.x))
					{
						continue;
					}
					goto IL_00a3;
					IL_00a3:
					rigidbody2D = WheelsRigitbodies[i];
				}
				Hub hub = new Hub();
				for (int j = 0; j < SuspensionModule.Hubs.Count; j++)
				{
					if (SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>().connectedBody == rigidbody2D)
					{
						hub.Hinge = SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>();
						hub.Spring = SuspensionModule.Hubs[j].GetComponent<SpringJoint2D>();
						hub.Slider = SuspensionModule.Hubs[j].GetComponent<SliderJoint2D>();
					}
				}
				_secondHub = hub;
			}
			return _secondHub;
		}
		set
		{
			_secondHub = value;
		}
	}

	public event DieDelegate OnDie;

	public event ValueChangedDelegate OnHeathChanged;

	public event ValueChangedDelegate OnTurboChanged;

	private void HealthChanged()
	{
		float num = _healthmodule._Barrel.Value / _healthmodule._Barrel.MaxValue;
		if (this.OnHeathChanged != null)
		{
			this.OnHeathChanged(num);
		}
		ChangeLabelValue();
		if (num <= 0f)
		{
			Death();
			if (this.OnDie != null)
			{
				this.OnDie();
			}
		}
	}

	private void TurboChanged()
	{
		float value = _turbomodule._Barrel.Value / _turbomodule._Barrel.MaxValue;
		if (this.OnTurboChanged != null)
		{
			this.OnTurboChanged(value);
		}
	}

	private void ClearEventListeners()
	{
		this.OnHeathChanged = null;
		this.OnTurboChanged = null;
	}

	private void OnDisable()
	{
		constructor.isAhead = true;
		_healthLabel = null;
		_healthLine = null;
		_healthLabelPoolTrabsform = null;
		if (BodySpritedamage.Count > 0)
		{
			for (int i = 0; i < BodySpritedamage.Count; i++)
			{
				BodySpritedamage[i].SetActive(value: true);
			}
		}
		if (BodySpritedamageforward.Count > 0)
		{
			for (int j = 0; j < BodySpritedamageforward.Count; j++)
			{
				BodySpritedamageforward[j].SetActive(value: true);
			}
		}
		BodySpritedamage.Clear();
		BodySpritedamageforward.Clear();
	}

	private void OnDestroy()
	{
		ClearEventListeners();
	}

	public void OnCollisionEnter2D(Collision2D coll)
	{
		if ((coll.gameObject.tag == CarMain || coll.gameObject.tag == CarMainChild || coll.gameObject.tag == strWheels) && freeze)
		{
			HealthModule._Barrel.Value = HealthModule._Barrel.Value - 1f;
		}
	}

	public void CheckBodyDetail(float rate)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (IsCivic && !isConvoi)
		{
			if (rate < 30f)
			{
				if (set30Perc)
				{
					return;
				}
				Animator[] componentsInChildren = base.gameObject.GetComponentsInChildren<Animator>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (!componentsInChildren[i].gameObject.transform.parent.name.Contains(strWheel))
					{
						componentsInChildren[i].SetFloat(strcarHP, rate);
					}
				}
				set30Perc = true;
			}
			else if (rate < 60f)
			{
				if (set60Perc)
				{
					return;
				}
				Animator[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<Animator>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					if (!componentsInChildren2[j].gameObject.transform.parent.name.Contains(strWheel))
					{
						componentsInChildren2[j].SetFloat(strcarHP, rate);
					}
				}
				set60Perc = true;
			}
			else
			{
				if (set100Perc)
				{
					return;
				}
				Animator[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<Animator>();
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					if (!componentsInChildren3[k].gameObject.transform.parent.name.Contains(strWheel))
					{
						componentsInChildren3[k].SetFloat(strcarHP, rate);
					}
				}
				set100Perc = true;
			}
		}
		else if (rate < 30f)
		{
			if (BodySpritedamage.Count > 2)
			{
				BodySpritedamage[0].SetActive(value: false);
				BodySpritedamage[1].SetActive(value: false);
				BodySpritedamage[2].SetActive(value: true);
			}
			if (BodySpritedamageforward.Count > 2)
			{
				BodySpritedamageforward[0].SetActive(value: false);
				BodySpritedamageforward[1].SetActive(value: false);
				BodySpritedamageforward[2].SetActive(value: true);
			}
		}
		else if (rate < 60f)
		{
			if (BodySpritedamage.Count > 2)
			{
				BodySpritedamage[0].SetActive(value: false);
				BodySpritedamage[1].SetActive(value: true);
				BodySpritedamage[2].SetActive(value: false);
			}
			if (BodySpritedamageforward.Count > 2)
			{
				BodySpritedamageforward[0].SetActive(value: false);
				BodySpritedamageforward[1].SetActive(value: true);
				BodySpritedamageforward[2].SetActive(value: false);
			}
		}
		else
		{
			if (BodySpritedamage.Count > 2)
			{
				BodySpritedamage[0].SetActive(value: true);
				BodySpritedamage[1].SetActive(value: false);
				BodySpritedamage[2].SetActive(value: false);
			}
			if (BodySpritedamageforward.Count > 2)
			{
				BodySpritedamageforward[0].SetActive(value: true);
				BodySpritedamageforward[1].SetActive(value: false);
				BodySpritedamageforward[2].SetActive(value: false);
			}
		}
	}

	private byte GetDegreeDamage(float p)
	{
		if (p < 30f)
		{
			return 2;
		}
		if (p < 60f)
		{
			return 1;
		}
		return 0;
	}

	public List<Rigidbody2D> BodyRegitBodyes()
	{
		Rigidbody2D[] componentsInChildren = GetComponentsInChildren<Rigidbody2D>();
		List<Rigidbody2D> list = new List<Rigidbody2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			list.Add(componentsInChildren[i]);
		}
		return list;
	}

	public Transform HealthLabel()
	{
		if (_healthLabel == null)
		{
			GameObject hPbar = Pool.GetHPbar();
			_healthLabelPoolTrabsform = hPbar.transform.parent;
			_healthLabel = hPbar.transform;
			Transform[] componentsInChildren = GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			foreach (Transform transform in array)
			{
				if (transform.name == str_helthBar_connector)
				{
					_healthLabel.parent = transform;
					hPbar.transform.position = transform.transform.position;
					hPbar.transform.rotation = transform.transform.rotation;
					hPbar.transform.localScale = new Vector3(0.333333f, 0.333333f, 1f);
					break;
				}
			}
		}
		return _healthLabel;
	}

	public void SetHealthLabel()
	{
		if (_healthLabel != null)
		{
			Transform[] componentsInChildren = GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			int num = 0;
			Transform transform;
			while (true)
			{
				if (num < array.Length)
				{
					transform = array[num];
					if (transform.name == str_helthBar_connector)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			_healthLabel.parent = transform;
			_healthLabel.position = transform.transform.position;
			_healthLabel.rotation = transform.transform.rotation;
			_healthLabel.localScale = new Vector3(0.333333f, 0.333333f, 1f);
		}
		else
		{
			HealthLabel();
		}
	}

	private Transform HealthLine()
	{
		if (_healthLabel != null && _healthLine == null)
		{
			_healthLine = _healthLabel.GetChild(0).transform;
		}
		return _healthLine;
	}

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool || isConvoi)
		{
			freeze = false;
			if (base.gameObject.name.Contains(ustr_Civilians))
			{
				IsCivic = true;
			}
			StartCoroutine(startCorut());
			if (isConvoi)
			{
				ConvoiCars[Progress.shop.SpecialMissionsRewardCar - 1].SetActive(value: true);
			}
		}
	}

	private IEnumerator startCorut()
	{
		if (isConvoi)
		{
			Init(isciviks: true, RaceLogic.instance.car.transform);
			StartCoroutine(convoiLogickDogonka());
		}
		if (IsCivic)
		{
			ColorSeted = false;
			inCarType = -1;
			set100Perc = false;
			set60Perc = false;
			set30Perc = false;
		}
		yield return 0;
		ChangeLabelValue();
	}

	private IEnumerator convoiLogickDogonka()
	{
		while (true)
		{
			yield return 0;
			Vector3 position = base.transform.position;
			float x = position.x;
			Vector3 position2 = RaceLogic.instance.car.transform.position;
			if (x > position2.x && Progress.shop.SpecialMissionsRewardCar != -1)
			{
				EngineModule.MaxSpeed = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].SpeedConvoi;
				EngineModule.Torque = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].TorqueConvoi;
			}
			else if (Progress.shop.SpecialMissionsRewardCar != -1)
			{
				EngineModule.MaxSpeed = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].SpeedConvoi * 3f;
				EngineModule.Torque = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].TorqueConvoi * 3f;
			}
		}
	}

	public void Init(bool isciviks = false, Transform target = null)
	{
		BigToSmall = false;
		AnDeathConvoi = false;
		firstHub = null;
		secondHub = null;
		Enabled = false;
		_target = target;
		isAhead = !constructor.isAhead;
		WheelsRigitbodies.Clear();
		if (!isConvoi)
		{
			IsCivic = constructor.IsCivic;
		}
		else
		{
			constructor.SetCar(4);
		}
		if (base.gameObject.name.Contains(ustr_Civilians))
		{
			IsCivic = true;
		}
		else
		{
			IsCivic = isciviks;
		}
		constructor.IsCivic = IsCivic;
		constructor.SetCar(constructor.carType);
		GetModulesReferences();
		yOffset = Vector3.zero;
		Car2Ddmg_new componentInChildren = base.gameObject.GetComponentInChildren<Car2Ddmg_new>();
		if (componentInChildren != null)
		{
			BodySprite = componentInChildren.BodySprite;
		}
		else
		{
			BodySprite = base.gameObject.GetComponentInChildren<BoxCollider2D>().GetComponent<tk2dSprite>();
		}
		Transform[] componentsInChildren = BodySprite.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = componentsInChildren[i];
			string name = transform.name;
			if (name.Equals(strdmg_0) || name.Equals(strdmg_1) || name.Equals(strdmg_2))
			{
				BodySpritedamage.Add(transform.gameObject);
			}
		}
		SuspensionModule.InitAI(GetComponent<Rigidbody2D>(), WheelsRigitbodies);
		EngineModule.InitAI(WheelsRigitbodies, 1, isConvoi);
		HealthModule.Init(CheckBodyDetail, HealthChanged);
		TurboModule.InitAI(BodyRegitBodyes(), TurboChanged);
		if (!isConvoi)
		{
			setPrevHubs();
		}
		SuspensionModule.Dir = (isAhead ? 1 : (-1));
		EngineModule.Dir = ((!isAhead) ? 1 : (-1));
		EngineModule.Break(onoff: false);
		GetModulesValues();
		if (!IsCivic)
		{
			base.gameObject.GetComponentInChildren<EatSensor>().Eating += Eating;
		}
		can_eat = true;
		if (_target != null)
		{
			Enabled = true;
		}
		setCivilOpt();
		if (isConvoi)
		{
			Shadow shadow = base.gameObject.AddComponent<Shadow>();
			shadow.Set(1.7f, Shadow.ShadowType.FixedSize);
		}
	}

	private void setCivilOpt()
	{
		if (!IsCivic || isConvoi)
		{
			return;
		}
		set100Perc = false;
		set60Perc = false;
		set30Perc = false;
		if (inCarType == -1)
		{
			inCarType = UnityEngine.Random.Range(0, 4);
		}
		if (!ColorSeted)
		{
			int num = Colorindex = RaceLogic.instance.GerCivilColorsIndex();
			if (constructor.carType == 1)
			{
				CarTupe1b.SetColors(num);
				CarTupe1f.SetColors(num);
			}
			else if (constructor.carType == 2)
			{
				CarTupe2b.SetColors(num);
				CarTupe2f.SetColors(num);
			}
			else if (constructor.carType == 3)
			{
				CarTupe3b.SetColors(num);
				CarTupe3f.SetColors(num);
			}
			else if (constructor.carType == 4)
			{
				CarTupe4b.SetColors(num);
				CarTupe4f.SetColors(num);
			}
			constructor._RGB = DifficultyConfig.instance.ColorsAll[num].Colored1;
			ColorSeted = true;
		}
		Animator[] componentsInChildren = base.gameObject.GetComponentsInChildren<Animator>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].SetInteger(_carMod, inCarType);
		}
		float num2 = _healthmodule._Barrel.Value / _healthmodule._Barrel.MaxValue;
		CheckBodyDetail(num2 * 100f);
	}

	private void setPrevHubs()
	{
		tHUBLastIndex = -1;
		int num = -1;
		for (int i = 0; i < tHUB_SJ.Count; i++)
		{
			if (tHUB_SJ[i].carType == constructor.carType)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			tHUBLastIndex = num;
			for (int j = 0; j < SuspensionModule.Hubs.Count; j++)
			{
				for (int k = 0; k < WheelsRigitbodies.Count; k++)
				{
					if (SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>().connectedBody == WheelsRigitbodies[k])
					{
						SuspensionModule.Hubs[j].GetComponent<SpringJoint2D>().connectedAnchor = tHUB_SJ[num].HUBs[j];
						SuspensionModule.Hubs[j].GetComponent<SliderJoint2D>().connectedAnchor = tHUB_SJ[num].HUBs[j];
						break;
					}
				}
			}
			return;
		}
		HubComplect hubComplect = new HubComplect();
		hubComplect.carType = constructor.carType;
		hubComplect.isAhead = constructor.isAhead;
		for (int l = 0; l < SuspensionModule.Hubs.Count; l++)
		{
			for (int m = 0; m < WheelsRigitbodies.Count; m++)
			{
				if (SuspensionModule.Hubs[l].GetComponent<HingeJoint2D>().connectedBody == WheelsRigitbodies[m])
				{
					hubComplect.HUBs.Add(SuspensionModule.Hubs[l].GetComponent<SpringJoint2D>().connectedAnchor);
					break;
				}
			}
		}
		tHUB_SJ.Add(hubComplect);
		tHUBLastIndex = tHUB_SJ.Count - 1;
	}

	private IEnumerator DelayToSwapWheels(int _index)
	{
		int t = 5;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		WheelsSwap();
	}

	public void GetModulesReferences()
	{
		RefreshData(CarsValues.StartData.GetCarSettings(isAi: true));
	}

	public void GetModulesValues()
	{
		if (Progress.shop.Undeground2)
		{
			if (!IsCivic)
			{
				RefreshData(CarsValues.StartData.GetCurrentAISettings(20 + constructor.carType - 1));
			}
		}
		else if (Progress.shop.TestFor9)
		{
			if (!IsCivic)
			{
				RefreshData(CarsValues.StartData.GetCurrentAISettings(16 + constructor.carType - 1));
			}
		}
		else if (Progress.shop.EsterLevelPlay)
		{
			if (!IsCivic)
			{
				RefreshData(CarsValues.StartData.GetCurrentAISettings(constructor.carType - 1));
			}
		}
		else if (!IsCivic)
		{
			RefreshData(CarsValues.StartData.GetCurrentAISettings(constructor.carType - 1 + 4 * (constructor.location - 1)));
		}
		else
		{
			RefreshData(null);
		}
	}

	public void RefreshData(CarsValues.StartData dt)
	{
		if (!IsCivic)
		{
			HealthModule.UpdateModuleValue(dt._health.BaseValue + DifficultyConfig.instance.GetDifNumWithoutCurrent(dt._health.BaseValue));
			TurboModule.Angle = dt._turbo.Angle;
			TurboModule._Barrel.MaxValue = dt._turbo.MaxValue * 100f;
			TurboModule._Barrel.Value = dt._turbo.Value * 100f;
			TurboModule._Barrel.UsageValue = dt._turbo.UsageValue * 100f;
			TurboModule.Power = dt._turbo.Power;
			if (dt._turbo.RestoreTime > 0f)
			{
				TurboModule._Barrel.Restore = true;
				TurboModule._Barrel.RestoreTime = dt._turbo.RestoreTime;
			}
			else
			{
				TurboModule._Barrel.Restore = false;
			}
			SuspensionModule.BodyMass = dt._suspansion.BodyMass;
			SuspensionModule.WheelMass = dt._suspansion.WheelMass;
			SuspensionModule.SpringMass = dt._suspansion.SpringMass;
			SuspensionModule.Distance = dt._suspansion.Distance;
			SuspensionModule.Frequency = dt._suspansion.Frequency;
			SuspensionModule.WheelsFriction = dt._suspansion.WheelsFriction;
			SuspensionModule.Damping = dt._suspansion.Damping;
			SuspensionModule.RotationAcceleration = dt._suspansion.RotationAcceleration;
			SuspensionModule.UpdateSettings();
			EngineModule.MaxSpeed = dt._engine.MaxSpeed;
			EngineModule.Torque = dt._engine.Torque;
			eat_delay = dt._ai.eat_delay;
			Explosive = dt._ai.Explosive;
			EatDamage = dt._ai.EatDamage + DifficultyConfig.instance.GetDifNumWithoutCurrent(dt._ai.EatDamage);
			reward = dt._ai.Reward;
			InvisibleSpeed = dt._ai.InvisibleSpeed;
			return;
		}
		if (Progress.shop.endlessLevel)
		{
			civic.BaseValue = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].HPConvoi;
			civic.MaxSpeed = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].SpeedConvoi;
			civic.Torque = TimeToDie.instance.timeToDie.TimeToDie[Progress.shop.SpecialMissionsRewardCar - 1].TorqueConvoi;
		}
		HealthModule.UpdateModuleValue(civic.BaseValue + DifficultyConfig.instance.GetDifNumWithoutCurrent(civic.BaseValue));
		TurboModule.Angle = civic.Angle;
		TurboModule._Barrel.MaxValue = civic.MaxValue * 100f;
		TurboModule._Barrel.Value = civic.Value * 100f;
		TurboModule._Barrel.UsageValue = civic.UsageValue * 100f;
		TurboModule.Power = civic.Power;
		if (civic.RestoreTime > 0f)
		{
			TurboModule._Barrel.Restore = true;
			TurboModule._Barrel.RestoreTime = civic.RestoreTime;
		}
		else
		{
			TurboModule._Barrel.Restore = false;
		}
		SuspensionModule.BodyMass = civic.BodyMass;
		SuspensionModule.WheelMass = civic.WheelMass;
		SuspensionModule.SpringMass = civic.SpringMass;
		SuspensionModule.Distance = civic.Distance;
		SuspensionModule.Frequency = civic.Frequency;
		SuspensionModule.WheelsFriction = civic.WheelsFriction;
		SuspensionModule.Damping = civic.Damping;
		SuspensionModule.RotationAcceleration = civic.RotationAcceleration;
		SuspensionModule.UpdateSettings();
		EngineModule.MaxSpeed = civic.MaxSpeed;
		EngineModule.Torque = civic.Torque;
		reward = civic.Reward;
	}

	public void Difficuld(int Engine = 1, int Susp = 1, int Power = 1)
	{
		EngineModule.ChangeDifficult(Engine);
		SuspensionModule.ChangeDifficult(Susp);
		TurboModule.ChangeDifficult(Power);
	}

	public void Death(bool withReward = true)
	{
		if (isConvoi && AnDeathConvoi)
		{
			return;
		}
		if (_healthLabel != null)
		{
			_healthLabel.parent = _healthLabelPoolTrabsform;
			_healthLabel.gameObject.SetActive(value: false);
		}
		if (base.gameObject.name.Contains("Enemy 1"))
		{
			if (constructor.carType == 1)
			{
				Progress.shop.CollKill1Car++;
			}
			else if (constructor.carType == 2)
			{
				Progress.shop.CollKill2Car++;
			}
			else if (constructor.carType == 3)
			{
				Progress.shop.CollKill3Car++;
			}
			else if (constructor.carType == 4)
			{
				Progress.shop.CollKill4Car++;
			}
		}
		if (base.gameObject.name.Contains("Enemy 2"))
		{
			if (constructor.carType == 1)
			{
				Progress.shop.CollKill1Car2++;
			}
			else if (constructor.carType == 2)
			{
				Progress.shop.CollKill2Car2++;
			}
			else if (constructor.carType == 3)
			{
				Progress.shop.CollKill3Car2++;
			}
			else if (constructor.carType == 4)
			{
				Progress.shop.CollKill4Car2++;
			}
		}
		if (base.gameObject.name.Contains("Enemy 3"))
		{
			if (constructor.carType == 1)
			{
				Progress.shop.CollKill1Car3++;
			}
			else if (constructor.carType == 2)
			{
				Progress.shop.CollKill3Car3++;
			}
			else if (constructor.carType == 3)
			{
				Progress.shop.CollKill2Car3++;
			}
			else if (constructor.carType == 4)
			{
				Progress.shop.CollKill4Car3++;
			}
		}
		HingeJoint2D[] componentsInChildren = GetComponentsInChildren<HingeJoint2D>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
		}
		SuspensionModule.ClearHubsLists();
		if (withReward)
		{
			if (!freeze)
			{
				RaceLogic.instance.CounterEmemys++;
			}
			if (Progress.shop.ArenaNew)
			{
				RaceLogic.instance.onAIKilled((int)(reward / 2f), this);
			}
			else
			{
				RaceLogic.instance.onAIKilled((int)reward, this);
			}
		}
		else
		{
			RaceLogic.instance.onAIKilled(this);
		}
		int num = (constructor.location - 1) * 4;
		for (int j = 0; j < constructor.colScraps; j++)
		{
			if (Progress.shop.EsterLevelPlay)
			{
				if (constructor.carType == 1)
				{
					constructor.scrap1 = "enemy_igor_easter_car01_scp01";
					constructor.scrap2 = "enemy_igor_easter_car01_scp02";
					constructor.scrap3 = "enemy_igor_easter_car01_scp03";
					constructor.scrap4 = "enemy_igor_easter_car01_scp04";
					constructor.scrap5 = "enemy_igor_easter_car01_wheel_f";
				}
				else if (constructor.carType == 2)
				{
					constructor.scrap1 = "enemy_igor_easter_car02_scp01";
					constructor.scrap2 = "enemy_igor_easter_car02_scp02";
					constructor.scrap3 = "enemy_igor_easter_car02_scp03";
					constructor.scrap4 = "enemy_igor_easter_car02_scp04";
					constructor.scrap5 = "enemy_igor_easter_car02_wheel";
				}
				else if (constructor.carType == 3)
				{
					constructor.scrap1 = "enemy_igor_easter_car03_scp01";
					constructor.scrap2 = "enemy_igor_easter_car03_scp02";
					constructor.scrap3 = "enemy_igor_easter_car03_scp03";
					constructor.scrap4 = "enemy_igor_easter_car03_scp04";
					constructor.scrap5 = "enemy_igor_easter_car03_wheel_f";
				}
				else if (constructor.carType == 4)
				{
					constructor.scrap1 = "enemy_igor_easter_car04_scp01";
					constructor.scrap2 = "enemy_igor_easter_car04_scp02";
					constructor.scrap3 = "enemy_igor_easter_car04_scp03";
					constructor.scrap4 = "enemy_igor_easter_car04_scp04";
					constructor.scrap5 = "enemy_igor_easter_car04_wheel";
				}
			}
			Pool.ScrapEnemy(base.transform.position, num + constructor.carType, constructor.location, IsCivic, constructor.scrap1, constructor.scrap2, constructor.scrap3, constructor.scrap4, constructor.scrap5, constructor._RGB);
			Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		}
		for (int k = 0; k < CollRubyForVzruv; k++)
		{
			GameObject gameObject = Pool.GameOBJECT(Pool.Bonus.ruby, base.transform.position);
			Vector2 force = new Vector2(UnityEngine.Random.Range(-25, 25), UnityEngine.Random.Range(0, 25));
			gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
		}
		if ((constructor.location == 3 || constructor.location == 4) && constructor.carType == 2)
		{
			Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		}
		if (RaceLogic.instance.car.gameObject.activeSelf)
		{
			switch (constructor.carType)
			{
			case 1:
			case 2:
			case 3:
				Audio.PlayAsyncRandom("crash_car_03_sn", "crash_car_04_sn", "crash_car_05_sn");
				break;
			case 4:
				Audio.PlayAsync("crash_car_02_sn");
				break;
			}
		}
		_yoffset = new Vector3(0f, 0f, 0f);
		Enabled = false;
		if (freeze)
		{
			for (int l = 0; l < 10; l++)
			{
				Pool.Scrap(Pool.Scraps.ice, base.transform.position, UnityEngine.Random.Range(0, 360), 5f);
			}
		}
		if (!isConvoi)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		for (int m = 0; m < SuspensionModule.Hubs.Count; m++)
		{
			SuspensionModule.Hubs[m].SetActive(value: false);
		}
		for (int n = 0; n < ConvoiOffObjs.Count; n++)
		{
			ConvoiOffObjs[n].SetActive(value: false);
		}
		for (int num2 = 0; num2 < BodyRegitBodyes().Count; num2++)
		{
			BodyRegitBodyes()[num2].isKinematic = false;
		}
		if (RaceLogic.instance.TimeToDieCorut != null)
		{
			StopCoroutine(RaceLogic.instance.TimeToDieCorut);
		}
		GameObject gameObject2 = new GameObject();
		gameObject2.transform.position = RaceLogic.instance.Convoi.transform.position;
		RaceLogic.instance.Convoi = gameObject2;
		gameObject2.SetActive(value: false);
		RaceLogic.instance.car.HealthModule.moduleEnabled = false;
		StartCoroutine(closeCar());
	}

	private IEnumerator closeCar()
	{
		yield return new WaitForSeconds(3f);
		RaceLogic.instance.car.gameObject.SetActive(value: false);
	}

	public void SetDamage(float damage)
	{
		HealthModule.IncreaseHealth(0f - damage);
	}

	private void Update()
	{
		if (freeze && EatDamage != 0f)
		{
			StartCoroutine(Freeze(this, 2f));
		}
		if (Time.timeScale != 0f && !NotCreateHP)
		{
			if (_healthLabel == null)
			{
				HealthLabel();
			}
			LookAtTarget();
			EatDelay();
		}
	}

	private IEnumerator Freeze(Car2DAIController _ai, float time)
	{
		float startHealth = _ai.HealthModule._Barrel.Value;
		float startDamage = _ai.EatDamage;
		_ai.HealthModule._Barrel.Value = 1f;
		_ai.EatDamage = 0f;
		_ai.EngineModule.Break(onoff: true);
		_ai.freeze = true;
		float t = 0f;
		while (t <= time)
		{
			t += Time.deltaTime;
			yield return null;
		}
		_ai.HealthModule._Barrel.Value = startHealth;
		_ai.EatDamage = startDamage;
		if (!base.transform.gameObject.activeSelf)
		{
			Kill(_ai.HealthModule._Barrel.Value + 1f, _ai);
		}
	}

	private void Kill(float _damage, Car2DAIController _ai)
	{
		_ai.EngineModule.Break(onoff: false);
		_ai.Death();
		for (int i = 0; i < 10; i++)
		{
			Pool.Scrap(Pool.Scraps.ice, base.transform.position, UnityEngine.Random.Range(0, 360), 5f);
		}
	}

	private void LookAtTarget()
	{
		timeleft -= Time.deltaTime;
		if (!IsCivic)
		{
			if (_target == null || timeleft > 0f)
			{
				return;
			}
			bool num = isAhead;
			Vector3 position = _target.position;
			float x = position.x;
			Vector3 position2 = base.transform.position;
			if (num == x - position2.x < 0f)
			{
				return;
			}
			Vector3 position3 = _target.position;
			float x2 = position3.x;
			Vector3 position4 = base.transform.position;
			isAhead = (x2 - position4.x < 0f);
		}
		else if (!isConvoi)
		{
			if (_target == null || timeleft > 0f)
			{
				return;
			}
			bool num2 = isAhead;
			Vector3 position5 = _target.position;
			float x3 = position5.x;
			Vector3 position6 = base.transform.position;
			if (num2 == x3 - position6.x > 0f)
			{
				return;
			}
			Vector3 position7 = _target.position;
			float x4 = position7.x;
			Vector3 position8 = base.transform.position;
			isAhead = (x4 - position8.x > 0f);
		}
		else
		{
			if (_target == null || timeleft > 0f)
			{
				return;
			}
			bool num3 = isAhead;
			Vector3 position9 = _target.position;
			float x5 = position9.x;
			Vector3 position10 = base.transform.position;
			if (num3 == x5 - position10.x > 0f)
			{
				return;
			}
			Vector3 position11 = _target.position;
			float x6 = position11.x;
			Vector3 position12 = base.transform.position;
			isAhead = (x6 - position12.x > 5000f);
		}
		constructor.Turn(!isAhead);
		SetHealthLabel();
		ChangeLabelValue();
		if (lastTorn != isAhead)
		{
			setCivilOpt();
		}
		lastTorn = isAhead;
		if (!isConvoi)
		{
			WheelsSwap();
		}
		if (BodySpritedamageforward.Count == 0)
		{
			Car2Ddmg_new componentInChildren = base.gameObject.GetComponentInChildren<Car2Ddmg_new>();
			if (componentInChildren != null)
			{
				bodyspriteforw = componentInChildren.BodySprite;
			}
			else
			{
				bodyspriteforw = base.gameObject.GetComponentInChildren<BoxCollider2D>().GetComponent<tk2dSprite>();
			}
			Transform[] componentsInChildren = bodyspriteforw.gameObject.transform.parent.gameObject.GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			foreach (Transform transform in array)
			{
				if (transform.name == strdmg_0)
				{
					BodySpritedamageforward.Add(transform.gameObject);
				}
			}
			Transform[] array2 = componentsInChildren;
			foreach (Transform transform2 in array2)
			{
				if (transform2.name == strdmg_1)
				{
					BodySpritedamageforward.Add(transform2.gameObject);
				}
			}
			Transform[] array3 = componentsInChildren;
			foreach (Transform transform3 in array3)
			{
				if (transform3.name == strdmg_2)
				{
					BodySpritedamageforward.Add(transform3.gameObject);
				}
			}
		}
		SuspensionModule.Dir = (isAhead ? 1 : (-1));
		EngineModule.Dir = ((!isAhead) ? 1 : (-1));
		TurboModule.Dir = ((!isAhead) ? 1 : (-1));
		if (!IsCivic)
		{
			base.gameObject.GetComponentInChildren<EatSensor>().Eating += Eating;
		}
		timeleft = returntime;
	}

	private void WheelsSwap()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < SuspensionModule.HubsClasses.Count; i++)
		{
			if (i == 0)
			{
				num = i;
				num2 = i;
				continue;
			}
			Vector3 position = SuspensionModule.HubsClasses[num].go.transform.position;
			float x = position.x;
			Vector3 position2 = SuspensionModule.HubsClasses[i].go.transform.position;
			if (x < position2.x)
			{
				num = i;
			}
			Vector3 position3 = SuspensionModule.HubsClasses[num2].go.transform.position;
			float x2 = position3.x;
			Vector3 position4 = SuspensionModule.HubsClasses[i].go.transform.position;
			if (x2 > position4.x)
			{
				num2 = i;
			}
		}
		Vector2 connectedAnchor = SuspensionModule.HubsClasses[num2].SpringJ.connectedAnchor;
		float x3 = connectedAnchor.x;
		Vector2 connectedAnchor2 = SuspensionModule.HubsClasses[num2].SliderJ.connectedAnchor;
		float x4 = connectedAnchor2.x;
		Vector2 connectedAnchor3 = SuspensionModule.HubsClasses[num].SpringJ.connectedAnchor;
		float x5 = connectedAnchor3.x;
		Vector2 connectedAnchor4 = SuspensionModule.HubsClasses[num].SliderJ.connectedAnchor;
		float x6 = connectedAnchor4.x;
		float num4;
		float num6;
		for (int j = 0; j < SuspensionModule.HubsClasses.Count; j++)
		{
			if (j != num && j != num2)
			{
				float num3 = x5;
				Vector2 connectedAnchor5 = SuspensionModule.HubsClasses[j].SpringJ.connectedAnchor;
				num4 = num3 - connectedAnchor5.x;
				float num5 = x6;
				Vector2 connectedAnchor6 = SuspensionModule.HubsClasses[j].SliderJ.connectedAnchor;
				num6 = num5 - connectedAnchor6.x;
				SpringJoint2D springJ = SuspensionModule.HubsClasses[j].SpringJ;
				float x7 = x3 + num4;
				Vector2 connectedAnchor7 = SuspensionModule.HubsClasses[j].SpringJ.connectedAnchor;
				springJ.connectedAnchor = new Vector2(x7, connectedAnchor7.y);
				SliderJoint2D sliderJ = SuspensionModule.HubsClasses[j].SliderJ;
				float x8 = x4 + num6;
				Vector2 connectedAnchor8 = SuspensionModule.HubsClasses[j].SliderJ.connectedAnchor;
				sliderJ.connectedAnchor = new Vector2(x8, connectedAnchor8.y);
			}
		}
		Vector2 connectedAnchor9 = SuspensionModule.HubsClasses[num2].SpringJ.connectedAnchor;
		num4 = connectedAnchor9.x;
		Vector2 connectedAnchor10 = SuspensionModule.HubsClasses[num2].SliderJ.connectedAnchor;
		num6 = connectedAnchor10.x;
		SpringJoint2D springJ2 = SuspensionModule.HubsClasses[num2].SpringJ;
		Vector2 connectedAnchor11 = SuspensionModule.HubsClasses[num].SpringJ.connectedAnchor;
		float x9 = connectedAnchor11.x;
		Vector2 connectedAnchor12 = SuspensionModule.HubsClasses[num2].SpringJ.connectedAnchor;
		springJ2.connectedAnchor = new Vector2(x9, connectedAnchor12.y);
		SliderJoint2D sliderJ2 = SuspensionModule.HubsClasses[num2].SliderJ;
		Vector2 connectedAnchor13 = SuspensionModule.HubsClasses[num].SliderJ.connectedAnchor;
		float x10 = connectedAnchor13.x;
		Vector2 connectedAnchor14 = SuspensionModule.HubsClasses[num2].SliderJ.connectedAnchor;
		sliderJ2.connectedAnchor = new Vector2(x10, connectedAnchor14.y);
		SuspensionModule.HubsClasses[num2].wheel.transform.position = SuspensionModule.HubsClasses[num].SpringJ.transform.position;
		SpringJoint2D springJ3 = SuspensionModule.HubsClasses[num].SpringJ;
		float x11 = num4;
		Vector2 connectedAnchor15 = SuspensionModule.HubsClasses[num].SpringJ.connectedAnchor;
		springJ3.connectedAnchor = new Vector2(x11, connectedAnchor15.y);
		SliderJoint2D sliderJ3 = SuspensionModule.HubsClasses[num].SliderJ;
		float x12 = num6;
		Vector2 connectedAnchor16 = SuspensionModule.HubsClasses[num].SliderJ.connectedAnchor;
		sliderJ3.connectedAnchor = new Vector2(x12, connectedAnchor16.y);
		Transform transform = SuspensionModule.HubsClasses[num].wheel.transform;
		Vector3 position5 = SuspensionModule.HubsClasses[num2].SpringJ.transform.position;
		float x13 = position5.x;
		Vector3 position6 = SuspensionModule.HubsClasses[num2].SpringJ.transform.position;
		float y = position6.y + 0.2f;
		Vector3 position7 = SuspensionModule.HubsClasses[num2].SpringJ.transform.position;
		transform.position = new Vector3(x13, y, position7.z);
	}

	public void Eating(GameObject g)
	{
		if ((g.tag != CarMain && g.tag != CarMainChild) || RaceLogic.instance.car.HealthModule.AnDeath || !can_eat)
		{
			return;
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			Temp = EatDamage + EatDamage * Progress.shop.forArenaDamageEnemy;
			if (Progress.shop.EsterLevelPlay)
			{
				TempToo = Temp * (float)(ConfigForEster.instance.CARS.Cars[Progress.shop.activeCar].CarPercent / 100);
				RaceLogic.instance.EatMainCar(Temp + TempToo);
			}
			if (constructor.carType == 4)
			{
				if (Progress.shop.EsterLevelPlay)
				{
					RaceLogic.instance.EatMainCar(ConfigForEster.instance.Config.DAMAGE_BOMBER);
					Death(withReward: false);
				}
				else
				{
					RaceLogic.instance.EatMainCar(Temp);
				}
			}
			else
			{
				RaceLogic.instance.EatMainCar(Temp);
			}
		}
		else
		{
			RaceLogic.instance.EatMainCar(EatDamage);
		}
		if (Explosive == 1f)
		{
			Pool.Animate(Pool.Explosion.exp28, base.transform.position);
			r = g.GetComponentInParent<Rigidbody2D>();
			if (r != null)
			{
				r.velocity = Vector2.zero;
				r.AddForce(r.mass * (r.transform.position - base.transform.position).normalized * 2500f);
			}
			Death();
		}
		else
		{
			can_eat = false;
			eatTime = eat_delay;
			if (RaceLogic.instance.car.gameObject.activeSelf)
			{
				Audio.PlayAsyncRandom(actor_bite_01, actor_bite_01);
			}
		}
	}

	private void EatDelay()
	{
		if (!can_eat)
		{
			if (eatTime > 0f)
			{
				eatTime -= Time.deltaTime;
			}
			else
			{
				can_eat = true;
			}
		}
	}

	private void ChangeLabelValue()
	{
		if (!(HealthLine() == null) && Enabled)
		{
			HealthLine().localScale = new Vector3(HealthModule._Barrel.Value / HealthModule._Barrel.MaxValue, 1f, 0f);
		}
	}
}

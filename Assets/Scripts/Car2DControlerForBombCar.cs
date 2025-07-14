using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DControlerForBombCar : MonoBehaviour
{
	public class Hub
	{
		public SpringJoint2D Spring;

		public SliderJoint2D Slider;

		public HingeJoint2D Hinge;
	}

	public delegate void ValueChangedDelegate(float value);

	public delegate void DieDelegate();

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

		[Header("DAMAGE")]
		public int Damage;
	}

	public bool AsBoss;

	public int BossNumber;

	private static string strdmg_0 = "dmg_0";

	private static string strdmg_1 = "dmg_1";

	private static string strdmg_2 = "dmg_2";

	private Hub _firstHub;

	private Hub _secondHub;

	[HideInInspector]
	private Car2DEngineModuleBase _enginemodule;

	[HideInInspector]
	private Car2DHealthModuleBase _healthmodule;

	[HideInInspector]
	private Car2DSuspensionModuleBase _suspensionmodule;

	[HideInInspector]
	private Car2DPowerModuleBase _turbomodule;

	private GameObject body;

	private EatSensor eatSensor;

	[HideInInspector]
	public float EatDamage = 1f;

	private float Explosive;

	private float eat_delay = 0.5f;

	[HideInInspector]
	public float InvisibleSpeed;

	private Transform _target;

	public bool freeze;

	private List<Rigidbody2D> wheelsrigitbodyes;

	private tk2dSprite BodySprite;

	private List<GameObject> BodySpritedamage_0 = new List<GameObject>();

	private List<GameObject> BodySpritedamage_1 = new List<GameObject>();

	private List<GameObject> BodySpritedamage_2 = new List<GameObject>();

	private List<GameObject> BodySpritedamageforward_0 = new List<GameObject>();

	private List<GameObject> BodySpritedamageforward_1 = new List<GameObject>();

	private List<GameObject> BodySpritedamageforward_2 = new List<GameObject>();

	private Transform _healthLabel;

	private Transform _healthLabelPoolTrabsform;

	private static string str_helthBar_connector = "helthBar_connector";

	private Transform _healthLine;

	private float lastTimeRoared;

	private float timespace = 20f;

	private Renderer[] _renderer;

	private Vector3 _yoffset = new Vector3(0f, 0f, 0f);

	private Car2DConstructor _const;

	private bool _enabled;

	private bool _iskinematic;

	public bool IsCivic;

	public CIVIC civic;

	private bool isAhead;

	private float returntime = 1f;

	private float timeleft;

	private tk2dSprite bodyspriteforw;

	private bool firstSwap;

	private float eatTime;

	private bool can_eat = true;

	private Rigidbody2D r;

	private static string CarEnemy = "CarEnemy";

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string actor_bite_01 = "actor_bite_01";

	private static string str_bomb_car = "bomb_car";

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

	private Renderer[] _Renderer
	{
		get
		{
			if (_renderer == null)
			{
				_renderer = base.gameObject.GetComponentsInChildren<Renderer>(includeInactive: true);
			}
			return _renderer;
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

	public event DieDelegate OnDie;

	public event ValueChangedDelegate OnHeathChanged;

	public event ValueChangedDelegate OnTurboChanged;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			_healthLabel = null;
			StartCoroutine(init_car());
		}
	}

	private IEnumerator init_car()
	{
		while (RaceLogic.instance.race == null)
		{
			yield return 0;
		}
		if (base.gameObject.transform.name.Contains(str_bomb_car))
		{
			Init(isciviks: false, RaceLogic.instance.race.start);
		}
		else
		{
			Init(isciviks: false, RaceLogic.instance.car.transform);
		}
	}

	public void Init(bool isciviks = false, Transform target = null)
	{
		Car2DSmallerBombConstructor component = GetComponent<Car2DSmallerBombConstructor>();
		if ((bool)component)
		{
			constructor.IsCivic = component.IsCivil;
			IsCivic = constructor.IsCivic;
			constructor.carType = component.CarType;
			constructor.isAhead = component.IsAhead;
			_target = RaceLogic.instance.car.transform;
			component.setActiveCont();
		}
		firstHub = null;
		secondHub = null;
		Enabled = false;
		_target = target;
		isAhead = !constructor.isAhead;
		if ((bool)component)
		{
			isAhead = component.IsAhead;
		}
		WheelsRigitbodies.Clear();
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
		Transform[] componentsInChildren = BodySprite.gameObject.transform.parent.parent.gameObject.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (transform.name == strdmg_0)
			{
				BodySpritedamage_0.Add(transform.gameObject);
			}
		}
		Transform[] array2 = componentsInChildren;
		foreach (Transform transform2 in array2)
		{
			if (transform2.name == strdmg_1)
			{
				BodySpritedamage_1.Add(transform2.gameObject);
			}
		}
		Transform[] array3 = componentsInChildren;
		foreach (Transform transform3 in array3)
		{
			if (transform3.name == strdmg_2)
			{
				BodySpritedamage_2.Add(transform3.gameObject);
			}
		}
		SuspensionModule.InitAI(GetComponent<Rigidbody2D>(), WheelsRigitbodies);
		EngineModule.InitAI(WheelsRigitbodies);
		HealthModule.Init(CheckBodyDetail, HealthChanged);
		TurboModule.InitAI(BodyRegitBodyes(), TurboChanged);
		SuspensionModule.Dir = (isAhead ? 1 : (-1));
		EngineModule.Dir = ((!isAhead) ? 1 : (-1));
		EngineModule.Break(onoff: false);
		GetModulesValues();
		EatSensor componentInChildren2 = base.gameObject.GetComponentInChildren<EatSensor>();
		if ((bool)componentInChildren2)
		{
			componentInChildren2.Eating += Eating;
		}
		can_eat = true;
		if (_target != null)
		{
			Enabled = true;
		}
		if ((IsCivic && base.gameObject.transform.name.Contains("pyro")) || base.gameObject.transform.name.Contains("Police_car") || base.gameObject.transform.name.Contains("Undergound_car_boss"))
		{
			Shadow shadow = base.gameObject.AddComponent<Shadow>();
			shadow.Set(1.3f, Shadow.ShadowType.FixedSize);
		}
		if (AsBoss)
		{
			SetHealthLabel();
		}
	}

	private void OnDisable()
	{
		_healthLabel = null;
		_healthLine = null;
		_healthLabelPoolTrabsform = null;
		if (BodySpritedamage_0.Count > 0)
		{
			for (int i = 0; i < BodySpritedamage_0.Count; i++)
			{
				BodySpritedamage_0[i].SetActive(value: true);
			}
			for (int j = 0; j < BodySpritedamage_1.Count; j++)
			{
				BodySpritedamage_1[j].SetActive(value: true);
			}
			for (int k = 0; k < BodySpritedamage_2.Count; k++)
			{
				BodySpritedamage_2[k].SetActive(value: true);
			}
		}
		if (BodySpritedamageforward_0.Count > 0)
		{
			for (int l = 0; l < BodySpritedamageforward_0.Count; l++)
			{
				BodySpritedamageforward_0[l].SetActive(value: true);
			}
			for (int m = 0; m < BodySpritedamageforward_1.Count; m++)
			{
				BodySpritedamageforward_1[m].SetActive(value: true);
			}
			for (int n = 0; n < BodySpritedamageforward_2.Count; n++)
			{
				BodySpritedamageforward_2[n].SetActive(value: true);
			}
		}
		BodySpritedamage_0.Clear();
		BodySpritedamage_1.Clear();
		BodySpritedamage_2.Clear();
		BodySpritedamageforward_0.Clear();
		BodySpritedamageforward_1.Clear();
		BodySpritedamageforward_2.Clear();
	}

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

	private void OnDestroy()
	{
		ClearEventListeners();
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == CarEnemy && freeze)
		{
			HealthModule._Barrel.Value = HealthModule._Barrel.Value + 1f;
		}
	}

	public void CheckBodyDetail(float rate)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (rate < 30f)
		{
			if (BodySpritedamage_0.Count > 0)
			{
				for (int i = 0; i < BodySpritedamage_0.Count; i++)
				{
					BodySpritedamage_0[i].SetActive(value: false);
				}
				for (int j = 0; j < BodySpritedamage_1.Count; j++)
				{
					BodySpritedamage_1[j].SetActive(value: false);
				}
				for (int k = 0; k < BodySpritedamage_2.Count; k++)
				{
					BodySpritedamage_2[k].SetActive(value: true);
				}
			}
			if (BodySpritedamageforward_0.Count > 0)
			{
				for (int l = 0; l < BodySpritedamageforward_0.Count; l++)
				{
					BodySpritedamageforward_0[l].SetActive(value: false);
				}
				for (int m = 0; m < BodySpritedamageforward_1.Count; m++)
				{
					BodySpritedamageforward_1[m].SetActive(value: false);
				}
				for (int n = 0; n < BodySpritedamageforward_2.Count; n++)
				{
					BodySpritedamageforward_2[n].SetActive(value: true);
				}
			}
			return;
		}
		if (rate < 60f)
		{
			if (BodySpritedamage_0.Count > 0)
			{
				for (int num = 0; num < BodySpritedamage_0.Count; num++)
				{
					BodySpritedamage_0[num].SetActive(value: false);
				}
				for (int num2 = 0; num2 < BodySpritedamage_1.Count; num2++)
				{
					BodySpritedamage_1[num2].SetActive(value: true);
				}
				for (int num3 = 0; num3 < BodySpritedamage_2.Count; num3++)
				{
					BodySpritedamage_2[num3].SetActive(value: false);
				}
			}
			if (BodySpritedamageforward_0.Count > 0)
			{
				for (int num4 = 0; num4 < BodySpritedamageforward_0.Count; num4++)
				{
					BodySpritedamageforward_0[num4].SetActive(value: false);
				}
				for (int num5 = 0; num5 < BodySpritedamageforward_1.Count; num5++)
				{
					BodySpritedamageforward_1[num5].SetActive(value: true);
				}
				for (int num6 = 0; num6 < BodySpritedamageforward_2.Count; num6++)
				{
					BodySpritedamageforward_2[num6].SetActive(value: false);
				}
			}
			return;
		}
		if (BodySpritedamage_0.Count > 0)
		{
			for (int num7 = 0; num7 < BodySpritedamage_0.Count; num7++)
			{
				BodySpritedamage_0[num7].SetActive(value: true);
			}
			for (int num8 = 0; num8 < BodySpritedamage_1.Count; num8++)
			{
				BodySpritedamage_1[num8].SetActive(value: false);
			}
			for (int num9 = 0; num9 < BodySpritedamage_2.Count; num9++)
			{
				BodySpritedamage_2[num9].SetActive(value: false);
			}
		}
		if (BodySpritedamageforward_0.Count > 0)
		{
			for (int num10 = 0; num10 < BodySpritedamageforward_0.Count; num10++)
			{
				BodySpritedamageforward_0[num10].SetActive(value: true);
			}
			for (int num11 = 0; num11 < BodySpritedamageforward_1.Count; num11++)
			{
				BodySpritedamageforward_1[num11].SetActive(value: false);
			}
			for (int num12 = 0; num12 < BodySpritedamageforward_2.Count; num12++)
			{
				BodySpritedamageforward_2[num12].SetActive(value: false);
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

	private List<Rigidbody2D> BodyRegitBodyes()
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
			HealthModule.UpdateModuleValue(civic.BaseValue);
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

	public void GetModulesReferences()
	{
		RefreshData();
	}

	public void GetModulesValues()
	{
		RefreshData();
	}

	public void RefreshData()
	{
		HealthModule.UpdateModuleValue(civic.BaseValue);
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
		EatDamage = civic.Damage;
	}

	public void Difficuld(int Engine = 1, int Susp = 1, int Power = 1)
	{
		EngineModule.ChangeDifficult(Engine);
		SuspensionModule.ChangeDifficult(Susp);
		TurboModule.ChangeDifficult(Power);
	}

	public void Death(bool reward = true)
	{
		if (_healthLabel != null)
		{
			_healthLabel.parent = _healthLabelPoolTrabsform;
			_healthLabel.gameObject.SetActive(value: false);
		}
		if (AsBoss)
		{
			RaceLogic.instance.gui.interface_messanger.AnimateWithText(GUIInterfaceMessage.Words.bosskiller);
			RaceLogic.instance.BossDeath = true;
			if (Progress.levels.active_boss_pack_last_openned == 1 && !Progress.shop.BossDeath1Reward)
			{
				RaceLogic.instance.onAIKilled(civic.Reward);
			}
			if (Progress.levels.active_boss_pack_last_openned == 2 && !Progress.shop.BossDeath2Reward)
			{
				RaceLogic.instance.onAIKilled(civic.Reward);
			}
			if (Progress.levels.active_boss_pack_last_openned == 3 && !Progress.shop.BossDeath3Reward)
			{
				RaceLogic.instance.onAIKilled(civic.Reward);
			}
			if (base.name == "Police_car_Boss_3")
			{
				if (Progress.shop.BossDeath3)
				{
					for (int i = 0; i < RaceLogic.instance.race.activeAIs.Count; i++)
					{
						RaceLogic.instance.race.activeAIs[i].Death(withReward: false);
					}
					RaceLogic.instance.BossDeath = false;
					RaceLogic.instance.car.EngineModule.Break(onoff: true);
					RaceLogic.instance.OnBossFinish();
				}
			}
			else if (base.name == "Police_car_Boss_2")
			{
				if (Progress.shop.BossDeath2)
				{
					RaceLogic.instance.BossDeath = false;
					for (int j = 0; j < RaceLogic.instance.race.activeAIs.Count; j++)
					{
						RaceLogic.instance.race.activeAIs[j].Death(withReward: false);
					}
					RaceLogic.instance.car.EngineModule.Break(onoff: true);
					RaceLogic.instance.OnBossFinish();
				}
			}
			else if (base.name == "Police_car_Boss_1")
			{
				if (Progress.shop.BossDeath1)
				{
					RaceLogic.instance.BossDeath = false;
					for (int k = 0; k < RaceLogic.instance.race.activeAIs.Count; k++)
					{
						RaceLogic.instance.race.activeAIs[k].Death(withReward: false);
					}
					RaceLogic.instance.car.EngineModule.Break(onoff: true);
					RaceLogic.instance.OnBossFinish();
				}
			}
			else if (base.name == "Undergound_car_boss")
			{
				Progress.shop.BossDeath1Undeground = true;
				if (Progress.shop.BossDeath1Undeground)
				{
					RaceLogic.instance.BossDeath = false;
					for (int l = 0; l < RaceLogic.instance.race.activeAIs.Count; l++)
					{
						RaceLogic.instance.race.activeAIs[l].Death(withReward: false);
					}
					RaceLogic.instance.car.EngineModule.Break(onoff: true);
					RaceLogic.instance.OnBossFinish();
				}
			}
			else if (base.name == "Undergound2_car_boss")
			{
				Progress.shop.BossDeath2Undeground = true;
				if (Progress.shop.BossDeath2Undeground)
				{
					RaceLogic.instance.BossDeath = false;
					for (int m = 0; m < RaceLogic.instance.race.activeAIs.Count; m++)
					{
						RaceLogic.instance.race.activeAIs[m].Death(withReward: false);
					}
					RaceLogic.instance.car.EngineModule.Break(onoff: true);
					RaceLogic.instance.OnBossFinish();
				}
			}
			if (base.name == "Police_car_Boss_3")
			{
				foreach (Car2DAIController activeAI in RaceLogic.instance.race.activeAIs)
				{
					activeAI.Death(withReward: false);
				}
				RaceLogic.instance.BossDeath = false;
				Progress.shop.BossDeath3 = true;
				RaceLogic.instance.car.EngineModule.Break(onoff: true);
				RaceLogic.instance.OnBossFinish();
			}
			GameCenter.OnBossDestroy(BossNumber);
		}
		if (base.name.Contains("small"))
		{
			if (!IsCivic)
			{
				GameCenter.OnDestroyEnemy();
			}
			else
			{
				GameCenter.OnDestroyCivil();
			}
		}
		if ((base.transform.name.Contains("Police_car") || base.transform.name.Contains("Undergound_car_boss")) && reward)
		{
			RaceLogic.instance.CounterEmemys++;
		}
		if (IsCivic)
		{
			for (int n = 0; n < constructor.CollRubyForVzruv; n++)
			{
				GameObject gameObject = Pool.GameOBJECT(Pool.Bonus.ruby, base.transform.position);
				Vector2 force = new Vector2(UnityEngine.Random.Range(-25, 25), UnityEngine.Random.Range(0, 25));
				gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
			}
		}
		if (!IsCivic && !AsBoss)
		{
			int num = (constructor.location - 1) * 4;
			for (int num2 = 0; num2 < constructor.colScraps; num2++)
			{
				Pool.ScrapEnemy(base.transform.position, num + constructor.carType, constructor.location, IsCivic, constructor.scrap1, constructor.scrap2, constructor.scrap3, constructor.scrap4, constructor.scrap5, constructor._RGB);
				Pool.Animate(Pool.Explosion.exp25, base.transform.position);
			}
		}
		if (base.gameObject.name.Contains("pyro"))
		{
			int num3 = (constructor.location - 1) * 4;
			for (int num4 = 0; num4 < constructor.colScraps; num4++)
			{
				Pool.ScrapEnemy(base.transform.position, num3 + constructor.carType, constructor.location, IsCivic, constructor.scrap1, constructor.scrap2, constructor.scrap3, constructor.scrap4, constructor.scrap5, constructor._RGB);
				Pool.Animate(Pool.Explosion.exp25, base.transform.position);
			}
		}
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
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
		_yoffset = new Vector3(0f, 0f, 0f);
		Enabled = false;
		base.gameObject.SetActive(value: false);
	}

	public void SetDamage(float damage)
	{
		HealthModule.IncreaseHealth(0f - damage);
	}

	private void Update()
	{
		if (Time.timeScale != 0f)
		{
			LookAtTarget();
			EatDelay();
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
		else
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
		constructor.Turn(!isAhead);
		SetHealthLabel();
		int num3 = -1;
		int num4 = -1;
		for (int i = 0; i < SuspensionModule.HubsClasses.Count; i++)
		{
			if (i == 0)
			{
				num3 = i;
				num4 = i;
				continue;
			}
			Vector3 position9 = SuspensionModule.HubsClasses[num3].go.transform.position;
			float x5 = position9.x;
			Vector3 position10 = SuspensionModule.HubsClasses[i].go.transform.position;
			if (x5 < position10.x)
			{
				num3 = i;
			}
			Vector3 position11 = SuspensionModule.HubsClasses[num4].go.transform.position;
			float x6 = position11.x;
			Vector3 position12 = SuspensionModule.HubsClasses[i].go.transform.position;
			if (x6 > position12.x)
			{
				num4 = i;
			}
		}
		Vector2 connectedAnchor = SuspensionModule.HubsClasses[num4].SpringJ.connectedAnchor;
		float x7 = connectedAnchor.x;
		Vector2 connectedAnchor2 = SuspensionModule.HubsClasses[num4].SliderJ.connectedAnchor;
		float x8 = connectedAnchor2.x;
		Vector2 connectedAnchor3 = SuspensionModule.HubsClasses[num3].SpringJ.connectedAnchor;
		float x9 = connectedAnchor3.x;
		Vector2 connectedAnchor4 = SuspensionModule.HubsClasses[num3].SliderJ.connectedAnchor;
		float x10 = connectedAnchor4.x;
		float num6;
		float num8;
		for (int j = 0; j < SuspensionModule.HubsClasses.Count; j++)
		{
			if (j != num3 && j != num4)
			{
				float num5 = x9;
				Vector2 connectedAnchor5 = SuspensionModule.HubsClasses[j].SpringJ.connectedAnchor;
				num6 = num5 - connectedAnchor5.x;
				float num7 = x10;
				Vector2 connectedAnchor6 = SuspensionModule.HubsClasses[j].SliderJ.connectedAnchor;
				num8 = num7 - connectedAnchor6.x;
				SpringJoint2D springJ = SuspensionModule.HubsClasses[j].SpringJ;
				float x11 = x7 + num6;
				Vector2 connectedAnchor7 = SuspensionModule.HubsClasses[j].SpringJ.connectedAnchor;
				springJ.connectedAnchor = new Vector2(x11, connectedAnchor7.y);
				SliderJoint2D sliderJ = SuspensionModule.HubsClasses[j].SliderJ;
				float x12 = x8 + num8;
				Vector2 connectedAnchor8 = SuspensionModule.HubsClasses[j].SliderJ.connectedAnchor;
				sliderJ.connectedAnchor = new Vector2(x12, connectedAnchor8.y);
				Transform transform = SuspensionModule.HubsClasses[j].wheel.transform;
				Vector3 position13 = SuspensionModule.HubsClasses[num4].wheel.transform.position;
				float x13 = position13.x + num6;
				Vector3 position14 = SuspensionModule.HubsClasses[j].wheel.transform.position;
				float y = position14.y;
				Vector3 position15 = SuspensionModule.HubsClasses[j].wheel.transform.position;
				transform.position = new Vector3(x13, y, position15.z);
			}
		}
		Vector2 connectedAnchor9 = SuspensionModule.HubsClasses[num4].SpringJ.connectedAnchor;
		num6 = connectedAnchor9.x;
		Vector2 connectedAnchor10 = SuspensionModule.HubsClasses[num4].SliderJ.connectedAnchor;
		num8 = connectedAnchor10.x;
		SpringJoint2D springJ2 = SuspensionModule.HubsClasses[num4].SpringJ;
		Vector2 connectedAnchor11 = SuspensionModule.HubsClasses[num3].SpringJ.connectedAnchor;
		float x14 = connectedAnchor11.x;
		Vector2 connectedAnchor12 = SuspensionModule.HubsClasses[num4].SpringJ.connectedAnchor;
		springJ2.connectedAnchor = new Vector2(x14, connectedAnchor12.y);
		SliderJoint2D sliderJ2 = SuspensionModule.HubsClasses[num4].SliderJ;
		Vector2 connectedAnchor13 = SuspensionModule.HubsClasses[num3].SliderJ.connectedAnchor;
		float x15 = connectedAnchor13.x;
		Vector2 connectedAnchor14 = SuspensionModule.HubsClasses[num4].SliderJ.connectedAnchor;
		sliderJ2.connectedAnchor = new Vector2(x15, connectedAnchor14.y);
		SuspensionModule.HubsClasses[num4].wheel.transform.position = SuspensionModule.HubsClasses[num3].SpringJ.transform.position;
		SpringJoint2D springJ3 = SuspensionModule.HubsClasses[num3].SpringJ;
		float x16 = num6;
		Vector2 connectedAnchor15 = SuspensionModule.HubsClasses[num3].SpringJ.connectedAnchor;
		springJ3.connectedAnchor = new Vector2(x16, connectedAnchor15.y);
		SliderJoint2D sliderJ3 = SuspensionModule.HubsClasses[num3].SliderJ;
		float x17 = num8;
		Vector2 connectedAnchor16 = SuspensionModule.HubsClasses[num3].SliderJ.connectedAnchor;
		sliderJ3.connectedAnchor = new Vector2(x17, connectedAnchor16.y);
		SuspensionModule.HubsClasses[num3].wheel.transform.position = SuspensionModule.HubsClasses[num4].SpringJ.transform.position;
		if (BodySpritedamageforward_0.Count == 0)
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
			Transform[] componentsInChildren = bodyspriteforw.gameObject.transform.parent.parent.gameObject.GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			foreach (Transform transform2 in array)
			{
				if (transform2.name == strdmg_0)
				{
					BodySpritedamageforward_0.Add(transform2.gameObject);
				}
			}
			Transform[] array2 = componentsInChildren;
			foreach (Transform transform3 in array2)
			{
				if (transform3.name == strdmg_1)
				{
					BodySpritedamageforward_1.Add(transform3.gameObject);
				}
			}
			Transform[] array3 = componentsInChildren;
			foreach (Transform transform4 in array3)
			{
				if (transform4.name == strdmg_2)
				{
					BodySpritedamageforward_2.Add(transform4.gameObject);
				}
			}
		}
		SuspensionModule.Dir = (isAhead ? 1 : (-1));
		EngineModule.Dir = ((!isAhead) ? 1 : (-1));
		TurboModule.Dir = ((!isAhead) ? 1 : (-1));
		EatSensor componentInChildren2 = base.gameObject.GetComponentInChildren<EatSensor>();
		if ((bool)componentInChildren2)
		{
			componentInChildren2.Eating += Eating;
		}
		timeleft = returntime;
	}

	public void Eating(GameObject g)
	{
		if (can_eat && base.transform.name.Contains(str_bomb_car))
		{
			if (g.tag != CarEnemy)
			{
				return;
			}
			g.SendMessageUpwards("ChangeHealth", 0f - EatDamage, SendMessageOptions.DontRequireReceiver);
			if (Explosive == 0f)
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
				Audio.PlayAsyncRandom(actor_bite_01, actor_bite_01);
			}
		}
		else if (can_eat && (base.transform.name.Contains("Police_car") || base.transform.name.Contains("Undergound_car_boss") || base.transform.name.Contains("Undergound2_car_boss")) && (!(g.tag != CarMain) || !(g.tag != CarMainChild)))
		{
			EatDamage = RaceLogic.instance.car.HealthModule.BaseValue / 100f * DifficultyConfig.instance.Locations.PercentDmgPoliseCar;
			RaceLogic.instance.EatMainCar(EatDamage);
			can_eat = false;
			eatTime = eat_delay;
			if (!AsBoss)
			{
				Audio.PlayAsyncRandom(actor_bite_01, actor_bite_01);
			}
			else
			{
				Audio.PlayAsyncRandom("actor_roar_05_sn", "actor_roar_06_sn", "actor_roar_07_sn", "actor_roar_08_sn");
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DController : MonoBehaviour, ICarControlls
{
	public delegate void DieDelegate();

	public delegate void ValueChangedDelegate(float value);

	[Serializable]
	public class HubComplect
	{
		public List<Vector2> HUBs = new List<Vector2>();

		public int carType = -1;
	}

	private float preDieBombNum;

	[Header("srapsNumNum")]
	public string scrap1numnum = "enemy_bodov_sherif_car00_scp03";

	[Header("SCRAPS")]
	public string scrap1;

	public string scrap2;

	public string scrap3;

	public string scrap4;

	public string scrap5;

	public string scrap6;

	public string scrap7;

	public string scrap8;

	[HideInInspector]
	private Car2DEngineModuleBase _enginemodule;

	[HideInInspector]
	private Car2DHealthModule _healthmodule;

	[HideInInspector]
	private Car2DGadgetsModule _gadgetmodule;

	[HideInInspector]
	private Car2DSuspensionModuleBase _suspensionmodule;

	[HideInInspector]
	private Car2DTurboModule _turbomodule;

	public bool electric;

	[HideInInspector]
	private Car2DWeaponModuleBase _bombmodule;

	[Header("Controllers")]
	public Car2DHullController_Animator animHull;

	public Car2DTurboController_Animator animTurbo;

	public Car2DWeapons1Controller_Animator animWeapon;

	public Car2DEngineController_Animator animEngine;

	public Car2DWheelController_animator animWheel;

	public Car2DGadgetlogic animGadgets;

	[Header("if needed, if not left empty")]
	public Car2DHullController_Animator animHull2;

	[Space(10f)]
	public List<GameObject> wheels = new List<GameObject>();

	public List<Rigidbody2D> wheelsrigitbodyes;

	private List<HubComplect> tHUB_SJ = new List<HubComplect>();

	private Transform _bmbtr;

	private bool _isKinematic = true;

	private bool _enabled;

	[Header("init Start")]
	public bool start;

	private CarRespawner respawner;

	private EatSensor Eat;

	private bool respawn;

	public GameObject ConectorEff;

	private bool can_eat = true;

	private static string CarEnemy = "CarEnemy";

	private static string ObjectStr = "Object";

	private float Damage = 10f;

	private float DamageAfterBonus = 10f;

	private float hp_max;

	private float coll;

	private float deltaHp;

	[HideInInspector]
	public float collCus;

	public GameObject eff;

	public GameObject x2damageeff;

	public GameObject redusedamageeff;

	[HideInInspector]
	public float timerForElectric = 2f;

	[HideInInspector]
	public float rotationUse;

	[HideInInspector]
	public bool turboUse;

	[HideInInspector]
	public bool avtomove;

	private Action<bool> turboAvailable;

	private Action<bool> fireAvailable;

	[Range(0f, 5f)]
	public int bombType;

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

	public Car2DHealthModule HealthModule
	{
		get
		{
			if (_healthmodule == null)
			{
				_healthmodule = base.gameObject.AddComponent<Car2DHealthModule>();
			}
			return _healthmodule;
		}
	}

	public Car2DGadgetsModule GadgetModule
	{
		get
		{
			if (_gadgetmodule == null)
			{
				_gadgetmodule = base.gameObject.AddComponent<Car2DGadgetsModule>();
			}
			return _gadgetmodule;
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

	public Car2DTurboModule TurboModule
	{
		get
		{
			if (_turbomodule == null)
			{
				_turbomodule = base.gameObject.AddComponent<Car2DTurboModule>();
			}
			return _turbomodule;
		}
	}

	public Car2DWeaponModuleBase BombModule
	{
		get
		{
			if (_bombmodule == null)
			{
				_bombmodule = base.gameObject.AddComponent<Car2DWeaponModuleBase>();
			}
			return _bombmodule;
		}
	}

	public Transform BompSpawner
	{
		get
		{
			_bmbtr = base.transform.Find("BombSpawner");
			if (_bmbtr == null)
			{
				GameObject gameObject = new GameObject("BombSpawner");
				gameObject.transform.parent = base.transform;
				if (Progress.shop.activeCar == 2)
				{
					gameObject.transform.localPosition = new Vector2(-0.9f, 0.9f);
				}
				else if (Progress.shop.activeCar == 3)
				{
					gameObject.transform.localPosition = new Vector2(-1.8f, 1f);
				}
				else if (Progress.shop.activeCar == 4)
				{
					gameObject.transform.localPosition = new Vector2(-1.1f, 0.9f);
				}
				else if (Progress.shop.activeCar == 6)
				{
					gameObject.transform.localPosition = new Vector2(-1.8f, 0.6f);
				}
				else if (Progress.shop.activeCar == 7)
				{
					gameObject.transform.localPosition = new Vector2(-1.8f, 0.6f);
				}
				else if (Progress.shop.activeCar == 8)
				{
					gameObject.transform.localPosition = new Vector2(-1.1f, 1.1f);
				}
				else if (Progress.shop.activeCar == 9)
				{
					gameObject.transform.localPosition = new Vector2(-1.1f, 1.1f);
				}
				else if (Progress.shop.activeCar == 10)
				{
					gameObject.transform.localPosition = new Vector2(-1.5f, 1.1f);
				}
				else if (Progress.shop.activeCar == 11)
				{
					gameObject.transform.localPosition = new Vector2(-1.5f, 1.1f);
				}
				else if (Progress.shop.activeCar == 12)
				{
					gameObject.transform.localPosition = new Vector2(-1f, 1f);
				}
				else if (Progress.shop.activeCar == 13)
				{
					gameObject.transform.localPosition = new Vector2(-1f, 1f);
				}
				else
				{
					gameObject.transform.localPosition = new Vector2(-0.8f, 0.8f);
				}
				_bmbtr = gameObject.transform;
			}
			return _bmbtr;
		}
	}

	public bool isKinematic
	{
		get
		{
			return _isKinematic;
		}
		set
		{
			if (SuspensionModule.moduleInited)
			{
				_isKinematic = value;
				Rigidbody2D[] componentsInChildren = base.gameObject.GetComponentsInChildren<Rigidbody2D>(includeInactive: true);
				foreach (Rigidbody2D rigidbody2D in componentsInChildren)
				{
					if (value)
					{
						rigidbody2D.bodyType = RigidbodyType2D.Static;
					}
					else
					{
						rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
					}
					rigidbody2D.isKinematic = value;
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("You need to init SuspansionModule");
			}
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
			Car2DSuspensionModuleBase suspensionModule = SuspensionModule;
			bool flag = _enabled = value;
			BombModule.moduleEnabled = flag;
			flag = flag;
			TurboModule.moduleEnabled = flag;
			flag = flag;
			HealthModule.moduleEnabled = flag;
			flag = flag;
			EngineModule.moduleEnabled = flag;
			flag = flag;
			GadgetModule.Enabled = flag;
			suspensionModule.moduleEnabled = flag;
		}
	}

	public event DieDelegate OnDie;

	public event ValueChangedDelegate OnHeathChanged;

	public event ValueChangedDelegate OnTurboChanged;

	public event ValueChangedDelegate OnWeaponChanged;

	public event ValueChangedDelegate OnBoostHealthChanged;

	public event ValueChangedDelegate OnBoostTurboChanged;

	private void HealthChanged()
	{
		float num = _healthmodule._Barrel.Value / _healthmodule._Barrel.MaxValue;
		if (this.OnHeathChanged != null)
		{
			this.OnHeathChanged(num);
		}
		if (num <= 0f)
		{
			preDieBombNum = BombModule._Barrel.Value;
			Death();
			if (this.OnDie != null)
			{
				this.OnDie();
			}
		}
	}

	private void BoostHealthChanged()
	{
		if (this.OnBoostHealthChanged != null)
		{
			this.OnBoostHealthChanged(_healthmodule.HealthBoost / _healthmodule._Barrel.MaxValue);
		}
	}

	private void BombShoot(Transform bmb)
	{
		WhenBompIsDropped(bmb);
	}

	private void BoostTurboChanged()
	{
		if (this.OnBoostTurboChanged != null)
		{
			this.OnBoostTurboChanged(_turbomodule.TurboBoost / _turbomodule._Barrel.MaxValue);
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

	private void WeaponChanged()
	{
		if (this.OnWeaponChanged != null)
		{
			this.OnWeaponChanged(_bombmodule._Barrel.Value);
		}
	}

	private void ClearEventListeners()
	{
		this.OnHeathChanged = null;
		this.OnTurboChanged = null;
		this.OnWeaponChanged = null;
		this.OnDie = null;
		this.OnBoostHealthChanged = null;
		this.OnBoostTurboChanged = null;
	}

	private void OnDestroy()
	{
		ClearEventListeners();
	}

	public List<Rigidbody2D> WheelsRigitbodies()
	{
		if (wheelsrigitbodyes == null)
		{
			List<Rigidbody2D> list = new List<Rigidbody2D>();
			foreach (GameObject wheel in wheels)
			{
				if (wheel.name.Contains("_1") && Progress.shop.Car.engineActLev == 0)
				{
					wheel.SetActive(value: true);
					list.Add(wheel.GetComponentInParent<Rigidbody2D>());
				}
				else if (wheel.name.Contains("_2") && Progress.shop.Car.engineActLev == 1)
				{
					wheel.SetActive(value: true);
					list.Add(wheel.GetComponentInParent<Rigidbody2D>());
				}
				else if (wheel.name.Contains("_3") && Progress.shop.Car.engineActLev == 2)
				{
					wheel.SetActive(value: true);
					list.Add(wheel.GetComponentInParent<Rigidbody2D>());
				}
				else if (wheel.name.Contains("_4") && Progress.shop.Car.engineActLev == 3)
				{
					wheel.SetActive(value: true);
					list.Add(wheel.GetComponentInParent<Rigidbody2D>());
				}
			}
			wheelsrigitbodyes = list;
		}
		return wheelsrigitbodyes;
	}

	public List<Transform> WheelsTransforms()
	{
		CircleCollider2D[] componentsInChildren = GetComponentsInChildren<CircleCollider2D>();
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (!componentsInChildren[i].isTrigger && componentsInChildren[i].GetComponent<Rigidbody2D>() != null)
			{
				list.Add(componentsInChildren[i].transform);
			}
		}
		return list;
	}

	private void setPrevHubs()
	{
		int num = 1;
		int num2 = -1;
		for (int i = 0; i < tHUB_SJ.Count; i++)
		{
			if (tHUB_SJ[i].carType == num)
			{
				num2 = i;
				break;
			}
		}
		if (num2 != -1)
		{
			for (int j = 0; j < SuspensionModule.Hubs.Count; j++)
			{
				for (int k = 0; k < wheelsrigitbodyes.Count; k++)
				{
					if (SuspensionModule.Hubs[j].GetComponent<HingeJoint2D>().connectedBody == wheelsrigitbodyes[k])
					{
						SuspensionModule.Hubs[j].GetComponent<SpringJoint2D>().connectedAnchor = tHUB_SJ[num2].HUBs[j];
						SuspensionModule.Hubs[j].GetComponent<SliderJoint2D>().connectedAnchor = tHUB_SJ[num2].HUBs[j];
						break;
					}
				}
			}
			return;
		}
		HubComplect hubComplect = new HubComplect();
		hubComplect.carType = num;
		for (int l = 0; l < SuspensionModule.Hubs.Count; l++)
		{
			for (int m = 0; m < wheelsrigitbodyes.Count; m++)
			{
				if (SuspensionModule.Hubs[l].GetComponent<HingeJoint2D>().connectedBody == wheelsrigitbodyes[m])
				{
					hubComplect.HUBs.Add(SuspensionModule.Hubs[l].GetComponent<SpringJoint2D>().connectedAnchor);
					break;
				}
			}
		}
		tHUB_SJ.Add(hubComplect);
	}

	public void StopUnstopCar(bool stop)
	{
		if (stop)
		{
			isKinematic = true;
		}
		else
		{
			isKinematic = false;
		}
	}

	private void TryShowGun(bool premiumEquipped3)
	{
		if (!Progress.shop.BuyedDrill || premiumEquipped3)
		{
			return;
		}
		TurelScript[] componentsInChildren = base.gameObject.GetComponentsInChildren<TurelScript>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.name == "Turel")
			{
				componentsInChildren[i].gameObject.SetActive(value: false);
			}
			if (componentsInChildren[i].gameObject.name == "turel_2")
			{
				componentsInChildren[i].gameObject.SetActive(value: true);
			}
			if (Progress.shop.NowSelectCarNeedForMe == 2)
			{
				componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, 0.4f);
			}
			else
			{
				componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, 0f);
			}
		}
	}

	public void ShowTurel()
	{
		TurelScript[] componentsInChildren = base.gameObject.GetComponentsInChildren<TurelScript>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.GetComponent<SpriteRenderer>() == null)
			{
				componentsInChildren[i].gameObject.SetActive(value: true);
				if (Progress.shop.NowSelectCarNeedForMe == 2)
				{
					componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, 0.4f);
				}
				else
				{
					componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, 0f);
				}
				if (componentsInChildren[i].gameObject.name == "turel_2")
				{
					componentsInChildren[i].gameObject.SetActive(value: false);
				}
			}
		}
	}

	public void HideTurel()
	{
		TurelScript[] componentsInChildren = base.gameObject.GetComponentsInChildren<TurelScript>(includeInactive: true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].gameObject.GetComponent<SpriteRenderer>() == null)
			{
				if (componentsInChildren[i].name != "LedoLuch")
				{
					componentsInChildren[i].gameObject.SetActive(value: false);
				}
				if (Progress.shop.NowSelectCarNeedForMe == 2)
				{
					componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, -0.4f);
				}
				else
				{
					componentsInChildren[i].gameObject.transform.localPosition = new Vector2(0f, 0f);
				}
			}
		}
	}

	public void OnEnemyBullet(Car2DGun.BulletType bulletType, GuiContainer gui)
	{
		switch (bulletType)
		{
		case Car2DGun.BulletType.Basic:
			break;
		case Car2DGun.BulletType.Slow:
			break;
		case Car2DGun.BulletType.Debuff:
			break;
		}
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

	private void DamageHull(float damage)
	{
		animHull.healt = damage;
		if (animHull2 != null)
		{
			animHull2.healt = damage;
		}
	}

	private void Start()
	{
		if (start)
		{
			Init(Progress.shop.Car);
		}
		respawner = base.gameObject.GetComponent<CarRespawner>();
	}

	public void Init(Progress.Shop.CarInfo carParams)
	{
		GetModulesReferences(carParams.id);
		SuspensionModule.Init(GetComponent<Rigidbody2D>(), WheelsRigitbodies());
		EngineModule.Init(WheelsRigitbodies());
		HealthModule.Init(DamageHull, HealthChanged, myCar: true);
		TurboModule.Init(BodyRegitBodyes(), TurboChanged);
		TurboModule._Barrel.Value = TurboModule._Barrel.MaxValue;
		BombModule.Init(base.transform, WeaponChanged, BombShoot);
		respawn = false;
		SetCarUpgrades(carParams);
		bombType = Mathf.Clamp(Progress.shop.Car.bombActLev, 0, 5);
		if (Game.currentState != Game.gameState.Shop)
		{
			FindRam();
			CarRespawner component = base.gameObject.GetComponent<CarRespawner>();
			component.CheckLayers();
			if (carParams.id == 2 && HealthModule._barrel.MaxValue < 800f)
			{
				HealthModule._barrel.MaxValue = 800f;
				HealthModule._barrel.Value = 800f;
			}
			if (carParams.id == 7 && HealthModule._barrel.MaxValue < 700f)
			{
				HealthModule._barrel.MaxValue = 700f;
				HealthModule._barrel.Value = 700f;
			}
			StartCoroutine(DelatToHubsLEX());
		}
	}

	private IEnumerator DelatToHubsLEX()
	{
		int del = 20;
		while (del > 0)
		{
			del--;
			yield return null;
		}
		setPrevHubs();
	}

	public void Restart(Progress.Shop.CarInfo carParams)
	{
		preDieBombNum = 0f;
		base.gameObject.SetActive(value: true);
		StartCoroutine(OnOFFLimits());
		Utilities.WaitForRealSeconds(0.5f);
		respawn = false;
		SetCarParams(carParams);
		Transform transform = base.gameObject.transform;
		Vector3 position = RaceLogic.instance.race.start.position;
		float x = position.x;
		Vector3 position2 = RaceLogic.instance.race.start.position;
		float y = position2.y + 2f;
		Vector3 position3 = RaceLogic.instance.race.start.position;
		transform.position = new Vector3(x, y, position3.z);
		base.transform.eulerAngles = Vector3.zero;
		electric = false;
		StartCoroutine(DelatToHubsLEX());
		base.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		base.gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
	}

	private void SetCarParams(Progress.Shop.CarInfo carParams)
	{
		rotationUse = 0f;
		GetModulesReferences(carParams.id);
		SetCarUpgrades(carParams);
		Audio.Stop("gfx_turbo_01_sn");
		HealthModule.HealthBoost = 0f;
		TurboModule.TurboBoost = 0f;
		TurboModule._Barrel.Value = TurboModule._Barrel.MaxValue;
		HealthModule._Barrel.Value = HealthModule._Barrel.MaxValue;
		bombType = Mathf.Clamp(Progress.shop.Car.bombActLev, 0, 5);
		FindRam();
		Rigidbody2D[] componentsInChildren = base.gameObject.GetComponentsInChildren<Rigidbody2D>();
		foreach (Rigidbody2D rigidbody2D in componentsInChildren)
		{
			rigidbody2D.velocity = Vector2.zero;
		}
		Enabled = true;
		turboUse = false;
		Turbo(Use: false);
		if (base.gameObject.activeSelf)
		{
			StartCoroutine(SuspensionModule.Shake());
		}
	}

	public void Respawn(Progress.Shop.CarInfo carParams)
	{
		respawn = true;
		base.gameObject.SetActive(value: true);
		StartCoroutine(OnOFFLimits());
		SetCarParams(carParams);
		respawner.Respawn();
		BombModule._Barrel.Value = preDieBombNum;
		electric = false;
		StartCoroutine(testTest());
	}

	private IEnumerator testTest()
	{
		_enginemodule.Break(onoff: true);
		yield return new WaitForSeconds(1f);
		_enginemodule.Break(onoff: false);
		_turbomodule.enabled = true;
		_turbomodule.moduleEnabled = true;
		_turbomodule._barrel.enabled = true;
		_turbomodule._barrel.Enable = true;
	}

	public IEnumerator OnOFFLimits()
	{
		if (SuspensionModule.Hubs.Count > 0)
		{
			foreach (GameObject hub in SuspensionModule.Hubs)
			{
				hub.GetComponent<HingeJoint2D>().enabled = false;
				hub.SetActive(value: false);
			}
			yield return new WaitForSeconds(0.05f);
			while (!SuspensionModule.Wheels[0].gameObject.activeSelf)
			{
				yield return 0;
			}
			foreach (GameObject hub2 in SuspensionModule.Hubs)
			{
				hub2.GetComponent<HingeJoint2D>().enabled = true;
				hub2.SetActive(value: true);
			}
		}
	}

	public void GetModulesReferences(int car_id)
	{
		CarsValues.StartData carSettings = CarsValues.StartData.GetCarSettings(isAi: false, car_id);
		HealthModule.BaseValue = carSettings._health.BaseValue;
		TurboModule.Angle = carSettings._turbo.Angle;
		TurboModule._Barrel.MaxValue = carSettings._turbo.MaxValue * 100f;
		TurboModule._Barrel.Value = carSettings._turbo.Value * 100f;
		TurboModule._Barrel.UsageValue = carSettings._turbo.UsageValue * 100f;
		TurboModule.Power = carSettings._turbo.Power;
		SuspensionModule.BodyMass = carSettings._suspansion.BodyMass;
		SuspensionModule.WheelMass = carSettings._suspansion.WheelMass;
		SuspensionModule.SpringMass = carSettings._suspansion.SpringMass;
		SuspensionModule.Distance = carSettings._suspansion.Distance;
		SuspensionModule.Frequency = carSettings._suspansion.Frequency;
		SuspensionModule.WheelsFriction = carSettings._suspansion.WheelsFriction;
		SuspensionModule.Damping = carSettings._suspansion.Damping;
		SuspensionModule.RotationAcceleration = carSettings._suspansion.RotationAcceleration;
		EngineModule.MaxSpeed = carSettings._engine.MaxSpeed;
		EngineModule.Torque = carSettings._engine.Torque;
		BombModule.Distance = carSettings._rockets.Distance;
		BombModule._Barrel.MaxValue = carSettings._rockets.MaxValue;
		BombModule._Barrel.Value = carSettings._rockets.Value;
		BombModule.StartPower = carSettings._rockets.StartPower;
		BombModule.PauseTime = carSettings._rockets.PauseTime;
		Damage = carSettings._ramDamage.Damage;
	}

	public void SetCarUpgrades(Progress.Shop.CarInfo carParams)
	{
		if (carParams.id == 0)
		{
			CarsValues.Data carSettings = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings.TurboUp.Value[carParams.turboActLev - 1], carSettings.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 1)
		{
			CarsValues.Data carSettings2 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings2.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings2.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings2.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings2.TurboUp.Value[carParams.turboActLev - 1], carSettings2.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings2.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 2)
		{
			CarsValues.Data carSettings3 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings3.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings3.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings3.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings3.TurboUp.Value[carParams.turboActLev - 1], carSettings3.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings3.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 3)
		{
			CarsValues.Data carSettings4 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings4.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings4.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings4.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings4.TurboUp.Value[carParams.turboActLev - 1], carSettings4.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings4.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 5)
		{
			CarsValues.Data carSettings5 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings5.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings5.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings5.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings5.TurboUp.Value[carParams.turboActLev - 1], carSettings5.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings5.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 6)
		{
			CarsValues.Data carSettings6 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings6.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings6.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings6.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings6.TurboUp.Value[carParams.turboActLev - 1], carSettings6.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings6.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 7)
		{
			CarsValues.Data carSettings7 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings7.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings7.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings7.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings7.TurboUp.Value[carParams.turboActLev - 1], carSettings7.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings7.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 8)
		{
			CarsValues.Data carSettings8 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings8.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings8.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings8.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings8.TurboUp.Value[carParams.turboActLev - 1], carSettings8.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings8.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 9)
		{
			CarsValues.Data carSettings9 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings9.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings9.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings9.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings9.TurboUp.Value[carParams.turboActLev - 1], carSettings9.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings9.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 10)
		{
			CarsValues.Data carSettings10 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings10.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings10.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings10.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings10.TurboUp.Value[carParams.turboActLev - 1], carSettings10.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings10.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 11)
		{
			CarsValues.Data carSettings11 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings11.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings11.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings11.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings11.TurboUp.Value[carParams.turboActLev - 1], carSettings11.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings11.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 12)
		{
			CarsValues.Data carSettings12 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings12.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings12.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings12.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings12.TurboUp.Value[carParams.turboActLev - 1], carSettings12.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings12.RamDamage[carParams.weaponActLev - 1];
			}
		}
		else if (carParams.id == 13)
		{
			CarsValues.Data carSettings13 = CarsValues.Data.GetCarSettings(carParams.id);
			if (carParams.healthActLev != 0)
			{
				HealthModule.UpdateModuleValue(carSettings13.Health[carParams.healthActLev - 1]);
			}
			if (carParams.engineActLev != 0)
			{
				EngineModule.UpdateModuleValue(carSettings13.Engine[carParams.engineActLev - 1]);
				SuspensionModule.SetFrictionCoeficient(carSettings13.Wheels[carParams.engineActLev - 1]);
			}
			if (carParams.turboActLev != 0)
			{
				TurboModule.UpdateModuleValue(carSettings13.TurboUp.Value[carParams.turboActLev - 1], carSettings13.TurboUp.Power[carParams.turboActLev - 1]);
			}
			if (carParams.weaponActLev != 0)
			{
				Damage = carSettings13.RamDamage[carParams.weaponActLev - 1];
			}
		}
	}

	public void FindRam()
	{
		if (Eat == null)
		{
			Eat = base.gameObject.GetComponentInChildren<EatSensor>();
		}
		if (Eat != null)
		{
			Eat.Eating += Eating;
		}
		can_eat = true;
	}

	private int Ruby()
	{
		float num = hp_max / coll;
		int result = (int)((DamageAfterBonus + deltaHp) / num);
		deltaHp = (DamageAfterBonus + deltaHp) % num;
		return result;
	}

	public void Eating(GameObject g)
	{
		if (!base.gameObject.activeSelf || HealthModule.AnDeath || (g.tag != CarEnemy && g.tag != ObjectStr))
		{
			return;
		}
		float num = Damage;
		if (Progress.fortune.SumPercentDamage != 0f)
		{
			float num2 = num * 0.01f * Progress.fortune.SumPercentDamage;
			num += num2;
		}
		DamageAfterBonus = num;
		if (!can_eat)
		{
			return;
		}
		animWeapon.Damage();
		if (Progress.settings.x2damage)
		{
			g.SendMessageUpwards("ChangeHealth", -1f * num * 2f, SendMessageOptions.DontRequireReceiver);
		}
		else if (Progress.settings.ReduceDamage)
		{
			g.SendMessageUpwards("ChangeHealth", -1f * num / 2f, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			g.SendMessageUpwards("ChangeHealth", 0f - num, SendMessageOptions.DontRequireReceiver);
		}
		if (Progress.settings.x2damage)
		{
			g.SendMessageUpwards("OnHit", num * 2f, SendMessageOptions.DontRequireReceiver);
		}
		else if (Progress.settings.ReduceDamage)
		{
			g.SendMessageUpwards("OnHit", num / 2f, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			g.SendMessageUpwards("OnHit", num, SendMessageOptions.DontRequireReceiver);
		}
		Car2DAIController componentInParent = g.GetComponentInParent<Car2DAIController>();
		Car2DControlerForBombCar componentInParent2 = g.GetComponentInParent<Car2DControlerForBombCar>();
		if (g != null && componentInParent != null)
		{
			if (!componentInParent.IsCivic)
			{
				collCus += 1f;
			}
			Pool.ScrapEnemyNumNum(g.transform.position, Progress.levels.active_pack, componentInParent.IsCivic, Progress.levels.active_pack, componentInParent.constructor.scrap1y, componentInParent.constructor.scrap2y, componentInParent.constructor.scrap3y, componentInParent.constructor.scrap4y, componentInParent.constructor.scrap5y, componentInParent.constructor._RGB);
			if (componentInParent.IsCivic)
			{
				hp_max = componentInParent.HealthModule._barrel.MaxValue;
				coll = componentInParent.CollRubyForYkys;
				int num3 = Ruby();
				if (num3 > 0)
				{
					for (int i = 0; i < num3; i++)
					{
						Vector3 position = g.transform.position;
						float x = position.x + 1f;
						Vector3 position2 = g.transform.position;
						GameObject gameObject = Pool.GameOBJECT(Pool.Bonus.ruby, new Vector2(x, position2.y + 1f));
						gameObject.GetComponent<CircleCollider2D>().enabled = true;
						Vector2 force = new Vector2(UnityEngine.Random.Range(20, 30), UnityEngine.Random.Range(10, 20));
						gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
						ScrapCommonLogic.instance.animateScrap(gameObject, g.transform.position, UnityEngine.Random.Range(-60, 120), UnityEngine.Random.Range(3, 6), 5f, rotate: true, 0.2f);
					}
				}
			}
		}
		if (g != null && componentInParent2 != null && componentInParent2.IsCivic)
		{
			hp_max = componentInParent2.HealthModule._barrel.MaxValue;
			coll = componentInParent2.constructor.CollRubyForYkys;
			int num4 = Ruby();
			if (num4 > 0)
			{
				for (int j = 0; j < num4; j++)
				{
					Vector3 position3 = g.transform.position;
					float x2 = position3.x + 1f;
					Vector3 position4 = g.transform.position;
					GameObject gameObject2 = Pool.GameOBJECT(Pool.Bonus.ruby, new Vector2(x2, position4.y + 1f));
					gameObject2.GetComponent<CircleCollider2D>().enabled = true;
					Vector2 force2 = new Vector2(UnityEngine.Random.Range(20, 30), UnityEngine.Random.Range(10, 20));
					gameObject2.GetComponent<Rigidbody2D>().AddForce(force2, ForceMode2D.Impulse);
					ScrapCommonLogic.instance.animateScrap(gameObject2, g.transform.position, UnityEngine.Random.Range(-60, 120), UnityEngine.Random.Range(3, 6), 5f, rotate: true, 0.2f);
				}
			}
		}
		StartCoroutine("EatDelay");
		Audio.PlayAsyncRandom("actor_bite_01", "actor_bite_01");
	}

	private IEnumerator EatDelay()
	{
		can_eat = false;
		yield return new WaitForSeconds(0.3f);
		can_eat = true;
		animWeapon.OffDamage();
	}

	public void DeathWithOnDie()
	{
		Death();
		this.OnDie();
	}

	public void Death()
	{
		Pool.ScrapForMainCar(base.transform.position, 1, scrap1, scrap2, scrap3, scrap4, scrap5, scrap6, scrap7, scrap8);
		if (!HealthModule.AnDeath)
		{
			Audio.Play("crash_car_01_sn");
		}
		if (!HealthModule.AnDeath)
		{
			Audio.Stop("gfx_turbo_01_sn");
		}
		if (eff != null)
		{
			eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		}
		Enabled = false;
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator timer_electric(float timer)
	{
		while (electric)
		{
			yield return 0;
			if (!HealthModule.AnDeath)
			{
				Audio.Play("electrick");
			}
			if (timer <= 0f)
			{
				electric = false;
				Accelerate(1f);
				EngineModule.enabled = true;
			}
			else
			{
				timer -= Time.deltaTime;
				EngineModule.enabled = false;
			}
		}
	}

	public void Update()
	{
		if (!Enabled)
		{
			return;
		}
		StartCoroutine(timer_electric(timerForElectric));
		UpdateKeys();
		if (Progress.settings.x2damage)
		{
			x2damageeff.SetActive(value: true);
			redusedamageeff.SetActive(value: false);
		}
		else if (Progress.settings.ReduceDamage)
		{
			x2damageeff.SetActive(value: false);
			redusedamageeff.SetActive(value: true);
		}
		else
		{
			x2damageeff.SetActive(value: false);
			redusedamageeff.SetActive(value: false);
		}
		if (electric)
		{
			TurboModule.isUsed = false;
			turboUse = false;
			TurboModule.enabled = false;
			TurboModule._barrel.enabled = false;
			if (eff == null)
			{
				eff = Pool.Animate(Pool.Explosion.exp2, base.transform, "GUI");
				eff.transform.position = ConectorEff.transform.position;
				return;
			}
			eff.gameObject.SetActive(value: true);
			if (!eff.GetComponent<ParticleSystem>().isPlaying)
			{
				eff.GetComponent<ParticleSystem>().Play(withChildren: true);
			}
			eff.transform.position = ConectorEff.transform.position;
		}
		else if (eff != null)
		{
			TurboModule.enabled = true;
			TurboModule._barrel.enabled = true;
			eff.GetComponent<ParticleSystem>().Stop(withChildren: true);
		}
	}

	private void UpdateKeys()
	{
		if (!electric)
		{
			Accelerate(1f);
		}
		if (Mathf.Abs(rotationUse) > 0.5f)
		{
			Rotate(-1 * Math.Sign(rotationUse));
		}
		else
		{
			Rotate(0f);
		}
		Turbo(turboUse);
		UpdateBarellsButtons();
	}

	public void SetCallbackControll(Action<bool> turboAvailable, Action<bool> fireAvailable)
	{
		this.turboAvailable = turboAvailable;
		this.fireAvailable = fireAvailable;
	}

	private void UpdateBarellsButtons()
	{
		if (turboAvailable != null)
		{
			turboAvailable((TurboModule._Barrel.Value >= 0.05f || TurboModule._Barrel.Restore) ? true : false);
		}
		if (fireAvailable != null)
		{
			fireAvailable((BombModule._Barrel.Value >= 0.05f) ? true : false);
		}
	}

	public void Accelerate(float direction, float multipler = 1f)
	{
		if (Time.timeScale != 0f)
		{
			EngineModule.Move(direction, multipler);
		}
	}

	public void Rotate(float direction)
	{
		if (SuspensionModule.moduleEnabled)
		{
			SuspensionModule.Rotate(direction);
		}
	}

	public void Turbo(bool Use)
	{
		TurboModule.UsePower(Use);
		if (!TurboModule.moduleEnabled)
		{
			return;
		}
		if ((TurboModule.isUsed && TurboModule._Barrel.Value > 0f) || (TurboModule.TurboBoost > 0f && TurboModule.boostUsed))
		{
			animTurbo.On();
		}
		else
		{
			animTurbo.Off();
		}
		if (Use && TurboModule._Barrel.Value > 0f)
		{
			if (!HealthModule.AnDeath)
			{
				Audio.Play("gfx_turbo_01_sn", Audio.soundVolume * 1.5f, loop: true);
			}
		}
		else if (!HealthModule.AnDeath)
		{
			Audio.Stop("gfx_turbo_01_sn");
		}
	}

	public void Jump(bool Use)
	{
	}

	public void Fire()
	{
		if (BombModule.moduleEnabled && !(BombModule._Barrel.Value <= 0f))
		{
			string name = "Bomb_" + bombType;
			Rigidbody2D component = Pool.instance.GetObject(name).GetComponent<Rigidbody2D>();
			Rigidbody2D item = component;
			component.gameObject.transform.localScale = Vector3.one * 1.5f;
			Vector2 dir = new Vector2(-1f, 1f);
			BombModule.Fire(item, BompSpawner.position, dir);
		}
	}

	public void WhenBompIsDropped(Transform tr)
	{
		BombLogic.instance.bomb(tr);
		if (!HealthModule.AnDeath)
		{
			Audio.PlayAsync("gfx_bomb_01_sn", Audio.soundVolume, loop: true);
		}
		if (!HealthModule.AnDeath)
		{
			Audio.PlayAsync("gfx_bomb_02_sn");
		}
	}

	public void AddExtraTurbo(float percent = 100f)
	{
		StartCoroutine(TurboModule.LerpFloat(TurboModule._Barrel.MaxValue / 100f * percent));
	}

	public void AddExtraHealth(float percent = 100f)
	{
		StartCoroutine(HealthModule.LerpFloat(HealthModule._Barrel.MaxValue / 100f * percent));
	}
}

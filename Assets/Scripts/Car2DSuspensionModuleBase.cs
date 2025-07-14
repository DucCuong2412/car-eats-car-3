using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Car2DSuspensionModuleBase : Car2DModuleBase, ISuspensionModule
{
	[SerializeField]
	public class HubClass
	{
		public GameObject wheel;

		public GameObject go;

		public SpringJoint2D SpringJ;

		public SliderJoint2D SliderJ;
	}

	public float BodyMass;

	public float WheelMass;

	public float SpringMass;

	public float Distance;

	public float Frequency;

	[HideInInspector]
	public float MaxFriquency = 20f;

	[HideInInspector]
	public int Dir;

	public float Damping;

	[Range(0.5f, 1000f)]
	public float WheelsFriction = 0.5f;

	public float RotationAcceleration = 300f;

	[HideInInspector]
	public List<SpringJoint2D> Springs = new List<SpringJoint2D>();

	[HideInInspector]
	public List<GameObject> Hubs = new List<GameObject>();

	public List<HubClass> HubsClasses = new List<HubClass>();

	public Rigidbody2D Body;

	public List<Rigidbody2D> Wheels;

	private float direction;

	private float dist;

	private bool up = true;

	private bool shaking;

	private int Difficult = 1;

	public Rigidbody2D rearwheel = new Rigidbody2D();

	public Rigidbody2D frontwheel = new Rigidbody2D();

	private bool IsConfigured;

	private float StartDistance;

	private bool isAI;

	private float RotationSpeed
	{
		get
		{
			return Body.angularVelocity;
		}
		set
		{
			if (Body != null)
			{
				Body.angularVelocity = value;
			}
		}
	}

	public override void onModuleEnable()
	{
		if (isAI)
		{
			StartCoroutine(AISuspension());
		}
	}

	public override void onModuleDisable()
	{
	}

	public override void onModuleInited()
	{
	}

	public void ClearHubsLists()
	{
		Springs.Clear();
		Hubs.Clear();
		HubsClasses.Clear();
	}

	public void Init(Rigidbody2D body, List<Rigidbody2D> wheels)
	{
		MaxFriquency = Frequency;
		Body = body;
		Wheels = wheels;
		AddHubs(wheels, body.transform);
		base.moduleInited = true;
		SetFrictionCoeficient();
		UpdateSettings();
		StartCoroutine(Shake());
	}

	public void UpdateSettings()
	{
		if (!base.moduleInited || !Body.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < Springs.Count; i++)
		{
			if (Springs[i] == null || !Springs[i].gameObject.activeSelf)
			{
				FindSprings();
				return;
			}
			Springs[i].dampingRatio = Damping;
			Springs[i].frequency = MaxFriquency;
			Springs[i].distance = 0.005f;
		}
		foreach (Rigidbody2D wheel in Wheels)
		{
			wheel.mass = WheelMass / (float)Wheels.Count;
		}
		Body.mass = BodyMass;
		SetFrictionCoeficient(WheelsFriction);
	}

	private void FindSprings()
	{
		Springs.Clear();
		SpringJoint2D[] componentsInChildren = Body.GetComponentsInChildren<SpringJoint2D>();
		foreach (SpringJoint2D item in componentsInChildren)
		{
			Springs.Add(item);
		}
		UpdateSettings();
	}

	private void AddHubs(List<Rigidbody2D> wheels, Transform Body)
	{
		for (int i = 0; i < wheels.Count; i++)
		{
			if (Hubs.Count == i)
			{
				GameObject gameObject = new GameObject();
				gameObject.layer = 8;
				gameObject.name = "Hub";
				gameObject.transform.parent = Body;
				gameObject.AddComponent<Rigidbody2D>().mass = SpringMass / (float)Wheels.Count;
				gameObject.AddComponent<HingeJoint2D>();
				Springs.Add(gameObject.AddComponent<SpringJoint2D>());
				gameObject.AddComponent<SliderJoint2D>().connectedBody = Body.GetComponent<Rigidbody2D>();
				gameObject.GetComponent<SpringJoint2D>().connectedBody = Body.GetComponent<Rigidbody2D>();
				gameObject.transform.position = wheels[i].transform.position;
				gameObject.GetComponent<SpringJoint2D>().anchor = Vector2.zero;
				gameObject.GetComponent<SliderJoint2D>().anchor = Vector2.zero;
				Hubs.Add(gameObject);
				HubsClasses.Add(new HubClass());
				HubsClasses[HubsClasses.Count - 1].wheel = wheels[i].gameObject;
				HubsClasses[HubsClasses.Count - 1].go = gameObject;
				HubsClasses[HubsClasses.Count - 1].SliderJ = gameObject.GetComponent<SliderJoint2D>();
				HubsClasses[HubsClasses.Count - 1].SpringJ = gameObject.GetComponent<SpringJoint2D>();
			}
			Hubs[i].transform.position = wheels[i].transform.position;
			Hubs[i].GetComponent<HingeJoint2D>().connectedBody = wheels[i];
			Hubs[i].GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = false;
			Hubs[i].GetComponent<SliderJoint2D>().autoConfigureAngle = false;
			Hubs[i].GetComponent<SpringJoint2D>().autoConfigureDistance = false;
			Hubs[i].GetComponent<SpringJoint2D>().connectedAnchor = Hubs[i].transform.localPosition;
			SliderJoint2D component = Hubs[i].GetComponent<SliderJoint2D>();
			component.connectedAnchor = Hubs[i].transform.localPosition;
			component.angle = -90f;
			component.useLimits = true;
			JointTranslationLimits2D limits = default(JointTranslationLimits2D);
			limits.max = 0f;
			limits.min = 0f - Distance;
			component.limits = limits;
			if (base.gameObject.name.Contains("Police_car") || base.gameObject.name.Contains("Enemy_pyro"))
			{
				SliderJoint2D component2 = Hubs[i].GetComponent<SliderJoint2D>();
				Vector2 connectedAnchor = Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor;
				component2.connectedAnchor = new Vector2(connectedAnchor.x, 0f);
				SpringJoint2D component3 = Hubs[i].GetComponent<SpringJoint2D>();
				Vector2 connectedAnchor2 = Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor;
				component3.connectedAnchor = new Vector2(connectedAnchor2.x, 0f);
			}
			if (base.gameObject.name.Contains("Police_car_Boss_1") || base.gameObject.name.Contains("Police_car_Boss_2"))
			{
				SliderJoint2D component4 = Hubs[i].GetComponent<SliderJoint2D>();
				Vector2 connectedAnchor3 = Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor;
				component4.connectedAnchor = new Vector2(connectedAnchor3.x, -1f);
				SpringJoint2D component5 = Hubs[i].GetComponent<SpringJoint2D>();
				Vector2 connectedAnchor4 = Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor;
				component5.connectedAnchor = new Vector2(connectedAnchor4.x, -1f);
			}
			if (base.gameObject.name.Contains("Civilian_easter"))
			{
				if (wheels[i].gameObject.name.Contains("RearWheel"))
				{
					Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor = new Vector2(2.2f, -0.83f);
					Hubs[i].GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(2.2f, -0.83f);
				}
				else if (wheels[i].gameObject.name.Contains("FrontWheel"))
				{
					Hubs[i].GetComponent<SliderJoint2D>().connectedAnchor = new Vector2(-0.32f, -0.92f);
					Hubs[i].GetComponent<SpringJoint2D>().connectedAnchor = new Vector2(-0.32f, -0.92f);
				}
			}
		}
	}

	public void SetFrictionCoeficient(float _friction = 1f, float _bouncines = 0.5f)
	{
		PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D("InstanceWheelMaterilal");
		physicsMaterial2D.friction = _friction;
		physicsMaterial2D.bounciness = _bouncines;
		foreach (Rigidbody2D wheel in Wheels)
		{
			if (!wheel.gameObject.activeSelf)
			{
				break;
			}
			Collider2D[] componentsInChildren = wheel.GetComponentsInChildren<Collider2D>(includeInactive: true);
			foreach (Collider2D collider2D in componentsInChildren)
			{
				collider2D.sharedMaterial = physicsMaterial2D;
				collider2D.isTrigger = true;
				collider2D.isTrigger = false;
			}
		}
	}

	public void Rotate(float dir)
	{
		direction = dir;
	}

	private void FixedUpdate()
	{
		if (base.moduleInited && base.moduleEnabled && direction != 0f)
		{
			RotationSpeed = direction * RotationAcceleration;
		}
	}

	public IEnumerator Shake()
	{
		if (shaking)
		{
			yield break;
		}
		while (base.moduleInited)
		{
			shaking = true;
			if (base.moduleEnabled)
			{
				dist = UnityEngine.Random.Range(0.15f, 0.3f);
				for (int i = 0; i < Springs.Count; i++)
				{
					if (Springs[i] != null)
					{
						Vector2 anchor = Springs[i].anchor;
						if (anchor.y < dist && up)
						{
							SpringJoint2D springJoint2D = Springs[i];
							Vector2 anchor2 = Springs[i].anchor;
							springJoint2D.anchor = new Vector2(0f, anchor2.y + 0.015f);
							goto IL_0122;
						}
					}
					up = false;
					goto IL_0122;
					IL_0122:
					if (Springs[i] != null)
					{
						Vector2 anchor3 = Springs[i].anchor;
						if (anchor3.y > 0f && !up)
						{
							SpringJoint2D springJoint2D2 = Springs[i];
							Vector2 anchor4 = Springs[i].anchor;
							springJoint2D2.anchor = new Vector2(0f, anchor4.y - 0.015f);
							continue;
						}
					}
					up = true;
				}
			}
			yield return new WaitForFixedUpdate();
		}
		shaking = false;
	}

	public void InitAI(Rigidbody2D body, List<Rigidbody2D> wheels, int dif = 1)
	{
		isAI = true;
		Body = body;
		Wheels = wheels;
		foreach (Rigidbody2D wheel in wheels)
		{
			if (rearwheel == null)
			{
				rearwheel = wheel;
			}
			else
			{
				Vector3 position = wheel.transform.position;
				float x = position.x;
				Vector3 position2 = rearwheel.transform.position;
				if (x < position2.x)
				{
					rearwheel = wheel;
				}
			}
			if (frontwheel == null)
			{
				frontwheel = wheel;
			}
			else
			{
				Vector3 position3 = wheel.transform.position;
				float x2 = position3.x;
				Vector3 position4 = frontwheel.transform.position;
				if (x2 > position4.x)
				{
					frontwheel = wheel;
				}
			}
		}
		AddHubs(wheels, body.transform);
		base.moduleInited = true;
		SetFrictionCoeficient(WheelsFriction);
		UpdateSettings();
		Difficult = dif;
		StartCoroutine(AISuspension());
	}

	public void ChangeDifficult(int dif = 1)
	{
		switch (dif)
		{
		case 1:
			RotationAcceleration += RotationAcceleration / 100f * 10f;
			break;
		case 2:
			RotationAcceleration += RotationAcceleration / 100f * 20f;
			break;
		}
		IsConfigured = true;
	}

	public IEnumerator AISuspension()
	{
		if (!IsConfigured)
		{
			ChangeDifficult(Difficult);
		}
		while (base.moduleEnabled)
		{
			Quaternion rotation = Body.transform.rotation;
			if (rotation.z > 0.45f)
			{
				Rotate(-1f);
			}
			else
			{
				Quaternion rotation2 = Body.transform.rotation;
				if (rotation2.z < -0.45f)
				{
					Rotate(1f);
				}
				else
				{
					Rotate(0f);
				}
			}
			yield return null;
		}
	}
}

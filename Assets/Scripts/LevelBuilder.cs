using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.AIObject;
using xmlClassTemplate.AIObjectCivic;
using xmlClassTemplate.CollectibleObject;
using xmlClassTemplate.LevelData;

public class LevelBuilder : MonoBehaviour
{
	[Serializable]
	public class Level
	{
		public LevelNode Lv;

		public int Index;
	}

	[SerializeField]
	private int level = 1;

	[SerializeField]
	private int pack = 1;

	[Range(0f, 100f)]
	public float groundActivationDistance = 60f;

	[Range(0f, 100f)]
	public float groundActivationDistanceAi = 40f;

	[Range(0f, 100f)]
	public float staticActivationDistance = 40f;

	[Range(0f, 100f)]
	public float dynamicActivationDistance = 60f;

	[Range(0f, 100f)]
	public float dynamicActivationDistanceAi = 50f;

	[Range(0f, 100f)]
	public float collectibleActivationDistance = 10f;

	[Range(0f, 100f)]
	public float constructionsActivationDistance = 60f;

	[Range(0f, 100f)]
	public float constructionsActivationDistanceAi = 50f;

	[Range(0f, 100f)]
	public float aiActivationDistance = 10f;

	[Range(0f, 100f)]
	public float ai_C_ActivationDistance = 23f;

	private GroundActivation groundActivator;

	private GroundActivationAi groundActivatorAi;

	private StaticObjectsActivator staticActivator;

	private DynamicObjectsActivator dynamicActivator;

	private DynamicObjectsActivatorAi dynamicActivatorAi;

	private CollectibleObjectsActivator collectibleActivator;

	private CollectibleObjectsRubyActivator collectibleActivatorRuby;

	private AIActivator aiActivator;

	public ConstructionsObjectsActivator constructionsActivator;

	public ConstructionsObjectsActivatorAi constructionsActivatorAi;

	private AIActivatorCivic aiActivator_c;

	private GameObject startSign;

	private GameObject finishSign;

	private LevelNode levelNodeInput;

	private LevelNode levelNode;

	private Camera p_camera;

	private float p_camWidth;

	private static LevelBuilder p_instance;

	private Transform carTr;

	private Transform tStart;

	private Transform tFinish;

	public static float camWidth => instance.p_camWidth;

	public static float groundAD => instance.p_camWidth + instance.groundActivationDistance;

	public static float groundADai => instance.p_camWidth + instance.groundActivationDistanceAi;

	public static float staticAD => instance.p_camWidth + instance.staticActivationDistance;

	public static float dynamicAD => instance.p_camWidth + instance.dynamicActivationDistance;

	public static float collectibleAD => instance.p_camWidth + instance.collectibleActivationDistance;

	public static float constructionsAD => instance.p_camWidth + instance.constructionsActivationDistance;

	public static float dynamicADai => instance.p_camWidth + instance.dynamicActivationDistanceAi;

	public static float constructionsADai => instance.p_camWidth + instance.constructionsActivationDistanceAi;

	public static float aiAD => instance.p_camWidth + instance.aiActivationDistance;

	public static float aiAD_C => instance.p_camWidth + instance.ai_C_ActivationDistance;

	public static LevelBuilder instance
	{
		get
		{
			if (p_instance == null)
			{
				p_instance = UnityEngine.Object.FindObjectOfType<LevelBuilder>();
			}
			return p_instance;
		}
	}

	private Vector2 startPosition => levelNode.startPosition;

	private Vector2 finishPosition => Vector3.right * levelNode.finishPosition + Vector3.right;

	public void Awake()
	{
		p_camera = Camera.main;
		Transform transform = Camera.main.transform;
		Vector3 position = Camera.main.transform.position;
		float x = position.x;
		Vector3 position2 = Camera.main.transform.position;
		transform.position = new Vector3(x, position2.y, -40f);
		if (p_camera != null)
		{
			p_camWidth = p_camera.aspect * p_camera.orthographicSize;
		}
	}

	private void OnDestroy()
	{
		p_instance = null;
	}

	public void StartBuilding(int pack, int level, Action<Transform, Transform> callback)
	{
		this.level = level;
		this.pack = pack;
		bool esterLevelPlay = Progress.shop.EsterLevelPlay;
		bool endlessLevel = Progress.shop.endlessLevel;
		bool arenaNew = Progress.shop.ArenaNew;
		bool bossLevel = Progress.shop.bossLevel;
		if (endlessLevel || bossLevel || arenaNew || esterLevelPlay)
		{
			int num = 0;
			List<int> list = new List<int>();
			list = EndlessLevelInfo.instance.GetChengDateInterval(pack, arenaNew, esterLevelPlay);
			List<int> list2 = new List<int>();
			list2 = EndlessLevelInfo.instance.GetIntervals(pack, arenaNew, esterLevelPlay);
			List<int> list3 = new List<int>();
			list3 = EndlessLevelInfo.instance.GetIntervals_civ(pack, arenaNew, esterLevelPlay);
			List<EndlessLevelInfo.CarTypesClass> list4 = new List<EndlessLevelInfo.CarTypesClass>();
			list4 = EndlessLevelInfo.instance.GetCarTypes(pack, arenaNew, esterLevelPlay);
			List<EndlessLevelInfo.CarTypesClassScraps> list5 = new List<EndlessLevelInfo.CarTypesClassScraps>();
			list5 = EndlessLevelInfo.instance.GetCarTypesScr(pack, arenaNew, esterLevelPlay);
			float num2 = 0f;
			float num3 = 0f;
			LevelNode levelNode = new LevelNode();
			levelNodeInput = Configs.Load<LevelNode>("GameData/Level_5_0");
			levelNodeInput.l_ai.Clear();
			string b = "MarkerUNgnr_Endless";
			LevelNode levelNode2 = null;
			List<Level> list6 = new List<Level>();
			list6.Clear();
			float num4 = 0f;
			float num5 = 0f;
			int num6 = 0;
			int num7 = 0;
			num7 = ((arenaNew || esterLevelPlay) ? 30 : 15);
			for (int i = 0; i < num7; i++)
			{
				levelNode.l_ai.Clear();
				num6 = UnityEngine.Random.Range(1, 10);
				int num8 = -1;
				int count = list6.Count;
				for (int j = 0; j < count; j++)
				{
					if (list6[j].Index == num6)
					{
						num8 = j;
						break;
					}
				}
				levelNode2 = null;
				if (num8 == -1)
				{
					levelNode2 = (Progress.levels.InUndeground ? ((!Progress.shop.Undeground2) ? Configs.Load<LevelNode>("GameData/Level_10_" + num6) : Configs.Load<LevelNode>("GameData/Level_12_" + num6)) : (esterLevelPlay ? Configs.Load<LevelNode>("GameData/Level_8_" + num6) : Configs.Load<LevelNode>("GameData/Level_" + (4 + pack).ToString() + "_" + num6)));
					list6.Add(new Level());
					list6[list6.Count - 1].Lv = levelNode2.Clone();
					list6[list6.Count - 1].Index = num6;
				}
				else
				{
					levelNode2 = list6[num8].Lv.Clone();
				}
				int num9 = levelNodeInput.l_ground.Count - 1;
				int index = 0;
				num4 = levelNodeInput.l_ground[levelNodeInput.l_ground.Count - 1].position.y - levelNodeInput.l_ground[levelNodeInput.l_ground.Count - 1].P4.y - levelNode2.l_ground[index].position.y;
				num5 = levelNodeInput.levelOffset.x - levelNode2.levelOffset.x;
				int count2 = levelNode2.l_colletible.Count;
				for (int k = 0; k < count2; k++)
				{
					levelNode2.l_colletible[k].position = new Vector3(levelNode2.l_colletible[k].position.x + levelNodeInput.levelLength + num5, levelNode2.l_colletible[k].position.y + num4, levelNode2.l_colletible[k].position.z);
				}
				count2 = levelNode2.l_colletibleRuby.Count;
				for (int l = 0; l < count2; l++)
				{
					levelNode2.l_colletibleRuby[l].position = new Vector3(levelNode2.l_colletibleRuby[l].position.x + levelNodeInput.levelLength + num5, levelNode2.l_colletibleRuby[l].position.y + num4, levelNode2.l_colletibleRuby[l].position.z);
				}
				count2 = levelNode2.l_construction.Count;
				for (int m = 0; m < count2; m++)
				{
					levelNode2.l_construction[m].position = new Vector3(levelNode2.l_construction[m].position.x + levelNodeInput.levelLength + num5, levelNode2.l_construction[m].position.y + num4, levelNode2.l_construction[m].position.z);
				}
				count2 = levelNode2.l_dynamic.Count;
				for (int n = 0; n < count2; n++)
				{
					levelNode2.l_dynamic[n].position = new Vector3(levelNode2.l_dynamic[n].position.x + levelNodeInput.levelLength + num5, levelNode2.l_dynamic[n].position.y + num4, levelNode2.l_dynamic[n].position.z);
				}
				count2 = levelNode2.l_static.Count;
				for (int num10 = 0; num10 < count2; num10++)
				{
					levelNode2.l_static[num10].position = new Vector3(levelNode2.l_static[num10].position.x + levelNodeInput.levelLength + num5, levelNode2.l_static[num10].position.y + num4, levelNode2.l_static[num10].position.z);
				}
				List<Vector3> list7 = new List<Vector3>();
				list7.Clear();
				count2 = levelNode2.l_construction.Count;
				for (int num11 = 0; num11 < count2; num11++)
				{
					if (levelNode2.l_construction[num11].name == b)
					{
						list7.Add(levelNode2.l_construction[num11].position);
					}
				}
				count2 = levelNode2.l_ground.Count;
				for (int num12 = 0; num12 < count2; num12++)
				{
					levelNode2.l_ground[num12].position = new Vector2(levelNode2.l_ground[num12].position.x + levelNodeInput.levelLength + num5, levelNode2.l_ground[num12].position.y + num4);
					if (levelNode2.l_ground[num12].position.x >= num2)
					{
						bool flag = true;
						int count3 = list7.Count;
						for (int num13 = 0; num13 < count3; num13++)
						{
							Vector3 vector = list7[num13];
							if (vector.x >= levelNode2.l_ground[num12].position.x - 15f)
							{
								Vector3 vector2 = list7[num13];
								if (vector2.x <= levelNode2.l_ground[num12].position.x + 15f)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							int index2 = UnityEngine.Random.Range(0, list4[num].tcarTupe.Count);
							levelNode.l_ai.Add(new AIObjectNode());
							levelNode.l_ai[levelNode.l_ai.Count - 1].isAhead = false;
							if (!esterLevelPlay)
							{
								levelNode.l_ai[levelNode.l_ai.Count - 1].location = Progress.levels.active_pack;
							}
							else
							{
								levelNode.l_ai[levelNode.l_ai.Count - 1].location = 2;
							}
							levelNode.l_ai[levelNode.l_ai.Count - 1].type = list4[num].tcarTupe[index2];
							int count4 = list5.Count;
							int index3 = levelNode.l_ai.Count - 1;
							for (int num14 = 0; num14 < count4; num14++)
							{
								if (list5[num14].carTupe == levelNode.l_ai[index3].type)
								{
									levelNode.l_ai[index3].coll = list5[num14].callScrNum;
									levelNode.l_ai[index3].scrap1 = list5[num14].Scrap1;
									levelNode.l_ai[index3].scrap2 = list5[num14].Scrap2;
									levelNode.l_ai[index3].scrap3 = list5[num14].Scrap3;
									levelNode.l_ai[index3].scrap4 = list5[num14].Scrap4;
									levelNode.l_ai[index3].scrap5 = list5[num14].Scrap5;
									levelNode.l_ai[index3].scrap1y = list5[num14].Scrap1y;
									levelNode.l_ai[index3].scrap2y = list5[num14].Scrap2y;
									levelNode.l_ai[index3].scrap3y = list5[num14].Scrap3y;
									levelNode.l_ai[index3].scrap4y = list5[num14].Scrap4y;
									levelNode.l_ai[index3].scrap5y = list5[num14].Scrap5y;
									break;
								}
							}
							levelNode.l_ai[levelNode.l_ai.Count - 1].position = new Vector3(levelNode2.l_ground[num12].position.x, levelNode2.l_ground[num12].position.y + 5f, 1f);
							num2 += (float)list2[num];
							if ((float)list[num] < levelNode2.l_ground[num12].position.x && num < list.Count - 1)
							{
								num++;
								if (num > list.Count - 1)
								{
									num = list.Count - 1;
								}
							}
						}
						else
						{
							num2 += 25f;
						}
					}
					if (esterLevelPlay && levelNode2.l_ground[num12].position.x >= num3)
					{
						levelNode.l_ai_c.Add(new AIObjectNodeCivic());
						levelNode.l_ai_c[levelNode.l_ai_c.Count - 1].position = new Vector3(levelNode2.l_ground[num12].position.x, levelNode2.l_ground[num12].position.y + 5f, 1f);
						num3 += (float)list3[num];
					}
				}
				levelNodeInput.l_ai.AddRange(levelNode.l_ai);
				levelNodeInput.l_ai_c.AddRange(levelNode.l_ai_c);
				levelNodeInput.l_colletible.AddRange(levelNode2.l_colletible);
				levelNodeInput.l_colletibleRuby.AddRange(levelNode2.l_colletibleRuby);
				levelNodeInput.l_construction.AddRange(levelNode2.l_construction);
				levelNodeInput.l_dynamic.AddRange(levelNode2.l_dynamic);
				levelNodeInput.l_ground.AddRange(levelNode2.l_ground);
				levelNodeInput.l_static.AddRange(levelNode2.l_static);
				levelNodeInput.levelLength += levelNode2.levelLength;
				levelNodeInput.finishPosition += 5000f;
			}
			list6.Clear();
		}
		else if (Progress.shop.Undeground2)
		{
			levelNodeInput = Configs.Load<LevelNode>("GameData/Level_11_" + level);
		}
		else if (Progress.shop.TestFor9)
		{
			levelNodeInput = Configs.Load<LevelNode>("GameData/Level_9_" + level);
		}
		else
		{
			levelNodeInput = Configs.Load<LevelNode>("GameData/Level_" + pack + "_" + level);
		}
		this.levelNode = levelNodeInput.Clone();
		Transform transform = Camera.main.transform;
		Vector2 startPosition = this.startPosition;
		transform.position = new Vector3(startPosition.x, 1000f, -10f);
		SpawnGround();
		SpawnDecor();
		SpawnObjects();
		SpawnCollectibles();
		SpawnCollectiblesRuby();
		SpawnConstructions();
		SpawnAIs();
		SpawnAIs_c();
		if (bossLevel)
		{
			StartCoroutine(SpawnBoss());
		}
		GameObject gameObject = new GameObject("Start");
		gameObject.transform.position = this.startPosition;
		gameObject.tag = "Start";
		tStart = gameObject.transform;
		GameObject gameObject2 = new GameObject("finish");
		gameObject2.transform.position = finishPosition;
		gameObject2.tag = "Finish";
		tFinish = gameObject2.transform;
		if (!Progress.shop.Tutorial)
		{
			SetStartFinishDeco();
		}
		callback(gameObject.transform, gameObject2.transform);
	}

	private IEnumerator SpawnBoss()
	{
		while (RaceLogic.instance.car == null)
		{
			yield return null;
		}
		Vector3 position = RaceLogic.instance.car.transform.position;
		float x = position.x + 100f;
		Vector3 position2 = RaceLogic.instance.car.transform.position;
		GameObject q = Pool.GameOBJECT(Pool.Bonus.bossTriger, new Vector2(x, position2.y));
		float time = 0f;
		while (time < 2f)
		{
			time += Time.deltaTime;
			q.SetActive(value: true);
			yield return 0;
		}
	}

	private void RemovePickedTickets()
	{
		List<CollectibleObjectNode> list = levelNodeInput.l_colletible.FindAll((CollectibleObjectNode node) => node.type == RaceLogic.enItem.Ticket);
		string[] array = Progress.levels.Pack().Level().tickets.Split('|');
		if (array.Length == 0)
		{
			return;
		}
		List<int> list2 = new List<int>();
		for (int i = 0; i < array.Length; i++)
		{
			if (int.TryParse(array[i], out int _))
			{
				list2.Add(int.Parse(array[i]));
			}
		}
		foreach (CollectibleObjectNode item in list)
		{
			if (list2.Contains(Mathf.RoundToInt(item.position.x)))
			{
				levelNodeInput.l_colletible.Remove(item);
			}
		}
	}

	private void SpawnGround()
	{
		Transform transform = Camera.main.transform;
		Vector3 position = Camera.main.transform.position;
		float x = position.x;
		Vector3 position2 = Camera.main.transform.position;
		transform.position = new Vector3(x, position2.y, -40f);
		StartCoroutine(groundai());
	}

	private IEnumerator groundai()
	{
		while (RaceLogic.instance.car == null)
		{
			yield return 0;
		}
		GameObject go = new GameObject("_Ground");
		go.transform.parent = base.transform;
		go.transform.position = Vector3.zero;
		groundActivator = go.AddComponent<GroundActivation>();
		groundActivator.CreateStack(Camera.main.transform, pack, 12);
		groundActivator.Restart(levelNode.l_ground);
		if (Progress.shop.endlessLevel)
		{
			GameObject gameObject = new GameObject("_GroundAi");
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = Vector3.zero;
			groundActivatorAi = gameObject.AddComponent<GroundActivationAi>();
			GameObject gameObject2 = Resources.Load("Police_convoy") as GameObject;
			GameObject original = gameObject2;
			Vector3 position = RaceLogic.instance.car.transform.position;
			float x = position.x + 75f;
			Vector3 position2 = RaceLogic.instance.car.transform.position;
			GameObject gameObject3 = UnityEngine.Object.Instantiate(original, new Vector3(x, position2.y, 0f), new Quaternion(0f, 0f, 0f, 0f));
			Car2DAIController component = gameObject3.GetComponent<Car2DAIController>();
			component.isKinematic = true;
			RaceLogic.instance.Convoi = gameObject3.gameObject;
			groundActivatorAi.CreateStack(gameObject3.gameObject.transform, pack, 6);
			groundActivatorAi.Restart(levelNode.l_ground);
		}
	}

	private void SpawnDecor()
	{
		StartCoroutine(decorAi());
	}

	private IEnumerator decorAi()
	{
		GameObject go = new GameObject("_Decor");
		go.transform.parent = base.transform;
		go.transform.position = Vector3.zero;
		staticActivator = go.AddComponent<StaticObjectsActivator>();
		staticActivator.CreateStack(Camera.main.transform, 80);
		staticActivator.Restart(levelNode.l_static);
		yield return 0;
	}

	private void SpawnObjects()
	{
		StartCoroutine(objAi());
	}

	private IEnumerator objAi()
	{
		GameObject go = new GameObject("_dynamics");
		go.transform.parent = base.transform;
		go.transform.position = Vector3.zero;
		dynamicActivator = go.AddComponent<DynamicObjectsActivator>();
		dynamicActivator.CreateStack(Camera.main.transform, 30);
		dynamicActivator.Restart(levelNode.l_dynamic);
		while (RaceLogic.instance.Convoi == null)
		{
			yield return 0;
		}
		if (Progress.shop.endlessLevel)
		{
			GameObject gameObject = new GameObject("_dynamicsAi");
			gameObject.transform.parent = base.transform;
			go.transform.position = Vector3.zero;
			dynamicActivatorAi = gameObject.AddComponent<DynamicObjectsActivatorAi>();
			dynamicActivatorAi.CreateStack(RaceLogic.instance.Convoi.transform, 30);
			dynamicActivatorAi.Restart(levelNode.l_dynamic);
			dynamicActivator.DOAA = dynamicActivatorAi;
		}
	}

	private void SpawnCollectibles()
	{
		GameObject gameObject = new GameObject("_collectibles");
		gameObject.transform.parent = base.transform;
		gameObject.transform.position = Vector3.zero;
		collectibleActivator = gameObject.AddComponent<CollectibleObjectsActivator>();
		collectibleActivator.CreateStack(Camera.main.transform, 5);
		collectibleActivator.Restart(levelNode.l_colletible);
	}

	private void SpawnCollectiblesRuby()
	{
		if (!Progress.shop.BlockSpavnRubi)
		{
			GameObject gameObject = new GameObject("_collectiblesRuby");
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = Vector3.zero;
			collectibleActivatorRuby = gameObject.AddComponent<CollectibleObjectsRubyActivator>();
			collectibleActivatorRuby.CreateStack(Camera.main.transform, 70);
			collectibleActivatorRuby.Restart(levelNode.l_colletibleRuby);
		}
	}

	private void SpawnConstructions()
	{
		StartCoroutine(ContrAi());
	}

	private IEnumerator ContrAi()
	{
		GameObject go = new GameObject("_constructions");
		go.transform.parent = base.transform;
		go.transform.position = Vector3.zero;
		constructionsActivator = go.AddComponent<ConstructionsObjectsActivator>();
		constructionsActivator.CreateStack(levelNode.l_construction, Camera.main.transform);
		if (Progress.shop.endlessLevel)
		{
			while (RaceLogic.instance.Convoi == null)
			{
				yield return 0;
			}
			GameObject goAi = new GameObject("_constructionsAi");
			goAi.transform.parent = base.transform;
			goAi.transform.position = Vector3.zero;
			constructionsActivatorAi = goAi.AddComponent<ConstructionsObjectsActivatorAi>();
			constructionsActivatorAi.CreateStack(levelNode.l_construction, RaceLogic.instance.Convoi.transform);
			constructionsActivator.COAA = constructionsActivatorAi;
		}
	}

	private void SpawnAIs()
	{
		if (!Progress.shop.endlessLevel)
		{
			GameObject gameObject = new GameObject("_AIs");
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3(-10000f, -10000f);
			aiActivator = gameObject.AddComponent<AIActivator>();
			int num = pack;
			aiActivator.CreateStack(Camera.main.transform, num);
			aiActivator.Restart(levelNode.l_ai);
			RaceLogic.instance.MaxEnemysInLevel += levelNode.l_ai.Count;
		}
		else
		{
			StartCoroutine(Ais());
		}
	}

	private IEnumerator Ais()
	{
		while (RaceLogic.instance.car == null)
		{
			yield return 0;
		}
		GameObject go = new GameObject("_AIs");
		go.transform.parent = base.transform;
		go.transform.localPosition = new Vector3(-10000f, -10000f);
		aiActivator = go.AddComponent<AIActivator>();
		aiActivator.CreateStack(RaceLogic.instance.car.gameObject.transform, pack);
		aiActivator.Restart(levelNode.l_ai);
	}

	private void SpawnAIs_c()
	{
		if (!Progress.shop.endlessLevel)
		{
			GameObject gameObject = new GameObject("_AIs_C");
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = Vector3.zero;
			aiActivator_c = gameObject.AddComponent<AIActivatorCivic>();
			aiActivator_c.CreateStack(Camera.main.transform, pack);
			aiActivator_c.Restart(levelNode.l_ai_c);
			RaceLogic.instance.MaxEnemysInLevel += levelNode.l_ai_c.Count;
		}
		else
		{
			StartCoroutine(Ais_c());
		}
	}

	private IEnumerator Ais_c()
	{
		while (RaceLogic.instance.car == null)
		{
			yield return 0;
		}
		GameObject go = new GameObject("_AIs_c");
		go.transform.parent = base.transform;
		go.transform.position = Vector3.zero;
		aiActivator_c = go.AddComponent<AIActivatorCivic>();
		aiActivator_c.CreateStack(RaceLogic.instance.car.gameObject.transform, pack);
		aiActivator_c.Restart(levelNode.l_ai_c);
	}

	private void RestartLevel()
	{
		SetActivators(enabled: false);
		RemovePickedTickets();
		levelNode = levelNodeInput.Clone();
		tStart.position = startPosition;
		tFinish.position = finishPosition;
		SetStartFinishDeco();
		RestartActivators();
		SetActivators(enabled: true);
		if (Progress.shop.bossLevel)
		{
			StartCoroutine(delayFromDelay());
		}
	}

	private IEnumerator delayFromDelay()
	{
		float t = 2f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		StartCoroutine(SpawnBoss());
	}

	private void SetActivators(bool enabled)
	{
		if (groundActivator != null)
		{
			groundActivator.enabled = enabled;
		}
		if (groundActivatorAi != null)
		{
			groundActivatorAi.enabled = enabled;
		}
		if (staticActivator != null)
		{
			staticActivator.enabled = enabled;
		}
		if (dynamicActivator != null)
		{
			dynamicActivator.enabled = enabled;
		}
		if (collectibleActivator != null)
		{
			collectibleActivator.enabled = enabled;
		}
		if (constructionsActivator != null)
		{
			constructionsActivator.enabled = enabled;
		}
		if (dynamicActivatorAi != null)
		{
			dynamicActivatorAi.enabled = enabled;
		}
		if (constructionsActivatorAi != null)
		{
			constructionsActivatorAi.enabled = enabled;
		}
		if (aiActivator != null)
		{
			aiActivator.enabled = enabled;
		}
		if (aiActivator_c != null)
		{
			aiActivator_c.enabled = enabled;
		}
	}

	private void RestartActivators()
	{
		if (groundActivator != null)
		{
			groundActivator.Restart(levelNode.l_ground);
		}
		if (staticActivator != null)
		{
			staticActivator.Restart(levelNode.l_static);
		}
		if (dynamicActivator != null)
		{
			dynamicActivator.Restart(levelNode.l_dynamic);
		}
		if (collectibleActivator != null)
		{
			collectibleActivator.Restart(levelNode.l_colletible);
		}
		if (collectibleActivatorRuby != null)
		{
			collectibleActivatorRuby.Restart(levelNode.l_colletibleRuby);
		}
		if (constructionsActivator != null)
		{
			constructionsActivator.Restart(levelNode.l_construction);
		}
		if (aiActivator != null)
		{
			aiActivator.Restart(levelNode.l_ai);
		}
		if (aiActivator_c != null)
		{
			aiActivator_c.Restart(levelNode.l_ai_c);
		}
		if (groundActivatorAi != null)
		{
			groundActivatorAi.Restart(levelNode.l_ground);
		}
		if (constructionsActivatorAi != null)
		{
			constructionsActivatorAi.Restart(levelNode.l_construction);
		}
	}

	private void SetStartFinishDeco()
	{
		if (startSign == null)
		{
			startSign = (UnityEngine.Object.Instantiate(Resources.Load("start_sign")) as GameObject);
		}
		startSign.SetActive(value: true);
		StartFinishSigns component = startSign.GetComponent<StartFinishSigns>();
		if (Progress.levels.active_pack == 1)
		{
			startSign.transform.position = startPosition + component.offsetloc1;
		}
		if (Progress.levels.active_pack == 2)
		{
			startSign.transform.position = startPosition + component.offsetloc2;
		}
		if (Progress.levels.active_pack == 3)
		{
			startSign.transform.position = startPosition + component.offsetloc3;
		}
		component.enabled = true;
		GameObject gameObject = GameObject.Find("_lockStart");
		if (gameObject == null)
		{
			gameObject = new GameObject("_lockStart");
			BoxCollider2D boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
			boxCollider2D.offset = Vector2.right * -20f;
			boxCollider2D.size = new Vector2(10f, 100f);
		}
		gameObject.transform.position = startPosition;
	}

	public static void LoadLevel(int lev, int packs)
	{
		if (instance.level != lev)
		{
			instance.levelNodeInput = Configs.Load<LevelNode>("GameData/Level_" + packs + "_" + lev);
			instance.RemovePickedTickets();
		}
		instance.level = lev;
		instance.RestartLevel();
	}

	private void Update()
	{
		if (p_camera != null)
		{
			p_camWidth = p_camera.aspect * p_camera.orthographicSize;
		}
	}

	public void buildTestRace(int ppack, int plevel, Action<Transform, Transform> callback)
	{
	}
}

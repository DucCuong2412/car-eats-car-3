using System.Collections.Generic;
using UnityEngine;
using xmlClassTemplate.AIObjectCivic;

public class AIActivatorCivic : MonoBehaviour
{
	public class AIObject
	{
		public GameObject gameObject;

		public Transform transform;

		public Car2DAIController aiController;

		public AIObjectNodeCivic node;

		public Shadow shadow;

		private Vector3 rotationOnEnable = Vector3.zero;

		private Vector3 positionOnEnable = Vector3.zero;

		private bool _enabled;

		private int layerCast = LayerMask.GetMask("Objects", "ObjectsBox", "EnemyCar", "Default", "Ground", "NoShadows");

		public bool enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
				aiController.isKinematic = !value;
			}
		}

		public Vector2 position
		{
			get
			{
				return transform.position;
			}
			set
			{
				transform.position = value;
			}
		}

		public AIObject()
		{
		}

		public AIObject(Car2DAIController controller)
		{
			gameObject = controller.gameObject;
			transform = controller.transform;
			aiController = controller;
			shadow = gameObject.AddComponent<Shadow>();
			shadow.Set(1.1f, Shadow.ShadowType.FixedSize);
			node = null;
			gameObject.SetActive(value: false);
		}

		public void SetEnabled(bool enabled)
		{
			aiController.Enabled = enabled;
			this.enabled = enabled;
			if (!enabled)
			{
				this.position += 100f * Vector2.up;
				return;
			}
			Vector2 origin = aiController.SuspensionModule.rearwheel.transform.position - 10f * Vector3.up;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, -Vector2.up, float.MaxValue, layerCast);
			Vector2 origin2 = aiController.SuspensionModule.frontwheel.transform.position - 10f * Vector3.up;
			RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, -Vector2.up, float.MaxValue, layerCast);
			Vector2 vector = raycastHit2D.point - raycastHit2D2.point;
			float y = vector.y;
			Vector2 vector2 = raycastHit2D.point - raycastHit2D2.point;
			float num = Mathf.Atan2(y, vector2.x);
			rotationOnEnable.z = 180f + num * 57.29578f;
			transform.eulerAngles = rotationOnEnable;
			float num2 = 3.75f;
			ref Vector3 reference = ref positionOnEnable;
			Vector2 position = this.position;
			reference.x = position.x;
			ref Vector3 reference2 = ref positionOnEnable;
			float num3 = num2;
			Vector2 point = raycastHit2D.point;
			float y2 = point.y;
			Vector2 point2 = raycastHit2D2.point;
			reference2.y = num3 + Mathf.Max(y2, point2.y) + 0.5f;
			transform.position = positionOnEnable;
		}
	}

	private Transform followTransform;

	private Stack<AIObjectNodeCivic> rStack = new Stack<AIObjectNodeCivic>();

	private Stack<AIObject> stack = new Stack<AIObject>();

	private List<AIObjectNodeCivic> needToActivateList = new List<AIObjectNodeCivic>();

	private List<AIObject> activeList = new List<AIObject>();

	private bool isStarted;

	private List<AIObject> tList = new List<AIObject>();

	private int tTimeToff = 10;

	private static string strCivilians_loc = "Civilians_loc";

	private static string strClone = "(Clone)";

	private static string strEmpty = string.Empty;

	private AIObjectNodeCivic node;

	private float distanceToActivate => LevelBuilder.aiAD_C;

	public void CreateStack(Transform follower, int pack, int stackLength = 10)
	{
		followTransform = follower;
		GameObject original = Resources.Load(strCivilians_loc + pack.ToString()) as GameObject;
		tList.Clear();
		for (int i = 0; i < stackLength; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(original);
			gameObject.transform.parent = base.transform;
			gameObject.name = gameObject.name.Replace(strClone, strEmpty);
			Car2DAIController component = gameObject.GetComponent<Car2DAIController>();
			tk2dSprite[] componentsInChildren = gameObject.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].SortingOrder -= i * 200;
			}
			AIObject aIObject = new AIObject(component);
			stack.Push(aIObject);
			tList.Add(aIObject);
		}
		enabledAll(enable: true);
		tTimeToff = 15;
		CarsValues.HashAllConfigs();
	}

	private void enabledAll(bool enable = false)
	{
		for (int i = 0; i < tList.Count; i++)
		{
			tList[i].aiController.NotCreateHP = enable;
			if (enable)
			{
				tList[i].gameObject.SetActive(value: true);
			}
			tList[i].aiController.CarTupe1b.transform.parent.parent.gameObject.SetActive(enable);
			tList[i].aiController.CarTupe1f.transform.parent.parent.gameObject.SetActive(enable);
			tList[i].aiController.CarTupe2b.transform.parent.parent.gameObject.SetActive(enable);
			tList[i].aiController.CarTupe2f.transform.parent.parent.gameObject.SetActive(enable);
			tList[i].aiController.CarTupe3b.transform.parent.parent.gameObject.SetActive(enable);
			tList[i].aiController.CarTupe3f.transform.parent.parent.gameObject.SetActive(enable);
			if (tList[i].aiController.CarTupe4b != null)
			{
				tList[i].aiController.CarTupe4b.transform.parent.parent.gameObject.SetActive(enable);
			}
			if (tList[i].aiController.CarTupe4f != null)
			{
				tList[i].aiController.CarTupe4f.transform.parent.parent.gameObject.SetActive(enable);
			}
			Car2DHashWheels componentInChildren = tList[i].gameObject.GetComponentInChildren<Car2DHashWheels>();
			if (componentInChildren != null)
			{
				componentInChildren.SetEnable(enable);
			}
			if (!enable)
			{
				tList[i].gameObject.SetActive(value: false);
			}
		}
	}

	public void Restart(List<AIObjectNodeCivic> list)
	{
		isStarted = false;
		rStack.Clear();
		needToActivateList.Clear();
		while (activeList.Count > 0)
		{
			activeList[0].aiController.Enabled = false;
			activeList[0].gameObject.SetActive(value: false);
			stack.Push(activeList[0]);
			activeList.RemoveAt(0);
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			rStack.Push(list[num]);
		}
		isStarted = true;
	}

	private void Update()
	{
		if (!isStarted)
		{
			return;
		}
		if (tTimeToff >= 0)
		{
			tTimeToff--;
			if (tTimeToff == 0)
			{
				enabledAll();
				tTimeToff = -1;
			}
		}
		if (rStack.Count > 0)
		{
			do
			{
				float x = rStack.Peek().position.x;
				Vector3 position = followTransform.position;
				if (!(x <= position.x + distanceToActivate))
				{
					break;
				}
				needToActivateList.Add(rStack.Pop());
			}
			while (rStack.Count != 0);
		}
		node = null;
		for (int num = needToActivateList.Count - 1; num >= 0; num--)
		{
			node = needToActivateList[num];
			if (node.isAhead)
			{
				float x2 = node.position.x;
				Vector3 position2 = followTransform.position;
				if (x2 < position2.x - distanceToActivate)
				{
					needToActivateList.RemoveAt(num);
					SpawnCar(node);
				}
			}
			else
			{
				float x3 = node.position.x;
				Vector3 position3 = followTransform.position;
				if (x3 < position3.x + distanceToActivate)
				{
					needToActivateList.RemoveAt(num);
					SpawnCar(node);
				}
			}
		}
		node = null;
		for (int num2 = activeList.Count - 1; num2 >= 0; num2--)
		{
			if (!activeList[num2].gameObject.activeSelf)
			{
				stack.Push(activeList[num2]);
				activeList.RemoveAt(num2);
			}
			else if (!activeList[num2].enabled)
			{
				Vector2 position4 = activeList[num2].position;
				float x4 = position4.x;
				Vector3 position5 = followTransform.position;
				if (x4 < position5.x - distanceToActivate)
				{
					if (activeList[num2].node.IsCivic && !activeList[num2].node.IsConvoi)
					{
						activeList[num2].gameObject.SetActive(value: false);
					}
					else
					{
						activeList[num2].position += Vector2.right * Time.deltaTime * activeList[num2].aiController.InvisibleSpeed;
					}
				}
				else
				{
					Vector2 position6 = activeList[num2].position;
					float x5 = position6.x;
					Vector3 position7 = followTransform.position;
					if (x5 > position7.x + (distanceToActivate - 3f))
					{
						activeList[num2].position -= Vector2.right * Time.deltaTime * activeList[num2].aiController.InvisibleSpeed;
					}
					else
					{
						activeList[num2].SetEnabled(enabled: true);
					}
				}
			}
			else
			{
				Vector2 position8 = activeList[num2].position;
				float x6 = position8.x;
				Vector3 position9 = followTransform.position;
				if (x6 < position9.x - distanceToActivate)
				{
					activeList[num2].SetEnabled(enabled: false);
				}
				else
				{
					Vector2 position10 = activeList[num2].position;
					float x7 = position10.x;
					Vector3 position11 = followTransform.position;
					if (x7 > position11.x + distanceToActivate)
					{
						activeList[num2].SetEnabled(enabled: false);
					}
				}
			}
		}
	}

	private void SpawnCar(AIObjectNodeCivic node)
	{
		if (Progress.shop.EsterLevelPlay)
		{
			Pool.GameOBJECT(Pool.Bonus.CivilEster, new Vector2(node.position.x, node.position.y));
		}
		else if (stack.Count == 0)
		{
			UnityEngine.Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!    No free ai civil car in stack!!!!!!! LEX");
			Vector2 position = activeList[0].position;
			float x = position.x;
			int index = 0;
			for (int i = 0; i < activeList.Count; i++)
			{
				float num = x;
				Vector2 position2 = activeList[i].position;
				if (num > position2.x)
				{
					Vector2 position3 = activeList[i].position;
					x = position3.x;
					index = i;
				}
			}
			activeList[index].gameObject.SetActive(value: false);
		}
		else
		{
			AIObject aIObject = stack.Pop();
			aIObject.transform.position = new Vector2(node.position.x, node.position.y);
			aIObject.transform.eulerAngles = new Vector3(0f, 0f, node.position.z);
			aIObject.node = node;
			aIObject.enabled = true;
			RaceLogic.instance.InitAI(aIObject.aiController, node.type, node.IsCivic, node.coll, node.Ram, node.guns, node.scrap1, node.scrap2, node.scrap3, node.scrap4, node.scrap5, node.scrap1y, node.scrap2y, node.scrap3y, node.scrap4y, node.scrap5y, node.RGB, node.Location, node.CollRubyForYkys, node.CollRubyForVzruv);
			activeList.Add(aIObject);
		}
	}
}

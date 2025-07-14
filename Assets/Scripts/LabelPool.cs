using System.Collections.Generic;
using UnityEngine;

public class LabelPool : PoolBase
{
	public class LabelObject
	{
		public Transform transform;

		public Animation animation;

		public UILabel label;

		public LabelObject(GameObject go)
		{
			go.SetActive(value: true);
			transform = go.transform;
			animation = go.GetComponentInChildren<Animation>();
			label = go.GetComponentInChildren<UILabel>();
		}
	}

	private static LabelPool p_instance;

	public List<LabelObject> labelList = new List<LabelObject>();

	private static LabelPool instance
	{
		get
		{
			if (p_instance == null)
			{
				GameObject gameObject = new GameObject("LabelPool");
				p_instance = gameObject.AddComponent<LabelPool>();
				p_instance.CreatePool(gameObject);
			}
			return p_instance;
		}
	}

	private void OnDestroy()
	{
		p_instance = null;
	}

	public static void Init(int count = 1)
	{
		instance.Add("BonusMessage", count);
		for (int i = 0; i < count; i++)
		{
			LabelObject item = new LabelObject(instance.GetObject("BonusMessage"));
			instance.labelList.Add(item);
		}
		foreach (LabelObject label in instance.labelList)
		{
			label.transform.gameObject.SetActive(value: false);
		}
	}

	public static LabelObject GetLabelObject()
	{
		for (int i = 0; i < instance.labelList.Count; i++)
		{
			if (!instance.labelList[i].transform.gameObject.activeSelf)
			{
				return instance.labelList[i];
			}
		}
		UnityEngine.Debug.Log("No free labels!!!");
		instance.labelList[0].animation.Stop();
		return instance.labelList[0];
	}
}

using System.Collections.Generic;
using UnityEngine;

public class ThumbsPool : PoolBase
{
	public class ThumbObject
	{
		public Transform transform;

		public UISprite sprite;

		public ThumbObject(GameObject go)
		{
			go.SetActive(value: true);
			transform = go.transform;
			sprite = go.GetComponentInChildren<UISprite>();
		}
	}

	private static ThumbsPool p_instance;

	public List<ThumbObject> thumbsList = new List<ThumbObject>();

	private static ThumbsPool instance
	{
		get
		{
			if (p_instance == null)
			{
				GameObject gameObject = new GameObject("ThumbsPool");
				p_instance = gameObject.AddComponent<ThumbsPool>();
				p_instance.CreatePool(gameObject);
			}
			return p_instance;
		}
	}

	public static void Init(int count = 20)
	{
		instance.Add("Thumb", count);
		for (int i = 0; i < count; i++)
		{
			ThumbObject item = new ThumbObject(instance.GetObject("Thumb"));
			instance.thumbsList.Add(item);
		}
		foreach (ThumbObject thumbs in instance.thumbsList)
		{
			thumbs.transform.gameObject.SetActive(value: false);
		}
	}

	public static ThumbObject GetThumbObject()
	{
		for (int i = 0; i < instance.thumbsList.Count; i++)
		{
			if (!instance.thumbsList[i].transform.gameObject.activeSelf)
			{
				return instance.thumbsList[i];
			}
		}
		UnityEngine.Debug.Log("No free thumbs!!!");
		return null;
	}

	private void OnDestroy()
	{
		p_instance = null;
	}
}

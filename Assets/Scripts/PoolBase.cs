using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolBase : MonoBehaviour
{
	[Serializable]
	public class PoolClass
	{
		public string Name = string.Empty;

		public List<GameObject> Objects = new List<GameObject>();

		public PoolClass()
		{
		}

		public PoolClass(string name, List<GameObject> objects)
		{
			Name = name;
			Objects = objects;
		}
	}

	public List<PoolClass> poolList = new List<PoolClass>();

	private GameObject poolObject;

	private Dictionary<string, List<GameObject>> objects;

	private static string _clone = "(Clone)";

	private static string _empty = string.Empty;

	public void CreatePool(GameObject go)
	{
		poolObject = go;
		objects = new Dictionary<string, List<GameObject>>();
	}

	public void Add(string objectName, int count = 1)
	{
		if (poolObject == null)
		{
			UnityEngine.Debug.Log("Create Pool before adding objects to it.");
			return;
		}
		GameObject gameObject = new GameObject($"_{objectName}");
		gameObject.transform.parent = poolObject.transform;
		for (int i = 0; i < count; i++)
		{
			LoadObject(objectName, gameObject);
		}
	}

	public void Add(GameObject instance, int count = 1)
	{
		if (poolObject == null)
		{
			UnityEngine.Debug.Log("Create Pool before adding objects to it.");
			return;
		}
		GameObject gameObject = new GameObject($"_{instance.gameObject.name}");
		gameObject.transform.parent = poolObject.transform;
		for (int i = 0; i < count; i++)
		{
			LoadObject(instance, gameObject);
		}
	}

	public void CopyToSerializedList()
	{
		foreach (string key in objects.Keys)
		{
			poolList.Add(new PoolClass(key, objects[key]));
		}
	}

	public void ReadFromSerializedList()
	{
		objects = new Dictionary<string, List<GameObject>>();
		foreach (PoolClass pool in poolList)
		{
			objects.Add(pool.Name, pool.Objects);
		}
		GameObject gameObject = new GameObject(string.Format("_{0}", "smoke_new"));
		gameObject.transform.parent = base.gameObject.transform;
		for (int i = 0; i < 2; i++)
		{
			LoadObject("smoke_new", gameObject);
		}
	}

	private GameObject LoadObject(string name, GameObject parentObject)
	{
		GameObject gameObject = Resources.Load(name) as GameObject;
		if (gameObject == null)
		{
			UnityEngine.Debug.LogError("Error loading " + name);
			return null;
		}
		return LoadObject(gameObject, parentObject);
	}

	private GameObject LoadObject(GameObject go, GameObject parentObject)
	{
		if (go == null)
		{
			return null;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(go);
		gameObject.name = gameObject.name.Replace(_clone, _empty);
		gameObject.transform.parent = parentObject.transform;
		if (!objects.ContainsKey(go.name))
		{
			objects.Add(go.name, new List<GameObject>());
		}
		objects[go.name].Add(gameObject);
		gameObject.SetActive(value: false);
		return gameObject;
	}

	public GameObject GetObject(string name)
	{
		if (name == null || !objects.ContainsKey(name))
		{
			return null;
		}
		foreach (GameObject item in objects[name])
		{
			if (!item.activeSelf)
			{
				return item;
			}
		}
		return LoadObject(objects[name][0], objects[name][0].transform.parent.gameObject);
	}

	public GameObject GetObject(GameObject go)
	{
		return GetObject(go.name);
	}

	public List<GameObject> allObjectsWithName(string name)
	{
		return objects[name];
	}

	public virtual GameObject spawnAtPoint(string name, Vector3 position)
	{
		GameObject @object = GetObject(name);
		return spawnAtPoint(@object, position);
	}

	public virtual GameObject spawnAtPoint(GameObject obj, Vector3 position)
	{
		if (obj == null)
		{
			return null;
		}
		obj.transform.position = position;
		obj.SetActive(value: true);
		return obj;
	}

	public virtual GameObject spawnAtPoint(string name, Transform tr)
	{
		GameObject @object = GetObject(name);
		return spawnAtPoint(@object, tr);
	}

	public virtual GameObject spawnAtPoint(GameObject obj, Transform tr)
	{
		if (obj == null)
		{
			return null;
		}
		obj.transform.position = tr.position;
		obj.SetActive(value: true);
		StartCoroutine(duplicatePosition(obj.transform, tr));
		return obj;
	}

	private IEnumerator duplicatePosition(Transform t_dirty, Transform t_example)
	{
		while (t_dirty.gameObject.activeSelf)
		{
			t_dirty.position = t_example.position;
			if (!t_example.gameObject.activeSelf)
			{
				t_dirty.gameObject.SetActive(value: false);
			}
			yield return null;
		}
	}

	public void Stop()
	{
		foreach (string key in objects.Keys)
		{
			for (int i = 0; i < objects[key].Count; i++)
			{
				objects[key][i].SetActive(value: false);
			}
		}
	}

	public IEnumerator EnableAllForFrame()
	{
		List<GameObject> enabledObjects = new List<GameObject>(1024);
		foreach (string key in objects.Keys)
		{
			List<GameObject> list = objects[key];
			if (list != null && list.Count > 0)
			{
				GameObject gameObject = list[0];
				gameObject.SetActive(value: true);
				enabledObjects.Add(gameObject);
			}
		}
		yield return null;
		int count = enabledObjects.Count;
		for (int i = 0; i < count; i++)
		{
			enabledObjects[i].SetActive(value: false);
		}
	}
}

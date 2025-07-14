using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrashGenerator : MonoBehaviour
{
	private static SpaceTrashGenerator _instance;

	private Transform _target;

	private float StartHeight = 12f;

	private float Height = 30f;

	private float Width = 100f;

	private float Density = 250f;

	private List<Transform> Objects = new List<Transform>();

	private Pool.Trash tr;

	private Vector2 vec;

	private RaycastHit2D hit;

	public static SpaceTrashGenerator instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_trash");
				_instance = gameObject.AddComponent<SpaceTrashGenerator>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public Transform Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void RefreshTrash()
	{
		for (int i = 0; i < Objects.Count; i++)
		{
			if (!Objects[i].gameObject.activeSelf)
			{
				Objects[i].gameObject.SetActive(value: true);
			}
			Transform transform = Objects[i].transform;
			Vector3 position = Target.position;
			float min = position.x - Width / 2f;
			Vector3 position2 = Target.position;
			transform.position = new Vector2(UnityEngine.Random.Range(min, position2.x + Width / 2f), StartHeight + UnityEngine.Random.Range(0f, Height));
		}
	}

	private IEnumerator UpdateTrash()
	{
		while (Objects.Count > 0)
		{
			Objects[0].gameObject.SetActive(value: false);
			Objects.RemoveAt(0);
		}
		int objNum = (int)(Height * Width / Density);
		for (int i = 0; i < objNum; i++)
		{
			Vector3 position = Target.position;
			AddNewObject(new Vector2(position.x + UnityEngine.Random.Range((0f - Width) / 2f, Width / 2f), StartHeight + UnityEngine.Random.Range(0f, Height)));
		}
		while (Target != null)
		{
			for (int j = 0; j < Objects.Count; j++)
			{
				Vector3 position2 = Objects[j].position;
				float x = position2.x;
				Vector3 position3 = Target.position;
				if (x < position3.x - Width / 2f)
				{
					RemoveObject(Objects[j]);
					Vector3 position4 = Target.position;
					vec = new Vector3(position4.x + Width / 2f, 100f) + 10f * Vector3.up;
					hit = Physics2D.Raycast(vec, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
					Vector3 position5 = Target.position;
					float x2 = position5.x + Width / 2f;
					Vector2 point = hit.point;
					AddNewObject(new Vector2(x2, point.y + StartHeight + UnityEngine.Random.Range(0f, Height)));
					continue;
				}
				Vector3 position6 = Objects[j].position;
				float x3 = position6.x;
				Vector3 position7 = Target.position;
				if (x3 > position7.x + Width / 2f)
				{
					RemoveObject(Objects[j]);
					Vector3 position8 = Target.position;
					vec = new Vector3(position8.x - Width / 2f, 100f) + 10f * Vector3.up;
					hit = Physics2D.Raycast(vec, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
					Vector3 position9 = Target.position;
					float x4 = position9.x - Width / 2f;
					Vector2 point2 = hit.point;
					AddNewObject(new Vector2(x4, point2.y + StartHeight + UnityEngine.Random.Range(0f, Height)));
				}
			}
			yield return null;
		}
	}

	private void AddNewObject(Vector2 pos)
	{
		tr = (Pool.Trash)UnityEngine.Random.Range(1, 6);
		Objects.Add(Pool.Animate(tr, pos).transform);
		ObjectActor componentInChildren = Objects[Objects.Count - 1].gameObject.GetComponentInChildren<ObjectActor>();
		if (componentInChildren != null)
		{
			componentInChildren.Health = 45;
		}
	}

	private void RemoveObject(Transform obj)
	{
		obj.gameObject.SetActive(value: false);
		Objects.Remove(obj);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Car2DHashWheels : MonoBehaviour
{
	public List<GameObject> Wheels = new List<GameObject>();

	public void SetEnable(bool enable = false)
	{
		for (int i = 0; i < Wheels.Count; i++)
		{
			Wheels[i].SetActive(enable);
			Rigidbody2D component = Wheels[i].GetComponent<Rigidbody2D>();
			if (component != null)
			{
				component.isKinematic = true;
			}
		}
	}
}

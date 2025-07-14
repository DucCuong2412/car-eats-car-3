using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float speed = 1.01f;

	private Transform t;

	private Vector3 vector;

	private void Start()
	{
		vector = Vector3.forward * speed;
		t = base.transform;
	}

	private void Update()
	{
		if (Time.timeScale != 0f)
		{
			t.Rotate(vector);
		}
	}
}

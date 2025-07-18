using UnityEngine;

public class CFX3_Demo_Translate : MonoBehaviour
{
	public float speed = 30f;

	public Vector3 rotation = Vector3.forward;

	public Vector3 axis = Vector3.forward;

	public bool gravity;

	private Vector3 dir;

	private void Start()
	{
		dir = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
		dir.Scale(rotation);
		base.transform.localEulerAngles = dir;
	}

	private void Update()
	{
		base.transform.Translate(axis * speed * Time.deltaTime, Space.Self);
	}
}

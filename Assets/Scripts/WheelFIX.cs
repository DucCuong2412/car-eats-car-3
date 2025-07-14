using UnityEngine;

public class WheelFIX : MonoBehaviour
{
	public GameObject wheel1;

	public GameObject wheel2;

	public Vector3 wheelpos1 = Vector3.zero;

	public Vector3 wheelpos2 = Vector3.zero;

	private void OnEnable()
	{
		if (wheelpos1 == Vector3.zero)
		{
			wheelpos1 = wheel1.transform.localPosition;
		}
		if (wheelpos2 == Vector3.zero)
		{
			wheelpos2 = wheel2.transform.localPosition;
		}
		wheel1.transform.localPosition = wheelpos1;
		wheel2.transform.localPosition = wheelpos2;
	}
}

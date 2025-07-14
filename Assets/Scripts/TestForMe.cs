using UnityEngine;

public class TestForMe : MonoBehaviour
{
	public GameObject gm;

	private void Update()
	{
		Transform transform = base.gameObject.transform;
		Vector3 localPosition = gm.transform.localPosition;
		float x = localPosition.x + 0.85f;
		Vector3 localPosition2 = gm.transform.localPosition;
		transform.localPosition = new Vector3(x, localPosition2.y + 0.6f, 0f);
		Quaternion localRotation = base.gameObject.transform.localRotation;
		localRotation.eulerAngles = new Vector3(0f, 0f, 0f);
		base.gameObject.transform.localRotation = localRotation;
		if (!gm.activeSelf)
		{
			base.gameObject.SetActive(value: false);
		}
		else
		{
			base.gameObject.SetActive(value: true);
		}
	}
}

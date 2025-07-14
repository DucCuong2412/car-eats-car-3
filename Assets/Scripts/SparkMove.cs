using UnityEngine;

public class SparkMove : MonoBehaviour
{
	[Header("Left-Right")]
	public float LeftSide = 1f;

	public float RightSide = 2f;

	public float speedLR = 0.05f;

	public bool goRight = true;

	[Header("Up-Down")]
	public float TopSide = 2f;

	public float DownSide = 1f;

	public float speedUD = 0.05f;

	public bool goUp = true;

	private Vector3 startPos;

	private void Start()
	{
		startPos = base.transform.position;
	}

	private void Update()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		if (!goRight)
		{
			Transform transform = base.transform;
			Vector3 position = base.transform.position;
			float x = position.x - speedLR;
			Vector3 position2 = base.transform.position;
			float y = position2.y;
			Vector3 position3 = base.transform.position;
			transform.position = new Vector3(x, y, position3.z);
			float num = startPos.x - LeftSide;
			Vector3 position4 = base.transform.position;
			if (num > position4.x)
			{
				goRight = true;
			}
		}
		else
		{
			Transform transform2 = base.transform;
			Vector3 position5 = base.transform.position;
			float x2 = position5.x + speedLR;
			Vector3 position6 = base.transform.position;
			float y2 = position6.y;
			Vector3 position7 = base.transform.position;
			transform2.position = new Vector3(x2, y2, position7.z);
			float num2 = startPos.x + RightSide;
			Vector3 position8 = base.transform.position;
			if (num2 < position8.x)
			{
				goRight = false;
			}
		}
		if (!goUp)
		{
			Transform transform3 = base.transform;
			Vector3 position9 = base.transform.position;
			float x3 = position9.x;
			Vector3 position10 = base.transform.position;
			float y3 = position10.y - speedUD;
			Vector3 position11 = base.transform.position;
			transform3.position = new Vector3(x3, y3, position11.z);
			float num3 = startPos.y - DownSide;
			Vector3 position12 = base.transform.position;
			if (num3 > position12.y)
			{
				goUp = true;
			}
		}
		else
		{
			Transform transform4 = base.transform;
			Vector3 position13 = base.transform.position;
			float x4 = position13.x;
			Vector3 position14 = base.transform.position;
			float y4 = position14.y + speedUD;
			Vector3 position15 = base.transform.position;
			transform4.position = new Vector3(x4, y4, position15.z);
			float num4 = startPos.y + TopSide;
			Vector3 position16 = base.transform.position;
			if (num4 < position16.y)
			{
				goUp = false;
			}
		}
	}
}

using UnityEngine;

public class CameraExample : FollowSystem
{
	public Offset offset = new Offset();

	public Zoom zoom = new Zoom();

	private void Start()
	{
		offset.Start(GameObject.FindGameObjectWithTag("CarMain").transform, Camera.main);
		zoom.Start(GameObject.FindGameObjectWithTag("CarMain").transform, Camera.main);
	}
}

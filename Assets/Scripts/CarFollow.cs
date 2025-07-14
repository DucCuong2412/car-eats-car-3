using UnityEngine;

public class CarFollow : FollowSystem
{
	public Offset offset = new Offset();

	public Zoom zoom = new Zoom();

	private static CarFollow _instance;

	private Coroutine stopCorut;

	public static CarFollow Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType<CarFollow>();
			}
			return _instance;
		}
	}

	public void OnEnable()
	{
		_instance = this;
	}

	public void OnDestroy()
	{
		_instance = null;
	}

	public void StartFollow(Transform car)
	{
		offset = null;
		offset = new Offset();
		offset.Start(car, Camera.main);
		zoom.Start(car, Camera.main);
	}

	public void LateUpdate()
	{
		if (Time.timeScale != 0f)
		{
			zoom.ZoomTool();
			offset.OffsetTool();
		}
	}

	public void StopCam()
	{
		stopCorut = StartCoroutine(offset.stopCam());
		zoom.OnCageZoom = true;
		zoom.OnCageZoomIter = 0f;
	}

	public void StartCam()
	{
		if (stopCorut != null)
		{
			StopCoroutine(stopCorut);
		}
		StartCoroutine(offset.startCam());
		zoom.OnCageZoom = false;
	}

	public void Stop()
	{
		offset.Enabled = false;
		zoom.Enabled = false;
	}
}

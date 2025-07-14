using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraControllerZoom : MonoBehaviour
{
	private static CameraControllerZoom _instance;

	public Camera cam;

	public GameObject leftUP;

	public GameObject leftDown;

	public GameObject rightDown;

	public GameObject rightUP;

	public CanvasScaler CS;

	private float DeltaToAvtoMoveBullet = 5f;

	private GameObject obj;

	private bool player;

	private Vector2 topLeft;

	private Vector2 topRight;

	private Vector2 botLeft;

	private Vector2 botRight;

	private float StartSize;

	private bool expanseSpeed;

	public GameObject levels;

	public float PlusPoY;

	private Vector3 lastVelos = new Vector3(0f, 0f, 0f);

	private float tempForSpeed = 1f;

	private void Start()
	{
		StartSize = cam.orthographicSize;
		topLeft = leftUP.transform.position;
		topRight = rightUP.transform.position;
		botLeft = leftDown.transform.position;
		botRight = rightDown.transform.position;
	}

	private void OnEnable()
	{
		_instance = this;
	}

	private void OnDisable()
	{
		_instance = null;
	}

	public void ZoomChange(ScrollRect sr, float koef, GameObject Obj, float minSize = 0f, float maxSize = 0f)
	{
		object[] obj = new object[6]
		{
			"_instance.cam.transform.position.x   ",
			null,
			null,
			null,
			null,
			null
		};
		Vector3 position = _instance.cam.transform.position;
		obj[1] = position.x;
		obj[2] = "   _instance.cam.aspect   ";
		obj[3] = _instance.cam.aspect;
		obj[4] = "    _instance.topRight.x    ";
		obj[5] = _instance.topRight.x;
		UnityEngine.Debug.Log(string.Concat(obj));
		object[] obj2 = new object[6]
		{
			"_instance.cam.transform.position.x   ",
			null,
			null,
			null,
			null,
			null
		};
		Vector3 position2 = _instance.cam.transform.position;
		obj2[1] = position2.x;
		obj2[2] = "   _instance.cam.aspect   ";
		obj2[3] = _instance.cam.aspect;
		obj2[4] = "    _instance.topLeft.x    ";
		obj2[5] = _instance.topLeft.x;
		UnityEngine.Debug.Log(string.Concat(obj2));
		Vector3 position3 = _instance.cam.transform.position;
		if (position3.x + _instance.cam.orthographicSize * _instance.cam.aspect >= _instance.topRight.x)
		{
			Vector3 position4 = _instance.cam.transform.position;
			if (position4.x - _instance.cam.orthographicSize * _instance.cam.aspect <= _instance.topLeft.x && koef >= 0f)
			{
				return;
			}
		}
		CS.matchWidthOrHeight += koef;
		sr.enabled = false;
		if (_instance.cam.orthographicSize < minSize && minSize != 0f)
		{
			_instance.cam.orthographicSize = minSize;
		}
		if (_instance.cam.orthographicSize > maxSize && maxSize != 0f)
		{
			_instance.cam.orthographicSize = maxSize;
		}
		sr.enabled = true;
		StartCoroutine(test(sr));
	}

	private IEnumerator test(ScrollRect sr)
	{
		float hor = sr.horizontalNormalizedPosition;
		float ver = sr.verticalNormalizedPosition;
		sr.horizontalNormalizedPosition = hor;
		sr.verticalNormalizedPosition = ver;
		yield return 0;
	}

	private void Update()
	{
		if (CS.matchWidthOrHeight <= 0f)
		{
			CS.matchWidthOrHeight = 0f;
		}
		else if (CS.matchWidthOrHeight >= 1f)
		{
			CS.matchWidthOrHeight = 1f;
		}
	}
}

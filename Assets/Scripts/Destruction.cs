using UnityEngine;

public class Destruction : MonoBehaviour
{
	public ParticleSystem par;

	private void Start()
	{
		EasyHill2DManager.Instance().getAllHill2DNodes();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 position = GetComponent<Camera>().ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			EasyHill2DManager.Instance().DestructSegmentCircle(position.x, position.y, 5f, 0.2f);
			Object.Instantiate(par, position, Quaternion.identity);
		}
		if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Vector3 position2 = GetComponent<Camera>().ScreenToWorldPoint(Input.touches[0].position);
			EasyHill2DManager.Instance().DestructSegmentCircle(position2.x, position2.y, 5f, 0.2f);
			ParticleSystem particleSystem = UnityEngine.Object.Instantiate(par, position2, Quaternion.identity);
			particleSystem.gameObject.SetActive(value: true);
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(25f, 25f, 400f, 200f), "Click or Touch on the terrain to destroy it.");
	}
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
	public int locnum;

	private string locnums = "1";

	public void OnGUI()
	{
		locnums = GUI.TextArea(new Rect(10f, 10f, 100f, 20f), locnums);
		if (GUI.Button(new Rect(10f, 40f, 100f, 20f), "SetLocation"))
		{
			locnum = int.Parse(locnums);
			if (base.transform.childCount > 0)
			{
				UnityEngine.Object.Destroy(base.transform.GetComponentInChildren<ParallaxSystem>().gameObject);
			}
			ParallaxSystem.Create(base.transform, locnum);
		}
	}

	public void Update()
	{
		Transform transform = base.transform;
		Vector3 position = base.transform.position;
		float x = position.x + UnityEngine.Input.GetAxisRaw("Horizontal") / 1f;
		Vector3 position2 = base.transform.position;
		transform.position = new Vector2(x, position2.y + UnityEngine.Input.GetAxisRaw("Vertical") / 3f);
	}
}

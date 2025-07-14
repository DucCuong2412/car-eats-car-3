using UnityEngine;

public class StartFinishSigns : MonoBehaviour
{
	public Vector2 offsetloc1;

	public Vector2 offsetloc2;

	public Vector2 offsetloc3;

	public Vector2 offsetloc9;

	public GameObject loc1;

	public GameObject loc2;

	public GameObject loc3;

	private void OnEnable()
	{
		if (loc1 != null && loc2 != null && loc3 != null)
		{
			if (RaceLogic.instance.pack == 1)
			{
				loc1.SetActive(value: true);
				loc2.SetActive(value: false);
				loc3.SetActive(value: false);
			}
			else if (RaceLogic.instance.pack == 2)
			{
				loc1.SetActive(value: false);
				loc2.SetActive(value: true);
				loc3.SetActive(value: false);
			}
			else if (RaceLogic.instance.pack == 3)
			{
				loc1.SetActive(value: false);
				loc2.SetActive(value: false);
				loc3.SetActive(value: true);
			}
		}
	}

	private void OnWillRenderObject()
	{
		RaycastHit2D hit = Physics2D.Raycast(base.transform.position + Vector3.up * 50f, -Vector2.up, 200f, 1 << LayerMask.NameToLayer("Ground"));
		if ((bool)hit && (bool)hit.collider)
		{
			if (Progress.shop.TestFor9)
			{
				base.transform.position = hit.point + offsetloc9;
			}
			else if (Progress.levels.active_pack == 1)
			{
				base.transform.position = hit.point + offsetloc1;
			}
			if (Progress.levels.active_pack == 2)
			{
				base.transform.position = hit.point + offsetloc2;
			}
			if (Progress.levels.active_pack == 3)
			{
				base.transform.position = hit.point + offsetloc3;
			}
			base.enabled = false;
		}
	}
}

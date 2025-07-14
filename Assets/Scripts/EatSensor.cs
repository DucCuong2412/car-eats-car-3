using UnityEngine;

public class EatSensor : MonoBehaviour
{
	public delegate void eDelegate(GameObject go);

	public event eDelegate Eating;

	private void OnTriggerStay2D(Collider2D c)
	{
		if (this.Eating != null)
		{
			this.Eating(c.gameObject);
		}
		PitGate component = c.gameObject.GetComponent<PitGate>();
		if ((bool)component)
		{
			component.SetDmg();
		}
	}

	public void OnDestroy()
	{
		this.Eating = null;
	}

	public void OnDisable()
	{
		this.Eating = null;
	}
}

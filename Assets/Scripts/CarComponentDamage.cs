using UnityEngine;

public class CarComponentDamage : MonoBehaviour
{
	public delegate void ElementDamageDelegate(byte index, float magnitude);

	public delegate void CollisDelegate(Collision2D coll, bool isWheel);

	private bool _isInit;

	private byte _index;

	public event ElementDamageDelegate ElementDamageEvent;

	public event CollisDelegate CollisEvent;

	public void Init(byte i)
	{
		_index = i;
		_isInit = true;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (_isInit && this.ElementDamageEvent != null && !(coll.collider == null))
		{
			this.ElementDamageEvent(_index, (0f - coll.relativeVelocity.magnitude) * 0.01f);
			if (this.CollisEvent != null)
			{
				this.CollisEvent(coll, isWheel: false);
			}
		}
	}
}

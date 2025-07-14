using UnityEngine;

public class FloaterScript : MonoBehaviour
{
	public Vector3 Angle = new Vector3(0f, 0f, 0f);

	public GameObject GO;

	private Transform _sprite;

	private Transform _Sprite
	{
		get
		{
			if (_sprite == null)
			{
				_sprite = base.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform;
			}
			return _sprite;
		}
	}

	private void Update()
	{
		_Sprite.parent = base.transform.parent.parent;
		_Sprite.gameObject.SetActive(value: true);
		_Sprite.transform.localRotation = GO.transform.localRotation;
	}

	private void OnEnable()
	{
		_Sprite.parent = base.transform.parent.parent;
		_Sprite.gameObject.SetActive(value: true);
		_Sprite.transform.eulerAngles = Angle;
	}

	private void OnDisable()
	{
		_Sprite.gameObject.SetActive(value: false);
	}
}

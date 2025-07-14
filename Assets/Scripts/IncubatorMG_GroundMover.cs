using UnityEngine;

public class IncubatorMG_GroundMover : MonoBehaviour
{
	public float Speed;

	public SpriteRenderer Sprite;

	private Vector2 _offset;

	private IncubatorMG_Controller _controller;

	private void Start()
	{
		_controller = UnityEngine.Object.FindObjectOfType<IncubatorMG_Controller>();
		_offset = new Vector2(0f, 0f);
		Sprite.material.mainTextureOffset = _offset;
	}

	private void Update()
	{
		if (!_controller.PauseOn)
		{
			_offset.x += Speed;
			Sprite.material.mainTextureOffset = _offset;
		}
	}
}

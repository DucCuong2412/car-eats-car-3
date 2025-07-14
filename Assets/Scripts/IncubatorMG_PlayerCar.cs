using System.Collections;
using UnityEngine;

public class IncubatorMG_PlayerCar : MonoBehaviour
{
	public Animator MoveAnimator;

	public int CurrentLine;

	public IncubatorMG_ObjLayers LayersObj;

	public GameObject Evo1;

	public GameObject Evo2;

	public GameObject Evo3;

	public BoxCollider2D BC2d;

	public float BoxCollederSizeEvo1;

	public float BoxCollederSizeEvo2;

	public float BoxCollederSizeEvo3;

	public int CarEvo = 1;

	private bool _canMove = true;

	private int _lastLine;

	private int tPlassedLayer;

	private int has_UP = Animator.StringToHash("move_UP");

	private int has_DOWN = Animator.StringToHash("move_DOWN");

	private void Start()
	{
		Evo1.SetActive(value: false);
		Evo2.SetActive(value: false);
		Evo3.SetActive(value: false);
		CarEvo = Progress.shop.Incubator_EvoStage;
		switch (CarEvo)
		{
		case 1:
		{
			Evo1.SetActive(value: true);
			BoxCollider2D bC2d3 = BC2d;
			float boxCollederSizeEvo3 = BoxCollederSizeEvo1;
			Vector2 size3 = BC2d.size;
			bC2d3.size = new Vector2(boxCollederSizeEvo3, size3.y);
			break;
		}
		case 2:
		{
			Evo2.SetActive(value: true);
			BoxCollider2D bC2d2 = BC2d;
			float boxCollederSizeEvo2 = BoxCollederSizeEvo2;
			Vector2 size2 = BC2d.size;
			bC2d2.size = new Vector2(boxCollederSizeEvo2, size2.y);
			break;
		}
		case 3:
		{
			Evo3.SetActive(value: true);
			BoxCollider2D bC2d = BC2d;
			float boxCollederSizeEvo = BoxCollederSizeEvo3;
			Vector2 size = BC2d.size;
			bC2d.size = new Vector2(boxCollederSizeEvo, size.y);
			break;
		}
		}
		CurrentLine = 0;
		_lastLine = 0;
		ChengLayer();
		_canMove = true;
	}

	public void SetMove(bool up)
	{
		if (!_canMove)
		{
			return;
		}
		StartCoroutine(DelayToMove());
		_lastLine = CurrentLine;
		if (up)
		{
			MoveAnimator.SetBool(has_UP, value: true);
			if (CurrentLine == -1)
			{
				CurrentLine = 0;
				ChengLayer();
			}
			else if (CurrentLine == 0)
			{
				CurrentLine = 1;
				ChengLayer();
			}
		}
		else
		{
			MoveAnimator.SetBool(has_DOWN, value: true);
			if (CurrentLine == 1)
			{
				CurrentLine = 0;
				ChengLayer();
			}
			else if (CurrentLine == 0)
			{
				CurrentLine = -1;
				ChengLayer();
			}
		}
	}

	private IEnumerator DelayToMove()
	{
		_canMove = false;
		float t = 0.2f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		_canMove = true;
	}

	public void ChengLayer()
	{
		tPlassedLayer = 0;
		switch (CurrentLine)
		{
		case 1:
			tPlassedLayer = 300;
			break;
		case 0:
			tPlassedLayer = 900;
			break;
		case -1:
			tPlassedLayer = 1500;
			break;
		}
		StartCoroutine(DelayToNewLayer());
	}

	private IEnumerator DelayToNewLayer()
	{
		float t = 0.3f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		int count = LayersObj.Sprites.Count;
		for (int i = 0; i < count; i++)
		{
			LayersObj.Sprites[i].sortingOrder = LayersObj.SpritesLayers[i] + tPlassedLayer;
		}
	}
}
